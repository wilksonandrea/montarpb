using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000615 RID: 1557
	[ComVisible(true)]
	public class MethodBody
	{
		// Token: 0x0600480B RID: 18443 RVA: 0x00105E6C File Offset: 0x0010406C
		protected MethodBody()
		{
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x00105E74 File Offset: 0x00104074
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.m_localSignatureMetadataToken;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x00105E7C File Offset: 0x0010407C
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.m_localVariables);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x00105E89 File Offset: 0x00104089
		public virtual int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x00105E91 File Offset: 0x00104091
		public virtual bool InitLocals
		{
			get
			{
				return this.m_initLocals;
			}
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x00105E99 File Offset: 0x00104099
		public virtual byte[] GetILAsByteArray()
		{
			return this.m_IL;
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x00105EA1 File Offset: 0x001040A1
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.m_exceptionHandlingClauses);
			}
		}

		// Token: 0x04001DDE RID: 7646
		private byte[] m_IL;

		// Token: 0x04001DDF RID: 7647
		private ExceptionHandlingClause[] m_exceptionHandlingClauses;

		// Token: 0x04001DE0 RID: 7648
		private LocalVariableInfo[] m_localVariables;

		// Token: 0x04001DE1 RID: 7649
		internal MethodBase m_methodBase;

		// Token: 0x04001DE2 RID: 7650
		private int m_localSignatureMetadataToken;

		// Token: 0x04001DE3 RID: 7651
		private int m_maxStackSize;

		// Token: 0x04001DE4 RID: 7652
		private bool m_initLocals;
	}
}
