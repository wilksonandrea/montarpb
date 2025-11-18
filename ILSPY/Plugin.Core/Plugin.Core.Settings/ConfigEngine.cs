using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Plugin.Core.Enums;

namespace Plugin.Core.Settings;

public class ConfigEngine
{
	private readonly FileInfo fileInfo_0;

	private readonly FileAccess fileAccess_0;

	private readonly string string_0 = Assembly.GetExecutingAssembly().GetName().Name;

	[DllImport("kernel32", CharSet = CharSet.Unicode)]
	private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);

	[DllImport("kernel32", CharSet = CharSet.Unicode)]
	private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);

	public ConfigEngine(string string_1 = null, FileAccess fileAccess_1 = FileAccess.ReadWrite)
	{
		fileAccess_0 = fileAccess_1;
		fileInfo_0 = new FileInfo(string_1 ?? string_0);
	}

	public byte ReadC(string Key, byte Defaultprop, string Section = null)
	{
		try
		{
			return byte.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public short ReadH(string Key, short Defaultprop, string Section = null)
	{
		try
		{
			return short.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public ushort ReadUH(string Key, ushort Defaultprop, string Section = null)
	{
		try
		{
			return ushort.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public int ReadD(string Key, int Defaultprop, string Section = null)
	{
		try
		{
			return int.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public uint ReadUD(string Key, uint Defaultprop, string Section = null)
	{
		try
		{
			return uint.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public long ReadQ(string Key, long Defaultprop, string Section = null)
	{
		try
		{
			return long.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public ulong ReadUQ(string Key, ulong Defaultprop, string Section = null)
	{
		try
		{
			return ulong.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public double ReadF(string Key, double Defaultprop, string Section = null)
	{
		try
		{
			return double.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public float ReadT(string Key, float Defaultprop, string Section = null)
	{
		try
		{
			return float.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public bool ReadX(string Key, bool Defaultprop, string Section = null)
	{
		try
		{
			return bool.Parse(method_0(Key, Section));
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	public string ReadS(string Key, string Defaultprop, string Section = null)
	{
		try
		{
			return method_0(Key, Section);
		}
		catch
		{
			CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning);
			return Defaultprop;
		}
	}

	private string method_0(string string_1, string string_2 = null)
	{
		StringBuilder stringBuilder = new StringBuilder(65025);
		if (fileAccess_0 == FileAccess.Write)
		{
			throw new Exception("Can`t read the file! No access!");
		}
		GetPrivateProfileString(string_2 ?? string_0, string_1, "", stringBuilder, 65025, fileInfo_0.FullName);
		return stringBuilder.ToString();
	}

	public void WriteC(string Key, byte Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteH(string Key, short Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteH(string Key, ushort Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteD(string Key, int Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteD(string Key, uint Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteQ(string Key, long Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteQ(string Key, ulong Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteF(string Key, double Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteT(string Key, float Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteX(string Key, bool Value, string Section = null)
	{
		try
		{
			method_1(Key, Value.ToString(), Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	public void WriteS(string Key, string Value, string Section = null)
	{
		try
		{
			method_1(Key, Value, Section);
		}
		catch
		{
			CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning);
		}
	}

	private void method_1(string string_1, string string_2, string string_3 = null)
	{
		if (fileAccess_0 == FileAccess.Read)
		{
			throw new Exception("Can`t write to file! No access!");
		}
		WritePrivateProfileString(string_3 ?? string_0, string_1, " " + string_2, fileInfo_0.FullName);
	}

	public void DeleteKey(string Key, string Section = null)
	{
		method_1(Key, null, Section ?? string_0);
	}

	public void DeleteSection(string Section = null)
	{
		method_1(null, null, Section ?? string_0);
	}

	public bool KeyExists(string Key, string Section = null)
	{
		return method_0(Key, Section).Length > 0;
	}
}
