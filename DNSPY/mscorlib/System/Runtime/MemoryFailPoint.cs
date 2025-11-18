using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime
{
	// Token: 0x02000716 RID: 1814
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x0011DFE4 File Offset: 0x0011C1E4
		// (set) Token: 0x06005124 RID: 20772 RVA: 0x0011DFF0 File Offset: 0x0011C1F0
		private static long LastKnownFreeAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, value);
			}
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x0011DFFD File Offset: 0x0011C1FD
		private static long AddToLastKnownFreeAddressSpace(long addend)
		{
			return Interlocked.Add(ref MemoryFailPoint.hiddenLastKnownFreeAddressSpace, addend);
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x0011E00A File Offset: 0x0011C20A
		// (set) Token: 0x06005127 RID: 20775 RVA: 0x0011E016 File Offset: 0x0011C216
		private static long LastTimeCheckingAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.hiddenLastTimeCheckingAddressSpace, value);
			}
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x0011E023 File Offset: 0x0011C223
		[SecuritySafeCritical]
		static MemoryFailPoint()
		{
			MemoryFailPoint.GetMemorySettings(out MemoryFailPoint.GCSegmentSize, out MemoryFailPoint.TopOfMemory);
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x0011E034 File Offset: 0x0011C234
		[SecurityCritical]
		public unsafe MemoryFailPoint(int sizeInMegabytes)
		{
			if (sizeInMegabytes <= 0)
			{
				throw new ArgumentOutOfRangeException("sizeInMegabytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			ulong num = (ulong)((ulong)((long)sizeInMegabytes) << 20);
			this._reservedMemory = num;
			ulong num2 = (ulong)(Math.Ceiling(num / MemoryFailPoint.GCSegmentSize) * MemoryFailPoint.GCSegmentSize);
			if (num2 >= MemoryFailPoint.TopOfMemory)
			{
				throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_TooBig"));
			}
			ulong num3 = (ulong)(Math.Ceiling((double)sizeInMegabytes / 16.0) * 16.0);
			num3 <<= 20;
			ulong num4 = 0UL;
			ulong num5 = 0UL;
			int i = 0;
			while (i < 3)
			{
				MemoryFailPoint.CheckForAvailableMemory(out num4, out num5);
				ulong memoryFailPointReservedMemory = SharedStatics.MemoryFailPointReservedMemory;
				ulong num6 = num2 + memoryFailPointReservedMemory;
				bool flag = num6 < num2 || num6 < memoryFailPointReservedMemory;
				bool flag2 = num4 < num3 + memoryFailPointReservedMemory + 16777216UL || flag;
				bool flag3 = num5 < num6 || flag;
				long num7 = (long)Environment.TickCount;
				if (num7 > MemoryFailPoint.LastTimeCheckingAddressSpace + 10000L || num7 < MemoryFailPoint.LastTimeCheckingAddressSpace || MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2)
				{
					MemoryFailPoint.CheckForFreeAddressSpace(num2, false);
				}
				bool flag4 = MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2;
				if (!flag2 && !flag3 && !flag4)
				{
					break;
				}
				switch (i)
				{
				case 0:
					GC.Collect();
					break;
				case 1:
					if (flag2)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							break;
						}
						finally
						{
							UIntPtr uintPtr = new UIntPtr(num2);
							void* ptr = Win32Native.VirtualAlloc(null, uintPtr, 4096, 4);
							if (ptr != null && !Win32Native.VirtualFree(ptr, UIntPtr.Zero, 32768))
							{
								__Error.WinIOError();
							}
						}
						goto IL_183;
					}
					break;
				case 2:
					goto IL_183;
				}
				IL_1B6:
				i++;
				continue;
				IL_183:
				if (flag2 || flag3)
				{
					InsufficientMemoryException ex = new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint"));
					throw ex;
				}
				if (flag4)
				{
					InsufficientMemoryException ex2 = new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
					throw ex2;
				}
				goto IL_1B6;
			}
			MemoryFailPoint.AddToLastKnownFreeAddressSpace((long)(-(long)num));
			if (MemoryFailPoint.LastKnownFreeAddressSpace < 0L)
			{
				MemoryFailPoint.CheckForFreeAddressSpace(num2, true);
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SharedStatics.AddMemoryFailPointReservation((long)num);
				this._mustSubtractReservation = true;
			}
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x0011E250 File Offset: 0x0011C450
		[SecurityCritical]
		private static void CheckForAvailableMemory(out ulong availPageFile, out ulong totalAddressSpaceFree)
		{
			Win32Native.MEMORYSTATUSEX memorystatusex = default(Win32Native.MEMORYSTATUSEX);
			if (!Win32Native.GlobalMemoryStatusEx(ref memorystatusex))
			{
				__Error.WinIOError();
			}
			availPageFile = memorystatusex.availPageFile;
			totalAddressSpaceFree = memorystatusex.availVirtual;
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0011E288 File Offset: 0x0011C488
		[SecurityCritical]
		private static bool CheckForFreeAddressSpace(ulong size, bool shouldThrow)
		{
			ulong num = MemoryFailPoint.MemFreeAfterAddress(null, size);
			MemoryFailPoint.LastKnownFreeAddressSpace = (long)num;
			MemoryFailPoint.LastTimeCheckingAddressSpace = (long)Environment.TickCount;
			if (num < size && shouldThrow)
			{
				throw new InsufficientMemoryException(Environment.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag"));
			}
			return num >= size;
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x0011E2D0 File Offset: 0x0011C4D0
		[SecurityCritical]
		private unsafe static ulong MemFreeAfterAddress(void* address, ulong size)
		{
			if (size >= MemoryFailPoint.TopOfMemory)
			{
				return 0UL;
			}
			ulong num = 0UL;
			Win32Native.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION = default(Win32Native.MEMORY_BASIC_INFORMATION);
			UIntPtr uintPtr = (UIntPtr)((ulong)((long)Marshal.SizeOf<Win32Native.MEMORY_BASIC_INFORMATION>(memory_BASIC_INFORMATION)));
			while ((byte*)address + size < MemoryFailPoint.TopOfMemory)
			{
				UIntPtr uintPtr2 = Win32Native.VirtualQuery(address, ref memory_BASIC_INFORMATION, uintPtr);
				if (uintPtr2 == UIntPtr.Zero)
				{
					__Error.WinIOError();
				}
				ulong num2 = memory_BASIC_INFORMATION.RegionSize.ToUInt64();
				if (memory_BASIC_INFORMATION.State == 65536U)
				{
					if (num2 >= size)
					{
						return num2;
					}
					num = Math.Max(num, num2);
				}
				address = (void*)((byte*)address + num2);
			}
			return num;
		}

		// Token: 0x0600512D RID: 20781
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMemorySettings(out ulong maxGCSegmentSize, out ulong topOfMemory);

		// Token: 0x0600512E RID: 20782 RVA: 0x0011E360 File Offset: 0x0011C560
		[SecuritySafeCritical]
		~MemoryFailPoint()
		{
			this.Dispose(false);
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0011E390 File Offset: 0x0011C590
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0011E3A0 File Offset: 0x0011C5A0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Dispose(bool disposing)
		{
			if (this._mustSubtractReservation)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					SharedStatics.AddMemoryFailPointReservation((long)(-(long)this._reservedMemory));
					this._mustSubtractReservation = false;
				}
			}
		}

		// Token: 0x040023F1 RID: 9201
		private static readonly ulong TopOfMemory;

		// Token: 0x040023F2 RID: 9202
		private static long hiddenLastKnownFreeAddressSpace;

		// Token: 0x040023F3 RID: 9203
		private static long hiddenLastTimeCheckingAddressSpace;

		// Token: 0x040023F4 RID: 9204
		private const int CheckThreshold = 10000;

		// Token: 0x040023F5 RID: 9205
		private const int LowMemoryFudgeFactor = 16777216;

		// Token: 0x040023F6 RID: 9206
		private const int MemoryCheckGranularity = 16;

		// Token: 0x040023F7 RID: 9207
		private static readonly ulong GCSegmentSize;

		// Token: 0x040023F8 RID: 9208
		private ulong _reservedMemory;

		// Token: 0x040023F9 RID: 9209
		private bool _mustSubtractReservation;
	}
}
