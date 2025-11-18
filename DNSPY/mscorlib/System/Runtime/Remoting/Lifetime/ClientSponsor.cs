using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200081F RID: 2079
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		// Token: 0x06005919 RID: 22809 RVA: 0x00139AAE File Offset: 0x00137CAE
		public ClientSponsor()
		{
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x00139AD7 File Offset: 0x00137CD7
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.m_renewalTime = renewalTime;
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x00139B07 File Offset: 0x00137D07
		// (set) Token: 0x0600591C RID: 22812 RVA: 0x00139B0F File Offset: 0x00137D0F
		public TimeSpan RenewalTime
		{
			get
			{
				return this.m_renewalTime;
			}
			set
			{
				this.m_renewalTime = value;
			}
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x00139B18 File Offset: 0x00137D18
		[SecurityCritical]
		public bool Register(MarshalByRefObject obj)
		{
			ILease lease = (ILease)obj.GetLifetimeService();
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				this.sponsorTable[obj] = lease;
			}
			return true;
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x00139B78 File Offset: 0x00137D78
		[SecurityCritical]
		public void Unregister(MarshalByRefObject obj)
		{
			ILease lease = null;
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				lease = (ILease)this.sponsorTable[obj];
			}
			if (lease != null)
			{
				lease.Unregister(this);
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00139BD0 File Offset: 0x00137DD0
		[SecurityCritical]
		public TimeSpan Renewal(ILease lease)
		{
			return this.m_renewalTime;
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x00139BD8 File Offset: 0x00137DD8
		[SecurityCritical]
		public void Close()
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((ILease)enumerator.Value).Unregister(this);
				}
				this.sponsorTable.Clear();
			}
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x00139C44 File Offset: 0x00137E44
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x00139C48 File Offset: 0x00137E48
		[SecuritySafeCritical]
		~ClientSponsor()
		{
		}

		// Token: 0x040028A2 RID: 10402
		private Hashtable sponsorTable = new Hashtable(10);

		// Token: 0x040028A3 RID: 10403
		private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);
	}
}
