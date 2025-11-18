using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000958 RID: 2392
	[SecurityCritical]
	[__DynamicallyInvokable]
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060061C9 RID: 25033 RVA: 0x0014E4D5 File Offset: 0x0014C6D5
		[__DynamicallyInvokable]
		protected SafeBuffer(bool ownsHandle)
			: base(ownsHandle)
		{
			this._numBytes = SafeBuffer.Uninitialized;
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0014E4EC File Offset: 0x0014C6EC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize(ulong numBytes)
		{
			if (numBytes < 0UL)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numBytes > (ulong)(-1))
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if (numBytes >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)numBytes;
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x0014E564 File Offset: 0x0014C764
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			if (numElements < 0U)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (sizeOfEachElement < 0U)
			{
				throw new ArgumentOutOfRangeException("sizeOfEachElement", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numElements * sizeOfEachElement > 4294967295U)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if ((ulong)(numElements * sizeOfEachElement) >= (ulong)SafeBuffer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)(checked(numElements * sizeOfEachElement));
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x0014E5F9 File Offset: 0x0014C7F9
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, Marshal.AlignedSizeOf<T>());
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0014E608 File Offset: 0x0014C808
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = false;
				base.DangerousAddRef(ref flag);
				pointer = (void*)this.handle;
			}
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x0014E660 File Offset: 0x0014C860
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void ReleasePointer()
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x0014E680 File Offset: 0x0014C880
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe T Read<T>(ulong byteOffset) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			T t;
			try
			{
				base.DangerousAddRef(ref flag);
				SafeBuffer.GenericPtrToStructure<T>(ptr, out t, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return t;
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x0014E704 File Offset: 0x0014C904
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			uint num2 = Marshal.AlignedSizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			bool flag;
			checked
			{
				this.SpaceCheck(ptr, unchecked((ulong)num2) * (ulong)(unchecked((long)count)));
				flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
			}
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafeBuffer.GenericPtrToStructure<T>(ptr + (ulong)num2 * (ulong)((long)i), out array[i + index], num);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x0014E818 File Offset: 0x0014CA18
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafeBuffer.GenericStructureToPtr<T>(ref value, ptr, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x0014E89C File Offset: 0x0014CA9C
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			uint num = Marshal.SizeOfType(typeof(T));
			uint num2 = Marshal.AlignedSizeOf<T>();
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			bool flag;
			checked
			{
				this.SpaceCheck(ptr, unchecked((ulong)num2) * (ulong)(unchecked((long)count)));
				flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
			}
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafeBuffer.GenericStructureToPtr<T>(ref array[i + index], ptr + (ulong)num2 * (ulong)((long)i), num);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x0014E9B0 File Offset: 0x0014CBB0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public ulong ByteLength
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (this._numBytes == SafeBuffer.Uninitialized)
				{
					throw SafeBuffer.NotInitialized();
				}
				return (ulong)this._numBytes;
			}
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x0014E9D5 File Offset: 0x0014CBD5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
		{
			if ((ulong)this._numBytes < sizeInBytes)
			{
				SafeBuffer.NotEnoughRoom();
			}
			if ((long)((byte*)ptr - (byte*)(void*)this.handle) > (long)((ulong)this._numBytes - sizeInBytes))
			{
				SafeBuffer.NotEnoughRoom();
			}
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x0014EA0E File Offset: 0x0014CC0E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void NotEnoughRoom()
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x0014EA1F File Offset: 0x0014CC1F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustCallInitialize"));
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0014EA30 File Offset: 0x0014CC30
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void GenericPtrToStructure<T>(byte* ptr, out T structure, uint sizeofT) where T : struct
		{
			structure = default(T);
			SafeBuffer.PtrToStructureNative(ptr, __makeref(structure), sizeofT);
		}

		// Token: 0x060061D8 RID: 25048
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void PtrToStructureNative(byte* ptr, TypedReference structure, uint sizeofT);

		// Token: 0x060061D9 RID: 25049 RVA: 0x0014EA46 File Offset: 0x0014CC46
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void GenericStructureToPtr<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct
		{
			SafeBuffer.StructureToPtrNative(__makeref(structure), ptr, sizeofT);
		}

		// Token: 0x060061DA RID: 25050
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void StructureToPtrNative(TypedReference structure, byte* ptr, uint sizeofT);

		// Token: 0x060061DB RID: 25051 RVA: 0x0014EA55 File Offset: 0x0014CC55
		// Note: this type is marked as 'beforefieldinit'.
		static SafeBuffer()
		{
		}

		// Token: 0x04002B85 RID: 11141
		private static readonly UIntPtr Uninitialized = ((UIntPtr.Size == 4) ? ((UIntPtr)uint.MaxValue) : ((UIntPtr)ulong.MaxValue));

		// Token: 0x04002B86 RID: 11142
		private UIntPtr _numBytes;
	}
}
