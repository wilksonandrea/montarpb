using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000404 RID: 1028
	[ComVisible(true)]
	public interface ISymbolWriter
	{
		// Token: 0x060033D0 RID: 13264
		void Initialize(IntPtr emitter, string filename, bool fFullBuild);

		// Token: 0x060033D1 RID: 13265
		ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x060033D2 RID: 13266
		void SetUserEntryPoint(SymbolToken entryMethod);

		// Token: 0x060033D3 RID: 13267
		void OpenMethod(SymbolToken method);

		// Token: 0x060033D4 RID: 13268
		void CloseMethod();

		// Token: 0x060033D5 RID: 13269
		void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x060033D6 RID: 13270
		int OpenScope(int startOffset);

		// Token: 0x060033D7 RID: 13271
		void CloseScope(int endOffset);

		// Token: 0x060033D8 RID: 13272
		void SetScopeRange(int scopeID, int startOffset, int endOffset);

		// Token: 0x060033D9 RID: 13273
		void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		// Token: 0x060033DA RID: 13274
		void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060033DB RID: 13275
		void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060033DC RID: 13276
		void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060033DD RID: 13277
		void Close();

		// Token: 0x060033DE RID: 13278
		void SetSymAttribute(SymbolToken parent, string name, byte[] data);

		// Token: 0x060033DF RID: 13279
		void OpenNamespace(string name);

		// Token: 0x060033E0 RID: 13280
		void CloseNamespace();

		// Token: 0x060033E1 RID: 13281
		void UsingNamespace(string fullName);

		// Token: 0x060033E2 RID: 13282
		void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

		// Token: 0x060033E3 RID: 13283
		void SetUnderlyingWriter(IntPtr underlyingWriter);
	}
}
