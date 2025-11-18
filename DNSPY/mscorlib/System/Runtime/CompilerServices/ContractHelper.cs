using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008A3 RID: 2211
	[__DynamicallyInvokable]
	public static class ContractHelper
	{
		// Token: 0x06005D74 RID: 23924 RVA: 0x00148E18 File Offset: 0x00147018
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			string text = "Contract failed";
			ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref text);
			return text;
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x00148E37 File Offset: 0x00147037
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x00148E44 File Offset: 0x00147044
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { failureKind }), "failureKind");
			}
			string text = "contract failed.";
			ContractFailedEventArgs contractFailedEventArgs = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			string text2;
			try
			{
				text = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
				EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
				if (eventHandler != null)
				{
					contractFailedEventArgs = new ContractFailedEventArgs(failureKind, text, conditionText, innerException);
					foreach (EventHandler<ContractFailedEventArgs> eventHandler2 in eventHandler.GetInvocationList())
					{
						try
						{
							eventHandler2(null, contractFailedEventArgs);
						}
						catch (Exception ex)
						{
							contractFailedEventArgs.thrownDuringHandler = ex;
							contractFailedEventArgs.SetUnwind();
						}
					}
					if (contractFailedEventArgs.Unwind)
					{
						if (Environment.IsCLRHosted)
						{
							ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, text, conditionText, innerException);
						}
						if (innerException == null)
						{
							innerException = contractFailedEventArgs.thrownDuringHandler;
						}
						throw new ContractException(failureKind, text, userMessage, conditionText, innerException);
					}
				}
			}
			finally
			{
				if (contractFailedEventArgs != null && contractFailedEventArgs.Handled)
				{
					text2 = null;
				}
				else
				{
					text2 = text;
				}
			}
			resultFailureMessage = text2;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x00148F50 File Offset: 0x00147150
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			if (Environment.IsCLRHosted)
			{
				ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			if (!Environment.UserInteractive)
			{
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind));
			Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06005D78 RID: 23928 RVA: 0x00148FA8 File Offset: 0x001471A8
		// (remove) Token: 0x06005D79 RID: 23929 RVA: 0x00149000 File Offset: 0x00147200
		internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
		{
			[SecurityCritical]
			add
			{
				RuntimeHelpers.PrepareContractedDelegate(value);
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Combine(ContractHelper.contractFailedEvent, value);
				}
			}
			[SecurityCritical]
			remove
			{
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Remove(ContractHelper.contractFailedEvent, value);
				}
			}
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x00149054 File Offset: 0x00147254
		private static string GetResourceNameForFailure(ContractFailureKind failureKind)
		{
			string text;
			switch (failureKind)
			{
			case ContractFailureKind.Precondition:
				text = "PreconditionFailed";
				break;
			case ContractFailureKind.Postcondition:
				text = "PostconditionFailed";
				break;
			case ContractFailureKind.PostconditionOnException:
				text = "PostconditionOnExceptionFailed";
				break;
			case ContractFailureKind.Invariant:
				text = "InvariantFailed";
				break;
			case ContractFailureKind.Assert:
				text = "AssertionFailed";
				break;
			case ContractFailureKind.Assume:
				text = "AssumptionFailed";
				break;
			default:
				Contract.Assume(false, "Unreachable code");
				text = "AssumptionFailed";
				break;
			}
			return text;
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x001490C8 File Offset: 0x001472C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
		{
			string text = ContractHelper.GetResourceNameForFailure(failureKind);
			string text2;
			if (!string.IsNullOrEmpty(conditionText))
			{
				text += "_Cnd";
				text2 = Environment.GetResourceString(text, new object[] { conditionText });
			}
			else
			{
				text2 = Environment.GetResourceString(text);
			}
			if (!string.IsNullOrEmpty(userMessage))
			{
				return text2 + "  " + userMessage;
			}
			return text2;
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00149120 File Offset: 0x00147320
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
		{
			string text = null;
			if (innerException != null)
			{
				text = innerException.ToString();
			}
			Environment.TriggerCodeContractFailure(failureKind, message, conditionText, text);
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x00149142 File Offset: 0x00147342
		// Note: this type is marked as 'beforefieldinit'.
		static ContractHelper()
		{
		}

		// Token: 0x04002A14 RID: 10772
		private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;

		// Token: 0x04002A15 RID: 10773
		private static readonly object lockObject = new object();

		// Token: 0x04002A16 RID: 10774
		internal const int COR_E_CODECONTRACTFAILED = -2146233022;
	}
}
