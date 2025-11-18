using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000878 RID: 2168
	internal class SmuggledObjRef
	{
		// Token: 0x06005C36 RID: 23606 RVA: 0x0014309C File Offset: 0x0014129C
		[SecurityCritical]
		public SmuggledObjRef(ObjRef objRef)
		{
			this._objRef = objRef;
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x001430AB File Offset: 0x001412AB
		public ObjRef ObjRef
		{
			[SecurityCritical]
			get
			{
				return this._objRef;
			}
		}

		// Token: 0x040029A6 RID: 10662
		[SecurityCritical]
		private ObjRef _objRef;
	}
}
