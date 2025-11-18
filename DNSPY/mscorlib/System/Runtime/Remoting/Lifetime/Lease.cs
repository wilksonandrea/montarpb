using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000822 RID: 2082
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x06005930 RID: 22832 RVA: 0x00139C70 File Offset: 0x00137E70
		internal Lease(TimeSpan initialLeaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject managedObject)
		{
			this.id = Lease.nextId++;
			this.renewOnCallTime = renewOnCallTime;
			this.sponsorshipTimeout = sponsorshipTimeout;
			this.initialLeaseTime = initialLeaseTime;
			this.managedObject = managedObject;
			this.leaseManager = LeaseManager.GetLeaseManager();
			this.sponsorTable = new Hashtable(10);
			this.state = LeaseState.Initial;
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x00139CD8 File Offset: 0x00137ED8
		internal void ActivateLease()
		{
			this.leaseTime = DateTime.UtcNow.Add(this.initialLeaseTime);
			this.state = LeaseState.Active;
			this.leaseManager.ActivateLease(this);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00139D11 File Offset: 0x00137F11
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x00139D14 File Offset: 0x00137F14
		// (set) Token: 0x06005934 RID: 22836 RVA: 0x00139D1C File Offset: 0x00137F1C
		public TimeSpan RenewOnCallTime
		{
			[SecurityCritical]
			get
			{
				return this.renewOnCallTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.renewOnCallTime = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateRenewOnCall", new object[] { this.state.ToString() }));
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x00139D57 File Offset: 0x00137F57
		// (set) Token: 0x06005936 RID: 22838 RVA: 0x00139D5F File Offset: 0x00137F5F
		public TimeSpan SponsorshipTimeout
		{
			[SecurityCritical]
			get
			{
				return this.sponsorshipTimeout;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.sponsorshipTimeout = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateSponsorshipTimeout", new object[] { this.state.ToString() }));
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005937 RID: 22839 RVA: 0x00139D9A File Offset: 0x00137F9A
		// (set) Token: 0x06005938 RID: 22840 RVA: 0x00139DA4 File Offset: 0x00137FA4
		public TimeSpan InitialLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.initialLeaseTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state != LeaseState.Initial)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateInitialLeaseTime", new object[] { this.state.ToString() }));
				}
				this.initialLeaseTime = value;
				if (TimeSpan.Zero.CompareTo(value) >= 0)
				{
					this.state = LeaseState.Null;
					return;
				}
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005939 RID: 22841 RVA: 0x00139E03 File Offset: 0x00138003
		public TimeSpan CurrentLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.leaseTime.Subtract(DateTime.UtcNow);
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x00139E15 File Offset: 0x00138015
		public LeaseState CurrentState
		{
			[SecurityCritical]
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00139E1D File Offset: 0x0013801D
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00139E2C File Offset: 0x0013802C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired && !(this.sponsorshipTimeout == TimeSpan.Zero))
				{
					object sponsorId = this.GetSponsorId(obj);
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						if (renewalTime > TimeSpan.Zero)
						{
							this.AddTime(renewalTime);
						}
						if (!this.sponsorTable.ContainsKey(sponsorId))
						{
							this.sponsorTable[sponsorId] = new Lease.SponsorStateInfo(renewalTime, Lease.SponsorState.Initial);
						}
					}
				}
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00139EE4 File Offset: 0x001380E4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Unregister(ISponsor sponsor)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					object sponsorId = this.GetSponsorId(sponsor);
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						if (sponsorId != null)
						{
							this.leaseManager.DeleteSponsor(sponsorId);
							Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
							this.sponsorTable.Remove(sponsorId);
						}
					}
				}
			}
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00139F84 File Offset: 0x00138184
		[SecurityCritical]
		private object GetSponsorId(ISponsor obj)
		{
			object obj2 = null;
			if (obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					obj2 = RemotingServices.GetRealProxy(obj);
				}
				else
				{
					obj2 = obj;
				}
			}
			return obj2;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00139FAC File Offset: 0x001381AC
		[SecurityCritical]
		private ISponsor GetSponsorFromId(object sponsorId)
		{
			RealProxy realProxy = sponsorId as RealProxy;
			object obj;
			if (realProxy != null)
			{
				obj = realProxy.GetTransparentProxy();
			}
			else
			{
				obj = sponsorId;
			}
			return (ISponsor)obj;
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x00139FD6 File Offset: 0x001381D6
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			return this.RenewInternal(renewalTime);
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x00139FE0 File Offset: 0x001381E0
		internal TimeSpan RenewInternal(TimeSpan renewalTime)
		{
			TimeSpan timeSpan;
			lock (this)
			{
				if (this.state == LeaseState.Expired)
				{
					timeSpan = TimeSpan.Zero;
				}
				else
				{
					this.AddTime(renewalTime);
					timeSpan = this.leaseTime.Subtract(DateTime.UtcNow);
				}
			}
			return timeSpan;
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x0013A040 File Offset: 0x00138240
		internal void Remove()
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			this.state = LeaseState.Expired;
			this.leaseManager.DeleteLease(this);
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x0013A060 File Offset: 0x00138260
		[SecurityCritical]
		internal void Cancel()
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					this.Remove();
					RemotingServices.Disconnect(this.managedObject, false);
					RemotingServices.Disconnect(this);
				}
			}
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x0013A0BC File Offset: 0x001382BC
		internal void RenewOnCall()
		{
			lock (this)
			{
				if (this.state != LeaseState.Initial && this.state != LeaseState.Expired)
				{
					this.AddTime(this.renewOnCallTime);
				}
			}
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x0013A114 File Offset: 0x00138314
		[SecurityCritical]
		internal void LeaseExpired(DateTime now)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					if (this.leaseTime.CompareTo(now) < 0)
					{
						this.ProcessNextSponsor();
					}
				}
			}
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x0013A16C File Offset: 0x0013836C
		[SecurityCritical]
		internal void SponsorCall(ISponsor sponsor)
		{
			bool flag = false;
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				try
				{
					object sponsorId = this.GetSponsorId(sponsor);
					this.sponsorCallThread = Thread.CurrentThread.GetHashCode();
					Lease.AsyncRenewal asyncRenewal = new Lease.AsyncRenewal(sponsor.Renewal);
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Waiting;
					IAsyncResult asyncResult = asyncRenewal.BeginInvoke(this, new AsyncCallback(this.SponsorCallback), null);
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting && this.state != LeaseState.Expired)
					{
						this.leaseManager.RegisterSponsorCall(this, sponsorId, this.sponsorshipTimeout);
					}
					this.sponsorCallThread = 0;
				}
				catch (Exception)
				{
					flag = true;
					this.sponsorCallThread = 0;
				}
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
			}
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x0013A260 File Offset: 0x00138460
		[SecurityCritical]
		internal void SponsorTimeout(object sponsorId)
		{
			lock (this)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
						if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting)
						{
							this.Unregister(this.GetSponsorFromId(sponsorId));
							this.ProcessNextSponsor();
						}
					}
				}
			}
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x0013A2FC File Offset: 0x001384FC
		[SecurityCritical]
		private void ProcessNextSponsor()
		{
			object obj = null;
			TimeSpan timeSpan = TimeSpan.Zero;
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					object key = enumerator.Key;
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)enumerator.Value;
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Initial && timeSpan == TimeSpan.Zero)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
					else if (sponsorStateInfo.renewalTime > timeSpan)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
				}
			}
			if (obj != null)
			{
				this.SponsorCall(this.GetSponsorFromId(obj));
				return;
			}
			this.Cancel();
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x0013A3C4 File Offset: 0x001385C4
		[SecurityCritical]
		internal void SponsorCallback(object obj)
		{
			this.SponsorCallback((IAsyncResult)obj);
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0013A3D4 File Offset: 0x001385D4
		[SecurityCritical]
		internal void SponsorCallback(IAsyncResult iar)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (hashCode == this.sponsorCallThread)
			{
				WaitCallback waitCallback = new WaitCallback(this.SponsorCallback);
				ThreadPool.QueueUserWorkItem(waitCallback, iar);
				return;
			}
			AsyncResult asyncResult = (AsyncResult)iar;
			Lease.AsyncRenewal asyncRenewal = (Lease.AsyncRenewal)asyncResult.AsyncDelegate;
			ISponsor sponsor = (ISponsor)asyncRenewal.Target;
			Lease.SponsorStateInfo sponsorStateInfo = null;
			if (!iar.IsCompleted)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			bool flag = false;
			TimeSpan timeSpan = TimeSpan.Zero;
			try
			{
				timeSpan = asyncRenewal.EndInvoke(iar);
			}
			catch (Exception)
			{
				flag = true;
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			object sponsorId = this.GetSponsorId(sponsor);
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Completed;
					sponsorStateInfo.renewalTime = timeSpan;
				}
			}
			if (sponsorStateInfo == null)
			{
				this.ProcessNextSponsor();
				return;
			}
			if (sponsorStateInfo.renewalTime == TimeSpan.Zero)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			this.RenewInternal(sponsorStateInfo.renewalTime);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0013A534 File Offset: 0x00138734
		private void AddTime(TimeSpan renewalSpan)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = this.leaseTime;
			DateTime dateTime2 = utcNow.Add(renewalSpan);
			if (this.leaseTime.CompareTo(dateTime2) < 0)
			{
				this.leaseManager.ChangedLeaseTime(this, dateTime2);
				this.leaseTime = dateTime2;
				this.state = LeaseState.Active;
			}
		}

		// Token: 0x040028A4 RID: 10404
		internal int id;

		// Token: 0x040028A5 RID: 10405
		internal DateTime leaseTime;

		// Token: 0x040028A6 RID: 10406
		internal TimeSpan initialLeaseTime;

		// Token: 0x040028A7 RID: 10407
		internal TimeSpan renewOnCallTime;

		// Token: 0x040028A8 RID: 10408
		internal TimeSpan sponsorshipTimeout;

		// Token: 0x040028A9 RID: 10409
		internal Hashtable sponsorTable;

		// Token: 0x040028AA RID: 10410
		internal int sponsorCallThread;

		// Token: 0x040028AB RID: 10411
		internal LeaseManager leaseManager;

		// Token: 0x040028AC RID: 10412
		internal MarshalByRefObject managedObject;

		// Token: 0x040028AD RID: 10413
		internal LeaseState state;

		// Token: 0x040028AE RID: 10414
		internal static volatile int nextId;

		// Token: 0x02000C75 RID: 3189
		// (Invoke) Token: 0x060070B9 RID: 28857
		internal delegate TimeSpan AsyncRenewal(ILease lease);

		// Token: 0x02000C76 RID: 3190
		[Serializable]
		internal enum SponsorState
		{
			// Token: 0x04003802 RID: 14338
			Initial,
			// Token: 0x04003803 RID: 14339
			Waiting,
			// Token: 0x04003804 RID: 14340
			Completed
		}

		// Token: 0x02000C77 RID: 3191
		internal sealed class SponsorStateInfo
		{
			// Token: 0x060070BC RID: 28860 RVA: 0x00184748 File Offset: 0x00182948
			internal SponsorStateInfo(TimeSpan renewalTime, Lease.SponsorState sponsorState)
			{
				this.renewalTime = renewalTime;
				this.sponsorState = sponsorState;
			}

			// Token: 0x04003805 RID: 14341
			internal TimeSpan renewalTime;

			// Token: 0x04003806 RID: 14342
			internal Lease.SponsorState sponsorState;
		}
	}
}
