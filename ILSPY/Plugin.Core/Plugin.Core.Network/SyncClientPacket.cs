using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;

namespace Plugin.Core.Network;

public class SyncClientPacket : IDisposable
{
	protected MemoryStream MStream;

	protected BinaryReader BReader;

	protected SafeHandle Handle;

	protected bool Disposed;

	public SyncClientPacket(byte[] byte_0)
	{
		MStream = new MemoryStream(byte_0, 0, byte_0.Length);
		BReader = new BinaryReader(MStream);
		Handle = new SafeFileHandle(IntPtr.Zero, ownsHandle: true);
		Disposed = false;
	}

	public byte[] ToArray()
	{
		return MStream.ToArray();
	}

	public void SetMStream(MemoryStream MStream)
	{
		this.MStream = MStream;
	}

	public void Dispose()
	{
		Dispose(Disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool Disposing)
	{
		if (!Disposed)
		{
			MStream.Dispose();
			BReader.Dispose();
			if (Disposing)
			{
				Handle.Dispose();
			}
			Disposed = true;
		}
	}

	public byte[] ReadB(int Length)
	{
		return BReader.ReadBytes(Length);
	}

	public byte ReadC()
	{
		return BReader.ReadByte();
	}

	public short ReadH()
	{
		return BReader.ReadInt16();
	}

	public ushort ReadUH()
	{
		return BReader.ReadUInt16();
	}

	public int ReadD()
	{
		return BReader.ReadInt32();
	}

	public uint ReadUD()
	{
		return BReader.ReadUInt32();
	}

	public float ReadT()
	{
		return BReader.ReadSingle();
	}

	public double ReadF()
	{
		return BReader.ReadDouble();
	}

	public long ReadQ()
	{
		return BReader.ReadInt64();
	}

	public ulong ReadUQ()
	{
		return BReader.ReadUInt64();
	}

	public string ReadN(int Length, string CodePage)
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

	public string ReadS(int Length)
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

	public string ReadU(int Length)
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

	public byte ReadC(out bool Exception)
	{
		try
		{
			byte result = BReader.ReadByte();
			Exception = false;
			return result;
		}
		catch
		{
			Exception = true;
			return 0;
		}
	}

	public ushort ReadUH(out bool Exception)
	{
		try
		{
			ushort result = BReader.ReadUInt16();
			Exception = false;
			return result;
		}
		catch
		{
			Exception = true;
			return 0;
		}
	}

	public void Advance(int Bytes)
	{
		if ((BReader.BaseStream.Position += Bytes) > BReader.BaseStream.Length)
		{
			CLogger.Print("Advance crashed.", LoggerType.Warning);
			throw new Exception("Offset has exceeded the buffer value.");
		}
	}

	public Half3 ReadUHV()
	{
		return new Half3(ReadUH(), ReadUH(), ReadUH());
	}

	public Half3 ReadTV()
	{
		return new Half3(ReadT(), ReadT(), ReadT());
	}
}
