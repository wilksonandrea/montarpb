using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088F RID: 2191
	internal class IllogicalCallContext
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x0014506C File Offset: 0x0014326C
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06005CC1 RID: 23745 RVA: 0x00145087 File Offset: 0x00143287
		// (set) Token: 0x06005CC2 RID: 23746 RVA: 0x0014508F File Offset: 0x0014328F
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x00145098 File Offset: 0x00143298
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x001450B2 File Offset: 0x001432B2
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x001450C0 File Offset: 0x001432C0
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x001450CE File Offset: 0x001432CE
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x001450E0 File Offset: 0x001432E0
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x0014513A File Offset: 0x0014333A
		public IllogicalCallContext()
		{
		}

		// Token: 0x040029DF RID: 10719
		private Hashtable m_Datastore;

		// Token: 0x040029E0 RID: 10720
		private object m_HostContext;

		// Token: 0x02000C7E RID: 3198
		internal struct Reader
		{
			// Token: 0x060070C8 RID: 28872 RVA: 0x00184A14 File Offset: 0x00182C14
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17001356 RID: 4950
			// (get) Token: 0x060070C9 RID: 28873 RVA: 0x00184A1D File Offset: 0x00182C1D
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x060070CA RID: 28874 RVA: 0x00184A28 File Offset: 0x00182C28
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17001357 RID: 4951
			// (get) Token: 0x060070CB RID: 28875 RVA: 0x00184A40 File Offset: 0x00182C40
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x04003815 RID: 14357
			private IllogicalCallContext m_ctx;
		}
	}
}
