using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003FF RID: 1023
	[ComVisible(true)]
	public interface ISymbolMethod
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060033A9 RID: 13225
		SymbolToken Token { get; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060033AA RID: 13226
		int SequencePointCount { get; }

		// Token: 0x060033AB RID: 13227
		void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060033AC RID: 13228
		ISymbolScope RootScope { get; }

		// Token: 0x060033AD RID: 13229
		ISymbolScope GetScope(int offset);

		// Token: 0x060033AE RID: 13230
		int GetOffset(ISymbolDocument document, int line, int column);

		// Token: 0x060033AF RID: 13231
		int[] GetRanges(ISymbolDocument document, int line, int column);

		// Token: 0x060033B0 RID: 13232
		ISymbolVariable[] GetParameters();

		// Token: 0x060033B1 RID: 13233
		ISymbolNamespace GetNamespace();

		// Token: 0x060033B2 RID: 13234
		bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
	}
}
