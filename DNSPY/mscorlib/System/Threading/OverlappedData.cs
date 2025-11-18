using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000505 RID: 1285
	internal sealed class OverlappedData
	{
		// Token: 0x06003C93 RID: 15507 RVA: 0x000E4740 File Offset: 0x000E2940
		[SecurityCritical]
		internal void ReInitialize()
		{
			this.m_asyncResult = null;
			this.m_iocb = null;
			this.m_iocbHelper = null;
			this.m_overlapped = null;
			this.m_userObject = null;
			this.m_pinSelf = (IntPtr)0;
			this.m_userObjectInternal = (IntPtr)0;
			this.m_AppDomainId = 0;
			this.m_nativeOverlapped.EventHandle = (IntPtr)0;
			this.m_isArray = 0;
			this.m_nativeOverlapped.InternalLow = (IntPtr)0;
			this.m_nativeOverlapped.InternalHigh = (IntPtr)0;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000E47CC File Offset: 0x000E29CC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (iocb != null)
			{
				this.m_iocbHelper = new _IOCompletionCallback(iocb, ref stackCrawlMark);
				this.m_iocb = iocb;
			}
			else
			{
				this.m_iocbHelper = null;
				this.m_iocb = null;
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000E4864 File Offset: 0x000E2A64
		[SecurityCritical]
		internal unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			if (!this.m_pinSelf.IsNull())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
			}
			this.m_userObject = userData;
			if (this.m_userObject != null)
			{
				if (this.m_userObject.GetType() == typeof(object[]))
				{
					this.m_isArray = 1;
				}
				else
				{
					this.m_isArray = 0;
				}
			}
			this.m_iocb = iocb;
			this.m_iocbHelper = null;
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x000E48DD File Offset: 0x000E2ADD
		// (set) Token: 0x06003C97 RID: 15511 RVA: 0x000E48EA File Offset: 0x000E2AEA
		[ComVisible(false)]
		internal IntPtr UserHandle
		{
			get
			{
				return this.m_nativeOverlapped.EventHandle;
			}
			set
			{
				this.m_nativeOverlapped.EventHandle = value;
			}
		}

		// Token: 0x06003C98 RID: 15512
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern NativeOverlapped* AllocateNativeOverlapped();

		// Token: 0x06003C99 RID: 15513
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FreeNativeOverlapped(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003C9A RID: 15514
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern OverlappedData GetOverlappedFromNative(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06003C9B RID: 15515
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CheckVMForIOPacket(out NativeOverlapped* pOVERLAP, out uint errorCode, out uint numBytes);

		// Token: 0x06003C9C RID: 15516 RVA: 0x000E48F8 File Offset: 0x000E2AF8
		public OverlappedData()
		{
		}

		// Token: 0x040019B0 RID: 6576
		internal IAsyncResult m_asyncResult;

		// Token: 0x040019B1 RID: 6577
		[SecurityCritical]
		internal IOCompletionCallback m_iocb;

		// Token: 0x040019B2 RID: 6578
		internal _IOCompletionCallback m_iocbHelper;

		// Token: 0x040019B3 RID: 6579
		internal Overlapped m_overlapped;

		// Token: 0x040019B4 RID: 6580
		private object m_userObject;

		// Token: 0x040019B5 RID: 6581
		private IntPtr m_pinSelf;

		// Token: 0x040019B6 RID: 6582
		private IntPtr m_userObjectInternal;

		// Token: 0x040019B7 RID: 6583
		private int m_AppDomainId;

		// Token: 0x040019B8 RID: 6584
		private byte m_isArray;

		// Token: 0x040019B9 RID: 6585
		private byte m_toBeCleaned;

		// Token: 0x040019BA RID: 6586
		internal NativeOverlapped m_nativeOverlapped;
	}
}
