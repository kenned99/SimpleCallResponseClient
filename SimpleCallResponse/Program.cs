using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleCallResponse
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            Console.WriteLine("Hvad vil du?\n\"c\" for at annullere");
            RecieveMessage(stream);
            while (true)
            {
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "c")
                {
                    break;
                }

                byte[] buffer = Encoding.UTF8.GetBytes(userInput);
                stream.Write(buffer, 0, buffer.Length);

            }
            client.Close();
        }

        public static async void RecieveMessage(NetworkStream stream)
        {
            while (true)
            {
                byte[] buffer = new byte[256];
                int nBytesRead = await stream.ReadAsync(buffer, 0, 256);
                string message = Encoding.UTF8.GetString(buffer, 0, nBytesRead);
                if (message != string.Empty)
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}