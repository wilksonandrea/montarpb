using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000532 RID: 1330
	[__DynamicallyInvokable]
	public static class Volatile
	{
		// Token: 0x06003E67 RID: 15975 RVA: 0x000E87B8 File Offset: 0x000E69B8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static bool Read(ref bool location)
		{
			bool flag = location;
			Thread.MemoryBarrier();
			return flag;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000E87D0 File Offset: 0x000E69D0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Read(ref sbyte location)
		{
			sbyte b = location;
			Thread.MemoryBarrier();
			return b;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000E87E8 File Offset: 0x000E69E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static byte Read(ref byte location)
		{
			byte b = location;
			Thread.MemoryBarrier();
			return b;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000E8800 File Offset: 0x000E6A00
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static short Read(ref short location)
		{
			short num = location;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000E8818 File Offset: 0x000E6A18
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Read(ref ushort location)
		{
			ushort num = location;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000E8830 File Offset: 0x000E6A30
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Read(ref int location)
		{
			int num = location;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000E8848 File Offset: 0x000E6A48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Read(ref uint location)
		{
			uint num = location;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000E885E File Offset: 0x000E6A5E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x000E886C File Offset: 0x000E6A6C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static ulong Read(ref ulong location)
		{
			fixed (ulong* ptr = &location)
			{
				ulong* ptr2 = ptr;
				return (ulong)Interlocked.CompareExchange(ref *(long*)ptr2, 0L, 0L);
			}
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x000E8888 File Offset: 0x000E6A88
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr Read(ref IntPtr location)
		{
			IntPtr intPtr = location;
			Thread.MemoryBarrier();
			return intPtr;
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000E88A0 File Offset: 0x000E6AA0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static UIntPtr Read(ref UIntPtr location)
		{
			UIntPtr uintPtr = location;
			Thread.MemoryBarrier();
			return uintPtr;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000E88B8 File Offset: 0x000E6AB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static float Read(ref float location)
		{
			float num = location;
			Thread.MemoryBarrier();
			return num;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000E88CE File Offset: 0x000E6ACE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static double Read(ref double location)
		{
			return Interlocked.CompareExchange(ref location, 0.0, 0.0);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000E88E8 File Offset: 0x000E6AE8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T Read<T>(ref T location) where T : class
		{
			T t = location;
			Thread.MemoryBarrier();
			return t;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000E8902 File Offset: 0x000E6B02
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref bool location, bool value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000E890C File Offset: 0x000E6B0C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref sbyte location, sbyte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000E8916 File Offset: 0x000E6B16
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref byte location, byte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000E8920 File Offset: 0x000E6B20
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref short location, short value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000E892A File Offset: 0x000E6B2A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref ushort location, ushort value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000E8934 File Offset: 0x000E6B34
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref int location, int value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000E893E File Offset: 0x000E6B3E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref uint location, uint value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000E8948 File Offset: 0x000E6B48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref long location, long value)
		{
			Interlocked.Exchange(ref location, value);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000E8954 File Offset: 0x000E6B54
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static void Write(ref ulong location, ulong value)
		{
			fixed (ulong* ptr = &location)
			{
				ulong* ptr2 = ptr;
				Interlocked.Exchange(ref *(long*)ptr2, (long)value);
			}
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000E8971 File Offset: 0x000E6B71
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x000E897B File Offset: 0x000E6B7B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x000E8985 File Offset: 0x000E6B85
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref float location, float value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000E898F File Offset: 0x000E6B8F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref double location, double value)
		{
			Interlocked.Exchange(ref location, value);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000E8999 File Offset: 0x000E6B99
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Thread.MemoryBarrier();
			location = value;
		}
	}
}
