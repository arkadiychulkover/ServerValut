using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerValut
{
    public partial class Form1 : Form
    {
        IPEndPoint ep;
        TcpListener listener;

        static Form1 form;

        static bool isStarted = false;

        Dictionary<string, TcpClient> clients = new();
        Dictionary<string, string> courses = new();

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
                    string clientEndPoint = client.Client.RemoteEndPoint!.ToString();

                    if (!clients.ContainsKey(clientEndPoint))
                    {
                        clients.Add(clientEndPoint, client);
                        this.Invoke(new Action(() =>
                        {
                            Conections.Items.Add(clientEndPoint + "conected");
                        }));
                        File.AppendAllText("log.txt", $"{DateTime.Now}: {clientEndPoint} connected.\n");
                        
                    }

                    Thread thread = new Thread(() => HandleClient(client));
                    thread.IsBackground = true;
                    thread.Start();
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        public void HandleClient(TcpClient client)
        {
            string endpoint = client.Client.RemoteEndPoint!.ToString();

            try
            {
                using NetworkStream stream = client.GetStream();
                using BinaryReader reader = new BinaryReader(stream);
                using BinaryWriter writer = new BinaryWriter(stream);

                while (client.Connected && isStarted)
                {
                    string message = reader.ReadString();
                    string response = courses.ContainsKey(message) ? courses[message] : "Course not found";

                    File.AppendAllText("log.txt" ,$"{message} sent to {endpoint} at {DateTime.Now.ToString()}\n");
                                        
                    writer.Write(response);
                    writer.Flush();
                }
            }
            catch
            {
            }
            finally
            {
                clients.Remove(endpoint);
                this.Invoke(new Action(() =>
                {
                    Conections.Items.Add(endpoint + "disconected");
                }));
                client.Close();
                File.AppendAllText("log.txt", $"{DateTime.Now}: {endpoint} disconnected.\n");
            }
        }
    }
}
