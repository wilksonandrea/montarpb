using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F5 RID: 2037
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x00136612 File Offset: 0x00134812
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x00136619 File Offset: 0x00134819
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x00136620 File Offset: 0x00134820
		public SoapLanguage()
		{
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00136628 File Offset: 0x00134828
		public SoapLanguage(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x00136637 File Offset: 0x00134837
		// (set) Token: 0x060057E6 RID: 22502 RVA: 0x0013663F File Offset: 0x0013483F
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

		// Token: 0x060057E7 RID: 22503 RVA: 0x00136648 File Offset: 0x00134848
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x00136655 File Offset: 0x00134855
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		// Token: 0x0400282E RID: 10286
		private string _value;
	}
}
