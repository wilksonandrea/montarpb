using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Plugin.Core.Enums;

namespace Plugin.Core.Settings
{
	// Token: 0x02000043 RID: 67
	public class ConfigEngine
	{
		// Token: 0x06000238 RID: 568
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);

		// Token: 0x06000239 RID: 569
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);

		// Token: 0x0600023A RID: 570 RVA: 0x00003265 File Offset: 0x00001465
		public ConfigEngine(string string_1 = null, FileAccess fileAccess_1 = FileAccess.ReadWrite)
		{
			this.fileAccess_0 = fileAccess_1;
			this.fileInfo_0 = new FileInfo(string_1 ?? this.string_0);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00019714 File Offset: 0x00017914
		public byte ReadC(string Key, byte Defaultprop, string Section = null)
		{
			try
			{
				return byte.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001975C File Offset: 0x0001795C
		public short ReadH(string Key, short Defaultprop, string Section = null)
		{
			try
			{
				return short.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000197A4 File Offset: 0x000179A4
		public ushort ReadUH(string Key, ushort Defaultprop, string Section = null)
		{
			try
			{
				return ushort.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000197EC File Offset: 0x000179EC
		public int ReadD(string Key, int Defaultprop, string Section = null)
		{
			try
			{
				return int.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00019834 File Offset: 0x00017A34
		public uint ReadUD(string Key, uint Defaultprop, string Section = null)
		{
			try
			{
				return uint.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001987C File Offset: 0x00017A7C
		public long ReadQ(string Key, long Defaultprop, string Section = null)
		{
			try
			{
				return long.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000198C4 File Offset: 0x00017AC4
		public ulong ReadUQ(string Key, ulong Defaultprop, string Section = null)
		{
			try
			{
				return ulong.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001990C File Offset: 0x00017B0C
		public double ReadF(string Key, double Defaultprop, string Section = null)
		{
			try
			{
				return double.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00019954 File Offset: 0x00017B54
		public float ReadT(string Key, float Defaultprop, string Section = null)
		{
			try
			{
				return float.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001999C File Offset: 0x00017B9C
		public bool ReadX(string Key, bool Defaultprop, string Section = null)
		{
			try
			{
				return bool.Parse(this.method_0(Key, Section));
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000199E4 File Offset: 0x00017BE4
		public string ReadS(string Key, string Defaultprop, string Section = null)
		{
			try
			{
				return this.method_0(Key, Section);
			}
			catch
			{
				CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
			}
			return Defaultprop;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00019A28 File Offset: 0x00017C28
		private string method_0(string string_1, string string_2 = null)
		{
			StringBuilder stringBuilder = new StringBuilder(65025);
			if (this.fileAccess_0 != FileAccess.Write)
			{
				ConfigEngine.GetPrivateProfileString(string_2 ?? this.string_0, string_1, "", stringBuilder, 65025, this.fileInfo_0.FullName);
				return stringBuilder.ToString();
			}
			throw new Exception("Can`t read the file! No access!");
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00019A84 File Offset: 0x00017C84
		public void WriteC(string Key, byte Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public void WriteH(string Key, short Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00019B0C File Offset: 0x00017D0C
		public void WriteH(string Key, ushort Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00019B50 File Offset: 0x00017D50
		public void WriteD(string Key, int Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00019B94 File Offset: 0x00017D94
		public void WriteD(string Key, uint Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00019BD8 File Offset: 0x00017DD8
		public void WriteQ(string Key, long Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00019C1C File Offset: 0x00017E1C
		public void WriteQ(string Key, ulong Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00019C60 File Offset: 0x00017E60
		public void WriteF(string Key, double Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00019CA4 File Offset: 0x00017EA4
		public void WriteT(string Key, float Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public void WriteX(string Key, bool Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value.ToString(), Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00019D2C File Offset: 0x00017F2C
		public void WriteS(string Key, string Value, string Section = null)
		{
			try
			{
				this.method_1(Key, Value, Section);
			}
			catch
			{
				CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000329F File Offset: 0x0000149F
		private void method_1(string string_1, string string_2, string string_3 = null)
		{
			if (this.fileAccess_0 != FileAccess.Read)
			{
				ConfigEngine.WritePrivateProfileString(string_3 ?? this.string_0, string_1, " " + string_2, this.fileInfo_0.FullName);
				return;
			}
			throw new Exception("Can`t write to file! No access!");
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000032DF File Offset: 0x000014DF
		public void DeleteKey(string Key, string Section = null)
		{
			this.method_1(Key, null, Section ?? this.string_0);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000032F4 File Offset: 0x000014F4
		public void DeleteSection(string Section = null)
		{
			this.method_1(null, null, Section ?? this.string_0);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00003309 File Offset: 0x00001509
		public bool KeyExists(string Key, string Section = null)
		{
			return this.method_0(Key, Section).Length > 0;
		}

		// Token: 0x040000D6 RID: 214
		private readonly FileInfo fileInfo_0;

		// Token: 0x040000D7 RID: 215
		private readonly FileAccess fileAccess_0;

		// Token: 0x040000D8 RID: 216
		private readonly string string_0 = Assembly.GetExecutingAssembly().GetName().Name;
	}
}
