using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007FB RID: 2043
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x001367D4 File Offset: 0x001349D4
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x001367DB File Offset: 0x001349DB
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001367E2 File Offset: 0x001349E2
		public SoapNcName()
		{
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x001367EA File Offset: 0x001349EA
		public SoapNcName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x001367F9 File Offset: 0x001349F9
		// (set) Token: 0x06005816 RID: 22550 RVA: 0x00136801 File Offset: 0x00134A01
		public string Value
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

		// Token: 0x06005817 RID: 22551 RVA: 0x0013680A File Offset: 0x00134A0A
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00136817 File Offset: 0x00134A17
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		// Token: 0x04002834 RID: 10292
		private string _value;
	}
}
