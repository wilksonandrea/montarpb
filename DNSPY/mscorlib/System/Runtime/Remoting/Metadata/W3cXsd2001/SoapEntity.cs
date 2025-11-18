using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007FE RID: 2046
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06005829 RID: 22569 RVA: 0x001368B5 File Offset: 0x00134AB5
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x001368BC File Offset: 0x00134ABC
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x001368C3 File Offset: 0x00134AC3
		public SoapEntity()
		{
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x001368CB File Offset: 0x00134ACB
		public SoapEntity(string value)
		{
			this._value = value;
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x001368DA File Offset: 0x00134ADA
		// (set) Token: 0x0600582E RID: 22574 RVA: 0x001368E2 File Offset: 0x00134AE2
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

		// Token: 0x0600582F RID: 22575 RVA: 0x001368EB File Offset: 0x00134AEB
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x001368F8 File Offset: 0x00134AF8
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		// Token: 0x04002837 RID: 10295
		private string _value;
	}
}
