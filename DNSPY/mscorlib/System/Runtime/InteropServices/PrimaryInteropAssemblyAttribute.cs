using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093B RID: 2363
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		// Token: 0x06006053 RID: 24659 RVA: 0x0014BCE7 File Offset: 0x00149EE7
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06006054 RID: 24660 RVA: 0x0014BCFD File Offset: 0x00149EFD
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06006055 RID: 24661 RVA: 0x0014BD05 File Offset: 0x00149F05
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002B2B RID: 11051
		internal int _major;

		// Token: 0x04002B2C RID: 11052
		internal int _minor;
	}
}
