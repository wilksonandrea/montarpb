using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E2 RID: 1506
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		// Token: 0x060045B9 RID: 17849 RVA: 0x0010102A File Offset: 0x000FF22A
		[__DynamicallyInvokable]
		public DefaultMemberAttribute(string memberName)
		{
			this.m_memberName = memberName;
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x00101039 File Offset: 0x000FF239
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x04001C9B RID: 7323
		private string m_memberName;
	}
}
