using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000910 RID: 2320
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct ArrayWithOffset
	{
		// Token: 0x06005FEB RID: 24555 RVA: 0x0014B384 File Offset: 0x00149584
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0014B3A7 File Offset: 0x001495A7
		[__DynamicallyInvokable]
		public object GetArray()
		{
			return this.m_array;
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x0014B3AF File Offset: 0x001495AF
		[__DynamicallyInvokable]
		public int GetOffset()
		{
			return this.m_offset;
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0014B3B7 File Offset: 0x001495B7
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x0014B3C6 File Offset: 0x001495C6
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x0014B3DE File Offset: 0x001495DE
		[__DynamicallyInvokable]
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x0014B40C File Offset: 0x0014960C
		[__DynamicallyInvokable]
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0014B416 File Offset: 0x00149616
		[__DynamicallyInvokable]
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x06005FF3 RID: 24563
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CalculateCount();

		// Token: 0x04002A62 RID: 10850
		private object m_array;

		// Token: 0x04002A63 RID: 10851
		private int m_offset;

		// Token: 0x04002A64 RID: 10852
		private int m_count;
	}
}
