using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F9 RID: 2041
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0013673E File Offset: 0x0013493E
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x00136745 File Offset: 0x00134945
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x0013674C File Offset: 0x0013494C
		public SoapNmtoken()
		{
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x00136754 File Offset: 0x00134954
		public SoapNmtoken(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x00136763 File Offset: 0x00134963
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x0013676B File Offset: 0x0013496B
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

		// Token: 0x06005807 RID: 22535 RVA: 0x00136774 File Offset: 0x00134974
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x00136781 File Offset: 0x00134981
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		// Token: 0x04002832 RID: 10290
		private string _value;
	}
}
