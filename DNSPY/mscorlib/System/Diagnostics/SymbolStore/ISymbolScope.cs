using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000402 RID: 1026
	[ComVisible(true)]
	public interface ISymbolScope
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060033C0 RID: 13248
		ISymbolMethod Method { get; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060033C1 RID: 13249
		ISymbolScope Parent { get; }

		// Token: 0x060033C2 RID: 13250
		ISymbolScope[] GetChildren();

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060033C3 RID: 13251
		int StartOffset { get; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060033C4 RID: 13252
		int EndOffset { get; }

		// Token: 0x060033C5 RID: 13253
		ISymbolVariable[] GetLocals();

		// Token: 0x060033C6 RID: 13254
		ISymbolNamespace[] GetNamespaces();
	}
}
