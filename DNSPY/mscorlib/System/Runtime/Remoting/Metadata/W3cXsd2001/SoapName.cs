using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F6 RID: 2038
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x0013665D File Offset: 0x0013485D
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x00136664 File Offset: 0x00134864
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x0013666B File Offset: 0x0013486B
		public SoapName()
		{
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00136673 File Offset: 0x00134873
		public SoapName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x00136682 File Offset: 0x00134882
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x0013668A File Offset: 0x0013488A
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

		// Token: 0x060057EF RID: 22511 RVA: 0x00136693 File Offset: 0x00134893
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001366A0 File Offset: 0x001348A0
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		// Token: 0x0400282F RID: 10287
		private string _value;
	}
}
