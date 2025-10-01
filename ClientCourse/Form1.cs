using System.Net.Sockets;
using System.IO;

namespace ClientCourse
{
    public partial class Form1 : Form
    {
        static TcpClient? client;
        static BinaryWriter? writer;
        static BinaryReader? reader;

        static bool connected = false;
        static Form1? form;

        public Form1()
        {
            InitializeComponent();
            form = this;
        }

        private void Conector_Click(object sender, EventArgs e)
        {
            Connect();
        }

        static void Connect()
        {
            if (!connected)
            {
                try
                {
                    client = new TcpClient("127.0.0.1", 2565);
                    NetworkStream stream = client.GetStream();

                    reader = new BinaryReader(stream);
                    writer = new BinaryWriter(stream);

                    string resp = reader.ReadString();

                    if (resp != "Connected")
                    {
                        form!.Invoke(new Action(() =>
                        {
                            form.Messages.Items.Add($"Server: {resp}");
                        }));
                        return;
                    }

                    connected = true;
                    MessageBox.Show("Connected to server");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                client?.Close();
                client = null;
                writer = null;
                reader = null;
                connected = false;
                MessageBox.Show("Disconnected");
            }
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (textBox1.Text != "")
                SendMessage(textBox1.Text);
            textBox1.Text = "";
        }

        static void SendMessage(string message)
        {
            if (connected && client != null && writer != null && reader != null)
            {
                try
                {
                    writer.Write(message);
                    writer.Flush();

                    string response = reader.ReadString();

                    form!.Invoke(new Action(() =>
                    {
                        form.Messages.Items.Add($"Server: {response}");
                    }));

                    if (response == "Limit reached. Try again later.")
                    {
                        connected = false;
                        MessageBox.Show("Disconnected");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    connected = false;
                }
            }
            else
            {
                MessageBox.Show("You are not connected to the server");
            }
        }
    }
}