using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x0200061B RID: 1563
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x06004853 RID: 18515 RVA: 0x00106A69 File Offset: 0x00104C69
		private Pointer()
		{
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x00106A74 File Offset: 0x00104C74
		[SecurityCritical]
		private Pointer(SerializationInfo info, StreamingContext context)
		{
			this._ptr = ((IntPtr)info.GetValue("_ptr", typeof(IntPtr))).ToPointer();
			this._ptrType = (RuntimeType)info.GetValue("_ptrType", typeof(RuntimeType));
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x00106AD0 File Offset: 0x00104CD0
		[SecurityCritical]
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			return new Pointer
			{
				_ptr = ptr,
				_ptrType = runtimeType
			};
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x00106B48 File Offset: 0x00104D48
		[SecurityCritical]
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00106B72 File Offset: 0x00104D72
		internal RuntimeType GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00106B7A File Offset: 0x00104D7A
		[SecurityCritical]
		internal object GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00106B8C File Offset: 0x00104D8C
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_ptr", new IntPtr(this._ptr));
			info.AddValue("_ptrType", this._ptrType);
		}

		// Token: 0x04001E08 RID: 7688
		[SecurityCritical]
		private unsafe void* _ptr;

		// Token: 0x04001E09 RID: 7689
		private RuntimeType _ptrType;
	}
}
