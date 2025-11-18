using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B9 RID: 2233
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class FixedBufferAttribute : Attribute
	{
		// Token: 0x06005DB0 RID: 23984 RVA: 0x001495D6 File Offset: 0x001477D6
		[__DynamicallyInvokable]
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06005DB1 RID: 23985 RVA: 0x001495EC File Offset: 0x001477EC
		[__DynamicallyInvokable]
		public Type ElementType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x001495F4 File Offset: 0x001477F4
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.length;
			}
		}

		// Token: 0x04002A1E RID: 10782
		private Type elementType;

		// Token: 0x04002A1F RID: 10783
		private int length;
	}
}
