using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095A RID: 2394
	[SecurityCritical]
	internal sealed class SafeHeapHandle : SafeBuffer
	{
		// Token: 0x060061EB RID: 25067 RVA: 0x0014EB12 File Offset: 0x0014CD12
		public SafeHeapHandle(ulong byteLength)
			: base(true)
		{
			this.Resize(byteLength);
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x0014EB22 File Offset: 0x0014CD22
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x0014EB34 File Offset: 0x0014CD34
		public void Resize(ulong byteLength)
		{
			if (base.IsClosed)
			{
				throw new ObjectDisposedException("SafeHeapHandle");
			}
			ulong num = 0UL;
			if (this.handle == IntPtr.Zero)
			{
				this.handle = Marshal.AllocHGlobal((IntPtr)((long)byteLength));
			}
			else
			{
				num = base.ByteLength;
				this.handle = Marshal.ReAllocHGlobal(this.handle, (IntPtr)((long)byteLength));
			}
			if (this.handle == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			if (byteLength > num)
			{
				ulong num2 = byteLength - num;
				if (num2 > 9223372036854775807UL)
				{
					GC.AddMemoryPressure(long.MaxValue);
					GC.AddMemoryPressure((long)(num2 - 9223372036854775807UL));
				}
				else
				{
					GC.AddMemoryPressure((long)num2);
				}
			}
			else
			{
				this.RemoveMemoryPressure(num - byteLength);
			}
			base.Initialize(byteLength);
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0014EBFE File Offset: 0x0014CDFE
		private void RemoveMemoryPressure(ulong removedBytes)
		{
			if (removedBytes == 0UL)
			{
				return;
			}
			if (removedBytes > 9223372036854775807UL)
			{
				GC.RemoveMemoryPressure(long.MaxValue);
				GC.RemoveMemoryPressure((long)(removedBytes - 9223372036854775807UL));
				return;
			}
			GC.RemoveMemoryPressure((long)removedBytes);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x0014EC38 File Offset: 0x0014CE38
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (handle != IntPtr.Zero)
			{
				this.RemoveMemoryPressure(base.ByteLength);
				Marshal.FreeHGlobal(handle);
			}
			return true;
		}
	}
}
