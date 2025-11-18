using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000506 RID: 1286
	[ComVisible(true)]
	public class Overlapped
	{
		// Token: 0x06003C9D RID: 15517 RVA: 0x000E4900 File Offset: 0x000E2B00
		public Overlapped()
		{
			this.m_overlappedData = (OverlappedData)Overlapped.s_overlappedDataCache.Allocate();
			this.m_overlappedData.m_overlapped = this;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000E492C File Offset: 0x000E2B2C
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.m_overlappedData = (OverlappedData)Overlapped.s_overlappedDataCache.Allocate();
			this.m_overlappedData.m_overlapped = this;
			this.m_overlappedData.m_nativeOverlapped.OffsetLow = offsetLo;
			this.m_overlappedData.m_nativeOverlapped.OffsetHigh = offsetHi;
			this.m_overlappedData.UserHandle = hEvent;
			this.m_overlappedData.m_asyncResult = ar;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000E499B File Offset: 0x000E2B9B
		[Obsolete("This constructor is not 64-bit compatible.  Use the constructor that takes an IntPtr for the event handle.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
			: this(offsetLo, offsetHi, new IntPtr(hEvent), ar)
		{
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x000E49AD File Offset: 0x000E2BAD
		// (set) Token: 0x06003CA1 RID: 15521 RVA: 0x000E49BA File Offset: 0x000E2BBA
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.m_overlappedData.m_asyncResult;
			}
			set
			{
				this.m_overlappedData.m_asyncResult = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000E49C8 File Offset: 0x000E2BC8
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x000E49DA File Offset: 0x000E2BDA
		public int OffsetLow
		{
			get
			{
				return this.m_overlappedData.m_nativeOverlapped.OffsetLow;
			}
			set
			{
				this.m_overlappedData.m_nativeOverlapped.OffsetLow = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000E49ED File Offset: 0x000E2BED
		// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x000E49FF File Offset: 0x000E2BFF
		public int OffsetHigh
		{
			get
			{
				return this.m_overlappedData.m_nativeOverlapped.OffsetHigh;
			}
			set
			{
				this.m_overlappedData.m_nativeOverlapped.OffsetHigh = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000E4A14 File Offset: 0x000E2C14
		// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x000E4A34 File Offset: 0x000E2C34
		[Obsolete("This property is not 64-bit compatible.  Use EventHandleIntPtr instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public int EventHandle
		{
			get
			{
				return this.m_overlappedData.UserHandle.ToInt32();
			}
			set
			{
				this.m_overlappedData.UserHandle = new IntPtr(value);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x000E4A47 File Offset: 0x000E2C47
		// (set) Token: 0x06003CA9 RID: 15529 RVA: 0x000E4A54 File Offset: 0x000E2C54
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.m_overlappedData.UserHandle;
			}
			set
			{
				this.m_overlappedData.UserHandle = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x000E4A62 File Offset: 0x000E2C62
		internal _IOCompletionCallback iocbHelper
		{
			get
			{
				return this.m_overlappedData.m_iocbHelper;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000E4A6F File Offset: 0x000E2C6F
		internal IOCompletionCallback UserCallback
		{
			[SecurityCritical]
			get
			{
				return this.m_overlappedData.m_iocb;
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000E4A7C File Offset: 0x000E2C7C
		[SecurityCritical]
		[Obsolete("This method is not safe.  Use Pack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb, null);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000E4A86 File Offset: 0x000E2C86
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			return this.m_overlappedData.Pack(iocb, userData);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000E4A95 File Offset: 0x000E2C95
		[SecurityCritical]
		[Obsolete("This method is not safe.  Use UnsafePack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.UnsafePack(iocb, null);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000E4A9F File Offset: 0x000E2C9F
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.m_overlappedData.UnsafePack(iocb, userData);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000E4AB0 File Offset: 0x000E2CB0
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000E4ADC File Offset: 0x000E2CDC
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Overlapped overlapped = OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
			OverlappedData.FreeNativeOverlapped(nativeOverlappedPtr);
			OverlappedData overlappedData = overlapped.m_overlappedData;
			overlapped.m_overlappedData = null;
			overlappedData.ReInitialize();
			Overlapped.s_overlappedDataCache.Free(overlappedData);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000E4B2A File Offset: 0x000E2D2A
		// Note: this type is marked as 'beforefieldinit'.
		static Overlapped()
		{
		}

		// Token: 0x040019BB RID: 6587
		private OverlappedData m_overlappedData;

		// Token: 0x040019BC RID: 6588
		private static PinnableBufferCache s_overlappedDataCache = new PinnableBufferCache("System.Threading.OverlappedData", () => new OverlappedData());

		// Token: 0x02000BF5 RID: 3061
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006F6D RID: 28525 RVA: 0x0017FACE File Offset: 0x0017DCCE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006F6E RID: 28526 RVA: 0x0017FADA File Offset: 0x0017DCDA
			public <>c()
			{
			}

			// Token: 0x06006F6F RID: 28527 RVA: 0x0017FAE2 File Offset: 0x0017DCE2
			internal object <.cctor>b__30_0()
			{
				return new OverlappedData();
			}

			// Token: 0x0400362B RID: 13867
			public static readonly Overlapped.<>c <>9 = new Overlapped.<>c();
		}
	}
}
