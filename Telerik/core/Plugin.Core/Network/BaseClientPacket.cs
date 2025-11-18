using Plugin.Core.Enums;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Plugin.Core.Network
{
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

		public BaseClientPacket()
		{
		}

		protected internal byte[] ReadB(int Length)
		{
			return this.BReader.ReadBytes(Length);
		}

		protected internal byte ReadC()
		{
			return this.BReader.ReadByte();
		}

		protected internal int ReadD()
		{
			return this.BReader.ReadInt32();
		}

		protected internal double ReadF()
		{
			return this.BReader.ReadDouble();
		}

		protected internal short ReadH()
		{
			return this.BReader.ReadInt16();
		}

		protected internal string ReadN(int Length, string CodePage)
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

		protected internal long ReadQ()
		{
			return this.BReader.ReadInt64();
		}

		protected internal string ReadS(int Length)
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

		protected internal float ReadT()
		{
			return this.BReader.ReadSingle();
		}

		protected internal string ReadU(int Length)
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

		protected internal uint ReadUD()
		{
			return this.BReader.ReadUInt32();
		}

		protected internal ushort ReadUH()
		{
			return this.BReader.ReadUInt16();
		}

		protected internal ulong ReadUQ()
		{
			return this.BReader.ReadUInt64();
		}
	}
}