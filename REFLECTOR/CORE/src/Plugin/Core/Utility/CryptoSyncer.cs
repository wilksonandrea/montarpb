namespace Plugin.Core.Utility
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class CryptoSyncer
    {
        private static readonly byte[] byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");
        private static readonly byte[] byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");

        public static byte[] Decrypt(byte[] Cipher) => 
            smethod_0(Cipher);

        public static byte[] Encrypt(byte[] Plain) => 
            smethod_0(Plain);

        public static string GetHWID()
        {
            string str2;
            try
            {
                string s = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + Environment.GetEnvironmentVariable("COMPUTERNAME") + Environment.UserName.Trim();
                using (MD5 md = MD5.Create())
                {
                    byte[] buffer = md.ComputeHash(Encoding.UTF8.GetBytes(s));
                    StringBuilder builder = new StringBuilder();
                    int index = 0;
                    while (true)
                    {
                        if (index >= buffer.Length)
                        {
                            str2 = builder.ToString();
                            break;
                        }
                        builder.Append(buffer[index].ToString("x3").Substring(0, 3));
                        if (index != (buffer.Length - 1))
                        {
                            builder.Append("-");
                        }
                        index++;
                    }
                }
            }
            catch (Exception)
            {
                str2 = "";
            }
            return str2;
        }

        public static char[] HexCodes(byte[] text)
        {
            char[] destination = new char[text.Length * 2];
            for (int i = 0; i < text.Length; i++)
            {
                text[i].ToString("x2").CopyTo(0, destination, i * 2, 2);
            }
            return destination;
        }

        public static void MD5File(string dist)
        {
            try
            {
                using (MD5 md = MD5.Create())
                {
                    using (FileStream stream = File.OpenRead(dist))
                    {
                        Console.WriteLine("MD5: " + BitConverter.ToString(md.ComputeHash(stream)).Replace("-", "").ToLower());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static int SHA1_Int32(string msg)
        {
            try
            {
                return int.Parse(SHA1File(msg), NumberStyles.HexNumber);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string SHA1File(string msg)
        {
            string str;
            try
            {
                using (SHA1 sha = SHA1.Create())
                {
                    byte[] buffer = sha.ComputeHash(Encoding.UTF8.GetBytes(msg));
                    StringBuilder builder = new StringBuilder();
                    int index = 0;
                    while (true)
                    {
                        if (index >= buffer.Length)
                        {
                            str = builder.ToString();
                            break;
                        }
                        builder.Append(buffer[index].ToString("x2"));
                        index++;
                    }
                }
            }
            catch (Exception)
            {
                str = "";
            }
            return str;
        }

        private static unsafe byte[] smethod_0(byte[] byte_2)
        {
            byte[] buffer4;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.Key = byte_0;
                using (ICryptoTransform transform = aes.CreateEncryptor())
                {
                    byte[] inputBuffer = (byte[]) byte_1.Clone();
                    byte[] buffer2 = new byte[byte_2.Length];
                    int num = 0;
                    while (true)
                    {
                        if (num >= byte_2.Length)
                        {
                            buffer4 = buffer2;
                            break;
                        }
                        byte[] buffer3 = transform.TransformFinalBlock(inputBuffer, 0, 0x10);
                        int num2 = Math.Min(0x10, byte_2.Length - num);
                        int index = 0;
                        while (true)
                        {
                            if (index >= num2)
                            {
                                int num4 = 15;
                                while (true)
                                {
                                    if (num4 >= 0)
                                    {
                                        byte* numPtr1 = &(inputBuffer[num4]);
                                        byte num5 = (byte) (numPtr1[0] + 1);
                                        numPtr1[0] = num5;
                                        if (num5 == 0)
                                        {
                                            num4--;
                                            continue;
                                        }
                                    }
                                    num += 0x10;
                                    break;
                                }
                                break;
                            }
                            buffer2[num + index] = (byte) (byte_2[num + index] ^ buffer3[index]);
                            index++;
                        }
                    }
                }
            }
            return buffer4;
        }
    }
}

