using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F7 RID: 1271
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000E309C File Offset: 0x000E129C
		[SecurityCritical]
		internal void Setup(SecurityContextDisableFlow flags)
		{
			this.useEC = false;
			Thread currentThread = Thread.CurrentThread;
			this._sc = currentThread.GetMutableExecutionContext().SecurityContext;
			this._sc._disableFlow = flags;
			this._thread = currentThread;
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000E30DC File Offset: 0x000E12DC
		[SecurityCritical]
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000E3115 File Offset: 0x000E1315
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000E3120 File Offset: 0x000E1320
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCMultiple"));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCOtherThread"));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
				}
				ExecutionContext.RestoreFlow();
			}
			else
			{
				if (!Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsSame(this._sc))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
				}
				SecurityContext.RestoreFlow();
			}
			this._thread = null;
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000E31D1 File Offset: 0x000E13D1
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000E31F8 File Offset: 0x000E13F8
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000E3210 File Offset: 0x000E1410
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._sc == this._sc && obj._thread == this._thread;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000E324C File Offset: 0x000E144C
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000E3256 File Offset: 0x000E1456
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x0400198B RID: 6539
		private bool useEC;

		// Token: 0x0400198C RID: 6540
		private ExecutionContext _ec;

		// Token: 0x0400198D RID: 6541
		private SecurityContext _sc;

		// Token: 0x0400198E RID: 6542
		private Thread _thread;
	}
}
