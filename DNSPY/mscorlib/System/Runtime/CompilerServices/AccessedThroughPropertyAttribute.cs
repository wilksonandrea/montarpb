using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008A4 RID: 2212
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		// Token: 0x06005D7E RID: 23934 RVA: 0x0014914E File Offset: 0x0014734E
		[__DynamicallyInvokable]
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x0014915D File Offset: 0x0014735D
		[__DynamicallyInvokable]
		public string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A17 RID: 10775
		private readonly string propertyName;
	}
}
