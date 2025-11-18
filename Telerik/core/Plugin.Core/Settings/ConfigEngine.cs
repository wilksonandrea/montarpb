using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Plugin.Core.Settings
{
	public class ConfigEngine
	{
		private readonly FileInfo fileInfo_0;

		private readonly FileAccess fileAccess_0;

		private readonly string string_0 = Assembly.GetExecutingAssembly().GetName().Name;

		public ConfigEngine(string string_1 = null, FileAccess fileAccess_1 = 3)
		{
			this.fileAccess_0 = fileAccess_1;
			this.fileInfo_0 = new FileInfo(string_1 ?? this.string_0);
		}

		public void DeleteKey(string Key, string Section = null)
		{
			this.method_1(Key, null, Section ?? this.string_0);
		}

		public void DeleteSection(string Section = null)
		{
			this.method_1(null, null, Section ?? this.string_0);
		}

		[DllImport("kernel32", CharSet=CharSet.Unicode, ExactSpelling=false)]
		private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);

		public bool KeyExists(string Key, string Section = null)
		{
			return this.method_0(Key, Section).Length > 0;
		}

		private string method_0(string string_1, string string_2 = null)
		{
			StringBuilder stringBuilder = new StringBuilder(65025);
			if (this.fileAccess_0 == FileAccess.Write)
			{
				throw new Exception("Can`t read the file! No access!");
			}
			ConfigEngine.GetPrivateProfileString(string_2 ?? this.string_0, string_1, "", stringBuilder, 65025, this.fileInfo_0.FullName);
			return stringBuilder.ToString();
		}

		private void method_1(string string_1, string string_2, string string_3 = null)
		{
			if (this.fileAccess_0 == FileAccess.Read)
			{
				throw new Exception("Can`t write to file! No access!");
			}
			ConfigEngine.WritePrivateProfileString(string_3 ?? this.string_0, string_1, string.Concat(" ", string_2), this.fileInfo_0.FullName);
		}

		public byte ReadC(string Key, byte Defaultprop, string Section = null)
		{
			byte num;
			try
			{
				num = byte.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return num;
		}

		public int ReadD(string Key, int Defaultprop, string Section = null)
		{
			int ınt32;
			try
			{
				ınt32 = int.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return ınt32;
		}

		public double ReadF(string Key, double Defaultprop, string Section = null)
		{
			double num;
			try
			{
				num = double.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return num;
		}

		public short ReadH(string Key, short Defaultprop, string Section = null)
		{
			short ınt16;
			try
			{
				ınt16 = short.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return ınt16;
		}

		public long ReadQ(string Key, long Defaultprop, string Section = null)
		{
			long ınt64;
			try
			{
				ınt64 = long.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return ınt64;
		}

		public string ReadS(string Key, string Defaultprop, string Section = null)
		{
			string str;
			try
			{
				str = this.method_0(Key, Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return str;
		}

		public float ReadT(string Key, float Defaultprop, string Section = null)
		{
			float single;
			try
			{
				single = float.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return single;
		}

		public uint ReadUD(string Key, uint Defaultprop, string Section = null)
		{
			uint uInt32;
			try
			{
				uInt32 = uint.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return uInt32;
		}

		public ushort ReadUH(string Key, ushort Defaultprop, string Section = null)
		{
			ushort uInt16;
			try
			{
				uInt16 = ushort.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return uInt16;
		}

		public ulong ReadUQ(string Key, ulong Defaultprop, string Section = null)
		{
			ulong uInt64;
			try
			{
				uInt64 = ulong.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return uInt64;
		}

		public bool ReadX(string Key, bool Defaultprop, string Section = null)
		{
			bool flag;
			try
			{
				flag = bool.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print(string.Concat("Read Parameter Failure: ", Key), LoggerType.Warning, null);
				return Defaultprop;
			}
			return flag;
		}

		public void WriteC(string Key, byte Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteD(string Key, int Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteD(string Key, uint Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteF(string Key, double Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteH(string Key, short Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteH(string Key, ushort Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		[DllImport("kernel32", CharSet=CharSet.Unicode, ExactSpelling=false)]
		private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);

		public void WriteQ(string Key, long Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteQ(string Key, ulong Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteS(string Key, string Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value, Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteT(string Key, float Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}

		public void WriteX(string Key, bool Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print(string.Concat("Write Parameter Failure: ", Key), LoggerType.Warning, null);
			}
		}
	}
}