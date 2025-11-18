using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F7 RID: 2039
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x001366A8 File Offset: 0x001348A8
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x001366AF File Offset: 0x001348AF
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001366B6 File Offset: 0x001348B6
		public SoapIdrefs()
		{
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x001366BE File Offset: 0x001348BE
		public SoapIdrefs(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x001366CD File Offset: 0x001348CD
		// (set) Token: 0x060057F6 RID: 22518 RVA: 0x001366D5 File Offset: 0x001348D5
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

		// Token: 0x060057F7 RID: 22519 RVA: 0x001366DE File Offset: 0x001348DE
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x001366EB File Offset: 0x001348EB
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		// Token: 0x04002830 RID: 10288
		private string _value;
	}
}
