using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionClass
{
   public class Encryption
    {
        public void EncryptionVariables()
        {
            byte[] bytes = Encoding.UTF8.GetBytes("a");
            byte key = 255;
            PrintBytesUTF8(bytes);
            EncryptByte(bytes, key);
            PrintBytesUTF8(bytes);
            DecryptByte(bytes, key);
            PrintBytesUTF8(bytes);
           //pis 2
        }

        //Substituion
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

        //Simple encrypt
        public static byte[] Encrypt(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += 69;
            }
            return bytes;
        }

        //Simpel Decrypt
        public static string Dekrypter(byte[] bytes)
        {

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= 69;
            }

            string message = Encoding.UTF8.GetString(bytes);

            return message;
        }
    }
}
