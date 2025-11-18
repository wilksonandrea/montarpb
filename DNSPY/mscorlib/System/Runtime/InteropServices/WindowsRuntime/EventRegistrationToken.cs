using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E2 RID: 2530
	[__DynamicallyInvokable]
	public struct EventRegistrationToken
	{
		// Token: 0x06006476 RID: 25718 RVA: 0x0015647B File Offset: 0x0015467B
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x00156484 File Offset: 0x00154684
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x0015648C File Offset: 0x0015468C
		[__DynamicallyInvokable]
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x001564A1 File Offset: 0x001546A1
		[__DynamicallyInvokable]
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x001564BC File Offset: 0x001546BC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x001564E9 File Offset: 0x001546E9
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002CF7 RID: 11511
		internal ulong m_value;
	}
}
