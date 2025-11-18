namespace Plugin.Core.Network
{
    using Microsoft.Win32.SafeHandles;
    using Plugin.Core.SharpDX;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SyncServerPacket : IDisposable
    {
        protected MemoryStream MStream;
        protected BinaryWriter BWriter;
        protected SafeHandle Handle;
        protected bool Disposed;

        public SyncServerPacket()
        {
            this.MStream = new MemoryStream();
            this.BWriter = new BinaryWriter(this.MStream);
            this.Handle = new SafeFileHandle(IntPtr.Zero, true);
            this.Disposed = false;
        }

        public SyncServerPacket(long long_0)
        {
            this.MStream = new MemoryStream();
            this.MStream.SetLength(long_0);
            this.BWriter = new BinaryWriter(this.MStream);
            this.Handle = new SafeFileHandle(IntPtr.Zero, true);
            this.Disposed = false;
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
                this.BWriter.Dispose();
                if (Disposing)
                {
                    this.Handle.Dispose();
                }
                this.Disposed = true;
            }
        }

        public void GoBack(int Value)
        {
            Stream baseStream = this.BWriter.BaseStream;
            baseStream.Position -= Value;
        }

        public void SetMStream(MemoryStream MStream)
        {
            this.MStream = MStream;
        }

        public byte[] ToArray() => 
            this.MStream.ToArray();

        public void WriteB(byte[] Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteB(byte[] Value, int Offset, int Length)
        {
            this.BWriter.Write(Value, Offset, Length);
        }

        public void WriteC(byte Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteD(int Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteD(uint Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteF(double Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteH(short Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteH(ushort Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteHV(Half3 Half)
        {
            this.WriteH(Half.X.RawValue);
            this.WriteH(Half.Y.RawValue);
            this.WriteH(Half.Z.RawValue);
        }

        public void WriteN(string Name, int Count, string CodePage)
        {
            if (Name != null)
            {
                this.WriteB(Encoding.GetEncoding(CodePage).GetBytes(Name));
                this.WriteB(new byte[Count - Name.Length]);
            }
        }

        public void WriteQ(long Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteQ(ulong Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteS(string Text, int Count)
        {
            if (Text != null)
            {
                this.WriteB(Encoding.UTF8.GetBytes(Text));
                this.WriteB(new byte[Count - Text.Length]);
            }
        }

        public void WriteT(float Value)
        {
            this.BWriter.Write(Value);
        }

        public void WriteTV(Half3 Half)
        {
            this.WriteT((float) Half.X);
            this.WriteT((float) Half.Y);
            this.WriteT((float) Half.Z);
        }

        public void WriteU(string Text, int Count)
        {
            if (Text != null)
            {
                this.WriteB(Encoding.Unicode.GetBytes(Text));
                this.WriteB(new byte[Count - (Text.Length * 2)]);
            }
        }
    }
}

