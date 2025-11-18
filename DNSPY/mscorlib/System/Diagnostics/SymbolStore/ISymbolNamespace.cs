using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000400 RID: 1024
	[ComVisible(true)]
	public interface ISymbolNamespace
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060033B3 RID: 13235
		string Name { get; }

		// Token: 0x060033B4 RID: 13236
		ISymbolNamespace[] GetNamespaces();

		// Token: 0x060033B5 RID: 13237
		ISymbolVariable[] GetVariables();
	}
}
