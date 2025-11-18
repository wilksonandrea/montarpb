using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000952 RID: 2386
	[__DynamicallyInvokable]
	public static class Marshal
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x0014C904 File Offset: 0x0014AB04
		private static bool IsWin32Atom(IntPtr ptr)
		{
			long num = (long)ptr;
			return (num & -65536L) == 0L;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0014C924 File Offset: 0x0014AB24
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static bool IsNotWin32Atom(IntPtr ptr)
		{
			long num = (long)ptr;
			return (num & -65536L) != 0L;
		}

		// Token: 0x060060CA RID: 24778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSystemMaxDBCSCharSize();

		// Token: 0x060060CB RID: 24779 RVA: 0x0014C944 File Offset: 0x0014AB44
		[SecurityCritical]
		public unsafe static string PtrToStringAnsi(IntPtr ptr)
		{
			if (IntPtr.Zero == ptr)
			{
				return null;
			}
			if (Marshal.IsWin32Atom(ptr))
			{
				return null;
			}
			if (Win32Native.lstrlenA(ptr) == 0)
			{
				return string.Empty;
			}
			return new string((sbyte*)(void*)ptr);
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0014C985 File Offset: 0x0014AB85
		[SecurityCritical]
		public unsafe static string PtrToStringAnsi(IntPtr ptr, int len)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (len < 0)
			{
				throw new ArgumentException("len");
			}
			return new string((sbyte*)(void*)ptr, 0, len);
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x0014C9BB File Offset: 0x0014ABBB
		[SecurityCritical]
		public unsafe static string PtrToStringUni(IntPtr ptr, int len)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (len < 0)
			{
				throw new ArgumentException("len");
			}
			return new string((char*)(void*)ptr, 0, len);
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0014C9F1 File Offset: 0x0014ABF1
		[SecurityCritical]
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			return Marshal.PtrToStringUni(ptr, len);
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0014C9FA File Offset: 0x0014ABFA
		[SecurityCritical]
		public unsafe static string PtrToStringUni(IntPtr ptr)
		{
			if (IntPtr.Zero == ptr)
			{
				return null;
			}
			if (Marshal.IsWin32Atom(ptr))
			{
				return null;
			}
			return new string((char*)(void*)ptr);
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0014CA20 File Offset: 0x0014AC20
		[SecurityCritical]
		public static string PtrToStringAuto(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0014CA28 File Offset: 0x0014AC28
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			if (structure == null)
			{
				throw new ArgumentNullException("structure");
			}
			return Marshal.SizeOfHelper(structure.GetType(), true);
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x0014CA44 File Offset: 0x0014AC44
		public static int SizeOf<T>(T structure)
		{
			return Marshal.SizeOf(structure);
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0014CA54 File Offset: 0x0014AC54
		public static int SizeOf(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!(t is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
			}
			return Marshal.SizeOfHelper(t, true);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0014CAB6 File Offset: 0x0014ACB6
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0014CAC8 File Offset: 0x0014ACC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = Marshal.SizeOfType(typeof(T));
			if (num == 1U || num == 2U)
			{
				return num;
			}
			if (IntPtr.Size == 8 && num == 4U)
			{
				return num;
			}
			return Marshal.AlignedSizeOfType(typeof(T));
		}

		// Token: 0x060060D6 RID: 24790
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint SizeOfType(Type type);

		// Token: 0x060060D7 RID: 24791
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint AlignedSizeOfType(Type type);

		// Token: 0x060060D8 RID: 24792
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int SizeOfHelper(Type t, bool throwIfNotMarshalable);

		// Token: 0x060060D9 RID: 24793 RVA: 0x0014CB0C File Offset: 0x0014AD0C
		public static IntPtr OffsetOf(Type t, string fieldName)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			FieldInfo field = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OffsetOfFieldNotFound", new object[] { t.FullName }), "fieldName");
			}
			RtFieldInfo rtFieldInfo = field as RtFieldInfo;
			if (rtFieldInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"), "fieldName");
			}
			return Marshal.OffsetOfHelper(rtFieldInfo);
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x0014CB8F File Offset: 0x0014AD8F
		public static IntPtr OffsetOf<T>(string fieldName)
		{
			return Marshal.OffsetOf(typeof(T), fieldName);
		}

		// Token: 0x060060DB RID: 24795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OffsetOfHelper(IRuntimeFieldInfo f);

		// Token: 0x060060DC RID: 24796
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		// Token: 0x060060DD RID: 24797 RVA: 0x0014CBA1 File Offset: 0x0014ADA1
		[SecurityCritical]
		public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x0014CBAA File Offset: 0x0014ADAA
		[SecurityCritical]
		public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x0014CBB5 File Offset: 0x0014ADB5
		[SecurityCritical]
		public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x0014CBC0 File Offset: 0x0014ADC0
		[SecurityCritical]
		public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x0014CBCB File Offset: 0x0014ADCB
		[SecurityCritical]
		public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x0014CBD6 File Offset: 0x0014ADD6
		[SecurityCritical]
		public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x0014CBE1 File Offset: 0x0014ADE1
		[SecurityCritical]
		public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x0014CBEC File Offset: 0x0014ADEC
		[SecurityCritical]
		public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x0014CBF7 File Offset: 0x0014ADF7
		[SecurityCritical]
		public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative(source, startIndex, destination, length);
		}

		// Token: 0x060060E6 RID: 24806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyToNative(object source, int startIndex, IntPtr destination, int length);

		// Token: 0x060060E7 RID: 24807 RVA: 0x0014CC02 File Offset: 0x0014AE02
		[SecurityCritical]
		public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x0014CC0D File Offset: 0x0014AE0D
		[SecurityCritical]
		public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x0014CC18 File Offset: 0x0014AE18
		[SecurityCritical]
		public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0014CC23 File Offset: 0x0014AE23
		[SecurityCritical]
		public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0014CC2E File Offset: 0x0014AE2E
		[SecurityCritical]
		public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x0014CC39 File Offset: 0x0014AE39
		[SecurityCritical]
		public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0014CC44 File Offset: 0x0014AE44
		[SecurityCritical]
		public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0014CC4F File Offset: 0x0014AE4F
		[SecurityCritical]
		public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged(source, destination, startIndex, length);
		}

		// Token: 0x060060EF RID: 24815
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyToManaged(IntPtr source, object destination, int startIndex, int length);

		// Token: 0x060060F0 RID: 24816
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_RU1")]
		public static extern byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs);

		// Token: 0x060060F1 RID: 24817 RVA: 0x0014CC5C File Offset: 0x0014AE5C
		[SecurityCritical]
		public unsafe static byte ReadByte(IntPtr ptr, int ofs)
		{
			byte b;
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				b = *ptr2;
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return b;
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x0014CC90 File Offset: 0x0014AE90
		[SecurityCritical]
		public static byte ReadByte(IntPtr ptr)
		{
			return Marshal.ReadByte(ptr, 0);
		}

		// Token: 0x060060F3 RID: 24819
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_RI2")]
		public static extern short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs);

		// Token: 0x060060F4 RID: 24820 RVA: 0x0014CC9C File Offset: 0x0014AE9C
		[SecurityCritical]
		public unsafe static short ReadInt16(IntPtr ptr, int ofs)
		{
			short num;
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 1) == 0)
				{
					num = *(short*)ptr2;
				}
				else
				{
					short num2;
					byte* ptr3 = (byte*)(&num2);
					*ptr3 = *ptr2;
					ptr3[1] = ptr2[1];
					num = num2;
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return num;
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x0014CCEC File Offset: 0x0014AEEC
		[SecurityCritical]
		public static short ReadInt16(IntPtr ptr)
		{
			return Marshal.ReadInt16(ptr, 0);
		}

		// Token: 0x060060F6 RID: 24822
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_RI4")]
		public static extern int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs);

		// Token: 0x060060F7 RID: 24823 RVA: 0x0014CCF8 File Offset: 0x0014AEF8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr, int ofs)
		{
			int num;
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 3) == 0)
				{
					num = *(int*)ptr2;
				}
				else
				{
					int num2;
					byte* ptr3 = (byte*)(&num2);
					*ptr3 = *ptr2;
					ptr3[1] = ptr2[1];
					ptr3[2] = ptr2[2];
					ptr3[3] = ptr2[3];
					num = num2;
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return num;
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x0014CD58 File Offset: 0x0014AF58
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int ReadInt32(IntPtr ptr)
		{
			return Marshal.ReadInt32(ptr, 0);
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x0014CD61 File Offset: 0x0014AF61
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			return (IntPtr)Marshal.ReadInt32(ptr, ofs);
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0014CD6F File Offset: 0x0014AF6F
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
		{
			return (IntPtr)Marshal.ReadInt32(ptr, ofs);
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0014CD7D File Offset: 0x0014AF7D
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			return (IntPtr)Marshal.ReadInt32(ptr, 0);
		}

		// Token: 0x060060FC RID: 24828
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_RI8")]
		public static extern long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs);

		// Token: 0x060060FD RID: 24829 RVA: 0x0014CD8C File Offset: 0x0014AF8C
		[SecurityCritical]
		public unsafe static long ReadInt64(IntPtr ptr, int ofs)
		{
			long num;
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 7) == 0)
				{
					num = *(long*)ptr2;
				}
				else
				{
					long num2;
					byte* ptr3 = (byte*)(&num2);
					*ptr3 = *ptr2;
					ptr3[1] = ptr2[1];
					ptr3[2] = ptr2[2];
					ptr3[3] = ptr2[3];
					ptr3[4] = ptr2[4];
					ptr3[5] = ptr2[5];
					ptr3[6] = ptr2[6];
					ptr3[7] = ptr2[7];
					num = num2;
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return num;
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0014CE0C File Offset: 0x0014B00C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long ReadInt64(IntPtr ptr)
		{
			return Marshal.ReadInt64(ptr, 0);
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0014CE18 File Offset: 0x0014B018
		[SecurityCritical]
		public unsafe static void WriteByte(IntPtr ptr, int ofs, byte val)
		{
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				*ptr2 = val;
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06006100 RID: 24832
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_WU1")]
		public static extern void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val);

		// Token: 0x06006101 RID: 24833 RVA: 0x0014CE4C File Offset: 0x0014B04C
		[SecurityCritical]
		public static void WriteByte(IntPtr ptr, byte val)
		{
			Marshal.WriteByte(ptr, 0, val);
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0014CE58 File Offset: 0x0014B058
		[SecurityCritical]
		public unsafe static void WriteInt16(IntPtr ptr, int ofs, short val)
		{
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 1) == 0)
				{
					*(short*)ptr2 = val;
				}
				else
				{
					byte* ptr3 = (byte*)(&val);
					*ptr2 = *ptr3;
					ptr2[1] = ptr3[1];
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06006103 RID: 24835
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_WI2")]
		public static extern void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val);

		// Token: 0x06006104 RID: 24836 RVA: 0x0014CEA4 File Offset: 0x0014B0A4
		[SecurityCritical]
		public static void WriteInt16(IntPtr ptr, short val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0014CEAE File Offset: 0x0014B0AE
		[SecurityCritical]
		public static void WriteInt16(IntPtr ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0014CEB9 File Offset: 0x0014B0B9
		[SecurityCritical]
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0014CEC4 File Offset: 0x0014B0C4
		[SecurityCritical]
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, (short)val);
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x0014CED0 File Offset: 0x0014B0D0
		[SecurityCritical]
		public unsafe static void WriteInt32(IntPtr ptr, int ofs, int val)
		{
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 3) == 0)
				{
					*(int*)ptr2 = val;
				}
				else
				{
					byte* ptr3 = (byte*)(&val);
					*ptr2 = *ptr3;
					ptr2[1] = ptr3[1];
					ptr2[2] = ptr3[2];
					ptr2[3] = ptr3[3];
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06006109 RID: 24841
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_WI4")]
		public static extern void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val);

		// Token: 0x0600610A RID: 24842 RVA: 0x0014CF2C File Offset: 0x0014B12C
		[SecurityCritical]
		public static void WriteInt32(IntPtr ptr, int val)
		{
			Marshal.WriteInt32(ptr, 0, val);
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0014CF36 File Offset: 0x0014B136
		[SecurityCritical]
		public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
		{
			Marshal.WriteInt32(ptr, ofs, (int)val);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x0014CF45 File Offset: 0x0014B145
		[SecurityCritical]
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			Marshal.WriteInt32(ptr, ofs, (int)val);
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x0014CF54 File Offset: 0x0014B154
		[SecurityCritical]
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			Marshal.WriteInt32(ptr, 0, (int)val);
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0014CF64 File Offset: 0x0014B164
		[SecurityCritical]
		public unsafe static void WriteInt64(IntPtr ptr, int ofs, long val)
		{
			try
			{
				byte* ptr2 = (byte*)(void*)ptr + ofs;
				if ((ptr2 & 7) == 0)
				{
					*(long*)ptr2 = val;
				}
				else
				{
					byte* ptr3 = (byte*)(&val);
					*ptr2 = *ptr3;
					ptr2[1] = ptr3[1];
					ptr2[2] = ptr3[2];
					ptr2[3] = ptr3[3];
					ptr2[4] = ptr3[4];
					ptr2[5] = ptr3[5];
					ptr2[6] = ptr3[6];
					ptr2[7] = ptr3[7];
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x0600610F RID: 24847
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mscoree.dll", EntryPoint = "ND_WI8")]
		public static extern void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val);

		// Token: 0x06006110 RID: 24848 RVA: 0x0014CFE0 File Offset: 0x0014B1E0
		[SecurityCritical]
		public static void WriteInt64(IntPtr ptr, long val)
		{
			Marshal.WriteInt64(ptr, 0, val);
		}

		// Token: 0x06006111 RID: 24849
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		// Token: 0x06006112 RID: 24850
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastWin32Error(int error);

		// Token: 0x06006113 RID: 24851 RVA: 0x0014CFEC File Offset: 0x0014B1EC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (((long)lastWin32Error & (long)((ulong)(-2147483648))) == (long)((ulong)(-2147483648)))
			{
				return lastWin32Error;
			}
			return (lastWin32Error & 65535) | -2147024896;
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x0014D020 File Offset: 0x0014B220
		[SecurityCritical]
		public static void Prelink(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
			Marshal.InternalPrelink(runtimeMethodInfo);
		}

		// Token: 0x06006115 RID: 24853
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InternalPrelink(IRuntimeMethodInfo m);

		// Token: 0x06006116 RID: 24854 RVA: 0x0014D068 File Offset: 0x0014B268
		[SecurityCritical]
		public static void PrelinkAll(Type c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			MethodInfo[] methods = c.GetMethods();
			if (methods != null)
			{
				for (int i = 0; i < methods.Length; i++)
				{
					Marshal.Prelink(methods[i]);
				}
			}
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x0014D0AC File Offset: 0x0014B2AC
		[SecurityCritical]
		public static int NumParamBytes(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
			return Marshal.InternalNumParamBytes(runtimeMethodInfo);
		}

		// Token: 0x06006118 RID: 24856
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalNumParamBytes(IRuntimeMethodInfo m);

		// Token: 0x06006119 RID: 24857
		[SecurityCritical]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetExceptionPointers();

		// Token: 0x0600611A RID: 24858
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetExceptionCode();

		// Token: 0x0600611B RID: 24859
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		// Token: 0x0600611C RID: 24860 RVA: 0x0014D0F3 File Offset: 0x0014B2F3
		[SecurityCritical]
		public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
		{
			Marshal.StructureToPtr(structure, ptr, fDeleteOld);
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x0014D102 File Offset: 0x0014B302
		[SecurityCritical]
		[ComVisible(true)]
		public static void PtrToStructure(IntPtr ptr, object structure)
		{
			Marshal.PtrToStructureHelper(ptr, structure, false);
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x0014D10C File Offset: 0x0014B30C
		[SecurityCritical]
		public static void PtrToStructure<T>(IntPtr ptr, T structure)
		{
			Marshal.PtrToStructure(ptr, structure);
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0014D11C File Offset: 0x0014B31C
		[SecurityCritical]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static object PtrToStructure(IntPtr ptr, Type structureType)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			if (structureType == null)
			{
				throw new ArgumentNullException("structureType");
			}
			if (structureType.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "structureType");
			}
			RuntimeType runtimeType = structureType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			object obj = runtimeType.CreateInstanceDefaultCtor(false, false, false, ref stackCrawlMark);
			Marshal.PtrToStructureHelper(ptr, obj, true);
			return obj;
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0014D1AA File Offset: 0x0014B3AA
		[SecurityCritical]
		public static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x06006121 RID: 24865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PtrToStructureHelper(IntPtr ptr, object structure, bool allowValueClasses);

		// Token: 0x06006122 RID: 24866
		[SecurityCritical]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		// Token: 0x06006123 RID: 24867 RVA: 0x0014D1C1 File Offset: 0x0014B3C1
		[SecurityCritical]
		public static void DestroyStructure<T>(IntPtr ptr)
		{
			Marshal.DestroyStructure(ptr, typeof(T));
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x0014D1D4 File Offset: 0x0014B3D4
		[SecurityCritical]
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeModule runtimeModule = m as RuntimeModule;
			if (runtimeModule == null)
			{
				ModuleBuilder moduleBuilder = m as ModuleBuilder;
				if (moduleBuilder != null)
				{
					runtimeModule = moduleBuilder.InternalModule;
				}
			}
			if (runtimeModule == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("Argument_MustBeRuntimeModule"));
			}
			return Marshal.GetHINSTANCE(runtimeModule.GetNativeHandle());
		}

		// Token: 0x06006125 RID: 24869
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetHINSTANCE(RuntimeModule m);

		// Token: 0x06006126 RID: 24870 RVA: 0x0014D240 File Offset: 0x0014B440
		[SecurityCritical]
		public static void ThrowExceptionForHR(int errorCode)
		{
			if (errorCode < 0)
			{
				Marshal.ThrowExceptionForHRInternal(errorCode, IntPtr.Zero);
			}
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x0014D251 File Offset: 0x0014B451
		[SecurityCritical]
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode < 0)
			{
				Marshal.ThrowExceptionForHRInternal(errorCode, errorInfo);
			}
		}

		// Token: 0x06006128 RID: 24872
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ThrowExceptionForHRInternal(int errorCode, IntPtr errorInfo);

		// Token: 0x06006129 RID: 24873 RVA: 0x0014D25E File Offset: 0x0014B45E
		[SecurityCritical]
		public static Exception GetExceptionForHR(int errorCode)
		{
			if (errorCode < 0)
			{
				return Marshal.GetExceptionForHRInternal(errorCode, IntPtr.Zero);
			}
			return null;
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x0014D271 File Offset: 0x0014B471
		[SecurityCritical]
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode < 0)
			{
				return Marshal.GetExceptionForHRInternal(errorCode, errorInfo);
			}
			return null;
		}

		// Token: 0x0600612B RID: 24875
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception GetExceptionForHRInternal(int errorCode, IntPtr errorInfo);

		// Token: 0x0600612C RID: 24876
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHRForException(Exception e);

		// Token: 0x0600612D RID: 24877
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetHRForException_WinRT(Exception e);

		// Token: 0x0600612E RID: 24878
		[SecurityCritical]
		[Obsolete("The GetUnmanagedThunkForManagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

		// Token: 0x0600612F RID: 24879
		[SecurityCritical]
		[Obsolete("The GetManagedThunkForUnmanagedMethodPtr method has been deprecated and will be removed in a future release.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature);

		// Token: 0x06006130 RID: 24880 RVA: 0x0014D280 File Offset: 0x0014B480
		[SecurityCritical]
		[Obsolete("The GetThreadFromFiberCookie method has been deprecated.  Use the hosting API to perform this operation.", false)]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			if (cookie == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "cookie");
			}
			return Marshal.InternalGetThreadFromFiberCookie(cookie);
		}

		// Token: 0x06006131 RID: 24881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread InternalGetThreadFromFiberCookie(int cookie);

		// Token: 0x06006132 RID: 24882 RVA: 0x0014D2A0 File Offset: 0x0014B4A0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(IntPtr cb)
		{
			UIntPtr uintPtr = new UIntPtr((uint)cb.ToInt32());
			IntPtr intPtr = Win32Native.LocalAlloc_NoSafeHandle(0, uintPtr);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x0014D2D7 File Offset: 0x0014B4D7
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0014D2E4 File Offset: 0x0014B4E4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void FreeHGlobal(IntPtr hglobal)
		{
			if (Marshal.IsNotWin32Atom(hglobal) && IntPtr.Zero != Win32Native.LocalFree(hglobal))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0014D30C File Offset: 0x0014B50C
		[SecurityCritical]
		public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
		{
			IntPtr intPtr = Win32Native.LocalReAlloc(pv, cb, 2);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0014D338 File Offset: 0x0014B538
		[SecurityCritical]
		public unsafe static IntPtr StringToHGlobalAnsi(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			UIntPtr uintPtr = new UIntPtr((uint)num);
			IntPtr intPtr = Win32Native.LocalAlloc_NoSafeHandle(0, uintPtr);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			s.ConvertToAnsi((byte*)(void*)intPtr, num, false, false);
			return intPtr;
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x0014D3A8 File Offset: 0x0014B5A8
		[SecurityCritical]
		public unsafe static IntPtr StringToHGlobalUni(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 2;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			UIntPtr uintPtr = new UIntPtr((uint)num);
			IntPtr intPtr = Win32Native.LocalAlloc_NoSafeHandle(0, uintPtr);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				string.wstrcpy((char*)(void*)intPtr, ptr, s.Length + 1);
			}
			return intPtr;
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x0014D42A File Offset: 0x0014B62A
		[SecurityCritical]
		public static IntPtr StringToHGlobalAuto(string s)
		{
			return Marshal.StringToHGlobalUni(s);
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x0014D432 File Offset: 0x0014B632
		[SecurityCritical]
		[Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibName(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			return Marshal.GetTypeLibName((ITypeLib)pTLB);
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x0014D440 File Offset: 0x0014B640
		[SecurityCritical]
		public static string GetTypeLibName(ITypeLib typelib)
		{
			if (typelib == null)
			{
				throw new ArgumentNullException("typelib");
			}
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			typelib.GetDocumentation(-1, out text, out text2, out num, out text3);
			return text;
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x0014D474 File Offset: 0x0014B674
		[SecurityCritical]
		internal static string GetTypeLibNameInternal(ITypeLib typelib)
		{
			if (typelib == null)
			{
				throw new ArgumentNullException("typelib");
			}
			ITypeLib2 typeLib = typelib as ITypeLib2;
			if (typeLib != null)
			{
				Guid managedNameGuid = Marshal.ManagedNameGuid;
				object obj;
				try
				{
					typeLib.GetCustData(ref managedNameGuid, out obj);
				}
				catch (Exception)
				{
					obj = null;
				}
				if (obj != null && obj.GetType() == typeof(string))
				{
					string text = (string)obj;
					text = text.Trim();
					if (text.EndsWith(".DLL", StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(0, text.Length - 4);
					}
					else if (text.EndsWith(".EXE", StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(0, text.Length - 4);
					}
					return text;
				}
			}
			return Marshal.GetTypeLibName(typelib);
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0014D530 File Offset: 0x0014B730
		[SecurityCritical]
		[Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibGuid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			return Marshal.GetTypeLibGuid((ITypeLib)pTLB);
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x0014D540 File Offset: 0x0014B740
		[SecurityCritical]
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			Guid guid = default(Guid);
			Marshal.FCallGetTypeLibGuid(ref guid, typelib);
			return guid;
		}

		// Token: 0x0600613E RID: 24894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallGetTypeLibGuid(ref Guid result, ITypeLib pTLB);

		// Token: 0x0600613F RID: 24895 RVA: 0x0014D55E File Offset: 0x0014B75E
		[SecurityCritical]
		[Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeLibLcid(ITypeLib pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			return Marshal.GetTypeLibLcid((ITypeLib)pTLB);
		}

		// Token: 0x06006140 RID: 24896
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetTypeLibLcid(ITypeLib typelib);

		// Token: 0x06006141 RID: 24897
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetTypeLibVersion(ITypeLib typeLibrary, out int major, out int minor);

		// Token: 0x06006142 RID: 24898 RVA: 0x0014D56C File Offset: 0x0014B76C
		[SecurityCritical]
		internal static Guid GetTypeInfoGuid(ITypeInfo typeInfo)
		{
			Guid guid = default(Guid);
			Marshal.FCallGetTypeInfoGuid(ref guid, typeInfo);
			return guid;
		}

		// Token: 0x06006143 RID: 24899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallGetTypeInfoGuid(ref Guid result, ITypeInfo typeInfo);

		// Token: 0x06006144 RID: 24900 RVA: 0x0014D58C File Offset: 0x0014B78C
		[SecurityCritical]
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			if (asm == null)
			{
				throw new ArgumentNullException("asm");
			}
			RuntimeAssembly runtimeAssembly = asm as RuntimeAssembly;
			if (runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "asm");
			}
			Guid guid = default(Guid);
			Marshal.FCallGetTypeLibGuidForAssembly(ref guid, runtimeAssembly);
			return guid;
		}

		// Token: 0x06006145 RID: 24901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallGetTypeLibGuidForAssembly(ref Guid result, RuntimeAssembly asm);

		// Token: 0x06006146 RID: 24902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetTypeLibVersionForAssembly(RuntimeAssembly inputAssembly, out int majorVersion, out int minorVersion);

		// Token: 0x06006147 RID: 24903 RVA: 0x0014D5E4 File Offset: 0x0014B7E4
		[SecurityCritical]
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			if (inputAssembly == null)
			{
				throw new ArgumentNullException("inputAssembly");
			}
			RuntimeAssembly runtimeAssembly = inputAssembly as RuntimeAssembly;
			if (runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "inputAssembly");
			}
			Marshal._GetTypeLibVersionForAssembly(runtimeAssembly, out majorVersion, out minorVersion);
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0014D632 File Offset: 0x0014B832
		[SecurityCritical]
		[Obsolete("Use System.Runtime.InteropServices.Marshal.GetTypeInfoName(ITypeInfo pTLB) instead. http://go.microsoft.com/fwlink/?linkid=14202&ID=0000011.", false)]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			return Marshal.GetTypeInfoName((ITypeInfo)pTI);
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0014D640 File Offset: 0x0014B840
		[SecurityCritical]
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			typeInfo.GetDocumentation(-1, out text, out text2, out num, out text3);
			return text;
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0014D674 File Offset: 0x0014B874
		[SecurityCritical]
		internal static string GetTypeInfoNameInternal(ITypeInfo typeInfo, out bool hasManagedName)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			ITypeInfo2 typeInfo2 = typeInfo as ITypeInfo2;
			if (typeInfo2 != null)
			{
				Guid managedNameGuid = Marshal.ManagedNameGuid;
				object obj;
				try
				{
					typeInfo2.GetCustData(ref managedNameGuid, out obj);
				}
				catch (Exception)
				{
					obj = null;
				}
				if (obj != null && obj.GetType() == typeof(string))
				{
					hasManagedName = true;
					return (string)obj;
				}
			}
			hasManagedName = false;
			return Marshal.GetTypeInfoName(typeInfo);
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0014D6F0 File Offset: 0x0014B8F0
		[SecurityCritical]
		internal static string GetManagedTypeInfoNameInternal(ITypeLib typeLib, ITypeInfo typeInfo)
		{
			bool flag;
			string typeInfoNameInternal = Marshal.GetTypeInfoNameInternal(typeInfo, out flag);
			if (flag)
			{
				return typeInfoNameInternal;
			}
			return Marshal.GetTypeLibNameInternal(typeLib) + "." + typeInfoNameInternal;
		}

		// Token: 0x0600614C RID: 24908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetLoadedTypeForGUID(ref Guid guid);

		// Token: 0x0600614D RID: 24909 RVA: 0x0014D71C File Offset: 0x0014B91C
		[SecurityCritical]
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			ITypeInfo typeInfo = null;
			ITypeLib typeLib = null;
			Assembly assembly = null;
			int num = 0;
			if (piTypeInfo == IntPtr.Zero)
			{
				return null;
			}
			typeInfo = (ITypeInfo)Marshal.GetObjectForIUnknown(piTypeInfo);
			Guid typeInfoGuid = Marshal.GetTypeInfoGuid(typeInfo);
			Type type = Marshal.GetLoadedTypeForGUID(ref typeInfoGuid);
			if (type != null)
			{
				return type;
			}
			try
			{
				typeInfo.GetContainingTypeLib(out typeLib, out num);
			}
			catch (COMException)
			{
				typeLib = null;
			}
			if (typeLib != null)
			{
				AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, null, null, null, null, AssemblyNameFlags.None);
				string fullName = assemblyNameFromTypelib.FullName;
				Assembly[] assemblies = Thread.GetDomain().GetAssemblies();
				int num2 = assemblies.Length;
				for (int i = 0; i < num2; i++)
				{
					if (string.Compare(assemblies[i].FullName, fullName, StringComparison.Ordinal) == 0)
					{
						assembly = assemblies[i];
					}
				}
				if (assembly == null)
				{
					TypeLibConverter typeLibConverter = new TypeLibConverter();
					assembly = typeLibConverter.ConvertTypeLibToAssembly(typeLib, Marshal.GetTypeLibName(typeLib) + ".dll", TypeLibImporterFlags.None, new ImporterCallback(), null, null, null, null);
				}
				type = assembly.GetType(Marshal.GetManagedTypeInfoNameInternal(typeLib, typeInfo), true, false);
				if (type != null && !type.IsVisible)
				{
					type = null;
				}
			}
			else
			{
				type = typeof(object);
			}
			return type;
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x0014D84C File Offset: 0x0014BA4C
		[SecuritySafeCritical]
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, null, false);
		}

		// Token: 0x0600614F RID: 24911
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetITypeInfoForType(Type t);

		// Token: 0x06006150 RID: 24912 RVA: 0x0014D856 File Offset: 0x0014BA56
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static IntPtr GetIUnknownForObject(object o)
		{
			return Marshal.GetIUnknownForObjectNative(o, false);
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0014D85F File Offset: 0x0014BA5F
		[SecurityCritical]
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			return Marshal.GetIUnknownForObjectNative(o, true);
		}

		// Token: 0x06006152 RID: 24914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectNative(object o, bool onlyInContext);

		// Token: 0x06006153 RID: 24915
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

		// Token: 0x06006154 RID: 24916 RVA: 0x0014D868 File Offset: 0x0014BA68
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static IntPtr GetIDispatchForObject(object o)
		{
			return Marshal.GetIDispatchForObjectNative(o, false);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0014D871 File Offset: 0x0014BA71
		[SecurityCritical]
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			return Marshal.GetIDispatchForObjectNative(o, true);
		}

		// Token: 0x06006156 RID: 24918
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectNative(object o, bool onlyInContext);

		// Token: 0x06006157 RID: 24919 RVA: 0x0014D87A File Offset: 0x0014BA7A
		[SecurityCritical]
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			return Marshal.GetComInterfaceForObjectNative(o, T, false, true);
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0014D885 File Offset: 0x0014BA85
		[SecurityCritical]
		public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
		{
			return Marshal.GetComInterfaceForObject(o, typeof(TInterface));
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0014D89C File Offset: 0x0014BA9C
		[SecurityCritical]
		public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
		{
			bool flag = mode == CustomQueryInterfaceMode.Allow;
			return Marshal.GetComInterfaceForObjectNative(o, T, false, flag);
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0014D8BB File Offset: 0x0014BABB
		[SecurityCritical]
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			return Marshal.GetComInterfaceForObjectNative(o, t, true, true);
		}

		// Token: 0x0600615B RID: 24923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComInterfaceForObjectNative(object o, Type t, bool onlyInContext, bool fEnalbeCustomizedQueryInterface);

		// Token: 0x0600615C RID: 24924
		[SecurityCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectForIUnknown(IntPtr pUnk);

		// Token: 0x0600615D RID: 24925
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetUniqueObjectForIUnknown(IntPtr unknown);

		// Token: 0x0600615E RID: 24926
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetTypedObjectForIUnknown(IntPtr pUnk, Type t);

		// Token: 0x0600615F RID: 24927
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateAggregatedObject(IntPtr pOuter, object o);

		// Token: 0x06006160 RID: 24928 RVA: 0x0014D8C6 File Offset: 0x0014BAC6
		[SecurityCritical]
		public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
		{
			return Marshal.CreateAggregatedObject(pOuter, o);
		}

		// Token: 0x06006161 RID: 24929
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CleanupUnusedObjectsInCurrentContext();

		// Token: 0x06006162 RID: 24930
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool AreComObjectsAvailableForCleanup();

		// Token: 0x06006163 RID: 24931
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		// Token: 0x06006164 RID: 24932 RVA: 0x0014D8D4 File Offset: 0x0014BAD4
		[SecurityCritical]
		public static IntPtr AllocCoTaskMem(int cb)
		{
			IntPtr intPtr = Win32Native.CoTaskMemAlloc(new UIntPtr((uint)cb));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0014D904 File Offset: 0x0014BB04
		[SecurityCritical]
		public unsafe static IntPtr StringToCoTaskMemUni(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 2;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Win32Native.CoTaskMemAlloc(new UIntPtr((uint)num));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				string.wstrcpy((char*)(void*)intPtr, ptr, s.Length + 1);
			}
			return intPtr;
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0014D97F File Offset: 0x0014BB7F
		[SecurityCritical]
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			return Marshal.StringToCoTaskMemUni(s);
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0014D988 File Offset: 0x0014BB88
		[SecurityCritical]
		public unsafe static IntPtr StringToCoTaskMemAnsi(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * Marshal.SystemMaxDBCSCharSize;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Win32Native.CoTaskMemAlloc(new UIntPtr((uint)num));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			s.ConvertToAnsi((byte*)(void*)intPtr, num, false, false);
			return intPtr;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0014D9F1 File Offset: 0x0014BBF1
		[SecurityCritical]
		public static void FreeCoTaskMem(IntPtr ptr)
		{
			if (Marshal.IsNotWin32Atom(ptr))
			{
				Win32Native.CoTaskMemFree(ptr);
			}
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x0014DA04 File Offset: 0x0014BC04
		[SecurityCritical]
		public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
		{
			IntPtr intPtr = Win32Native.CoTaskMemRealloc(pv, new UIntPtr((uint)cb));
			if (intPtr == IntPtr.Zero && cb != 0)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x0014DA38 File Offset: 0x0014BC38
		[SecurityCritical]
		public static int ReleaseComObject(object o)
		{
			__ComObject _ComObject = null;
			try
			{
				_ComObject = (__ComObject)o;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
			}
			return _ComObject.ReleaseSelf();
		}

		// Token: 0x0600616B RID: 24939
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalReleaseComObject(object o);

		// Token: 0x0600616C RID: 24940 RVA: 0x0014DA7C File Offset: 0x0014BC7C
		[SecurityCritical]
		public static int FinalReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			__ComObject _ComObject = null;
			try
			{
				_ComObject = (__ComObject)o;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
			}
			_ComObject.FinalReleaseSelf();
			return 0;
		}

		// Token: 0x0600616D RID: 24941
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFinalReleaseComObject(object o);

		// Token: 0x0600616E RID: 24942 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
		[SecurityCritical]
		public static object GetComObjectData(object obj, object key)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			__ComObject _ComObject = null;
			try
			{
				_ComObject = (__ComObject)obj;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "obj");
			}
			if (obj.GetType().IsWindowsRuntimeObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "obj");
			}
			return _ComObject.GetData(key);
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0014DB54 File Offset: 0x0014BD54
		[SecurityCritical]
		public static bool SetComObjectData(object obj, object key, object data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			__ComObject _ComObject = null;
			try
			{
				_ComObject = (__ComObject)obj;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "obj");
			}
			if (obj.GetType().IsWindowsRuntimeObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "obj");
			}
			return _ComObject.SetData(key, data);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0014DBD8 File Offset: 0x0014BDD8
		[SecurityCritical]
		public static object CreateWrapperOfType(object o, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsCOMObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotComObject"), "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
			}
			if (t.IsWindowsRuntimeObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeIsWinRTType"), "t");
			}
			if (o == null)
			{
				return null;
			}
			if (!o.GetType().IsCOMObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjNotComObject"), "o");
			}
			if (o.GetType().IsWindowsRuntimeObject)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ObjIsWinRTObject"), "o");
			}
			if (o.GetType() == t)
			{
				return o;
			}
			object obj = Marshal.GetComObjectData(o, t);
			if (obj == null)
			{
				obj = Marshal.InternalCreateWrapperOfType(o, t);
				if (!Marshal.SetComObjectData(o, t, obj))
				{
					obj = Marshal.GetComObjectData(o, t);
				}
			}
			return obj;
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0014DCCF File Offset: 0x0014BECF
		[SecurityCritical]
		public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
		{
			return (TWrapper)((object)Marshal.CreateWrapperOfType(o, typeof(TWrapper)));
		}

		// Token: 0x06006172 RID: 24946
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalCreateWrapperOfType(object o, Type t);

		// Token: 0x06006173 RID: 24947 RVA: 0x0014DCEB File Offset: 0x0014BEEB
		[SecurityCritical]
		[Obsolete("This API did not perform any operation and will be removed in future versions of the CLR.", false)]
		public static void ReleaseThreadCache()
		{
		}

		// Token: 0x06006174 RID: 24948
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTypeVisibleFromCom(Type t);

		// Token: 0x06006175 RID: 24949
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		// Token: 0x06006176 RID: 24950
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AddRef(IntPtr pUnk);

		// Token: 0x06006177 RID: 24951
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Release(IntPtr pUnk);

		// Token: 0x06006178 RID: 24952 RVA: 0x0014DCED File Offset: 0x0014BEED
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void FreeBSTR(IntPtr ptr)
		{
			if (Marshal.IsNotWin32Atom(ptr))
			{
				Win32Native.SysFreeString(ptr);
			}
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0014DD00 File Offset: 0x0014BF00
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static IntPtr StringToBSTR(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			if (s.Length + 1 < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Win32Native.SysAllocStringLen(s, s.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0014DD52 File Offset: 0x0014BF52
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static string PtrToStringBSTR(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr, (int)Win32Native.SysStringLen(ptr));
		}

		// Token: 0x0600617B RID: 24955
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant);

		// Token: 0x0600617C RID: 24956 RVA: 0x0014DD60 File Offset: 0x0014BF60
		[SecurityCritical]
		public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
		{
			Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
		}

		// Token: 0x0600617D RID: 24957
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectForNativeVariant(IntPtr pSrcNativeVariant);

		// Token: 0x0600617E RID: 24958 RVA: 0x0014DD6E File Offset: 0x0014BF6E
		[SecurityCritical]
		public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
		{
			return (T)((object)Marshal.GetObjectForNativeVariant(pSrcNativeVariant));
		}

		// Token: 0x0600617F RID: 24959
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars);

		// Token: 0x06006180 RID: 24960 RVA: 0x0014DD7C File Offset: 0x0014BF7C
		[SecurityCritical]
		public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
		{
			object[] objectsForNativeVariants = Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
			T[] array = null;
			if (objectsForNativeVariants != null)
			{
				array = new T[objectsForNativeVariants.Length];
				Array.Copy(objectsForNativeVariants, array, objectsForNativeVariants.Length);
			}
			return array;
		}

		// Token: 0x06006181 RID: 24961
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStartComSlot(Type t);

		// Token: 0x06006182 RID: 24962
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEndComSlot(Type t);

		// Token: 0x06006183 RID: 24963
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType);

		// Token: 0x06006184 RID: 24964 RVA: 0x0014DDAC File Offset: 0x0014BFAC
		[SecurityCritical]
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is RuntimeMethodInfo))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeInterfaceMethod"), "m");
			}
			if (m.DeclaringType.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "m");
			}
			return Marshal.InternalGetComSlotForMethodInfo((IRuntimeMethodInfo)m);
		}

		// Token: 0x06006185 RID: 24965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetComSlotForMethodInfo(IRuntimeMethodInfo m);

		// Token: 0x06006186 RID: 24966 RVA: 0x0014DE3C File Offset: 0x0014C03C
		[SecurityCritical]
		public static Guid GenerateGuidForType(Type type)
		{
			Guid guid = default(Guid);
			Marshal.FCallGenerateGuidForType(ref guid, type);
			return guid;
		}

		// Token: 0x06006187 RID: 24967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallGenerateGuidForType(ref Guid result, Type type);

		// Token: 0x06006188 RID: 24968 RVA: 0x0014DE5C File Offset: 0x0014C05C
		[SecurityCritical]
		public static string GenerateProgIdForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsImport)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustNotBeComImport"), "type");
			}
			if (type.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
			}
			if (!RegistrationServices.TypeRequiresRegistrationHelper(type))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
			}
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(type);
			for (int i = 0; i < customAttributes.Count; i++)
			{
				if (customAttributes[i].Constructor.DeclaringType == typeof(ProgIdAttribute))
				{
					IList<CustomAttributeTypedArgument> constructorArguments = customAttributes[i].ConstructorArguments;
					string text = (string)constructorArguments[0].Value;
					if (text == null)
					{
						text = string.Empty;
					}
					return text;
				}
			}
			return type.FullName;
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x0014DF48 File Offset: 0x0014C148
		[SecurityCritical]
		public static object BindToMoniker(string monikerName)
		{
			object obj = null;
			IBindCtx bindCtx = null;
			Marshal.CreateBindCtx(0U, out bindCtx);
			IMoniker moniker = null;
			uint num;
			Marshal.MkParseDisplayName(bindCtx, monikerName, out num, out moniker);
			Marshal.BindMoniker(moniker, 0U, ref Marshal.IID_IUnknown, out obj);
			return obj;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x0014DF80 File Offset: 0x0014C180
		[SecurityCritical]
		public static object GetActiveObject(string progID)
		{
			object obj = null;
			Guid guid;
			try
			{
				Marshal.CLSIDFromProgIDEx(progID, out guid);
			}
			catch (Exception)
			{
				Marshal.CLSIDFromProgID(progID, out guid);
			}
			Marshal.GetActiveObject(ref guid, IntPtr.Zero, out obj);
			return obj;
		}

		// Token: 0x0600618B RID: 24971
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

		// Token: 0x0600618C RID: 24972
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

		// Token: 0x0600618D RID: 24973
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void CreateBindCtx(uint reserved, out IBindCtx ppbc);

		// Token: 0x0600618E RID: 24974
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void MkParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out uint pchEaten, out IMoniker ppmk);

		// Token: 0x0600618F RID: 24975
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void BindMoniker(IMoniker pmk, uint grfOpt, ref Guid iidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x06006190 RID: 24976
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06006191 RID: 24977
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSwitchCCW(object oldtp, object newtp);

		// Token: 0x06006192 RID: 24978
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InternalWrapIUnknownWithComObject(IntPtr i);

		// Token: 0x06006193 RID: 24979 RVA: 0x0014DFC4 File Offset: 0x0014C1C4
		[SecurityCritical]
		private static IntPtr LoadLicenseManager()
		{
			Assembly assembly = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Type type = assembly.GetType("System.ComponentModel.LicenseManager");
			if (type == null || !type.IsVisible)
			{
				return IntPtr.Zero;
			}
			return type.TypeHandle.Value;
		}

		// Token: 0x06006194 RID: 24980
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ChangeWrapperHandleStrength(object otp, bool fIsWeak);

		// Token: 0x06006195 RID: 24981
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitializeWrapperForWinRT(object o, ref IntPtr pUnk);

		// Token: 0x06006196 RID: 24982
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitializeManagedWinRTFactoryObject(object o, RuntimeType runtimeClassType);

		// Token: 0x06006197 RID: 24983
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetNativeActivationFactory(Type type);

		// Token: 0x06006198 RID: 24984
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetInspectableIids(ObjectHandleOnStack obj, ObjectHandleOnStack guids);

		// Token: 0x06006199 RID: 24985 RVA: 0x0014E010 File Offset: 0x0014C210
		[SecurityCritical]
		internal static Guid[] GetInspectableIids(object obj)
		{
			Guid[] array = null;
			__ComObject _ComObject = obj as __ComObject;
			if (_ComObject != null)
			{
				Marshal._GetInspectableIids(JitHelpers.GetObjectHandleOnStack<__ComObject>(ref _ComObject), JitHelpers.GetObjectHandleOnStack<Guid[]>(ref array));
			}
			return array;
		}

		// Token: 0x0600619A RID: 24986
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetCachedWinRTTypeByIid(ObjectHandleOnStack appDomainObj, Guid iid, out IntPtr rthHandle);

		// Token: 0x0600619B RID: 24987 RVA: 0x0014E040 File Offset: 0x0014C240
		[SecurityCritical]
		internal static Type GetCachedWinRTTypeByIid(AppDomain ad, Guid iid)
		{
			IntPtr intPtr;
			Marshal._GetCachedWinRTTypeByIid(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), iid, out intPtr);
			return Type.GetTypeFromHandleUnsafe(intPtr);
		}

		// Token: 0x0600619C RID: 24988
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetCachedWinRTTypes(ObjectHandleOnStack appDomainObj, ref int epoch, ObjectHandleOnStack winrtTypes);

		// Token: 0x0600619D RID: 24989 RVA: 0x0014E064 File Offset: 0x0014C264
		[SecurityCritical]
		internal static Type[] GetCachedWinRTTypes(AppDomain ad, ref int epoch)
		{
			IntPtr[] array = null;
			Marshal._GetCachedWinRTTypes(JitHelpers.GetObjectHandleOnStack<AppDomain>(ref ad), ref epoch, JitHelpers.GetObjectHandleOnStack<IntPtr[]>(ref array));
			Type[] array2 = new Type[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Type.GetTypeFromHandleUnsafe(array[i]);
			}
			return array2;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x0014E0AC File Offset: 0x0014C2AC
		[SecurityCritical]
		internal static Type[] GetCachedWinRTTypes(AppDomain ad)
		{
			int num = 0;
			return Marshal.GetCachedWinRTTypes(ad, ref num);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x0014E0C4 File Offset: 0x0014C2C4
		[SecurityCritical]
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (t as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "t");
			}
			Type baseType = t.BaseType;
			if (baseType == null || (baseType != typeof(Delegate) && baseType != typeof(MulticastDelegate)))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "t");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x0014E18D File Offset: 0x0014C38D
		[SecurityCritical]
		public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
		{
			return (TDelegate)((object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate)));
		}

		// Token: 0x060061A1 RID: 24993
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		// Token: 0x060061A2 RID: 24994 RVA: 0x0014E1A4 File Offset: 0x0014C3A4
		[SecurityCritical]
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x0014E1BA File Offset: 0x0014C3BA
		[SecurityCritical]
		public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
		{
			return Marshal.GetFunctionPointerForDelegate((Delegate)((object)d));
		}

		// Token: 0x060061A4 RID: 24996
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		// Token: 0x060061A5 RID: 24997 RVA: 0x0014E1CC File Offset: 0x0014C3CC
		[SecurityCritical]
		public static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.ToBSTR();
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x0014E1E2 File Offset: 0x0014C3E2
		[SecurityCritical]
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.ToAnsiStr(false);
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x0014E1F9 File Offset: 0x0014C3F9
		[SecurityCritical]
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.ToUniStr(false);
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x0014E210 File Offset: 0x0014C410
		[SecurityCritical]
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Win32Native.ZeroMemory(s, (UIntPtr)(Win32Native.SysStringLen(s) * 2U));
			Marshal.FreeBSTR(s);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x0014E22B File Offset: 0x0014C42B
		[SecurityCritical]
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Win32Native.ZeroMemory(s, (UIntPtr)((ulong)((long)Win32Native.lstrlenA(s))));
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x0014E245 File Offset: 0x0014C445
		[SecurityCritical]
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Win32Native.ZeroMemory(s, (UIntPtr)((ulong)((long)(Win32Native.lstrlenW(s) * 2))));
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x0014E261 File Offset: 0x0014C461
		[SecurityCritical]
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.ToAnsiStr(true);
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x0014E278 File Offset: 0x0014C478
		[SecurityCritical]
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.ToUniStr(true);
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x0014E28F File Offset: 0x0014C48F
		[SecurityCritical]
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Win32Native.ZeroMemory(s, (UIntPtr)((ulong)((long)Win32Native.lstrlenA(s))));
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x0014E2A9 File Offset: 0x0014C4A9
		[SecurityCritical]
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Win32Native.ZeroMemory(s, (UIntPtr)((ulong)((long)(Win32Native.lstrlenW(s) * 2))));
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x0014E2C5 File Offset: 0x0014C4C5
		// Note: this type is marked as 'beforefieldinit'.
		static Marshal()
		{
		}

		// Token: 0x04002B63 RID: 11107
		private const int LMEM_FIXED = 0;

		// Token: 0x04002B64 RID: 11108
		private const int LMEM_MOVEABLE = 2;

		// Token: 0x04002B65 RID: 11109
		private const long HIWORDMASK = -65536L;

		// Token: 0x04002B66 RID: 11110
		private static Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

		// Token: 0x04002B67 RID: 11111
		public static readonly int SystemDefaultCharSize = 2;

		// Token: 0x04002B68 RID: 11112
		public static readonly int SystemMaxDBCSCharSize = Marshal.GetSystemMaxDBCSCharSize();

		// Token: 0x04002B69 RID: 11113
		private const string s_strConvertedTypeInfoAssemblyName = "InteropDynamicTypes";

		// Token: 0x04002B6A RID: 11114
		private const string s_strConvertedTypeInfoAssemblyTitle = "Interop Dynamic Types";

		// Token: 0x04002B6B RID: 11115
		private const string s_strConvertedTypeInfoAssemblyDesc = "Type dynamically generated from ITypeInfo's";

		// Token: 0x04002B6C RID: 11116
		private const string s_strConvertedTypeInfoNameSpace = "InteropDynamicTypes";

		// Token: 0x04002B6D RID: 11117
		internal static readonly Guid ManagedNameGuid = new Guid("{0F21F359-AB84-41E8-9A78-36D110E6D2F9}");
	}
}
