using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BB RID: 1467
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		// Token: 0x06004462 RID: 17506 RVA: 0x000FC20B File Offset: 0x000FA40B
		[__DynamicallyInvokable]
		public AssemblyTitleAttribute(string title)
		{
			this.m_title = title;
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x000FC21A File Offset: 0x000FA41A
		[__DynamicallyInvokable]
		public string Title
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_title;
			}
		}

		// Token: 0x04001C0A RID: 7178
		private string m_title;
	}
}
