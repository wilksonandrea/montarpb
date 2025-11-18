using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Plugin.Core.Network
{
	public class SyncClientPacket : IDisposable
	{
		protected MemoryStream MStream;

		protected BinaryReader BReader;

		protected SafeHandle Handle;

		protected bool Disposed;

		public SyncClientPacket(byte[] byte_0)
		{
			this.MStream = new MemoryStream(byte_0, 0, (int)byte_0.Length);
			this.BReader = new BinaryReader(this.MStream);
			this.Handle = new SafeFileHandle(IntPtr.Zero, true);
			this.Disposed = false;
		}

		public void Advance(int Bytes)
		{
			Stream baseStream = this.BReader.BaseStream;
			long position = baseStream.Position + (long)Bytes;
			long ınt64 = position;
			baseStream.Position = position;
			if (ınt64 > this.BReader.BaseStream.Length)
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
			if (this.Disposed)
			{
				return;
			}
			this.MStream.Dispose();
			this.BReader.Dispose();
			if (Disposing)
			{
				this.Handle.Dispose();
			}
			this.Disposed = true;
		}

		public byte[] ReadB(int Length)
		{
			return this.BReader.ReadBytes(Length);
		}

		public byte ReadC()
		{
			return this.BReader.ReadByte();
		}

		public byte ReadC(out bool Exception)
		{
			byte num;
			try
			{
				Exception = false;
				num = this.BReader.ReadByte();
			}
			catch
			{
				Exception = true;
				num = 0;
			}
			return num;
		}

		public int ReadD()
		{
			return this.BReader.ReadInt32();
		}

		public double ReadF()
		{
			return this.BReader.ReadDouble();
		}

		public short ReadH()
		{
			return this.BReader.ReadInt16();
		}

		public string ReadN(int Length, string CodePage)
		{
			string str = "";
			try
			{
				str = Encoding.GetEncoding(CodePage).GetString(this.ReadB(Length));
				int ınt32 = str.IndexOf('\0');
				if (ınt32 != -1)
				{
					str = str.Substring(0, ınt32);
				}
			}
			catch
			{
			}
			return str;
		}

		public long ReadQ()
		{
			return this.BReader.ReadInt64();
		}

		public string ReadS(int Length)
		{
			string str = "";
			try
			{
				str = Encoding.UTF8.GetString(this.ReadB(Length));
				int ınt32 = str.IndexOf('\0');
				if (ınt32 != -1)
				{
					str = str.Substring(0, ınt32);
				}
			}
			catch
			{
			}
			return str;
		}

		public float ReadT()
		{
			return this.BReader.ReadSingle();
		}

		public Half3 ReadTV()
		{
			return new Half3(this.ReadT(), this.ReadT(), this.ReadT());
		}

		public string ReadU(int Length)
		{
			string str = "";
			try
			{
				str = Encoding.Unicode.GetString(this.ReadB(Length));
				int ınt32 = str.IndexOf('\0');
				if (ınt32 != -1)
				{
					str = str.Substring(0, ınt32);
				}
			}
			catch
			{
			}
			return str;
		}

		public uint ReadUD()
		{
			return this.BReader.ReadUInt32();
		}

		public ushort ReadUH()
		{
			return this.BReader.ReadUInt16();
		}

		public ushort ReadUH(out bool Exception)
		{
			ushort uInt16;
			try
			{
				Exception = false;
				uInt16 = this.BReader.ReadUInt16();
			}
			catch
			{
				Exception = true;
				uInt16 = 0;
			}
			return uInt16;
		}

		public Half3 ReadUHV()
		{
			return new Half3(this.ReadUH(), this.ReadUH(), this.ReadUH());
		}

		public ulong ReadUQ()
		{
			return this.BReader.ReadUInt64();
		}

		public void SetMStream(MemoryStream MStream)
		{
			this.MStream = MStream;
		}

		public byte[] ToArray()
		{
			return this.MStream.ToArray();
		}
	}
}