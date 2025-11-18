using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200073A RID: 1850
	[Serializable]
	internal class MemberHolder
	{
		// Token: 0x060051CD RID: 20941 RVA: 0x0011FB55 File Offset: 0x0011DD55
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this.memberType = type;
			this.context = ctx;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0011FB6B File Offset: 0x0011DD6B
		public override int GetHashCode()
		{
			return this.memberType.GetHashCode();
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0011FB78 File Offset: 0x0011DD78
		public override bool Equals(object obj)
		{
			if (!(obj is MemberHolder))
			{
				return false;
			}
			MemberHolder memberHolder = (MemberHolder)obj;
			return memberHolder.memberType == this.memberType && memberHolder.context.State == this.context.State;
		}

		// Token: 0x04002448 RID: 9288
		internal MemberInfo[] members;

		// Token: 0x04002449 RID: 9289
		internal Type memberType;

		// Token: 0x0400244A RID: 9290
		internal StreamingContext context;
	}
}
