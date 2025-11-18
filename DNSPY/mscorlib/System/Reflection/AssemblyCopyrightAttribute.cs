using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B6 RID: 1462
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		// Token: 0x06004458 RID: 17496 RVA: 0x000FC198 File Offset: 0x000FA398
		[__DynamicallyInvokable]
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.m_copyright = copyright;
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x000FC1A7 File Offset: 0x000FA3A7
		[__DynamicallyInvokable]
		public string Copyright
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_copyright;
			}
		}

		// Token: 0x04001C05 RID: 7173
		private string m_copyright;
	}
}
