using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000055 RID: 85
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00005CFC File Offset: 0x00003EFC
		internal Array()
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00005D04 File Offset: 0x00003F04
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(array);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00005D1C File Offset: 0x00003F1C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Resize<T>(ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException("newSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Array.Copy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
				array = array3;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00005D78 File Offset: 0x00003F78
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int length)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 1, &length, null);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005DF4 File Offset: 0x00003FF4
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0 || length2 < 0)
			{
				throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = length1;
			ptr[1] = length2;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 2, ptr, null);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00005E8C File Offset: 0x0000408C
		[SecuritySafeCritical]
		public unsafe static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (length1 < 0)
			{
				throw new ArgumentOutOfRangeException("length1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length2 < 0)
			{
				throw new ArgumentOutOfRangeException("length2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length3 < 0)
			{
				throw new ArgumentOutOfRangeException("length3", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = length1;
			ptr[1] = length2;
			ptr[2] = length3;
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, 3, ptr, null);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00005F50 File Offset: 0x00004150
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			int* ptr;
			if (lengths == null || lengths.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &lengths[0];
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, null);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006028 File Offset: 0x00004228
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("len", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000060A8 File Offset: 0x000042A8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RanksAndBounds"));
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NeedAtLeast1Rank"));
			}
			RuntimeType runtimeType = elementType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "elementType");
			}
			for (int i = 0; i < lengths.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths[" + i.ToString() + "]", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
			}
			int* ptr;
			if (lengths == null || lengths.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &lengths[0];
			}
			int* ptr2;
			if (lowerBounds == null || lowerBounds.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &lowerBounds[0];
			}
			return Array.InternalCreate((void*)runtimeType.TypeHandle.Value, lengths.Length, ptr, ptr2);
		}

		// Token: 0x06000295 RID: 661
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Array InternalCreate(void* elementType, int rank, int* pLengths, int* pLowerBounds);

		// Token: 0x06000296 RID: 662 RVA: 0x000061C9 File Offset: 0x000043C9
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length)
		{
			return Array.CreateInstance(elementType, length);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000061D2 File Offset: 0x000043D2
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
		{
			return Array.CreateInstance(elementType, length1, length2);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000061DC File Offset: 0x000043DC
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
		{
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000061E5 File Offset: 0x000043E5
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			return Array.CreateInstance(elementType, lengths, lowerBounds);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000061EF File Offset: 0x000043EF
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length, false);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00006224 File Offset: 0x00004424
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, false);
		}

		// Token: 0x0600029C RID: 668
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length, bool reliable);

		// Token: 0x0600029D RID: 669 RVA: 0x00006232 File Offset: 0x00004432
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length, true);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006240 File Offset: 0x00004440
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00006274 File Offset: 0x00004474
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceIndex > 2147483647L || sourceIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (destinationIndex > 2147483647L || destinationIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		// Token: 0x060002A0 RID: 672
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Clear(Array array, int index, int length);

		// Token: 0x060002A1 RID: 673 RVA: 0x00006308 File Offset: 0x00004508
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int[] array = indices)
			{
				int* ptr;
				if (indices == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006378 File Offset: 0x00004578
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000063BC File Offset: 0x000045BC
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000640C File Offset: 0x0000460C
		[SecuritySafeCritical]
		public unsafe object GetValue(int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			return TypedReference.InternalToObject((void*)(&typedReference));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00006463 File Offset: 0x00004663
		[ComVisible(false)]
		public object GetValue(long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00006494 File Offset: 0x00004694
		[ComVisible(false)]
		public object GetValue(long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000064FC File Offset: 0x000046FC
		[ComVisible(false)]
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000658C File Offset: 0x0000478C
		[ComVisible(false)]
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			return this.GetValue(array);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006610 File Offset: 0x00004810
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need1DArray"));
			}
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 1, &index);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006654 File Offset: 0x00004854
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2)
		{
			if (this.Rank != 2)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need2DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = index1;
			ptr[1] = index2;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 2, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000066A4 File Offset: 0x000048A4
		[SecuritySafeCritical]
		public unsafe void SetValue(object value, int index1, int index2, int index3)
		{
			if (this.Rank != 3)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Need3DArray"));
			}
			int* ptr = stackalloc int[(UIntPtr)12];
			*ptr = index1;
			ptr[1] = index2;
			ptr[2] = index3;
			TypedReference typedReference = default(TypedReference);
			this.InternalGetReference((void*)(&typedReference), 3, ptr);
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00006700 File Offset: 0x00004900
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe void SetValue(object value, params int[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			TypedReference typedReference = default(TypedReference);
			fixed (int[] array = indices)
			{
				int* ptr;
				if (indices == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.InternalGetReference((void*)(&typedReference), indices.Length, ptr);
			}
			Array.InternalSetValue((void*)(&typedReference), value);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000676E File Offset: 0x0000496E
		[ComVisible(false)]
		public void SetValue(object value, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000067A0 File Offset: 0x000049A0
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00006808 File Offset: 0x00004A08
		[ComVisible(false)]
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.SetValue(value, (int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000689C File Offset: 0x00004A9C
		[ComVisible(false)]
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankIndices"));
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
				}
				array[i] = (int)num;
			}
			this.SetValue(value, array);
		}

		// Token: 0x060002B1 RID: 689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void InternalGetReference(void* elemRef, int rank, int* pIndices);

		// Token: 0x060002B2 RID: 690
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalSetValue(void* target, object value);

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002B3 RID: 691
		[__DynamicallyInvokable]
		public extern int Length
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00006921 File Offset: 0x00004B21
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002B5 RID: 693
		[ComVisible(false)]
		public extern long LongLength
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002B6 RID: 694
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLength(int dimension);

		// Token: 0x060002B7 RID: 695 RVA: 0x0000692A File Offset: 0x00004B2A
		[ComVisible(false)]
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002B8 RID: 696
		[__DynamicallyInvokable]
		public extern int Rank
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002B9 RID: 697
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUpperBound(int dimension);

		// Token: 0x060002BA RID: 698
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLowerBound(int dimension);

		// Token: 0x060002BB RID: 699
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetDataPtrOffsetInternal();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006934 File Offset: 0x00004B34
		[__DynamicallyInvokable]
		int ICollection.Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000693C File Offset: 0x00004B3C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000693F File Offset: 0x00004B3F
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00006942 File Offset: 0x00004B42
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00006945 File Offset: 0x00004B45
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000037 RID: 55
		[__DynamicallyInvokable]
		object IList.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetValue(index);
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetValue(value, index);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000695B File Offset: 0x00004B5B
		[__DynamicallyInvokable]
		int IList.Add(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000696C File Offset: 0x00004B6C
		[__DynamicallyInvokable]
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= this.GetLowerBound(0);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006981 File Offset: 0x00004B81
		[__DynamicallyInvokable]
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006996 File Offset: 0x00004B96
		[__DynamicallyInvokable]
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000699F File Offset: 0x00004B9F
		[__DynamicallyInvokable]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000069B0 File Offset: 0x00004BB0
		[__DynamicallyInvokable]
		void IList.Remove(object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000069C1 File Offset: 0x00004BC1
		[__DynamicallyInvokable]
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000069D2 File Offset: 0x00004BD2
		[__DynamicallyInvokable]
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000069DC File Offset: 0x00004BDC
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength"), "other");
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006A50 File Offset: 0x00004C50
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00006AB0 File Offset: 0x00004CB0
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00006ABC File Offset: 0x00004CBC
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = ((this.Length >= 8) ? (this.Length - 8) : 0); i < this.Length; i++)
			{
				num = Array.CombineHashCodes(num, comparer.GetHashCode(this.GetValue(i)));
			}
			return num;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006B14 File Offset: 0x00004D14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, null);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00006B46 File Offset: 0x00004D46
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006B54 File Offset: 0x00004D54
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.BinarySearch(array, lowerBound, array.Length, value, comparer);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006B88 File Offset: 0x00004D88
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < lowerBound) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - lowerBound) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			if (comparer == Comparer.Default)
			{
				int num;
				bool flag = Array.TrySZBinarySearch(array, index, length, value, out num);
				if (flag)
				{
					return num;
				}
			}
			int i = index;
			int num2 = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num2)
				{
					int median = Array.GetMedian(i, num2);
					int num3;
					try
					{
						num3 = comparer.Compare(array2[median], value);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
					}
					if (num3 == 0)
					{
						return median;
					}
					if (num3 < 0)
					{
						i = median + 1;
					}
					else
					{
						num2 = median - 1;
					}
				}
			}
			else
			{
				while (i <= num2)
				{
					int median2 = Array.GetMedian(i, num2);
					int num4;
					try
					{
						num4 = comparer.Compare(array.GetValue(median2), value);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					if (num4 == 0)
					{
						return median2;
					}
					if (num4 < 0)
					{
						i = median2 + 1;
					}
					else
					{
						num2 = median2 - 1;
					}
				}
			}
			return ~i;
		}

		// Token: 0x060002D3 RID: 723
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZBinarySearch(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002D4 RID: 724 RVA: 0x00006D00 File Offset: 0x00004F00
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006D1C File Offset: 0x00004F1C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006D38 File Offset: 0x00004F38
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006D44 File Offset: 0x00004F44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00006E05 File Offset: 0x00005005
		[__DynamicallyInvokable]
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006E38 File Offset: 0x00005038
		[ComVisible(false)]
		public void CopyTo(Array array, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported"));
			}
			this.CopyTo(array, (int)index);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00006E6A File Offset: 0x0000506A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static T[] Empty<T>()
		{
			return EmptyArray<T>.Value;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006E71 File Offset: 0x00005071
		[__DynamicallyInvokable]
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006E80 File Offset: 0x00005080
		[__DynamicallyInvokable]
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006ED8 File Offset: 0x000050D8
		[__DynamicallyInvokable]
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> list = new List<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					list.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006F37 File Offset: 0x00005137
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006F52 File Offset: 0x00005152
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00006F70 File Offset: 0x00005170
		[__DynamicallyInvokable]
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006FFC File Offset: 0x000051FC
		[__DynamicallyInvokable]
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007055 File Offset: 0x00005255
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007074 File Offset: 0x00005274
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007090 File Offset: 0x00005290
		[__DynamicallyInvokable]
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007138 File Offset: 0x00005338
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000717C File Offset: 0x0000537C
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			int lowerBound = this.GetLowerBound(0);
			if (this.Rank == 1 && lowerBound == 0)
			{
				return new Array.SZArrayEnumerator(this);
			}
			return new Array.ArrayEnumerator(this, lowerBound, this.Length);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000071B4 File Offset: 0x000053B4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, lowerBound, array.Length);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000071E8 File Offset: 0x000053E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007220 File Offset: 0x00005420
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			int num;
			bool flag = Array.TrySZIndexOf(array, startIndex, count, value, out num);
			if (flag)
			{
				return num;
			}
			object[] array2 = array as object[];
			int num2 = startIndex + count;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num2; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num2; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k < num2; k++)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007344 File Offset: 0x00005544
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000735F File Offset: 0x0000555F
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000737C File Offset: 0x0000557C
		[__DynamicallyInvokable]
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || count > array.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x060002EE RID: 750
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002EF RID: 751 RVA: 0x000073E8 File Offset: 0x000055E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, array.Length - 1 + lowerBound, array.Length);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007424 File Offset: 0x00005624
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1 - lowerBound);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00007454 File Offset: 0x00005654
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (array.Length == 0)
			{
				return lowerBound - 1;
			}
			if (startIndex < lowerBound || startIndex >= array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > startIndex - lowerBound + 1)
			{
				throw new ArgumentOutOfRangeException("endIndex", Environment.GetResourceString("ArgumentOutOfRange_EndIndexStartIndex"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			int num;
			bool flag = Array.TrySZLastIndexOf(array, startIndex, count, value, out num);
			if (flag)
			{
				return num;
			}
			object[] array2 = array as object[];
			int num2 = startIndex - count + 1;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num2; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num2; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k >= num2; k--)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007596 File Offset: 0x00005796
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000075B5 File Offset: 0x000057B5
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000075D8 File Offset: 0x000057D8
		[__DynamicallyInvokable]
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return -1;
			}
			else
			{
				if (startIndex < 0 || startIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
			}
		}

		// Token: 0x060002F5 RID: 757
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZLastIndexOf(Array sourceArray, int sourceIndex, int count, object value, out int retVal);

		// Token: 0x060002F6 RID: 758 RVA: 0x0000767B File Offset: 0x0000587B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000076A0 File Offset: 0x000058A0
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - (index - array.GetLowerBound(0)) < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			bool flag = Array.TrySZReverse(array, index, length);
			if (flag)
			{
				return;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < num)
				{
					object obj = array2[i];
					array2[i] = array2[num];
					array2[num] = obj;
					i++;
					num--;
				}
				return;
			}
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x060002F8 RID: 760
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZReverse(Array array, int index, int count);

		// Token: 0x060002F9 RID: 761 RVA: 0x0000778F File Offset: 0x0000598F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000077B4 File Offset: 0x000059B4
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000077D9 File Offset: 0x000059D9
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000077E5 File Offset: 0x000059E5
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000077F1 File Offset: 0x000059F1
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00007816 File Offset: 0x00005A16
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000783B File Offset: 0x00005A3B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007848 File Offset: 0x00005A48
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (items != null && keys.GetLowerBound(0) != items.GetLowerBound(0))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_LowerBoundsMustMatch"));
			}
			if (index < keys.GetLowerBound(0) || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - (index - keys.GetLowerBound(0)) < length || (items != null && index - items.GetLowerBound(0) > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if (comparer == Comparer.Default || comparer == null)
				{
					bool flag = Array.TrySZSort(keys, items, index, index + length - 1);
					if (flag)
					{
						return;
					}
				}
				object[] array = keys as object[];
				object[] array2 = null;
				if (array != null)
				{
					array2 = items as object[];
				}
				if (array != null && (items == null || array2 != null))
				{
					Array.SorterObjectArray sorterObjectArray = new Array.SorterObjectArray(array, array2, comparer);
					sorterObjectArray.Sort(index, length);
					return;
				}
				Array.SorterGenericArray sorterGenericArray = new Array.SorterGenericArray(keys, items, comparer);
				sorterGenericArray.Sort(index, length);
			}
		}

		// Token: 0x06000301 RID: 769
		[SecurityCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySZSort(Array keys, Array items, int left, int right);

		// Token: 0x06000302 RID: 770 RVA: 0x0000797A File Offset: 0x00005B7A
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000799B File Offset: 0x00005B9B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000079B7 File Offset: 0x00005BB7
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000079C2 File Offset: 0x00005BC2
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000079CE File Offset: 0x00005BCE
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000079E9 File Offset: 0x00005BE9
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007A08 File Offset: 0x00005C08
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<T>.Default) && Array.TrySZSort(array, null, index, index + length - 1))
				{
					return;
				}
				ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007A90 File Offset: 0x00005C90
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (length > 1)
			{
				if ((comparer == null || comparer == Comparer<TKey>.Default) && Array.TrySZSort(keys, items, index, index + length - 1))
				{
					return;
				}
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007B38 File Offset: 0x00005D38
		[__DynamicallyInvokable]
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
			Array.Sort<T>(array, comparer);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00007B70 File Offset: 0x00005D70
		[__DynamicallyInvokable]
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600030C RID: 780
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Initialize();

		// Token: 0x040001EE RID: 494
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x040001EF RID: 495
		internal const int MaxByteArrayLength = 2147483591;

		// Token: 0x02000AC2 RID: 2754
		internal sealed class FunctorComparer<T> : IComparer<T>
		{
			// Token: 0x060069A1 RID: 27041 RVA: 0x0016B734 File Offset: 0x00169934
			public FunctorComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x060069A2 RID: 27042 RVA: 0x0016B743 File Offset: 0x00169943
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x040030CC RID: 12492
			private Comparison<T> comparison;
		}

		// Token: 0x02000AC3 RID: 2755
		private struct SorterObjectArray
		{
			// Token: 0x060069A3 RID: 27043 RVA: 0x0016B752 File Offset: 0x00169952
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x060069A4 RID: 27044 RVA: 0x0016B774 File Offset: 0x00169974
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x060069A5 RID: 27045 RVA: 0x0016B7F0 File Offset: 0x001699F0
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x060069A6 RID: 27046 RVA: 0x0016B849 File Offset: 0x00169A49
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x060069A7 RID: 27047 RVA: 0x0016B86C File Offset: 0x00169A6C
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					object obj = this.keys[median];
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys[num], obj) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(obj, this.keys[num2]) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object obj2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = obj2;
							if (this.items != null)
							{
								object obj3 = this.items[num];
								this.items[num] = this.items[num2];
								this.items[num2] = obj3;
							}
						}
						num++;
						num2--;
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x060069A8 RID: 27048 RVA: 0x0016BA50 File Offset: 0x00169C50
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
				}
			}

			// Token: 0x060069A9 RID: 27049 RVA: 0x0016BABC File Offset: 0x00169CBC
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x060069AA RID: 27050 RVA: 0x0016BB40 File Offset: 0x00169D40
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x060069AB RID: 27051 RVA: 0x0016BBDC File Offset: 0x00169DDC
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x060069AC RID: 27052 RVA: 0x0016BC2C File Offset: 0x00169E2C
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object obj2 = ((this.items != null) ? this.items[lo + i - 1] : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x060069AD RID: 27053 RVA: 0x0016BD14 File Offset: 0x00169F14
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object obj2 = ((this.items != null) ? this.items[i + 1] : null);
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x040030CD RID: 12493
			private object[] keys;

			// Token: 0x040030CE RID: 12494
			private object[] items;

			// Token: 0x040030CF RID: 12495
			private IComparer comparer;
		}

		// Token: 0x02000AC4 RID: 2756
		private struct SorterGenericArray
		{
			// Token: 0x060069AE RID: 27054 RVA: 0x0016BDC1 File Offset: 0x00169FC1
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x060069AF RID: 27055 RVA: 0x0016BDE4 File Offset: 0x00169FE4
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x060069B0 RID: 27056 RVA: 0x0016BE8C File Offset: 0x0016A08C
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x060069B1 RID: 27057 RVA: 0x0016BF05 File Offset: 0x0016A105
			internal void Sort(int left, int length)
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					this.IntrospectiveSort(left, length);
					return;
				}
				this.DepthLimitedQuickSort(left, length + left - 1, 32);
			}

			// Token: 0x060069B2 RID: 27058 RVA: 0x0016BF28 File Offset: 0x0016A128
			private void DepthLimitedQuickSort(int left, int right, int depthLimit)
			{
				do
				{
					if (depthLimit == 0)
					{
						try
						{
							this.Heapsort(left, right);
							break;
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
						}
					}
					int num = left;
					int num2 = right;
					int median = Array.GetMedian(num, num2);
					try
					{
						this.SwapIfGreaterWithItems(num, median);
						this.SwapIfGreaterWithItems(num, num2);
						this.SwapIfGreaterWithItems(median, num2);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex2);
					}
					object value = this.keys.GetValue(median);
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys.GetValue(num), value) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(value, this.keys.GetValue(num2)) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[] { this.comparer }));
						}
						catch (Exception ex3)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex3);
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object value2 = this.keys.GetValue(num);
							this.keys.SetValue(this.keys.GetValue(num2), num);
							this.keys.SetValue(value2, num2);
							if (this.items != null)
							{
								object value3 = this.items.GetValue(num);
								this.items.SetValue(this.items.GetValue(num2), num);
								this.items.SetValue(value3, num2);
							}
						}
						if (num != 2147483647)
						{
							num++;
						}
						if (num2 != -2147483648)
						{
							num2--;
						}
					}
					while (num <= num2);
					depthLimit--;
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.DepthLimitedQuickSort(left, num2, depthLimit);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.DepthLimitedQuickSort(num, right, depthLimit);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x060069B3 RID: 27059 RVA: 0x0016C14C File Offset: 0x0016A34C
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), ex);
				}
			}

			// Token: 0x060069B4 RID: 27060 RVA: 0x0016C1BC File Offset: 0x0016A3BC
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x060069B5 RID: 27061 RVA: 0x0016C240 File Offset: 0x0016A440
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x060069B6 RID: 27062 RVA: 0x0016C2E8 File Offset: 0x0016A4E8
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x060069B7 RID: 27063 RVA: 0x0016C338 File Offset: 0x0016A538
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				object obj = ((this.items != null) ? this.items.GetValue(lo + i - 1) : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(obj, lo + i - 1);
				}
			}

			// Token: 0x060069B8 RID: 27064 RVA: 0x0016C44C File Offset: 0x0016A64C
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					object obj = ((this.items != null) ? this.items.GetValue(i + 1) : null);
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(obj, num + 1);
					}
				}
			}

			// Token: 0x040030D0 RID: 12496
			private Array keys;

			// Token: 0x040030D1 RID: 12497
			private Array items;

			// Token: 0x040030D2 RID: 12498
			private IComparer comparer;
		}

		// Token: 0x02000AC5 RID: 2757
		[Serializable]
		private sealed class SZArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x060069B9 RID: 27065 RVA: 0x0016C51D File Offset: 0x0016A71D
			internal SZArrayEnumerator(Array array)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = array.Length;
			}

			// Token: 0x060069BA RID: 27066 RVA: 0x0016C53F File Offset: 0x0016A73F
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060069BB RID: 27067 RVA: 0x0016C547 File Offset: 0x0016A747
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x170011E7 RID: 4583
			// (get) Token: 0x060069BC RID: 27068 RVA: 0x0016C578 File Offset: 0x0016A778
			public object Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array.GetValue(this._index);
				}
			}

			// Token: 0x060069BD RID: 27069 RVA: 0x0016C5CD File Offset: 0x0016A7CD
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x040030D3 RID: 12499
			private Array _array;

			// Token: 0x040030D4 RID: 12500
			private int _index;

			// Token: 0x040030D5 RID: 12501
			private int _endIndex;
		}

		// Token: 0x02000AC6 RID: 2758
		[Serializable]
		private sealed class ArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x060069BE RID: 27070 RVA: 0x0016C5D8 File Offset: 0x0016A7D8
			internal ArrayEnumerator(Array array, int index, int count)
			{
				this.array = array;
				this.index = index - 1;
				this.startIndex = index;
				this.endIndex = index + count;
				this._indices = new int[array.Rank];
				int num = 1;
				for (int i = 0; i < array.Rank; i++)
				{
					this._indices[i] = array.GetLowerBound(i);
					num *= array.GetLength(i);
				}
				this._indices[this._indices.Length - 1]--;
				this._complete = num == 0;
			}

			// Token: 0x060069BF RID: 27071 RVA: 0x0016C66C File Offset: 0x0016A86C
			private void IncArray()
			{
				int rank = this.array.Rank;
				this._indices[rank - 1]++;
				for (int i = rank - 1; i >= 0; i--)
				{
					if (this._indices[i] > this.array.GetUpperBound(i))
					{
						if (i == 0)
						{
							this._complete = true;
							return;
						}
						for (int j = i; j < rank; j++)
						{
							this._indices[j] = this.array.GetLowerBound(j);
						}
						this._indices[i - 1]++;
					}
				}
			}

			// Token: 0x060069C0 RID: 27072 RVA: 0x0016C6FA File Offset: 0x0016A8FA
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060069C1 RID: 27073 RVA: 0x0016C702 File Offset: 0x0016A902
			public bool MoveNext()
			{
				if (this._complete)
				{
					this.index = this.endIndex;
					return false;
				}
				this.index++;
				this.IncArray();
				return !this._complete;
			}

			// Token: 0x170011E8 RID: 4584
			// (get) Token: 0x060069C2 RID: 27074 RVA: 0x0016C738 File Offset: 0x0016A938
			public object Current
			{
				get
				{
					if (this.index < this.startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._complete)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.array.GetValue(this._indices);
				}
			}

			// Token: 0x060069C3 RID: 27075 RVA: 0x0016C78C File Offset: 0x0016A98C
			public void Reset()
			{
				this.index = this.startIndex - 1;
				int num = 1;
				for (int i = 0; i < this.array.Rank; i++)
				{
					this._indices[i] = this.array.GetLowerBound(i);
					num *= this.array.GetLength(i);
				}
				this._complete = num == 0;
				this._indices[this._indices.Length - 1]--;
			}

			// Token: 0x040030D6 RID: 12502
			private Array array;

			// Token: 0x040030D7 RID: 12503
			private int index;

			// Token: 0x040030D8 RID: 12504
			private int endIndex;

			// Token: 0x040030D9 RID: 12505
			private int startIndex;

			// Token: 0x040030DA RID: 12506
			private int[] _indices;

			// Token: 0x040030DB RID: 12507
			private bool _complete;
		}
	}
}
