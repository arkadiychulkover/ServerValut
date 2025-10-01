using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerValut
{
    public partial class Form1 : Form
    {
        IPEndPoint ep;
        TcpListener listener;

        static bool isStarted = false;

        static int usersCount = 0;

        Dictionary<string, TcpClient> clients = new();
        Dictionary<string, string> courses = new();

        Dictionary<string, int> requestCounts = new();
        Dictionary<string, DateTime> bannedClients = new();

        Dictionary<string, string> accountsData = new();

        public Form1()
        {
            InitializeComponent();
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2565);
            listener = new TcpListener(ep);

            courses.Add("Usd/Euro", "1.5/1");
            courses.Add("Euro/Usd", "1/1.5");
            courses.Add("Usd/Uan", "1/40");
            courses.Add("Uan/Usd", "40/1");
            courses.Add("Euro/Uan", "1.5/40");
            courses.Add("Uan/Euro", "40/1.5");
        }

        private void Starter_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                isStarted = true;
                listener.Start();
                Thread thread = new Thread(Listen);
                thread.IsBackground = true;
                thread.Start();

                Conections.Items.Add("Server started");
                File.AppendAllText("log.txt", $"{DateTime.Now}: Server started.\n");
            }
            else
            {
                isStarted = false;
                listener.Stop();

                foreach (var client in clients.Values)
                    client.Close();
                clients.Clear();

                Conections.Items.Add("Server stopped");
                File.AppendAllText("log.txt", $"{DateTime.Now}: Server stopped.\n");
            }
        }

        public void Listen()
        {
            while (isStarted)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    string endpoint = client.Client.RemoteEndPoint!.ToString();
                    string clientIp = ((IPEndPoint)client.Client.RemoteEndPoint!).Address.ToString();

                    if (bannedClients.ContainsKey(clientIp) &&
                        DateTime.Now < bannedClients[clientIp].AddMinutes(1))
                    {
                        using var stream = client.GetStream();
                        using var writer = new BinaryWriter(stream);
                        writer.Write("You are banned. Try again later.");
                        writer.Flush();
                        client.Close();
                        continue;
                    }
                    else if (bannedClients.ContainsKey(clientIp))
                    {
                        bannedClients.Remove(clientIp);
                    }

                    if (usersCount >= numericUsers.Value)
                    {
                        using var stream = client.GetStream();
                        using var writer = new BinaryWriter(stream);
                        writer.Write("Server doesnt have any places. Try again later");
                        writer.Flush();
                        client.Close();
                        continue;
                    }

                    NetworkStream str = client.GetStream();
                    BinaryReader reader = new BinaryReader(str);
                    BinaryWriter writerr = new(str);

                    if (!clients.ContainsKey(endpoint))
                    {
                        string usData = reader.ReadString();
                        string[] uspas = usData.Split(':');
                        if (accountsData.ContainsKey(uspas[0]) && accountsData[uspas[0]] == uspas[1])
                        {

                            clients.Add(endpoint, client);
                            requestCounts[clientIp] = 0;

                            this.Invoke(new Action(() =>
                            {
                                Conections.Items.Add($"{endpoint} connected");
                            }));
                            File.AppendAllText("log.txt", $"{DateTime.Now}: {endpoint} connected.\n");

                            var stream = client.GetStream();
                            var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                            writer.Write("Connected");
                            writer.Flush();
                            usersCount++;
                        }
                        else 
                        {
                            writerr.Write("Password or username incorrect. Try again later");
                            writerr.Flush();
                            client.Close();
                        }
                    }

                    Thread thread = new Thread(() => HandleClient(client, endpoint, clientIp));
                    thread.IsBackground = true;
                    thread.Start();
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        public void HandleClient(TcpClient client, string endpoint, string ip)
        {
            try
            {
                using NetworkStream stream = client.GetStream();
                using BinaryReader reader = new BinaryReader(stream);
                using BinaryWriter writer = new BinaryWriter(stream);

                while (client.Connected && isStarted)
                {
                    string message = reader.ReadString();

                    if (!requestCounts.ContainsKey(ip))
                        requestCounts[ip] = 0;

                    requestCounts[ip]++;

                    int limit = (int)numericUpDown1.Value;

                    if (requestCounts[ip] > limit)
                    {
                        writer.Write("Limit reached. Try again later.");
                        writer.Flush();

                        bannedClients[ip] = DateTime.Now;
                        break;
                    }

                    string response = courses.ContainsKey(message) ? courses[message] : "Course not found";

                    File.AppendAllText("log.txt", $"{message} sent to {endpoint} at {DateTime.Now}\n");

                    writer.Write(response);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", $"Error with {endpoint}: {ex.Message}\n");
            }
            finally
            {
                clients.Remove(endpoint);
                this.Invoke(new Action(() =>
                {
                    Conections.Items.Add($"{endpoint} disconnected");
                }));
                client.Close();
                File.AppendAllText("log.txt", $"{DateTime.Now}: {endpoint} disconnected.\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = UsGetter.Text;
            string[] uspas = data.Split(":");

            accountsData[uspas[0]] = uspas[1];
            this.Invoke(new Action(() =>
            {
                NamesAndPasswords.Items.Add($"{data}");
            }));
            UsGetter.Text = "";
        }
    }
}
