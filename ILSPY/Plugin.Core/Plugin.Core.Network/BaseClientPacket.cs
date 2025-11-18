using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Plugin.Core.Enums;

namespace Plugin.Core.Network;

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
		return BReader.ReadBytes(Length);
	}

	protected internal byte ReadC()
	{
		return BReader.ReadByte();
	}

	protected internal short ReadH()
	{
		return BReader.ReadInt16();
	}

	protected internal ushort ReadUH()
	{
		return BReader.ReadUInt16();
	}

	protected internal int ReadD()
	{
		return BReader.ReadInt32();
	}

	protected internal uint ReadUD()
	{
		return BReader.ReadUInt32();
	}

	protected internal float ReadT()
	{
		return BReader.ReadSingle();
	}

	protected internal long ReadQ()
	{
		return BReader.ReadInt64();
	}

	protected internal ulong ReadUQ()
	{
		return BReader.ReadUInt64();
	}

	protected internal double ReadF()
	{
		return BReader.ReadDouble();
	}

	protected internal string ReadN(int Length, string CodePage)
	{
		string text = "";
		try
		{
			text = Encoding.GetEncoding(CodePage).GetString(ReadB(Length));
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.Substring(0, num);
				return text;
			}
			return text;
		}
		catch
		{
			return text;
		}
	}

	protected internal string ReadS(int Length)
	{
		string text = "";
		try
		{
			text = Encoding.UTF8.GetString(ReadB(Length));
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.Substring(0, num);
				return text;
			}
			return text;
		}
		catch
		{
			return text;
		}
	}

	protected internal string ReadU(int Length)
	{
		string text = "";
		try
		{
			text = Encoding.Unicode.GetString(ReadB(Length));
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.Substring(0, num);
				return text;
			}
			return text;
		}
		catch
		{
			return text;
		}
	}
}
