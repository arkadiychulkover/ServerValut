using System.Net.Sockets;
using System.IO;

namespace ClientCourse
{
    public partial class Form1 : Form
    {
        static TcpClient? client;
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
                if (client == null)
                    client = new TcpClient();
                client.Connect("127.0.0.1", 2565);
                connected = true;
                MessageBox.Show("Connected to server");
            }
            else
            {
                client?.Close();
                client = null;
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
            if (connected && client != null)
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);

                writer.Write(message);
                writer.Flush();

                string response = reader.ReadString();
                form!.Messages.Items.Add($"Server: {response}");
            }
            else
            {
                MessageBox.Show("You are not connected to the server");
            }
        }
    }
}
