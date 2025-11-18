using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000644 RID: 1604
	internal sealed class LineNumberInfo
	{
		// Token: 0x06004B03 RID: 19203 RVA: 0x0010F83F File Offset: 0x0010DA3F
		internal LineNumberInfo()
		{
			this.m_DocumentCount = 0;
			this.m_iLastFound = 0;
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x0010F858 File Offset: 0x0010DA58
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			int num = this.FindDocument(document);
			this.m_Documents[num].AddLineNumberInfo(document, iOffset, iStartLine, iStartColumn, iEndLine, iEndColumn);
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x0010F884 File Offset: 0x0010DA84
		private int FindDocument(ISymbolDocumentWriter document)
		{
			if (this.m_iLastFound < this.m_DocumentCount && this.m_Documents[this.m_iLastFound].m_document == document)
			{
				return this.m_iLastFound;
			}
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				if (this.m_Documents[i].m_document == document)
				{
					this.m_iLastFound = i;
					return this.m_iLastFound;
				}
			}
			this.EnsureCapacity();
			this.m_iLastFound = this.m_DocumentCount;
			this.m_Documents[this.m_iLastFound] = new REDocument(document);
			checked
			{
				this.m_DocumentCount++;
				return this.m_iLastFound;
			}
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x0010F924 File Offset: 0x0010DB24
		private void EnsureCapacity()
		{
			if (this.m_DocumentCount == 0)
			{
				this.m_Documents = new REDocument[16];
				return;
			}
			if (this.m_DocumentCount == this.m_Documents.Length)
			{
				REDocument[] array = new REDocument[this.m_DocumentCount * 2];
				Array.Copy(this.m_Documents, array, this.m_DocumentCount);
				this.m_Documents = array;
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x0010F980 File Offset: 0x0010DB80
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				this.m_Documents[i].EmitLineNumberInfo(symWriter);
			}
		}

		// Token: 0x04001F01 RID: 7937
		private int m_DocumentCount;

		// Token: 0x04001F02 RID: 7938
		private REDocument[] m_Documents;

		// Token: 0x04001F03 RID: 7939
		private const int InitialSize = 16;

		// Token: 0x04001F04 RID: 7940
		private int m_iLastFound;
	}
}
