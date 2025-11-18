using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096C RID: 2412
	[Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
	[ComVisible(true)]
	public interface IRegistrationServices
	{
		// Token: 0x06006233 RID: 25139
		[SecurityCritical]
		bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

		// Token: 0x06006234 RID: 25140
		[SecurityCritical]
		bool UnregisterAssembly(Assembly assembly);

		// Token: 0x06006235 RID: 25141
		[SecurityCritical]
		Type[] GetRegistrableTypesInAssembly(Assembly assembly);

		// Token: 0x06006236 RID: 25142
		[SecurityCritical]
		string GetProgIdForType(Type type);

		// Token: 0x06006237 RID: 25143
		[SecurityCritical]
		void RegisterTypeForComClients(Type type, ref Guid g);

		// Token: 0x06006238 RID: 25144
		Guid GetManagedCategoryGuid();

		// Token: 0x06006239 RID: 25145
		[SecurityCritical]
		bool TypeRequiresRegistration(Type type);

		// Token: 0x0600623A RID: 25146
		bool TypeRepresentsComType(Type type);
	}
}
