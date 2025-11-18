using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000783 RID: 1923
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x060053C9 RID: 21449 RVA: 0x00126C0F File Offset: 0x00124E0F
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00126C1E File Offset: 0x00124E1E
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x00126C34 File Offset: 0x00124E34
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyNotFound", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x040025C0 RID: 9664
		internal string assemblyString;

		// Token: 0x040025C1 RID: 9665
		private Assembly assembly;
	}
}
