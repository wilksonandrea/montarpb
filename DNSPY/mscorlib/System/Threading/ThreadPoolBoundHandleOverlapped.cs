using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000508 RID: 1288
	[SecurityCritical]
	internal sealed class ThreadPoolBoundHandleOverlapped : Overlapped
	{
		// Token: 0x06003CB9 RID: 15545 RVA: 0x000E4C28 File Offset: 0x000E2E28
		public unsafe ThreadPoolBoundHandleOverlapped(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			this._userCallback = callback;
			this._userState = state;
			this._preAllocated = preAllocated;
			this._nativeOverlapped = base.Pack(ThreadPoolBoundHandleOverlapped.s_completionCallback, pinData);
			this._nativeOverlapped->OffsetLow = 0;
			this._nativeOverlapped->OffsetHigh = 0;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000E4C7C File Offset: 0x000E2E7C
		private unsafe static void CompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			ThreadPoolBoundHandleOverlapped threadPoolBoundHandleOverlapped = (ThreadPoolBoundHandleOverlapped)Overlapped.Unpack(nativeOverlapped);
			if (threadPoolBoundHandleOverlapped._completed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NativeOverlappedReused"));
			}
			threadPoolBoundHandleOverlapped._completed = true;
			if (threadPoolBoundHandleOverlapped._boundHandle == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_NativeOverlappedAlreadyFree"));
			}
			threadPoolBoundHandleOverlapped._userCallback(errorCode, numBytes, nativeOverlapped);
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000E4CDA File Offset: 0x000E2EDA
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolBoundHandleOverlapped()
		{
		}

		// Token: 0x040019BF RID: 6591
		private static readonly IOCompletionCallback s_completionCallback = new IOCompletionCallback(ThreadPoolBoundHandleOverlapped.CompletionCallback);

		// Token: 0x040019C0 RID: 6592
		private readonly IOCompletionCallback _userCallback;

		// Token: 0x040019C1 RID: 6593
		internal readonly object _userState;

		// Token: 0x040019C2 RID: 6594
		internal PreAllocatedOverlapped _preAllocated;

		// Token: 0x040019C3 RID: 6595
		internal unsafe NativeOverlapped* _nativeOverlapped;

		// Token: 0x040019C4 RID: 6596
		internal ThreadPoolBoundHandle _boundHandle;

		// Token: 0x040019C5 RID: 6597
		internal bool _completed;
	}
}
