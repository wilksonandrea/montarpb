using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Plugin.Core.Utility
{
    public static class CryptoSyncer
    {
        private static readonly byte[] byte_0;
        private static readonly byte[] byte_1;

        static CryptoSyncer()
        {
            byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");
            byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");
        }

        private static byte[] smethod_0(byte[] byte_2)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.Key = byte_0;

                using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
                {
                    byte[] counter = (byte[])byte_1.Clone();
                    byte[] result = new byte[byte_2.Length];

                    for (int i = 0; i < byte_2.Length; i += 16)
                    {
                        byte[] block = cryptoTransform.TransformFinalBlock(counter, 0, 16);
                        int blockSize = Math.Min(16, byte_2.Length - i);

                        for (int j = 0; j < blockSize; j++)
                        {
                            result[i + j] = (byte)(byte_2[i + j] ^ block[j]);
                        }

                        int index = 15;
                        while (index >= 0 && ++counter[index] == 0)
                        {
                            index--;
                        }
                    }
                    return result;
                }
            }
        }

        public static byte[] Encrypt(byte[] Plain)
        {
            return smethod_0(Plain);
        }

        public static byte[] Decrypt(byte[] Cipher)
        {
            return smethod_0(Cipher);
        }

        public static void MD5File(string dist)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                using (FileStream inputStream = File.OpenRead(dist))
                {
                    string hash = BitConverter.ToString(md5.ComputeHash(inputStream)).Replace("-", "").ToLower();
                    Console.WriteLine("MD5: " + hash);
                }
            }
            catch { }
        }

        public static char[] HexCodes(byte[] text)
        {
            char[] array = new char[text.Length * 2];
            for (int i = 0; i < text.Length; i++)
            {
                text[i].ToString("x2").CopyTo(0, array, i * 2, 2);
            }
            return array;
        }

        public static string SHA1File(string msg)
        {
            try
            {
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(msg));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                        sb.Append(hash[i].ToString("x2"));
                    return sb.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public static int SHA1_Int32(string msg)
        {
            try
            {
                return int.Parse(SHA1File(msg), NumberStyles.HexNumber);
            }
            catch
            {
                return 0;
            }
        }

        public static string GetHWID()
        {
            try
            {
                string s = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") +
                           Environment.GetEnvironmentVariable("COMPUTERNAME") +
                           Environment.UserName.Trim();

                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("x3").Substring(0, 3));
                        if (i != hash.Length - 1)
                            sb.Append("-");
                    }
                    return sb.ToString();
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
