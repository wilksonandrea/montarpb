using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000740 RID: 1856
	[ComVisible(true)]
	[Serializable]
	public abstract class SerializationBinder
	{
		// Token: 0x060051D7 RID: 20951 RVA: 0x0011FC13 File Offset: 0x0011DE13
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		// Token: 0x060051D8 RID: 20952
		public abstract Type BindToType(string assemblyName, string typeName);

		// Token: 0x060051D9 RID: 20953 RVA: 0x0011FC1B File Offset: 0x0011DE1B
		protected SerializationBinder()
		{
		}
	}
}
