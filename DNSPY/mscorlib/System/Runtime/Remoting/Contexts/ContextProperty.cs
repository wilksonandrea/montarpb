using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080D RID: 2061
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ContextProperty
	{
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x00138C5D File Offset: 0x00136E5D
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x00138C65 File Offset: 0x00136E65
		public virtual object Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x00138C6D File Offset: 0x00136E6D
		internal ContextProperty(string name, object prop)
		{
			this._name = name;
			this._property = prop;
		}

		// Token: 0x0400287A RID: 10362
		internal string _name;

		// Token: 0x0400287B RID: 10363
		internal object _property;
	}
}
