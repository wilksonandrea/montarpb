using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093F RID: 2367
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		// Token: 0x0600605E RID: 24670 RVA: 0x0014BD70 File Offset: 0x00149F70
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x0014BD95 File Offset: 0x00149F95
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06006060 RID: 24672 RVA: 0x0014BD9D File Offset: 0x00149F9D
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x0014BDA5 File Offset: 0x00149FA5
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06006062 RID: 24674 RVA: 0x0014BDAD File Offset: 0x00149FAD
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002B32 RID: 11058
		internal int _major;

		// Token: 0x04002B33 RID: 11059
		internal int _minor;

		// Token: 0x04002B34 RID: 11060
		internal int _build;

		// Token: 0x04002B35 RID: 11061
		internal int _revision;
	}
}
