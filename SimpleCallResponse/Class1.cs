using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCallResponse
{
    class Encryption
    {
        public Encryption()
        {
            byte[] bytes = Encoding.UTF8.GetBytes("a");
            byte key = 255;
            PrintBytesUTF8(bytes);
            EncryptByte(bytes, key);
            PrintBytesUTF8(bytes);
            DecryptByte(bytes, key);
            PrintBytesUTF8(bytes);
        }

        public void EncryptByte(byte[] bytes, byte key)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int change = i + key;
                while (change + bytes[i] > 255) change -= 256;
                bytes[i] += (byte)change;
            }
        }

        public void DecryptByte(byte[] bytes, byte key)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int change = i + key;
                while (change - bytes[i] < 0) change += 256;
                bytes[i] -= (byte)change;
            }
        }

        public void PrintBytesUTF8(byte[] bytes)
        {
            string toPrint = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(toPrint);
        }
    }
}
