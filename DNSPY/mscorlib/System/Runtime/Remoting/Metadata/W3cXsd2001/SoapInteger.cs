using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007EB RID: 2027
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06005789 RID: 22409 RVA: 0x00135E88 File Offset: 0x00134088
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x00135E8F File Offset: 0x0013408F
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x00135E96 File Offset: 0x00134096
		public SoapInteger()
		{
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x00135E9E File Offset: 0x0013409E
		public SoapInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600578D RID: 22413 RVA: 0x00135EB2 File Offset: 0x001340B2
		// (set) Token: 0x0600578E RID: 22414 RVA: 0x00135EBA File Offset: 0x001340BA
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
			}
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x00135EC8 File Offset: 0x001340C8
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x00135EDA File Offset: 0x001340DA
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002822 RID: 10274
		private decimal _value;
	}
}
