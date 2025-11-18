using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000920 RID: 2336
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		// Token: 0x0600600F RID: 24591 RVA: 0x0014B55A File Offset: 0x0014975A
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x0014B569 File Offset: 0x00149769
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A7D RID: 10877
		internal string _val;
	}
}
