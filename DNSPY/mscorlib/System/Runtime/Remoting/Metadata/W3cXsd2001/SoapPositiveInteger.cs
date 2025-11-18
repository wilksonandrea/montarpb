using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007EC RID: 2028
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06005791 RID: 22417 RVA: 0x00135EED File Offset: 0x001340ED
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x00135EF4 File Offset: 0x001340F4
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		// Token: 0x06005793 RID: 22419 RVA: 0x00135EFB File Offset: 0x001340FB
		public SoapPositiveInteger()
		{
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x00135F04 File Offset: 0x00134104
		public SoapPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x00135F5A File Offset: 0x0013415A
		// (set) Token: 0x06005796 RID: 22422 RVA: 0x00135F64 File Offset: 0x00134164
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
				}
			}
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x00135FB4 File Offset: 0x001341B4
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x00135FC6 File Offset: 0x001341C6
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002823 RID: 10275
		private decimal _value;
	}
}
