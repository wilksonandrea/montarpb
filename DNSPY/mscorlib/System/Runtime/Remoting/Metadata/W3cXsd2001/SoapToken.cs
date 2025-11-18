using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F4 RID: 2036
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060057D8 RID: 22488 RVA: 0x001364DF File Offset: 0x001346DF
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001364E6 File Offset: 0x001346E6
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x001364ED File Offset: 0x001346ED
		public SoapToken()
		{
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x001364F5 File Offset: 0x001346F5
		public SoapToken(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x0013650A File Offset: 0x0013470A
		// (set) Token: 0x060057DD RID: 22493 RVA: 0x00136512 File Offset: 0x00134712
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

		// Token: 0x060057DE RID: 22494 RVA: 0x00136521 File Offset: 0x00134721
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x0013652E File Offset: 0x0013472E
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x00136538 File Offset: 0x00134738
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] array = new char[] { '\r', '\t' };
			int num = value.LastIndexOfAny(array);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			num = value.IndexOf("  ");
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:token", value }));
			}
			return value;
		}

		// Token: 0x0400282D RID: 10285
		private string _value;
	}
}
