using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200073B RID: 1851
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		// Token: 0x060051D0 RID: 20944 RVA: 0x0011FBBF File Offset: 0x0011DDBF
		public OptionalFieldAttribute()
		{
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x0011FBCE File Offset: 0x0011DDCE
		// (set) Token: 0x060051D2 RID: 20946 RVA: 0x0011FBD6 File Offset: 0x0011DDD6
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_OptionalFieldVersionValue"));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x0400244B RID: 9291
		private int versionAdded = 1;
	}
}
