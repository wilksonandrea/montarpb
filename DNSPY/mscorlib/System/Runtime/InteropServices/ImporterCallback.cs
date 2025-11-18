using System;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000953 RID: 2387
	internal class ImporterCallback : ITypeLibImporterNotifySink
	{
		// Token: 0x060061B0 RID: 25008 RVA: 0x0014E2F5 File Offset: 0x0014C4F5
		public void ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
		{
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0014E2F8 File Offset: 0x0014C4F8
		[SecuritySafeCritical]
		public Assembly ResolveRef(object TypeLib)
		{
			Assembly assembly;
			try
			{
				ITypeLibConverter typeLibConverter = new TypeLibConverter();
				assembly = typeLibConverter.ConvertTypeLibToAssembly(TypeLib, Marshal.GetTypeLibName((ITypeLib)TypeLib) + ".dll", TypeLibImporterFlags.None, new ImporterCallback(), null, null, null, null);
			}
			catch (Exception)
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x0014E34C File Offset: 0x0014C54C
		public ImporterCallback()
		{
		}
	}
}
