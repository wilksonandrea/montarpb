using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000331 RID: 817
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x060028EB RID: 10475 RVA: 0x00096A78 File Offset: 0x00094C78
		[SecurityCritical]
		private WindowsImpersonationContext()
		{
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00096A8C File Offset: 0x00094C8C
		[SecurityCritical]
		internal WindowsImpersonationContext(SafeAccessTokenHandle safeTokenHandle, WindowsIdentity wi, bool isImpersonating, FrameSecurityDescriptor fsd)
		{
			if (safeTokenHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (isImpersonating)
			{
				if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), safeTokenHandle, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
				this.m_wi = wi;
			}
			this.m_fsd = fsd;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00096B00 File Offset: 0x00094D00
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
			}
			else
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
				if (num < 0)
				{
					throw new SecurityException(Win32Native.GetMessage(num));
				}
			}
			WindowsIdentity.UpdateThreadWI(this.m_wi);
			if (this.m_fsd != null)
			{
				this.m_fsd.SetTokenHandles(null, null);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x00096B84 File Offset: 0x00094D84
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			bool flag = false;
			try
			{
				int num;
				if (this.m_safeTokenHandle.IsInvalid)
				{
					num = Win32.RevertToSelf();
					if (num < 0)
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				else
				{
					num = Win32.RevertToSelf();
					if (num >= 0)
					{
						num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
					}
					else
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				flag = num >= 0;
				if (this.m_fsd != null)
				{
					this.m_fsd.SetTokenHandles(null, null);
				}
			}
			catch (Exception ex)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), ex);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00096C28 File Offset: 0x00094E28
		[SecuritySafeCritical]
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
			{
				this.Undo();
				this.m_safeTokenHandle.Dispose();
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00096C53 File Offset: 0x00094E53
		[ComVisible(false)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04001080 RID: 4224
		[SecurityCritical]
		private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04001081 RID: 4225
		private WindowsIdentity m_wi;

		// Token: 0x04001082 RID: 4226
		private FrameSecurityDescriptor m_fsd;
	}
}
