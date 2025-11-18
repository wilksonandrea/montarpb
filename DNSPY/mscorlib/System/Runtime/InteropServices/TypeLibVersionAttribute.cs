using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093E RID: 2366
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		// Token: 0x0600605B RID: 24667 RVA: 0x0014BD4A File Offset: 0x00149F4A
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x0014BD60 File Offset: 0x00149F60
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x0014BD68 File Offset: 0x00149F68
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002B30 RID: 11056
		internal int _major;

		// Token: 0x04002B31 RID: 11057
		internal int _minor;
	}
}
