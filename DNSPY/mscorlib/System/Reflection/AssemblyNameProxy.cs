using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005CA RID: 1482
	[ComVisible(true)]
	public class AssemblyNameProxy : MarshalByRefObject
	{
		// Token: 0x060044B9 RID: 17593 RVA: 0x000FCBFA File Offset: 0x000FADFA
		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000FCC02 File Offset: 0x000FAE02
		public AssemblyNameProxy()
		{
		}
	}
}
