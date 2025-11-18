using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007F8 RID: 2040
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x001366F3 File Offset: 0x001348F3
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x001366FA File Offset: 0x001348FA
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x00136701 File Offset: 0x00134901
		public SoapEntities()
		{
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x00136709 File Offset: 0x00134909
		public SoapEntities(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x060057FD RID: 22525 RVA: 0x00136718 File Offset: 0x00134918
		// (set) Token: 0x060057FE RID: 22526 RVA: 0x00136720 File Offset: 0x00134920
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

		// Token: 0x060057FF RID: 22527 RVA: 0x00136729 File Offset: 0x00134929
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x00136736 File Offset: 0x00134936
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		// Token: 0x04002831 RID: 10289
		private string _value;
	}
}
