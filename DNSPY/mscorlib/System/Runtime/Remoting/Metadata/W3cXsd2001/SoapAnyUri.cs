using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F0 RID: 2032
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060057B1 RID: 22449 RVA: 0x00136299 File Offset: 0x00134499
		public static string XsdType
		{
			get
			{
				return "anyURI";
			}
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x001362A0 File Offset: 0x001344A0
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x001362A7 File Offset: 0x001344A7
		public SoapAnyUri()
		{
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x001362AF File Offset: 0x001344AF
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060057B5 RID: 22453 RVA: 0x001362BE File Offset: 0x001344BE
		// (set) Token: 0x060057B6 RID: 22454 RVA: 0x001362C6 File Offset: 0x001344C6
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

		// Token: 0x060057B7 RID: 22455 RVA: 0x001362CF File Offset: 0x001344CF
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x001362D7 File Offset: 0x001344D7
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		// Token: 0x04002827 RID: 10279
		private string _value;
	}
}
