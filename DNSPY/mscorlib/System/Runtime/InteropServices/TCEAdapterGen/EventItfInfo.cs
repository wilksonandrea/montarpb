using System;
using System.Reflection;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C5 RID: 2501
	internal class EventItfInfo
	{
		// Token: 0x060063B8 RID: 25528 RVA: 0x00154327 File Offset: 0x00152527
		public EventItfInfo(string strEventItfName, string strSrcItfName, string strEventProviderName, RuntimeAssembly asmImport, RuntimeAssembly asmSrcItf)
		{
			this.m_strEventItfName = strEventItfName;
			this.m_strSrcItfName = strSrcItfName;
			this.m_strEventProviderName = strEventProviderName;
			this.m_asmImport = asmImport;
			this.m_asmSrcItf = asmSrcItf;
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x00154354 File Offset: 0x00152554
		public Type GetEventItfType()
		{
			Type type = this.m_asmImport.GetType(this.m_strEventItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x0015438C File Offset: 0x0015258C
		public Type GetSrcItfType()
		{
			Type type = this.m_asmSrcItf.GetType(this.m_strSrcItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x001543C1 File Offset: 0x001525C1
		public string GetEventProviderName()
		{
			return this.m_strEventProviderName;
		}

		// Token: 0x04002CD8 RID: 11480
		private string m_strEventItfName;

		// Token: 0x04002CD9 RID: 11481
		private string m_strSrcItfName;

		// Token: 0x04002CDA RID: 11482
		private string m_strEventProviderName;

		// Token: 0x04002CDB RID: 11483
		private RuntimeAssembly m_asmImport;

		// Token: 0x04002CDC RID: 11484
		private RuntimeAssembly m_asmSrcItf;
	}
}
