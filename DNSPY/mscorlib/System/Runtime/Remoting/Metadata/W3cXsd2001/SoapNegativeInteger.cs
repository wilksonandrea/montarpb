using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007EF RID: 2031
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x001361B1 File Offset: 0x001343B1
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x001361B8 File Offset: 0x001343B8
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x001361BF File Offset: 0x001343BF
		public SoapNegativeInteger()
		{
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x001361C8 File Offset: 0x001343C8
		public SoapNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (value > -1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:negativeInteger", value));
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060057AD RID: 22445 RVA: 0x00136219 File Offset: 0x00134419
		// (set) Token: 0x060057AE RID: 22446 RVA: 0x00136224 File Offset: 0x00134424
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > -1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:negativeInteger", value));
				}
			}
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x00136274 File Offset: 0x00134474
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x00136286 File Offset: 0x00134486
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002826 RID: 10278
		private decimal _value;
	}
}
