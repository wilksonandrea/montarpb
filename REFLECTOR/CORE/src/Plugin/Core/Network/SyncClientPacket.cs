namespace Plugin.Core.Network
{
    using Microsoft.Win32.SafeHandles;
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SharpDX;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SyncClientPacket : IDisposable
    {
        protected MemoryStream MStream;
        protected BinaryReader BReader;
        protected SafeHandle Handle;
        protected bool Disposed;

        public SyncClientPacket(byte[] byte_0)
        {
            this.MStream = new MemoryStream(byte_0, 0, byte_0.Length);
            this.BReader = new BinaryReader(this.MStream);
            this.Handle = new SafeFileHandle(IntPtr.Zero, true);
            this.Disposed = false;
        }

        public void Advance(int Bytes)
        {
            Stream baseStream = this.BReader.BaseStream;
            if ((baseStream.Position += Bytes) > this.BReader.BaseStream.Length)
            {
                CLogger.Print("Advance crashed.", LoggerType.Warning, null);
                throw new Exception("Offset has exceeded the buffer value.");
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (!this.Disposed)
            {
                this.MStream.Dispose();
                this.BReader.Dispose();
                if (Disposing)
                {
                    this.Handle.Dispose();
                }
                this.Disposed = true;
            }
        }

        public byte[] ReadB(int Length) => 
            this.BReader.ReadBytes(Length);

        public byte ReadC() => 
            this.BReader.ReadByte();

        public byte ReadC(out bool Exception)
        {
            try
            {
                Exception = false;
                return this.BReader.ReadByte();
            }
            catch
            {
                Exception = true;
                return 0;
            }
        }

        public int ReadD() => 
            this.BReader.ReadInt32();

        public double ReadF() => 
            this.BReader.ReadDouble();

        public short ReadH() => 
            this.BReader.ReadInt16();

        public string ReadN(int Length, string CodePage)
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

        public long ReadQ() => 
            this.BReader.ReadInt64();

        public string ReadS(int Length)
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

        public float ReadT() => 
            this.BReader.ReadSingle();

        public Half3 ReadTV() => 
            new Half3(this.ReadT(), this.ReadT(), this.ReadT());

        public string ReadU(int Length)
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

        public uint ReadUD() => 
            this.BReader.ReadUInt32();

        public ushort ReadUH() => 
            this.BReader.ReadUInt16();

        public ushort ReadUH(out bool Exception)
        {
            try
            {
                Exception = false;
                return this.BReader.ReadUInt16();
            }
            catch
            {
                Exception = true;
                return 0;
            }
        }

        public Half3 ReadUHV() => 
            new Half3(this.ReadUH(), this.ReadUH(), this.ReadUH());

        public ulong ReadUQ() => 
            this.BReader.ReadUInt64();

        public void SetMStream(MemoryStream MStream)
        {
            this.MStream = MStream;
        }

        public byte[] ToArray() => 
            this.MStream.ToArray();
    }
}

