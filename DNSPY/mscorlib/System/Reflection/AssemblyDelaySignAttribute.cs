using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C3 RID: 1475
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		// Token: 0x06004472 RID: 17522 RVA: 0x000FC2D1 File Offset: 0x000FA4D1
		[__DynamicallyInvokable]
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.m_delaySign = delaySign;
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x000FC2E0 File Offset: 0x000FA4E0
		[__DynamicallyInvokable]
		public bool DelaySign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_delaySign;
			}
		}

		// Token: 0x04001C12 RID: 7186
		private bool m_delaySign;
	}
}
