using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E4 RID: 2020
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x001357FC File Offset: 0x001339FC
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x00135803 File Offset: 0x00133A03
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x0013580A File Offset: 0x00133A0A
		public SoapYearMonth()
		{
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0013581D File Offset: 0x00133A1D
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x00135837 File Offset: 0x00133A37
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06005749 RID: 22345 RVA: 0x00135858 File Offset: 0x00133A58
		// (set) Token: 0x0600574A RID: 22346 RVA: 0x00135860 File Offset: 0x00133A60
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x0600574B RID: 22347 RVA: 0x00135869 File Offset: 0x00133A69
		// (set) Token: 0x0600574C RID: 22348 RVA: 0x00135871 File Offset: 0x00133A71
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0013587A File Offset: 0x00133A7A
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001358B0 File Offset: 0x00133AB0
		public static SoapYearMonth Parse(string value)
		{
			int num = 0;
			if (value[0] == '-')
			{
				num = -1;
			}
			return new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), num);
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001358E3 File Offset: 0x00133AE3
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYearMonth()
		{
		}

		// Token: 0x04002813 RID: 10259
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002814 RID: 10260
		private int _sign;

		// Token: 0x04002815 RID: 10261
		private static string[] formats = new string[] { "yyyy-MM", "'+'yyyy-MM", "'-'yyyy-MM", "yyyy-MMzzz", "'+'yyyy-MMzzz", "'-'yyyy-MMzzz" };
	}
}
