using System;
using System.IO;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000654 RID: 1620
	[Serializable]
	internal class ModuleBuilderData
	{
		// Token: 0x06004CA4 RID: 19620 RVA: 0x0011584F File Offset: 0x00113A4F
		[SecurityCritical]
		internal ModuleBuilderData(ModuleBuilder module, string strModuleName, string strFileName, int tkFile)
		{
			this.m_globalTypeBuilder = new TypeBuilder(module);
			this.m_module = module;
			this.m_tkFile = tkFile;
			this.InitNames(strModuleName, strFileName);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0011587C File Offset: 0x00113A7C
		[SecurityCritical]
		private void InitNames(string strModuleName, string strFileName)
		{
			this.m_strModuleName = strModuleName;
			if (strFileName == null)
			{
				this.m_strFileName = strModuleName;
				return;
			}
			string extension = Path.GetExtension(strFileName);
			if (extension == null || extension == string.Empty)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoModuleFileExtension", new object[] { strFileName }));
			}
			this.m_strFileName = strFileName;
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x001158D3 File Offset: 0x00113AD3
		[SecurityCritical]
		internal virtual void ModifyModuleName(string strModuleName)
		{
			this.InitNames(strModuleName, null);
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x001158DD File Offset: 0x00113ADD
		// (set) Token: 0x06004CA8 RID: 19624 RVA: 0x001158E5 File Offset: 0x00113AE5
		internal int FileToken
		{
			get
			{
				return this.m_tkFile;
			}
			set
			{
				this.m_tkFile = value;
			}
		}

		// Token: 0x04001F63 RID: 8035
		internal string m_strModuleName;

		// Token: 0x04001F64 RID: 8036
		internal string m_strFileName;

		// Token: 0x04001F65 RID: 8037
		internal bool m_fGlobalBeenCreated;

		// Token: 0x04001F66 RID: 8038
		internal bool m_fHasGlobal;

		// Token: 0x04001F67 RID: 8039
		[NonSerialized]
		internal TypeBuilder m_globalTypeBuilder;

		// Token: 0x04001F68 RID: 8040
		[NonSerialized]
		internal ModuleBuilder m_module;

		// Token: 0x04001F69 RID: 8041
		private int m_tkFile;

		// Token: 0x04001F6A RID: 8042
		internal bool m_isSaved;

		// Token: 0x04001F6B RID: 8043
		[NonSerialized]
		internal ResWriterData m_embeddedRes;

		// Token: 0x04001F6C RID: 8044
		internal const string MULTI_BYTE_VALUE_CLASS = "$ArrayType$";

		// Token: 0x04001F6D RID: 8045
		internal string m_strResourceFileName;

		// Token: 0x04001F6E RID: 8046
		internal byte[] m_resourceBytes;
	}
}
