using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000091 RID: 145
	[ComVisible(true)]
	public class ResolveEventArgs : EventArgs
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001A073 File Offset: 0x00018273
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001A07B File Offset: 0x0001827B
		public Assembly RequestingAssembly
		{
			get
			{
				return this._RequestingAssembly;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001A083 File Offset: 0x00018283
		public ResolveEventArgs(string name)
		{
			this._Name = name;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001A092 File Offset: 0x00018292
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this._Name = name;
			this._RequestingAssembly = requestingAssembly;
		}

		// Token: 0x0400037A RID: 890
		private string _Name;

		// Token: 0x0400037B RID: 891
		private Assembly _RequestingAssembly;
	}
}
