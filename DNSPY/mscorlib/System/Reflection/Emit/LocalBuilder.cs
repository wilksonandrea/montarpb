using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000647 RID: 1607
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ComVisible(true)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x06004B13 RID: 19219 RVA: 0x0010FC5B File Offset: 0x0010DE5B
		private LocalBuilder()
		{
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x0010FC63 File Offset: 0x0010DE63
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder)
			: this(localIndex, localType, methodBuilder, false)
		{
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x0010FC6F File Offset: 0x0010DE6F
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
		{
			this.m_isPinned = isPinned;
			this.m_localIndex = localIndex;
			this.m_localType = localType;
			this.m_methodBuilder = methodBuilder;
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x0010FC94 File Offset: 0x0010DE94
		internal int GetLocalIndex()
		{
			return this.m_localIndex;
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0010FC9C File Offset: 0x0010DE9C
		internal MethodInfo GetMethodBuilder()
		{
			return this.m_methodBuilder;
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0010FCA4 File Offset: 0x0010DEA4
		public override bool IsPinned
		{
			get
			{
				return this.m_isPinned;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0010FCAC File Offset: 0x0010DEAC
		public override Type LocalType
		{
			get
			{
				return this.m_localType;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x0010FCB4 File Offset: 0x0010DEB4
		public override int LocalIndex
		{
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0010FCBC File Offset: 0x0010DEBC
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0010FCC8 File Offset: 0x0010DEC8
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)methodBuilder.Module;
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (moduleBuilder.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(moduleBuilder);
			fieldSigHelper.AddArgument(this.m_localType);
			int num;
			byte[] array = fieldSigHelper.InternalGetSignature(out num);
			byte[] array2 = new byte[num - 1];
			Array.Copy(array, 1, array2, 0, num - 1);
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddLocalSymInfo(name, array2, this.m_localIndex, startOffset, endOffset);
				return;
			}
			methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, array2, this.m_localIndex, startOffset, endOffset);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0010FDAF File Offset: 0x0010DFAF
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0010FDB6 File Offset: 0x0010DFB6
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x0010FDBD File Offset: 0x0010DFBD
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0010FDC4 File Offset: 0x0010DFC4
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F0E RID: 7950
		private int m_localIndex;

		// Token: 0x04001F0F RID: 7951
		private Type m_localType;

		// Token: 0x04001F10 RID: 7952
		private MethodInfo m_methodBuilder;

		// Token: 0x04001F11 RID: 7953
		private bool m_isPinned;
	}
}
