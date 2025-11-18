using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094A RID: 2378
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct GCHandle
	{
		// Token: 0x06006086 RID: 24710 RVA: 0x0014C0F6 File Offset: 0x0014A2F6
		[SecuritySafeCritical]
		static GCHandle()
		{
			if (GCHandle.s_probeIsActive)
			{
				GCHandle.s_cookieTable = new GCHandleCookieTable();
			}
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x0014C119 File Offset: 0x0014A319
		[SecurityCritical]
		internal GCHandle(object value, GCHandleType type)
		{
			if (type > GCHandleType.Pinned)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			this.m_handle = GCHandle.InternalAlloc(value, type);
			if (type == GCHandleType.Pinned)
			{
				this.SetIsPinned();
			}
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x0014C14B File Offset: 0x0014A34B
		[SecurityCritical]
		internal GCHandle(IntPtr handle)
		{
			GCHandle.InternalCheckDomain(handle);
			this.m_handle = handle;
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x0014C15A File Offset: 0x0014A35A
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value, GCHandleType.Normal);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x0014C163 File Offset: 0x0014A363
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x0014C16C File Offset: 0x0014A36C
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void Free()
		{
			IntPtr handle = this.m_handle;
			if (handle != IntPtr.Zero && Interlocked.CompareExchange(ref this.m_handle, IntPtr.Zero, handle) == handle)
			{
				if (GCHandle.s_probeIsActive)
				{
					GCHandle.s_cookieTable.RemoveHandleIfPresent(handle);
				}
				GCHandle.InternalFree((IntPtr)((int)handle & -2));
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600608C RID: 24716 RVA: 0x0014C1DF File Offset: 0x0014A3DF
		// (set) Token: 0x0600608D RID: 24717 RVA: 0x0014C20E File Offset: 0x0014A40E
		[__DynamicallyInvokable]
		public object Target
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				return GCHandle.InternalGet(this.GetHandleValue());
			}
			[SecurityCritical]
			[__DynamicallyInvokable]
			set
			{
				if (this.m_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
				}
				GCHandle.InternalSet(this.GetHandleValue(), value, this.IsPinned());
			}
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0014C244 File Offset: 0x0014A444
		[SecurityCritical]
		public IntPtr AddrOfPinnedObject()
		{
			if (this.IsPinned())
			{
				return GCHandle.InternalAddrOfPinnedObject(this.GetHandleValue());
			}
			if (this.m_handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotPinned"));
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x0014C296 File Offset: 0x0014A496
		[__DynamicallyInvokable]
		public bool IsAllocated
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_handle != IntPtr.Zero;
			}
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x0014C2A8 File Offset: 0x0014A4A8
		[SecurityCritical]
		public static explicit operator GCHandle(IntPtr value)
		{
			return GCHandle.FromIntPtr(value);
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x0014C2B0 File Offset: 0x0014A4B0
		[SecurityCritical]
		public static GCHandle FromIntPtr(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
			}
			IntPtr intPtr = value;
			if (GCHandle.s_probeIsActive)
			{
				intPtr = GCHandle.s_cookieTable.GetHandle(value);
				if (IntPtr.Zero == intPtr)
				{
					Mda.FireInvalidGCHandleCookieProbe(value);
					return new GCHandle(IntPtr.Zero);
				}
			}
			return new GCHandle(intPtr);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0014C317 File Offset: 0x0014A517
		public static explicit operator IntPtr(GCHandle value)
		{
			return GCHandle.ToIntPtr(value);
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x0014C31F File Offset: 0x0014A51F
		public static IntPtr ToIntPtr(GCHandle value)
		{
			if (GCHandle.s_probeIsActive)
			{
				return GCHandle.s_cookieTable.FindOrAddHandle(value.m_handle);
			}
			return value.m_handle;
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x0014C343 File Offset: 0x0014A543
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_handle.GetHashCode();
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x0014C350 File Offset: 0x0014A550
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			if (o == null || !(o is GCHandle))
			{
				return false;
			}
			GCHandle gchandle = (GCHandle)o;
			return this.m_handle == gchandle.m_handle;
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x0014C382 File Offset: 0x0014A582
		[__DynamicallyInvokable]
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.m_handle == b.m_handle;
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x0014C395 File Offset: 0x0014A595
		[__DynamicallyInvokable]
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return a.m_handle != b.m_handle;
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x0014C3A8 File Offset: 0x0014A5A8
		internal IntPtr GetHandleValue()
		{
			return new IntPtr((int)this.m_handle & -2);
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x0014C3BD File Offset: 0x0014A5BD
		internal bool IsPinned()
		{
			return ((int)this.m_handle & 1) != 0;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x0014C3CF File Offset: 0x0014A5CF
		internal void SetIsPinned()
		{
			this.m_handle = new IntPtr((int)this.m_handle | 1);
		}

		// Token: 0x0600609B RID: 24731
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAlloc(object value, GCHandleType type);

		// Token: 0x0600609C RID: 24732
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFree(IntPtr handle);

		// Token: 0x0600609D RID: 24733
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalGet(IntPtr handle);

		// Token: 0x0600609E RID: 24734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSet(IntPtr handle, object value, bool isPinned);

		// Token: 0x0600609F RID: 24735
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue, bool isPinned);

		// Token: 0x060060A0 RID: 24736
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalAddrOfPinnedObject(IntPtr handle);

		// Token: 0x060060A1 RID: 24737
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCheckDomain(IntPtr handle);

		// Token: 0x060060A2 RID: 24738
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GCHandleType InternalGetHandleType(IntPtr handle);

		// Token: 0x04002B4D RID: 11085
		private const GCHandleType MaxHandleType = GCHandleType.Pinned;

		// Token: 0x04002B4E RID: 11086
		private IntPtr m_handle;

		// Token: 0x04002B4F RID: 11087
		private static volatile GCHandleCookieTable s_cookieTable;

		// Token: 0x04002B50 RID: 11088
		private static volatile bool s_probeIsActive = Mda.IsInvalidGCHandleCookieProbeEnabled();
	}
}
