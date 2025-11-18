using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000645 RID: 1605
	internal sealed class REDocument
	{
		// Token: 0x06004B08 RID: 19208 RVA: 0x0010F9AC File Offset: 0x0010DBAC
		internal REDocument(ISymbolDocumentWriter document)
		{
			this.m_iLineNumberCount = 0;
			this.m_document = document;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x0010F9C4 File Offset: 0x0010DBC4
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			this.EnsureCapacity();
			this.m_iOffsets[this.m_iLineNumberCount] = iOffset;
			this.m_iLines[this.m_iLineNumberCount] = iStartLine;
			this.m_iColumns[this.m_iLineNumberCount] = iStartColumn;
			this.m_iEndLines[this.m_iLineNumberCount] = iEndLine;
			this.m_iEndColumns[this.m_iLineNumberCount] = iEndColumn;
			checked
			{
				this.m_iLineNumberCount++;
			}
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x0010FA30 File Offset: 0x0010DC30
		private void EnsureCapacity()
		{
			if (this.m_iLineNumberCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_iLines = new int[16];
				this.m_iColumns = new int[16];
				this.m_iEndLines = new int[16];
				this.m_iEndColumns = new int[16];
				return;
			}
			if (this.m_iLineNumberCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iLineNumberCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
				this.m_iOffsets = array;
				array = new int[num];
				Array.Copy(this.m_iLines, array, this.m_iLineNumberCount);
				this.m_iLines = array;
				array = new int[num];
				Array.Copy(this.m_iColumns, array, this.m_iLineNumberCount);
				this.m_iColumns = array;
				array = new int[num];
				Array.Copy(this.m_iEndLines, array, this.m_iLineNumberCount);
				this.m_iEndLines = array;
				array = new int[num];
				Array.Copy(this.m_iEndColumns, array, this.m_iLineNumberCount);
				this.m_iEndColumns = array;
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x0010FB44 File Offset: 0x0010DD44
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			if (this.m_iLineNumberCount == 0)
			{
				return;
			}
			int[] array = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
			int[] array2 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iLines, array2, this.m_iLineNumberCount);
			int[] array3 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iColumns, array3, this.m_iLineNumberCount);
			int[] array4 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndLines, array4, this.m_iLineNumberCount);
			int[] array5 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndColumns, array5, this.m_iLineNumberCount);
			symWriter.DefineSequencePoints(this.m_document, array, array2, array3, array4, array5);
		}

		// Token: 0x04001F05 RID: 7941
		private int[] m_iOffsets;

		// Token: 0x04001F06 RID: 7942
		private int[] m_iLines;

		// Token: 0x04001F07 RID: 7943
		private int[] m_iColumns;

		// Token: 0x04001F08 RID: 7944
		private int[] m_iEndLines;

		// Token: 0x04001F09 RID: 7945
		private int[] m_iEndColumns;

		// Token: 0x04001F0A RID: 7946
		internal ISymbolDocumentWriter m_document;

		// Token: 0x04001F0B RID: 7947
		private int m_iLineNumberCount;

		// Token: 0x04001F0C RID: 7948
		private const int InitialSize = 16;
	}
}
