using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts.Internal
{
	// Token: 0x02000419 RID: 1049
	[Obsolete("Use the ContractHelper class in the System.Runtime.CompilerServices namespace instead.")]
	[__DynamicallyInvokable]
	public static class ContractHelper
	{
		// Token: 0x06003430 RID: 13360 RVA: 0x000C6ADD File Offset: 0x000C4CDD
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			return ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000C6AE8 File Offset: 0x000C4CE8
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailure(kind, displayMessage, userMessage, conditionText, innerException);
		}
	}
}
