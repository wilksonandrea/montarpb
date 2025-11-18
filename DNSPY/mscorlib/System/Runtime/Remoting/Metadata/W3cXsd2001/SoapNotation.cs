using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F2 RID: 2034
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x001363E5 File Offset: 0x001345E5
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x001363EC File Offset: 0x001345EC
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x001363F3 File Offset: 0x001345F3
		public SoapNotation()
		{
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001363FB File Offset: 0x001345FB
		public SoapNotation(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x0013640A File Offset: 0x0013460A
		// (set) Token: 0x060057CC RID: 22476 RVA: 0x00136412 File Offset: 0x00134612
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

		// Token: 0x060057CD RID: 22477 RVA: 0x0013641B File Offset: 0x0013461B
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00136423 File Offset: 0x00134623
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		// Token: 0x0400282B RID: 10283
		private string _value;
	}
}
