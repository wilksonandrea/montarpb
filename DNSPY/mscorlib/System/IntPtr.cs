using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	// Token: 0x020000FD RID: 253
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct IntPtr : ISerializable
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x0002F976 File Offset: 0x0002DB76
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsNull()
		{
			return this.m_value == null;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002F982 File Offset: 0x0002DB82
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002F98C File Offset: 0x0002DB8C
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public IntPtr(long value)
		{
			this.m_value = checked((int)value);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002F997 File Offset: 0x0002DB97
		[SecurityCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		[SecurityCritical]
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			if (IntPtr.Size == 4 && (@int > 2147483647L || @int < -2147483648L))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_InvalidPtrValue"));
			}
			this.m_value = @int;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0002F9EB File Offset: 0x0002DBEB
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", (long)this.m_value);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0002FA0E File Offset: 0x0002DC0E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is IntPtr && this.m_value == ((IntPtr)obj).m_value;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002FA2D File Offset: 0x0002DC2D
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002FA37 File Offset: 0x0002DC37
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public int ToInt32()
		{
			return this.m_value;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002FA40 File Offset: 0x0002DC40
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public long ToInt64()
		{
			return (long)this.m_value;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.m_value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002FA70 File Offset: 0x0002DC70
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return this.m_value.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002FA92 File Offset: 0x0002DC92
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002FA9A File Offset: 0x0002DC9A
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0002FAA2 File Offset: 0x0002DCA2
		[SecurityCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002FAAA File Offset: 0x0002DCAA
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002FAB3 File Offset: 0x0002DCB3
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator int(IntPtr value)
		{
			return value.m_value;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002FABD File Offset: 0x0002DCBD
		[SecuritySafeCritical]
		[NonVersionable]
		public static explicit operator long(IntPtr value)
		{
			return (long)value.m_value;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002FADA File Offset: 0x0002DCDA
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002FAEF File Offset: 0x0002DCEF
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return pointer + offset;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0002FAF8 File Offset: 0x0002DCF8
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr operator +(IntPtr pointer, int offset)
		{
			return new IntPtr(pointer.ToInt32() + offset);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0002FB08 File Offset: 0x0002DD08
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr Subtract(IntPtr pointer, int offset)
		{
			return pointer - offset;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0002FB11 File Offset: 0x0002DD11
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[NonVersionable]
		public static IntPtr operator -(IntPtr pointer, int offset)
		{
			return new IntPtr(pointer.ToInt32() - offset);
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002FB21 File Offset: 0x0002DD21
		[__DynamicallyInvokable]
		public static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return 4;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002FB24 File Offset: 0x0002DD24
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		// Token: 0x040005A8 RID: 1448
		[SecurityCritical]
		private unsafe void* m_value;

		// Token: 0x040005A9 RID: 1449
		public static readonly IntPtr Zero;
	}
}
