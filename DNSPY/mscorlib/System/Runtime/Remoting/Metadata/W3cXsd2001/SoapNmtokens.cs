using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007FA RID: 2042
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x00136789 File Offset: 0x00134989
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x00136790 File Offset: 0x00134990
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x00136797 File Offset: 0x00134997
		public SoapNmtokens()
		{
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0013679F File Offset: 0x0013499F
		public SoapNmtokens(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x001367AE File Offset: 0x001349AE
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x001367B6 File Offset: 0x001349B6
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

		// Token: 0x0600580F RID: 22543 RVA: 0x001367BF File Offset: 0x001349BF
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x001367CC File Offset: 0x001349CC
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		// Token: 0x04002833 RID: 10291
		private string _value;
	}
}
