using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000643 RID: 1603
	internal sealed class ScopeTree
	{
		// Token: 0x06004AFC RID: 19196 RVA: 0x0010F5C2 File Offset: 0x0010D7C2
		internal ScopeTree()
		{
			this.m_iOpenScopeCount = 0;
			this.m_iCount = 0;
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0010F5D8 File Offset: 0x0010D7D8
		internal int GetCurrentActiveScopeIndex()
		{
			int num = 0;
			int num2 = this.m_iCount - 1;
			if (this.m_iCount == 0)
			{
				return -1;
			}
			while (num > 0 || this.m_ScopeActions[num2] == ScopeAction.Close)
			{
				if (this.m_ScopeActions[num2] == ScopeAction.Open)
				{
					num--;
				}
				else
				{
					num++;
				}
				num2--;
			}
			return num2;
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0010F624 File Offset: 0x0010D824
		internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0010F664 File Offset: 0x0010D864
		internal void AddUsingNamespaceToCurrentScope(string strNamespace)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddUsingNamespace(strNamespace);
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x0010F6A0 File Offset: 0x0010D8A0
		internal void AddScopeInfo(ScopeAction sa, int iOffset)
		{
			if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchingSymScope"));
			}
			this.EnsureCapacity();
			this.m_ScopeActions[this.m_iCount] = sa;
			this.m_iOffsets[this.m_iCount] = iOffset;
			this.m_localSymInfos[this.m_iCount] = null;
			checked
			{
				this.m_iCount++;
			}
			if (sa == ScopeAction.Open)
			{
				this.m_iOpenScopeCount++;
				return;
			}
			this.m_iOpenScopeCount--;
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x0010F728 File Offset: 0x0010D928
		internal void EnsureCapacity()
		{
			if (this.m_iCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_ScopeActions = new ScopeAction[16];
				this.m_localSymInfos = new LocalSymInfo[16];
				return;
			}
			if (this.m_iCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iCount);
				this.m_iOffsets = array;
				ScopeAction[] array2 = new ScopeAction[num];
				Array.Copy(this.m_ScopeActions, array2, this.m_iCount);
				this.m_ScopeActions = array2;
				LocalSymInfo[] array3 = new LocalSymInfo[num];
				Array.Copy(this.m_localSymInfos, array3, this.m_iCount);
				this.m_localSymInfos = array3;
			}
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x0010F7E0 File Offset: 0x0010D9E0
		internal void EmitScopeTree(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iCount; i++)
			{
				if (this.m_ScopeActions[i] == ScopeAction.Open)
				{
					symWriter.OpenScope(this.m_iOffsets[i]);
				}
				else
				{
					symWriter.CloseScope(this.m_iOffsets[i]);
				}
				if (this.m_localSymInfos[i] != null)
				{
					this.m_localSymInfos[i].EmitLocalSymInfo(symWriter);
				}
			}
		}

		// Token: 0x04001EFB RID: 7931
		internal int[] m_iOffsets;

		// Token: 0x04001EFC RID: 7932
		internal ScopeAction[] m_ScopeActions;

		// Token: 0x04001EFD RID: 7933
		internal int m_iCount;

		// Token: 0x04001EFE RID: 7934
		internal int m_iOpenScopeCount;

		// Token: 0x04001EFF RID: 7935
		internal const int InitialSize = 16;

		// Token: 0x04001F00 RID: 7936
		internal LocalSymInfo[] m_localSymInfos;
	}
}
