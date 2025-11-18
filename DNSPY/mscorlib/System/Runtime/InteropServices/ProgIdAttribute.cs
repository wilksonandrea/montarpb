using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091F RID: 2335
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		// Token: 0x0600600D RID: 24589 RVA: 0x0014B543 File Offset: 0x00149743
		public ProgIdAttribute(string progId)
		{
			this._val = progId;
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x0014B552 File Offset: 0x00149752
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7C RID: 10876
		internal string _val;
	}
}
