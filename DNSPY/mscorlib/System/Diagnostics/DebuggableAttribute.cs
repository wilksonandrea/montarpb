using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003EB RID: 1003
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggableAttribute : Attribute
	{
		// Token: 0x0600330F RID: 13071 RVA: 0x000C49F0 File Offset: 0x000C2BF0
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000C4A25 File Offset: 0x000C2C25
		[__DynamicallyInvokable]
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000C4A34 File Offset: 0x000C2C34
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000C4A41 File Offset: 0x000C2C41
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000C4A52 File Offset: 0x000C2C52
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x040016A3 RID: 5795
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		// Token: 0x02000B84 RID: 2948
		[Flags]
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public enum DebuggingModes
		{
			// Token: 0x040034E2 RID: 13538
			[__DynamicallyInvokable]
			None = 0,
			// Token: 0x040034E3 RID: 13539
			[__DynamicallyInvokable]
			Default = 1,
			// Token: 0x040034E4 RID: 13540
			[__DynamicallyInvokable]
			DisableOptimizations = 256,
			// Token: 0x040034E5 RID: 13541
			[__DynamicallyInvokable]
			IgnoreSymbolStoreSequencePoints = 2,
			// Token: 0x040034E6 RID: 13542
			[__DynamicallyInvokable]
			EnableEditAndContinue = 4
		}
	}
}
