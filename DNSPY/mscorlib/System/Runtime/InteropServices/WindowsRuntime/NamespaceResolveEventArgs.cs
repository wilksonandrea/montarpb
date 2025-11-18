using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FA RID: 2554
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060064EB RID: 25835 RVA: 0x00157A68 File Offset: 0x00155C68
		public string NamespaceName
		{
			get
			{
				return this._NamespaceName;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x00157A70 File Offset: 0x00155C70
		public Assembly RequestingAssembly
		{
			get
			{
				return this._RequestingAssembly;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060064ED RID: 25837 RVA: 0x00157A78 File Offset: 0x00155C78
		public Collection<Assembly> ResolvedAssemblies
		{
			get
			{
				return this._ResolvedAssemblies;
			}
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x00157A80 File Offset: 0x00155C80
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this._NamespaceName = namespaceName;
			this._RequestingAssembly = requestingAssembly;
			this._ResolvedAssemblies = new Collection<Assembly>();
		}

		// Token: 0x04002D34 RID: 11572
		private string _NamespaceName;

		// Token: 0x04002D35 RID: 11573
		private Assembly _RequestingAssembly;

		// Token: 0x04002D36 RID: 11574
		private Collection<Assembly> _ResolvedAssemblies;
	}
}
