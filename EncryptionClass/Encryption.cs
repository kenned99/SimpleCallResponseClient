using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace EncryptionClass
{
   public class Encryption
    {
        public static BigInteger key;

        //Substituion
        public void EncryptByte(byte[] bytes, byte[] key)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int change = i + key[0];
                while (change + bytes[i] > 255) change -= 256;
                bytes[i] += (byte)change;
            }
        }

        public void DecryptByte(byte[] bytes, byte[] key)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int change = i + key[0];
                while (change - bytes[i] < 0) change += 256;
                bytes[i] -= (byte)change;
            }
        }

        public void PrintBytesUTF8(byte[] bytes)
        {
            string toPrint = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(toPrint);
        }

        public static byte[] Encrypt(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += 69;
            }
            return bytes;
        }

        public static string Dekrypter(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= 69;
            }

            string message = Encoding.UTF8.GetString(bytes);

            return message;
        }

        //Permutation encrypt
        public static string PermutationEncrypt(string plainText, string key)
        {
            char[] chars = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == ' ')
                {
                    chars[i] = ' ';
                }

                else
                {
                    int j = plainText[i] - 97;
                    chars[i] = key[j];
                }
            }
            return new string(chars);
        }

        //Permutation Decrypt
        public static string PermutationDecrypt(string cipherText, string key)
        {
            char[] chars = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText[i] == ' ')
                {
                    chars[i] = ' ';
                }
                else
                {
                    int j = key.IndexOf(cipherText[i]) + 97;
                    chars[i] = (char)j;
                }
            }
            return new string(chars);
        }
        public static byte[] CreatePublicKey(BigInteger privkey, BigInteger genkey, BigInteger norkey)
        {
            /* BigInteger pkey = new BigInteger(Int32.Parse(privkey)); // private key
             BigInteger gkey = new BigInteger(Int32.Parse(genkey)); // i anden
             BigInteger nkey = new BigInteger(Int32.Parse(norkey)); // MOD*/

            BigInteger mykey = BigInteger.ModPow(genkey, privkey, norkey);
            return mykey.ToByteArray();
        }

        public static byte[] CreatePrivateKey(BigInteger privatekey, BigInteger keyfromclient, BigInteger norkey)
        {
            /* BigInteger pkey = new BigInteger(privatekey3);
             BigInteger mkey = new BigInteger(meutralkey);
             BigInteger skey = new BigInteger(samekey);*/

            return BigInteger.ModPow(keyfromclient, privatekey, norkey).ToByteArray();
        }
    }
}
