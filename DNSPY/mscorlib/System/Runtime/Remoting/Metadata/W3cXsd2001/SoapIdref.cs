using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007FD RID: 2045
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x0013686A File Offset: 0x00134A6A
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x00136871 File Offset: 0x00134A71
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x00136878 File Offset: 0x00134A78
		public SoapIdref()
		{
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x00136880 File Offset: 0x00134A80
		public SoapIdref(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06005825 RID: 22565 RVA: 0x0013688F File Offset: 0x00134A8F
		// (set) Token: 0x06005826 RID: 22566 RVA: 0x00136897 File Offset: 0x00134A97
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

		// Token: 0x06005827 RID: 22567 RVA: 0x001368A0 File Offset: 0x00134AA0
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x001368AD File Offset: 0x00134AAD
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		// Token: 0x04002836 RID: 10294
		private string _value;
	}
}
