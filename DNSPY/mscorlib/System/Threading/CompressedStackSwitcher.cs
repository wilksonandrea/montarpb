using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EF RID: 1263
	internal struct CompressedStackSwitcher : IDisposable
	{
		// Token: 0x06003BA7 RID: 15271 RVA: 0x000E2590 File Offset: 0x000E0790
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is CompressedStackSwitcher))
			{
				return false;
			}
			CompressedStackSwitcher compressedStackSwitcher = (CompressedStackSwitcher)obj;
			return this.curr_CS == compressedStackSwitcher.curr_CS && this.prev_CS == compressedStackSwitcher.prev_CS && this.prev_ADStack == compressedStackSwitcher.prev_ADStack;
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000E25E0 File Offset: 0x000E07E0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000E25F3 File Offset: 0x000E07F3
		public static bool operator ==(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000E2608 File Offset: 0x000E0808
		public static bool operator !=(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000E2620 File Offset: 0x000E0820
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000E2628 File Offset: 0x000E0828
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch (Exception ex)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), ex);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000E266C File Offset: 0x000E086C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Undo()
		{
			if (this.curr_CS == null && this.prev_CS == null)
			{
				return;
			}
			if (this.prev_ADStack != (IntPtr)0)
			{
				CompressedStack.RestoreAppDomainStack(this.prev_ADStack);
			}
			CompressedStack.SetCompressedStackThread(this.prev_CS);
			this.prev_CS = null;
			this.curr_CS = null;
			this.prev_ADStack = (IntPtr)0;
		}

		// Token: 0x04001976 RID: 6518
		internal CompressedStack curr_CS;

		// Token: 0x04001977 RID: 6519
		internal CompressedStack prev_CS;

		// Token: 0x04001978 RID: 6520
		internal IntPtr prev_ADStack;
	}
}
