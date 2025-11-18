namespace Plugin.Core.Utility
{
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Crypto.Prng;
    using Org.BouncyCastle.Security;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class Bitwise
    {
        private static readonly string string_0 = string.Format("\n", new object[0]);
        private static readonly string string_1 = string.Format("", new object[0]);
        private static readonly char[] char_0 = new char[0x100];
        private static readonly string[] string_2 = new string[0x100];
        private static readonly string[] string_3 = new string[0x10];
        private static readonly string[] string_4 = new string[0x10];
        public static readonly int[] CRYPTO = new int[] { 0x74c2, 0x7ff7, 0x550 };

        static Bitwise()
        {
            for (int i = 0; i < 10; i++)
            {
                StringBuilder builder = new StringBuilder(3);
                builder.Append(" 0");
                builder.Append(i);
                string_2[i] = builder.ToString().ToUpper();
            }
            for (int j = 10; j < 0x10; j++)
            {
                StringBuilder builder2 = new StringBuilder(3);
                builder2.Append(" 0");
                builder2.Append((char) ((0x61 + j) - 10));
                string_2[j] = builder2.ToString().ToUpper();
            }
            for (int k = 0x10; k < string_2.Length; k++)
            {
                StringBuilder builder3 = new StringBuilder(3);
                builder3.Append(' ');
                builder3.Append(k.ToString("X"));
                string_2[k] = builder3.ToString().ToUpper();
            }
            int index = 0;
            while (index < string_3.Length)
            {
                int num5 = string_3.Length - index;
                StringBuilder builder4 = new StringBuilder(num5 * 3);
                int num6 = 0;
                while (true)
                {
                    if (num6 >= num5)
                    {
                        string_3[index] = builder4.ToString().ToUpper();
                        index++;
                        break;
                    }
                    builder4.Append("   ");
                    num6++;
                }
            }
            int num7 = 0;
            while (num7 < string_4.Length)
            {
                int capacity = string_4.Length - num7;
                StringBuilder builder5 = new StringBuilder(capacity);
                int num9 = 0;
                while (true)
                {
                    if (num9 >= capacity)
                    {
                        string_4[num7] = builder5.ToString().ToUpper();
                        num7++;
                        break;
                    }
                    builder5.Append(' ');
                    num9++;
                }
            }
            for (int m = 0; m < char_0.Length; m++)
            {
                char_0[m] = ((m <= 0x1f) || (m >= 0x7f)) ? '.' : ((char) m);
            }
        }

        public static byte[] ArrayRandomize(byte[] Source)
        {
            if ((Source == null) || (Source.Length < 2))
            {
                return Source;
            }
            byte[] destinationArray = new byte[Source.Length];
            Array.Copy(Source, destinationArray, Source.Length);
            Random random = new Random();
            for (int i = destinationArray.Length - 1; i > 0; i--)
            {
                int index = random.Next(i + 1);
                ref byte numRef = ref destinationArray[i];
                byte num3 = destinationArray[index];
                byte num4 = destinationArray[i];
                numRef = num3;
                destinationArray[index] = num4;
            }
            return destinationArray;
        }

        public static int BytesToInt(byte Byte1, byte Byte2, byte Byte3, byte Byte4) => 
            (((Byte4 << 0x18) | (Byte3 << 0x10)) | (Byte2 << 8)) | Byte1;

        public static byte[] Decrypt(byte[] Data, int Shift)
        {
            byte[] destinationArray = new byte[Data.Length];
            Array.Copy(Data, 0, destinationArray, 0, destinationArray.Length);
            byte num = destinationArray[destinationArray.Length - 1];
            for (int i = destinationArray.Length - 1; i > 0; i--)
            {
                destinationArray[i] = (byte) (((destinationArray[i - 1] & 0xff) << ((8 - Shift) & 0x1f)) | ((destinationArray[i] & 0xff) >> (Shift & 0x1f)));
            }
            destinationArray[0] = (byte) ((num << ((8 - Shift) & 0x1f)) | ((destinationArray[0] & 0xff) >> (Shift & 0x1f)));
            return destinationArray;
        }

        public static byte[] Encrypt(byte[] Data, int Shift)
        {
            byte[] destinationArray = new byte[Data.Length];
            Array.Copy(Data, 0, destinationArray, 0, destinationArray.Length);
            byte num = destinationArray[0];
            for (int i = 0; i < (destinationArray.Length - 1); i++)
            {
                destinationArray[i] = (byte) (((destinationArray[i + 1] & 0xff) >> ((8 - Shift) & 0x1f)) | ((destinationArray[i] & 0xff) << (Shift & 0x1f)));
            }
            destinationArray[destinationArray.Length - 1] = (byte) ((num >> ((8 - Shift) & 0x1f)) | ((destinationArray[destinationArray.Length - 1] & 0xff) << (Shift & 0x1f)));
            return destinationArray;
        }

        public static string GenerateRandomPassword(string AllowerChars, int Length, string Salt)
        {
            string str;
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[Length];
                provider.GetBytes(data);
                char[] chArray = new char[Length];
                int index = 0;
                while (true)
                {
                    if (index >= Length)
                    {
                        str = HashString(new string(chArray), Salt, Length);
                        break;
                    }
                    chArray[index] = AllowerChars[data[index] % AllowerChars.Length];
                    index++;
                }
            }
            return str;
        }

        public static uint GenerateRandomUInt()
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[4];
                provider.GetBytes(data);
                return BitConverter.ToUInt32(data, 0);
            }
        }

        public static ushort GenerateRandomUShort()
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[2];
                provider.GetBytes(data);
                return BitConverter.ToUInt16(data, 0);
            }
        }

        public static List<byte[]> GenerateRSAKeyPair(int SessionId, int SecurityKey, int SeedLength)
        {
            List<byte[]> list = new List<byte[]>();
            RsaKeyPairGenerator generator1 = new RsaKeyPairGenerator();
            generator1.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), SeedLength));
            RsaKeyParameters @public = (RsaKeyParameters) generator1.GenerateKeyPair().Public;
            list.Add(@public.Modulus.ToByteArrayUnsigned());
            list.Add(@public.Exponent.ToByteArrayUnsigned());
            byte[] bytes = BitConverter.GetBytes((int) (SessionId + SecurityKey));
            Array.Copy(bytes, 0, list[0], 0, Math.Min(bytes.Length, list[0].Length));
            return list;
        }

        public static string HashString(string Text, string Salt, int Length = 0x20)
        {
            using (HMACMD5 hmacmd = new HMACMD5(Encoding.UTF8.GetBytes(Salt)))
            {
                return smethod_0(hmacmd.ComputeHash(Encoding.UTF8.GetBytes(Text))).Substring(0, Length);
            }
        }

        public static string HexArrayToString(byte[] Buffer, int Length)
        {
            string str = "";
            try
            {
                str = Encoding.Unicode.GetString(Buffer, 0, Length);
                int index = str.IndexOf('\0');
                if (index != -1)
                {
                    str = str.Substring(0, index);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
            return str;
        }

        public static byte[] HexStringToByteArray(string HexString)
        {
            string source = HexString.Replace(":", "").Replace("-", "").Replace(" ", "");
            byte[] buffer = new byte[source.Length / 2];
            for (int i = 0; i < source.Length; i += 2)
            {
                buffer[i / 2] = (byte) ((smethod_2(source.ElementAt<char>(i)) << 4) | smethod_2(source.ElementAt<char>(i + 1)));
            }
            return buffer;
        }

        public static bool ProcessPacket(byte[] PacketData, int BytesToSkip, int BytesToKeepAtEnd, ushort[] Opcodes)
        {
            Class4 class2 = new Class4 {
                ushort_0 = BitConverter.ToUInt16(PacketData, 2)
            };
            if (Opcodes.FirstOrDefault<ushort>(new Func<ushort, bool>(class2.method_0)) != 0)
            {
                return false;
            }
            int count = (PacketData.Length - BytesToSkip) - BytesToKeepAtEnd;
            if (count < 0)
            {
                count = 0;
            }
            int num2 = BytesToSkip + BytesToKeepAtEnd;
            if (PacketData.Length < num2)
            {
                CLogger.Print("PacketData is too short to apply encryption logic.", LoggerType.Warning, null);
                return false;
            }
            byte[] sourceArray = CryptoSyncer.Encrypt(PacketData.Skip<byte>(BytesToSkip).Take<byte>(count).ToArray<byte>());
            if (sourceArray.Length != count)
            {
                CLogger.Print("Encrypted data length mismatch! Encryption function changed data size.", LoggerType.Warning, null);
                return false;
            }
            Array.Copy(sourceArray, 0, PacketData, BytesToSkip, sourceArray.Length);
            return true;
        }

        public static string ReadFile(string Path)
        {
            string str = "";
            using (MD5 md = MD5.Create())
            {
                using (FileStream stream = new FileInfo(Path).Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    str = BitConverter.ToString(md.ComputeHash(stream)).Replace("-", string.Empty);
                    stream.Close();
                }
            }
            return str;
        }

        private static string smethod_0(byte[] byte_0)
        {
            StringBuilder builder = new StringBuilder();
            byte[] buffer = byte_0;
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(((int) buffer[i]).ToString("x2"));
            }
            return builder.ToString();
        }

        private static byte[] smethod_1(string string_5)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(string_5));
            }
        }

        private static int smethod_2(char char_1) => 
            ((char_1 < '0') || (char_1 > '9')) ? (((char_1 < 'A') || (char_1 > 'F')) ? (((char_1 < 'a') || (char_1 > 'f')) ? 0 : ((char_1 - 'a') + 10)) : ((char_1 - 'A') + 10)) : (char_1 - '0');

        public static string ToByteString(byte[] Result)
        {
            string str = "";
            char[] separator = new char[] { '-', ',', '.', ':', '\t' };
            foreach (string str2 in BitConverter.ToString(Result).Split(separator))
            {
                str = str + " " + str2;
            }
            return str;
        }

        public static string ToHexData(string EventName, byte[] BuffData)
        {
            int length = BuffData.Length;
            int num2 = 0;
            int num3 = BuffData.Length;
            StringBuilder builder = new StringBuilder((((((length / 0x10) + ((length % 15) != 0)) + 4) * 80) + EventName.Length) + 0x10);
            builder.Append(string_1 + "+--------+-------------------------------------------------+----------------+");
            builder.Append($"{string_0}[!] {EventName}; Length: [{BuffData.Length} Bytes] </>");
            builder.Append(string_0 + "         +-------------------------------------------------+");
            builder.Append(string_0 + "         |  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F |");
            builder.Append(string_0 + "+--------+-------------------------------------------------+----------------+");
            int index = 0;
            while (index < num3)
            {
                int num5 = index - num2;
                int num1 = num5 & 15;
                if (num1 == 0)
                {
                    builder.Append(string_0);
                    builder.Append(((num5 & 0xffffffffL) | 0x100000000L).ToString("X"));
                    builder[builder.Length - 9] = '|';
                    builder.Append('|');
                }
                builder.Append(string_2[BuffData[index]]);
                if (num1 == 15)
                {
                    builder.Append(" |");
                    int num7 = index - 15;
                    while (true)
                    {
                        if (num7 > index)
                        {
                            builder.Append('|');
                            break;
                        }
                        builder.Append(char_0[BuffData[num7]]);
                        num7++;
                    }
                }
                index++;
            }
            if (((index - num2) & 15) != 0)
            {
                int num8 = length & 15;
                builder.Append(string_3[num8]);
                builder.Append(" |");
                int num9 = index - num8;
                while (true)
                {
                    if (num9 >= index)
                    {
                        builder.Append(string_4[num8]);
                        builder.Append('|');
                        break;
                    }
                    builder.Append(char_0[BuffData[num9]]);
                    num9++;
                }
            }
            builder.Append(string_0 + "+--------+-------------------------------------------------+----------------+");
            return builder.ToString();
        }

        [CompilerGenerated]
        private sealed class Class4
        {
            public ushort ushort_0;

            internal bool method_0(ushort ushort_1) => 
                ushort_1 == this.ushort_0;
        }
    }
}

