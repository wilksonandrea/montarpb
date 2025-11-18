using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003FE RID: 1022
	[ComVisible(true)]
	public interface ISymbolDocumentWriter
	{
		// Token: 0x060033A7 RID: 13223
		void SetSource(byte[] source);

		// Token: 0x060033A8 RID: 13224
		void SetCheckSum(Guid algorithmId, byte[] checkSum);
	}
}
