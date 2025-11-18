using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F3 RID: 2035
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x060057CF RID: 22479 RVA: 0x0013642B File Offset: 0x0013462B
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00136432 File Offset: 0x00134632
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00136439 File Offset: 0x00134639
		public SoapNormalizedString()
		{
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00136441 File Offset: 0x00134641
		public SoapNormalizedString(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x060057D3 RID: 22483 RVA: 0x00136456 File Offset: 0x00134656
		// (set) Token: 0x060057D4 RID: 22484 RVA: 0x0013645E File Offset: 0x0013465E
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = this.Validate(value);
			}
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0013646D File Offset: 0x0013466D
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x0013647A File Offset: 0x0013467A
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00136484 File Offset: 0x00134684
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] array = new char[] { '\r', '\n', '\t' };
			int num = value.LastIndexOfAny(array);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:normalizedString", value }));
			}
			return value;
		}

		// Token: 0x0400282C RID: 10284
		private string _value;
	}
}
