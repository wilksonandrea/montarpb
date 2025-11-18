using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003FD RID: 1021
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600339D RID: 13213
		string URL { get; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600339E RID: 13214
		Guid DocumentType { get; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600339F RID: 13215
		Guid Language { get; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060033A0 RID: 13216
		Guid LanguageVendor { get; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060033A1 RID: 13217
		Guid CheckSumAlgorithmId { get; }

		// Token: 0x060033A2 RID: 13218
		byte[] GetCheckSum();

		// Token: 0x060033A3 RID: 13219
		int FindClosestLine(int line);

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060033A4 RID: 13220
		bool HasEmbeddedSource { get; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060033A5 RID: 13221
		int SourceLength { get; }

		// Token: 0x060033A6 RID: 13222
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
