using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000AF RID: 175
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x00020771 File Offset: 0x0001E971
		[__DynamicallyInvokable]
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this.m_attributeTarget = validOn;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00020792 File Offset: 0x0001E992
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this.m_attributeTarget = validOn;
			this.m_allowMultiple = allowMultiple;
			this.m_inherited = inherited;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x000207C1 File Offset: 0x0001E9C1
		[__DynamicallyInvokable]
		public AttributeTargets ValidOn
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_attributeTarget;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000207C9 File Offset: 0x0001E9C9
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000207D1 File Offset: 0x0001E9D1
		[__DynamicallyInvokable]
		public bool AllowMultiple
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_allowMultiple;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_allowMultiple = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000207DA File Offset: 0x0001E9DA
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x000207E2 File Offset: 0x0001E9E2
		[__DynamicallyInvokable]
		public bool Inherited
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_inherited;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_inherited = value;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000207EB File Offset: 0x0001E9EB
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeUsageAttribute()
		{
		}

		// Token: 0x040003E6 RID: 998
		internal AttributeTargets m_attributeTarget = AttributeTargets.All;

		// Token: 0x040003E7 RID: 999
		internal bool m_allowMultiple;

		// Token: 0x040003E8 RID: 1000
		internal bool m_inherited = true;

		// Token: 0x040003E9 RID: 1001
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
