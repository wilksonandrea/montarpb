using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CA RID: 2506
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public sealed class InterfaceImplementedInVersionAttribute : Attribute
	{
		// Token: 0x060063C8 RID: 25544 RVA: 0x001546F4 File Offset: 0x001528F4
		[__DynamicallyInvokable]
		public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
		{
			this.m_interfaceType = interfaceType;
			this.m_majorVersion = majorVersion;
			this.m_minorVersion = minorVersion;
			this.m_buildVersion = buildVersion;
			this.m_revisionVersion = revisionVersion;
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060063C9 RID: 25545 RVA: 0x00154721 File Offset: 0x00152921
		[__DynamicallyInvokable]
		public Type InterfaceType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_interfaceType;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x00154729 File Offset: 0x00152929
		[__DynamicallyInvokable]
		public byte MajorVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_majorVersion;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060063CB RID: 25547 RVA: 0x00154731 File Offset: 0x00152931
		[__DynamicallyInvokable]
		public byte MinorVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_minorVersion;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060063CC RID: 25548 RVA: 0x00154739 File Offset: 0x00152939
		[__DynamicallyInvokable]
		public byte BuildVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_buildVersion;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060063CD RID: 25549 RVA: 0x00154741 File Offset: 0x00152941
		[__DynamicallyInvokable]
		public byte RevisionVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_revisionVersion;
			}
		}

		// Token: 0x04002CE3 RID: 11491
		private Type m_interfaceType;

		// Token: 0x04002CE4 RID: 11492
		private byte m_majorVersion;

		// Token: 0x04002CE5 RID: 11493
		private byte m_minorVersion;

		// Token: 0x04002CE6 RID: 11494
		private byte m_buildVersion;

		// Token: 0x04002CE7 RID: 11495
		private byte m_revisionVersion;
	}
}
