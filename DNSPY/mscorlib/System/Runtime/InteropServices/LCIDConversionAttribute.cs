using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091C RID: 2332
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		// Token: 0x06006009 RID: 24585 RVA: 0x0014B51C File Offset: 0x0014971C
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x0014B52B File Offset: 0x0014972B
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7B RID: 10875
		internal int _val;
	}
}
