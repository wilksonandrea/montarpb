using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200059F RID: 1439
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdHiddenLengthArrayMarshaler
	{
		// Token: 0x06004301 RID: 17153
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, IntPtr cbElementSize, ushort vt);

		// Token: 0x06004302 RID: 17154
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x06004303 RID: 17155
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x06004304 RID: 17156 RVA: 0x000F96A8 File Offset: 0x000F78A8
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToNative(ref managedArray[i], out ptr[i]);
				}
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000F96E8 File Offset: 0x000F78E8
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToNative(managedArray[i], ptr + i);
				}
			}
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000F9728 File Offset: 0x000F7928
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = HResultExceptionMarshaler.ConvertToNative(managedArray[i]);
				}
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000F9760 File Offset: 0x000F7960
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = NullableMarshaler.ConvertToNative<T>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000F97A4 File Offset: 0x000F79A4
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = KeyValuePairMarshaler.ConvertToNative<K, V>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x06004309 RID: 17161
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int elementCount);

		// Token: 0x0600430A RID: 17162
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x0600430B RID: 17163 RVA: 0x000F97E8 File Offset: 0x000F79E8
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToManaged(out managedArray[i], ref ptr[i]);
				}
			}
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000F9828 File Offset: 0x000F7A28
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToManaged(ptr + i, ref managedArray[i]);
				}
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000F986C File Offset: 0x000F7A6C
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = HResultExceptionMarshaler.ConvertToManaged(ptr[i]);
				}
			}
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000F98A4 File Offset: 0x000F7AA4
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = NullableMarshaler.ConvertToManaged<T>(ptr[i]);
				}
			}
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000F98E8 File Offset: 0x000F7AE8
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)(void*)pNativeHome;
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = KeyValuePairMarshaler.ConvertToManaged<K, V>(ptr[i]);
				}
			}
		}

		// Token: 0x06004310 RID: 17168
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNativeContents(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);

		// Token: 0x06004311 RID: 17169 RVA: 0x000F992C File Offset: 0x000F7B2C
		[SecurityCritical]
		internal unsafe static void ClearNativeContents_Type(IntPtr pNativeHome, int cElements)
		{
			TypeNameNative* ptr = *(IntPtr*)(void*)pNativeHome;
			if (ptr != null)
			{
				for (int i = 0; i < cElements; i++)
				{
					SystemTypeMarshaler.ClearNative(ptr);
					ptr++;
				}
			}
		}
	}
}
