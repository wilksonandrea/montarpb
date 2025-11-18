namespace Plugin.Core.Network
{
    using Plugin.Core.Enums;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class BaseClientPacket
    {
        protected MemoryStream MStream;
        protected BinaryReader BReader;
        protected SafeHandle Handle;
        protected bool Disposed;
        protected int SECURITY_KEY;
        protected int HASH_CODE;
        protected int SEED_LENGTH;
        protected NationsEnum NATIONS;

        protected internal byte[] ReadB(int Length) => 
            this.BReader.ReadBytes(Length);

        protected internal byte ReadC() => 
            this.BReader.ReadByte();

        protected internal int ReadD() => 
            this.BReader.ReadInt32();

        protected internal double ReadF() => 
            this.BReader.ReadDouble();

        protected internal short ReadH() => 
            this.BReader.ReadInt16();

        protected internal string ReadN(int Length, string CodePage)
        {
            string str = "";
            try
            {
                str = Encoding.GetEncoding(CodePage).GetString(this.ReadB(Length));
                int index = str.IndexOf('\0');
                if (index != -1)
                {
                    str = str.Substring(0, index);
                }
            }
            catch
            {
            }
            return str;
        }

        protected internal long ReadQ() => 
            this.BReader.ReadInt64();

        protected internal string ReadS(int Length)
        {
            string str = "";
            try
            {
                str = Encoding.UTF8.GetString(this.ReadB(Length));
                int index = str.IndexOf('\0');
                if (index != -1)
                {
                    str = str.Substring(0, index);
                }
            }
            catch
            {
            }
            return str;
        }

        protected internal float ReadT() => 
            this.BReader.ReadSingle();

        protected internal string ReadU(int Length)
        {
            string str = "";
            try
            {
                str = Encoding.Unicode.GetString(this.ReadB(Length));
                int index = str.IndexOf('\0');
                if (index != -1)
                {
                    str = str.Substring(0, index);
                }
            }
            catch
            {
            }
            return str;
        }

        protected internal uint ReadUD() => 
            this.BReader.ReadUInt32();

        protected internal ushort ReadUH() => 
            this.BReader.ReadUInt16();

        protected internal ulong ReadUQ() => 
            this.BReader.ReadUInt64();
    }
}

