using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BB RID: 1979
	[Serializable]
	internal class DynamicTypeInfo : TypeInfo
	{
		// Token: 0x060055A0 RID: 21920 RVA: 0x0012FE9F File Offset: 0x0012E09F
		[SecurityCritical]
		internal DynamicTypeInfo(RuntimeType typeOfObj)
			: base(typeOfObj)
		{
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x0012FEA8 File Offset: 0x0012E0A8
		[SecurityCritical]
		public override bool CanCastTo(Type castType, object o)
		{
			return ((MarshalByRefObject)o).IsInstanceOfType(castType);
		}
	}
}
