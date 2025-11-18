using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020004E7 RID: 1255
	[__DynamicallyInvokable]
	public struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000E22A6 File Offset: 0x000E04A6
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000E22AE File Offset: 0x000E04AE
		[__DynamicallyInvokable]
		public T PreviousValue
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<PreviousValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PreviousValue>k__BackingField = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000E22B7 File Offset: 0x000E04B7
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x000E22BF File Offset: 0x000E04BF
		[__DynamicallyInvokable]
		public T CurrentValue
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<CurrentValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentValue>k__BackingField = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000E22C8 File Offset: 0x000E04C8
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x000E22D0 File Offset: 0x000E04D0
		[__DynamicallyInvokable]
		public bool ThreadContextChanged
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<ThreadContextChanged>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ThreadContextChanged>k__BackingField = value;
			}
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000E22D9 File Offset: 0x000E04D9
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}

		// Token: 0x04001968 RID: 6504
		[CompilerGenerated]
		private T <PreviousValue>k__BackingField;

		// Token: 0x04001969 RID: 6505
		[CompilerGenerated]
		private T <CurrentValue>k__BackingField;

		// Token: 0x0400196A RID: 6506
		[CompilerGenerated]
		private bool <ThreadContextChanged>k__BackingField;
	}
}
