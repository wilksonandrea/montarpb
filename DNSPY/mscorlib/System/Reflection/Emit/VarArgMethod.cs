using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000639 RID: 1593
	internal sealed class VarArgMethod
	{
		// Token: 0x06004A36 RID: 18998 RVA: 0x0010C4F1 File Offset: 0x0010A6F1
		internal VarArgMethod(DynamicMethod dm, SignatureHelper signature)
		{
			this.m_dynamicMethod = dm;
			this.m_signature = signature;
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0010C507 File Offset: 0x0010A707
		internal VarArgMethod(RuntimeMethodInfo method, SignatureHelper signature)
		{
			this.m_method = method;
			this.m_signature = signature;
		}

		// Token: 0x04001EA2 RID: 7842
		internal RuntimeMethodInfo m_method;

		// Token: 0x04001EA3 RID: 7843
		internal DynamicMethod m_dynamicMethod;

		// Token: 0x04001EA4 RID: 7844
		internal SignatureHelper m_signature;
	}
}
