using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000649 RID: 1609
	internal class LocalSymInfo
	{
		// Token: 0x06004B73 RID: 19315 RVA: 0x00111262 File Offset: 0x0010F462
		internal LocalSymInfo()
		{
			this.m_iLocalSymCount = 0;
			this.m_iNameSpaceCount = 0;
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00111278 File Offset: 0x0010F478
		private void EnsureCapacityNamespace()
		{
			if (this.m_iNameSpaceCount == 0)
			{
				this.m_namespace = new string[16];
				return;
			}
			if (this.m_iNameSpaceCount == this.m_namespace.Length)
			{
				string[] array = new string[checked(this.m_iNameSpaceCount * 2)];
				Array.Copy(this.m_namespace, array, this.m_iNameSpaceCount);
				this.m_namespace = array;
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x001112D4 File Offset: 0x0010F4D4
		private void EnsureCapacity()
		{
			if (this.m_iLocalSymCount == 0)
			{
				this.m_strName = new string[16];
				this.m_ubSignature = new byte[16][];
				this.m_iLocalSlot = new int[16];
				this.m_iStartOffset = new int[16];
				this.m_iEndOffset = new int[16];
				return;
			}
			if (this.m_iLocalSymCount == this.m_strName.Length)
			{
				int num = checked(this.m_iLocalSymCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iLocalSlot, array, this.m_iLocalSymCount);
				this.m_iLocalSlot = array;
				array = new int[num];
				Array.Copy(this.m_iStartOffset, array, this.m_iLocalSymCount);
				this.m_iStartOffset = array;
				array = new int[num];
				Array.Copy(this.m_iEndOffset, array, this.m_iLocalSymCount);
				this.m_iEndOffset = array;
				string[] array2 = new string[num];
				Array.Copy(this.m_strName, array2, this.m_iLocalSymCount);
				this.m_strName = array2;
				byte[][] array3 = new byte[num][];
				Array.Copy(this.m_ubSignature, array3, this.m_iLocalSymCount);
				this.m_ubSignature = array3;
			}
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x001113E8 File Offset: 0x0010F5E8
		internal void AddLocalSymInfo(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			this.EnsureCapacity();
			this.m_iStartOffset[this.m_iLocalSymCount] = startOffset;
			this.m_iEndOffset[this.m_iLocalSymCount] = endOffset;
			this.m_iLocalSlot[this.m_iLocalSymCount] = slot;
			this.m_strName[this.m_iLocalSymCount] = strName;
			this.m_ubSignature[this.m_iLocalSymCount] = signature;
			checked
			{
				this.m_iLocalSymCount++;
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00111451 File Offset: 0x0010F651
		internal void AddUsingNamespace(string strNamespace)
		{
			this.EnsureCapacityNamespace();
			this.m_namespace[this.m_iNameSpaceCount] = strNamespace;
			checked
			{
				this.m_iNameSpaceCount++;
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00111478 File Offset: 0x0010F678
		internal virtual void EmitLocalSymInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iLocalSymCount; i++)
			{
				symWriter.DefineLocalVariable(this.m_strName[i], FieldAttributes.PrivateScope, this.m_ubSignature[i], SymAddressKind.ILOffset, this.m_iLocalSlot[i], 0, 0, this.m_iStartOffset[i], this.m_iEndOffset[i]);
			}
			for (int i = 0; i < this.m_iNameSpaceCount; i++)
			{
				symWriter.UsingNamespace(this.m_namespace[i]);
			}
		}

		// Token: 0x04001F31 RID: 7985
		internal string[] m_strName;

		// Token: 0x04001F32 RID: 7986
		internal byte[][] m_ubSignature;

		// Token: 0x04001F33 RID: 7987
		internal int[] m_iLocalSlot;

		// Token: 0x04001F34 RID: 7988
		internal int[] m_iStartOffset;

		// Token: 0x04001F35 RID: 7989
		internal int[] m_iEndOffset;

		// Token: 0x04001F36 RID: 7990
		internal int m_iLocalSymCount;

		// Token: 0x04001F37 RID: 7991
		internal string[] m_namespace;

		// Token: 0x04001F38 RID: 7992
		internal int m_iNameSpaceCount;

		// Token: 0x04001F39 RID: 7993
		internal const int InitialSize = 16;
	}
}
