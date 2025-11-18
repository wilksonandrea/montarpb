using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095D RID: 2397
	internal class NativeBuffer : IDisposable
	{
		// Token: 0x0600620D RID: 25101 RVA: 0x0014F327 File Offset: 0x0014D527
		[SecuritySafeCritical]
		static NativeBuffer()
		{
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x0014F347 File Offset: 0x0014D547
		public NativeBuffer(ulong initialMinCapacity = 0UL)
		{
			this.EnsureByteCapacity(initialMinCapacity);
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600620F RID: 25103 RVA: 0x0014F358 File Offset: 0x0014D558
		protected unsafe void* VoidPointer
		{
			[SecurityCritical]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._handle != null)
				{
					return this._handle.DangerousGetHandle().ToPointer();
				}
				return null;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06006210 RID: 25104 RVA: 0x0014F383 File Offset: 0x0014D583
		protected unsafe byte* BytePointer
		{
			[SecurityCritical]
			get
			{
				return (byte*)this.VoidPointer;
			}
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x0014F38B File Offset: 0x0014D58B
		[SecuritySafeCritical]
		public SafeHandle GetHandle()
		{
			return this._handle ?? NativeBuffer.s_emptyHandle;
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06006212 RID: 25106 RVA: 0x0014F39C File Offset: 0x0014D59C
		public ulong ByteCapacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x0014F3A4 File Offset: 0x0014D5A4
		[SecuritySafeCritical]
		public void EnsureByteCapacity(ulong minCapacity)
		{
			if (this._capacity < minCapacity)
			{
				this.Resize(minCapacity);
				this._capacity = minCapacity;
			}
		}

		// Token: 0x1700110F RID: 4367
		public unsafe byte this[ulong index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.BytePointer[index];
			}
			[SecuritySafeCritical]
			set
			{
				if (index >= this._capacity)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.BytePointer[index] = value;
			}
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x0014F3F4 File Offset: 0x0014D5F4
		[SecuritySafeCritical]
		private void Resize(ulong byteLength)
		{
			if (byteLength == 0UL)
			{
				this.ReleaseHandle();
				return;
			}
			if (this._handle == null)
			{
				this._handle = NativeBuffer.s_handleCache.Acquire(byteLength);
				return;
			}
			this._handle.Resize(byteLength);
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x0014F426 File Offset: 0x0014D626
		[SecuritySafeCritical]
		private void ReleaseHandle()
		{
			if (this._handle != null)
			{
				NativeBuffer.s_handleCache.Release(this._handle);
				this._capacity = 0UL;
				this._handle = null;
			}
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x0014F44F File Offset: 0x0014D64F
		[SecuritySafeCritical]
		public virtual void Free()
		{
			this.ReleaseHandle();
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x0014F457 File Offset: 0x0014D657
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Free();
		}

		// Token: 0x04002B8F RID: 11151
		private static readonly SafeHeapHandleCache s_handleCache = new SafeHeapHandleCache(64UL, 2048UL, 0);

		// Token: 0x04002B90 RID: 11152
		[SecurityCritical]
		private static readonly SafeHandle s_emptyHandle = new NativeBuffer.EmptySafeHandle();

		// Token: 0x04002B91 RID: 11153
		[SecurityCritical]
		private SafeHeapHandle _handle;

		// Token: 0x04002B92 RID: 11154
		private ulong _capacity;

		// Token: 0x02000C9A RID: 3226
		[SecurityCritical]
		private sealed class EmptySafeHandle : SafeHandle
		{
			// Token: 0x0600711D RID: 28957 RVA: 0x0018518E File Offset: 0x0018338E
			public EmptySafeHandle()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x17001365 RID: 4965
			// (get) Token: 0x0600711E RID: 28958 RVA: 0x0018519C File Offset: 0x0018339C
			public override bool IsInvalid
			{
				[SecurityCritical]
				get
				{
					return true;
				}
			}

			// Token: 0x0600711F RID: 28959 RVA: 0x0018519F File Offset: 0x0018339F
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				return true;
			}
		}
	}
}
