using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007FC RID: 2044
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x0013681F File Offset: 0x00134A1F
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x00136826 File Offset: 0x00134A26
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0013682D File Offset: 0x00134A2D
		public SoapId()
		{
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x00136835 File Offset: 0x00134A35
		public SoapId(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x00136844 File Offset: 0x00134A44
		// (set) Token: 0x0600581E RID: 22558 RVA: 0x0013684C File Offset: 0x00134A4C
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

		// Token: 0x0600581F RID: 22559 RVA: 0x00136855 File Offset: 0x00134A55
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x00136862 File Offset: 0x00134A62
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		// Token: 0x04002835 RID: 10293
		private string _value;
	}
}
