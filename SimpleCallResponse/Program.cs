using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using EncryptionClass;
using System.Numerics;

namespace SimpleCallResponse
{
    class Program
    {
        public static Encryption Encrypt = new Encryption();
        public static byte PrivateKey = 15;
        public static byte[] key;
        public static BigInteger nkey = 11231231232;
        public static BigInteger gkey = 12;
        public static BigInteger pkey = 9872349782;

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);
           

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            Console.WriteLine("Hvad vil du?\n\"c\" for at annullere");
            

            byte[] keybuffer = EncryptionClass.Encryption.CreatePublicKey(pkey, gkey, nkey);
            stream.Write(keybuffer, 0, keybuffer.Length);

            byte[] buffer = new byte[256];
            int read = stream.Read(buffer, 0, buffer.Length);

            byte[] keyfromserver = new byte[read];
            Array.Copy(buffer, 0, keyfromserver, 0, read);
            key = EncryptionClass.Encryption.CreatePrivateKey(pkey, new BigInteger(keyfromserver), nkey);


            RecieveMessage(stream);
            while (true)
            {
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "c")
                {
                    break;
                }
                byte[] messagebuffer = Encoding.UTF8.GetBytes(userInput);
                Encrypt.EncryptByte(messagebuffer, key);
                stream.Write(messagebuffer, 0, messagebuffer.Length);
            }
            client.Close();
        }

        public static async void RecieveMessage(NetworkStream stream)
        {
            while (true)
            {
                byte[] buffer = new byte[256];
                int nBytesRead = await stream.ReadAsync(buffer, 0, 256);
                Encrypt.DecryptByte(buffer, key);
                string message = Encoding.UTF8.GetString(buffer, 0, nBytesRead);
                if (message != string.Empty)
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}