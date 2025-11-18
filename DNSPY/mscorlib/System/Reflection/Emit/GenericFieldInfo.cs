using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000638 RID: 1592
	internal sealed class GenericFieldInfo
	{
		// Token: 0x06004A35 RID: 18997 RVA: 0x0010C4DB File Offset: 0x0010A6DB
		internal GenericFieldInfo(RuntimeFieldHandle fieldHandle, RuntimeTypeHandle context)
		{
			this.m_fieldHandle = fieldHandle;
			this.m_context = context;
		}

		// Token: 0x04001EA0 RID: 7840
		internal RuntimeFieldHandle m_fieldHandle;

		// Token: 0x04001EA1 RID: 7841
		internal RuntimeTypeHandle m_context;
	}
}
