using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000612 RID: 1554
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscationAttribute : Attribute
	{
		// Token: 0x060047F9 RID: 18425 RVA: 0x00105BBB File Offset: 0x00103DBB
		public ObfuscationAttribute()
		{
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x00105BE3 File Offset: 0x00103DE3
		// (set) Token: 0x060047FB RID: 18427 RVA: 0x00105BEB File Offset: 0x00103DEB
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x00105BF4 File Offset: 0x00103DF4
		// (set) Token: 0x060047FD RID: 18429 RVA: 0x00105BFC File Offset: 0x00103DFC
		public bool Exclude
		{
			get
			{
				return this.m_exclude;
			}
			set
			{
				this.m_exclude = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x00105C05 File Offset: 0x00103E05
		// (set) Token: 0x060047FF RID: 18431 RVA: 0x00105C0D File Offset: 0x00103E0D
		public bool ApplyToMembers
		{
			get
			{
				return this.m_applyToMembers;
			}
			set
			{
				this.m_applyToMembers = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x00105C16 File Offset: 0x00103E16
		// (set) Token: 0x06004801 RID: 18433 RVA: 0x00105C1E File Offset: 0x00103E1E
		public string Feature
		{
			get
			{
				return this.m_feature;
			}
			set
			{
				this.m_feature = value;
			}
		}

		// Token: 0x04001DCD RID: 7629
		private bool m_strip = true;

		// Token: 0x04001DCE RID: 7630
		private bool m_exclude = true;

		// Token: 0x04001DCF RID: 7631
		private bool m_applyToMembers = true;

		// Token: 0x04001DD0 RID: 7632
		private string m_feature = "all";
	}
}
