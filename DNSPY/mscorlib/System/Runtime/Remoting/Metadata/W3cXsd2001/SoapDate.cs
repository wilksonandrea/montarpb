using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E3 RID: 2019
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x00135692 File Offset: 0x00133892
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x00135699 File Offset: 0x00133899
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x001356A0 File Offset: 0x001338A0
		public SoapDate()
		{
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x001356C8 File Offset: 0x001338C8
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x001356F8 File Offset: 0x001338F8
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x0013572C File Offset: 0x0013392C
		// (set) Token: 0x0600573E RID: 22334 RVA: 0x00135734 File Offset: 0x00133934
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value.Date;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x00135743 File Offset: 0x00133943
		// (set) Token: 0x06005740 RID: 22336 RVA: 0x0013574B File Offset: 0x0013394B
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

		// Token: 0x06005741 RID: 22337 RVA: 0x00135754 File Offset: 0x00133954
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x0013578C File Offset: 0x0013398C
		public static SoapDate Parse(string value)
		{
			int num = 0;
			if (value[0] == '-')
			{
				num = -1;
			}
			return new SoapDate(DateTime.ParseExact(value, SoapDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), num);
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001357BF File Offset: 0x001339BF
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDate()
		{
		}

		// Token: 0x04002810 RID: 10256
		private DateTime _value = DateTime.MinValue.Date;

		// Token: 0x04002811 RID: 10257
		private int _sign;

		// Token: 0x04002812 RID: 10258
		private static string[] formats = new string[] { "yyyy-MM-dd", "'+'yyyy-MM-dd", "'-'yyyy-MM-dd", "yyyy-MM-ddzzz", "'+'yyyy-MM-ddzzz", "'-'yyyy-MM-ddzzz" };
	}
}
