using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B7 RID: 1463
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		// Token: 0x0600445A RID: 17498 RVA: 0x000FC1AF File Offset: 0x000FA3AF
		[__DynamicallyInvokable]
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.m_trademark = trademark;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x000FC1BE File Offset: 0x000FA3BE
		[__DynamicallyInvokable]
		public string Trademark
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_trademark;
			}
		}

		// Token: 0x04001C06 RID: 7174
		private string m_trademark;
	}
}
