using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008EC RID: 2284
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StateMachineAttribute : Attribute
	{
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06005E0A RID: 24074 RVA: 0x0014A3E1 File Offset: 0x001485E1
		// (set) Token: 0x06005E0B RID: 24075 RVA: 0x0014A3E9 File Offset: 0x001485E9
		[__DynamicallyInvokable]
		public Type StateMachineType
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<StateMachineType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StateMachineType>k__BackingField = value;
			}
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x0014A3F2 File Offset: 0x001485F2
		[__DynamicallyInvokable]
		public StateMachineAttribute(Type stateMachineType)
		{
			this.StateMachineType = stateMachineType;
		}

		// Token: 0x04002A4D RID: 10829
		[CompilerGenerated]
		private Type <StateMachineType>k__BackingField;
	}
}
