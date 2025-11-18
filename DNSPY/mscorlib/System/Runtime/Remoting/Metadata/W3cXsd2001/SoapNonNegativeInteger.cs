using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007EE RID: 2030
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060057A1 RID: 22433 RVA: 0x001360C5 File Offset: 0x001342C5
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x001360CC File Offset: 0x001342CC
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		// Token: 0x060057A3 RID: 22435 RVA: 0x001360D3 File Offset: 0x001342D3
		public SoapNonNegativeInteger()
		{
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x001360DC File Offset: 0x001342DC
		public SoapNonNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonNegativeInteger", value));
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x00136132 File Offset: 0x00134332
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x0013613C File Offset: 0x0013433C
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonNegativeInteger", value));
				}
			}
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x0013618C File Offset: 0x0013438C
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x0013619E File Offset: 0x0013439E
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002825 RID: 10277
		private decimal _value;
	}
}
