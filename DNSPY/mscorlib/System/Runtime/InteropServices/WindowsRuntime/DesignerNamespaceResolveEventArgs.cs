using System;
using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FB RID: 2555
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x00157AA1 File Offset: 0x00155CA1
		public string NamespaceName
		{
			get
			{
				return this._NamespaceName;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x00157AA9 File Offset: 0x00155CA9
		public Collection<string> ResolvedAssemblyFiles
		{
			get
			{
				return this._ResolvedAssemblyFiles;
			}
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x00157AB1 File Offset: 0x00155CB1
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this._NamespaceName = namespaceName;
			this._ResolvedAssemblyFiles = new Collection<string>();
		}

		// Token: 0x04002D37 RID: 11575
		private string _NamespaceName;

		// Token: 0x04002D38 RID: 11576
		private Collection<string> _ResolvedAssemblyFiles;
	}
}
