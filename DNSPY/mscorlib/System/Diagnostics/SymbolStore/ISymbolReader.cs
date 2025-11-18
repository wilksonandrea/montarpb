using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000401 RID: 1025
	[ComVisible(true)]
	public interface ISymbolReader
	{
		// Token: 0x060033B6 RID: 13238
		ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x060033B7 RID: 13239
		ISymbolDocument[] GetDocuments();

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060033B8 RID: 13240
		SymbolToken UserEntryPoint { get; }

		// Token: 0x060033B9 RID: 13241
		ISymbolMethod GetMethod(SymbolToken method);

		// Token: 0x060033BA RID: 13242
		ISymbolMethod GetMethod(SymbolToken method, int version);

		// Token: 0x060033BB RID: 13243
		ISymbolVariable[] GetVariables(SymbolToken parent);

		// Token: 0x060033BC RID: 13244
		ISymbolVariable[] GetGlobalVariables();

		// Token: 0x060033BD RID: 13245
		ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

		// Token: 0x060033BE RID: 13246
		byte[] GetSymAttribute(SymbolToken parent, string name);

		// Token: 0x060033BF RID: 13247
		ISymbolNamespace[] GetNamespaces();
	}
}
