using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007ED RID: 2029
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06005799 RID: 22425 RVA: 0x00135FD9 File Offset: 0x001341D9
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x00135FE0 File Offset: 0x001341E0
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x00135FE7 File Offset: 0x001341E7
		public SoapNonPositiveInteger()
		{
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x00135FF0 File Offset: 0x001341F0
		public SoapNonPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value > 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonPositiveInteger", value));
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600579D RID: 22429 RVA: 0x00136046 File Offset: 0x00134246
		// (set) Token: 0x0600579E RID: 22430 RVA: 0x00136050 File Offset: 0x00134250
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonPositiveInteger", value));
				}
			}
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x001360A0 File Offset: 0x001342A0
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x001360B2 File Offset: 0x001342B2
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002824 RID: 10276
		private decimal _value;
	}
}
