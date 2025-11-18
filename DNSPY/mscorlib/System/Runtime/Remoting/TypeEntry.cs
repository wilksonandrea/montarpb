using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C2 RID: 1986
	[ComVisible(true)]
	public class TypeEntry
	{
		// Token: 0x060055F4 RID: 22004 RVA: 0x00130DD8 File Offset: 0x0012EFD8
		protected TypeEntry()
		{
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060055F5 RID: 22005 RVA: 0x00130DE0 File Offset: 0x0012EFE0
		// (set) Token: 0x060055F6 RID: 22006 RVA: 0x00130DE8 File Offset: 0x0012EFE8
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060055F7 RID: 22007 RVA: 0x00130DF1 File Offset: 0x0012EFF1
		// (set) Token: 0x060055F8 RID: 22008 RVA: 0x00130DF9 File Offset: 0x0012EFF9
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x00130E02 File Offset: 0x0012F002
		internal void CacheRemoteAppEntry(RemoteAppEntry entry)
		{
			this._cachedRemoteAppEntry = entry;
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00130E0B File Offset: 0x0012F00B
		internal RemoteAppEntry GetRemoteAppEntry()
		{
			return this._cachedRemoteAppEntry;
		}

		// Token: 0x04002785 RID: 10117
		private string _typeName;

		// Token: 0x04002786 RID: 10118
		private string _assemblyName;

		// Token: 0x04002787 RID: 10119
		private RemoteAppEntry _cachedRemoteAppEntry;
	}
}
