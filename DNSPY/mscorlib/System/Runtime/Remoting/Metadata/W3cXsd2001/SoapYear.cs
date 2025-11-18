using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007E5 RID: 2021
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x00135920 File Offset: 0x00133B20
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x00135927 File Offset: 0x00133B27
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x0013592E File Offset: 0x00133B2E
		public SoapYear()
		{
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x00135941 File Offset: 0x00133B41
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x0013595B File Offset: 0x00133B5B
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x0013597C File Offset: 0x00133B7C
		// (set) Token: 0x06005756 RID: 22358 RVA: 0x00135984 File Offset: 0x00133B84
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

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06005757 RID: 22359 RVA: 0x0013598D File Offset: 0x00133B8D
		// (set) Token: 0x06005758 RID: 22360 RVA: 0x00135995 File Offset: 0x00133B95
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

		// Token: 0x06005759 RID: 22361 RVA: 0x0013599E File Offset: 0x00133B9E
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x001359D4 File Offset: 0x00133BD4
		public static SoapYear Parse(string value)
		{
			int num = 0;
			if (value[0] == '-')
			{
				num = -1;
			}
			return new SoapYear(DateTime.ParseExact(value, SoapYear.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), num);
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00135A07 File Offset: 0x00133C07
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYear()
		{
		}

		// Token: 0x04002816 RID: 10262
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002817 RID: 10263
		private int _sign;

		// Token: 0x04002818 RID: 10264
		private static string[] formats = new string[] { "yyyy", "'+'yyyy", "'-'yyyy", "yyyyzzz", "'+'yyyyzzz", "'-'yyyyzzz" };
	}
}
