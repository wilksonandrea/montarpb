using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000754 RID: 1876
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x060052D1 RID: 21201 RVA: 0x00122D97 File Offset: 0x00120F97
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00122DA6 File Offset: 0x00120FA6
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x040024BC RID: 9404
		private string m_typeName;
	}
}
