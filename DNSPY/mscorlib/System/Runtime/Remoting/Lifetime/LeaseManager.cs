using System;
using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000824 RID: 2084
	internal class LeaseManager
	{
		// Token: 0x06005950 RID: 22864 RVA: 0x0013A5DC File Offset: 0x001387DC
		internal static bool IsInitialized()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			return leaseManager != null;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0013A600 File Offset: 0x00138800
		[SecurityCritical]
		internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			if (leaseManager == null)
			{
				DomainSpecificRemotingData domainSpecificRemotingData = remotingData;
				lock (domainSpecificRemotingData)
				{
					if (remotingData.LeaseManager == null)
					{
						remotingData.LeaseManager = new LeaseManager(pollTime);
					}
					leaseManager = remotingData.LeaseManager;
				}
			}
			return leaseManager;
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0013A668 File Offset: 0x00138868
		internal static LeaseManager GetLeaseManager()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			return remotingData.LeaseManager;
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0013A688 File Offset: 0x00138888
		[SecurityCritical]
		private LeaseManager(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
			this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
			this.waitHandle = new AutoResetEvent(false);
			this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, null, -1, -1);
			this.leaseTimer.Change((int)pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x0013A710 File Offset: 0x00138910
		internal void ChangePollTime(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x0013A71C File Offset: 0x0013891C
		internal void ActivateLease(Lease lease)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable[lease] = lease.leaseTime;
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0013A770 File Offset: 0x00138970
		internal void DeleteLease(Lease lease)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable.Remove(lease);
			}
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x0013A7B8 File Offset: 0x001389B8
		[Conditional("_LOGGING")]
		internal void DumpLeases(Lease[] leases)
		{
			for (int i = 0; i < leases.Length; i++)
			{
			}
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0013A7D4 File Offset: 0x001389D4
		internal ILease GetLease(MarshalByRefObject obj)
		{
			bool flag = true;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity == null)
			{
				return null;
			}
			return identity.Lease;
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0013A7F8 File Offset: 0x001389F8
		internal void ChangedLeaseTime(Lease lease, DateTime newTime)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable[lease] = newTime;
			}
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0013A844 File Offset: 0x00138A44
		internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				DateTime dateTime = DateTime.UtcNow.Add(sponsorshipTimeOut);
				this.sponsorTable[sponsorId] = new LeaseManager.SponsorInfo(lease, sponsorId, dateTime);
			}
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x0013A8A4 File Offset: 0x00138AA4
		internal void DeleteSponsor(object sponsorId)
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				this.sponsorTable.Remove(sponsorId);
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x0013A8EC File Offset: 0x00138AEC
		[SecurityCritical]
		private void LeaseTimeAnalyzer(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.leaseToTimeTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DateTime dateTime = (DateTime)enumerator.Value;
					Lease lease = (Lease)enumerator.Key;
					if (dateTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(lease);
					}
				}
				for (int i = 0; i < this.tempObjects.Count; i++)
				{
					Lease lease2 = (Lease)this.tempObjects[i];
					this.leaseToTimeTable.Remove(lease2);
				}
			}
			for (int j = 0; j < this.tempObjects.Count; j++)
			{
				Lease lease3 = (Lease)this.tempObjects[j];
				if (lease3 != null)
				{
					lease3.LeaseExpired(utcNow);
				}
			}
			this.tempObjects.Clear();
			Hashtable hashtable2 = this.sponsorTable;
			lock (hashtable2)
			{
				IDictionaryEnumerator enumerator2 = this.sponsorTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					object key = enumerator2.Key;
					LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo)enumerator2.Value;
					if (sponsorInfo.sponsorWaitTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(sponsorInfo);
					}
				}
				for (int k = 0; k < this.tempObjects.Count; k++)
				{
					LeaseManager.SponsorInfo sponsorInfo2 = (LeaseManager.SponsorInfo)this.tempObjects[k];
					this.sponsorTable.Remove(sponsorInfo2.sponsorId);
				}
			}
			for (int l = 0; l < this.tempObjects.Count; l++)
			{
				LeaseManager.SponsorInfo sponsorInfo3 = (LeaseManager.SponsorInfo)this.tempObjects[l];
				if (sponsorInfo3 != null && sponsorInfo3.lease != null)
				{
					sponsorInfo3.lease.SponsorTimeout(sponsorInfo3.sponsorId);
					this.tempObjects[l] = null;
				}
			}
			this.tempObjects.Clear();
			this.leaseTimer.Change((int)this.pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x040028B1 RID: 10417
		private Hashtable leaseToTimeTable = new Hashtable();

		// Token: 0x040028B2 RID: 10418
		private Hashtable sponsorTable = new Hashtable();

		// Token: 0x040028B3 RID: 10419
		private TimeSpan pollTime;

		// Token: 0x040028B4 RID: 10420
		private AutoResetEvent waitHandle;

		// Token: 0x040028B5 RID: 10421
		private TimerCallback leaseTimeAnalyzerDelegate;

		// Token: 0x040028B6 RID: 10422
		private volatile Timer leaseTimer;

		// Token: 0x040028B7 RID: 10423
		private ArrayList tempObjects = new ArrayList(10);

		// Token: 0x02000C78 RID: 3192
		internal class SponsorInfo
		{
			// Token: 0x060070BD RID: 28861 RVA: 0x0018475E File Offset: 0x0018295E
			internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
			{
				this.lease = lease;
				this.sponsorId = sponsorId;
				this.sponsorWaitTime = sponsorWaitTime;
			}

			// Token: 0x04003807 RID: 14343
			internal Lease lease;

			// Token: 0x04003808 RID: 14344
			internal object sponsorId;

			// Token: 0x04003809 RID: 14345
			internal DateTime sponsorWaitTime;
		}
	}
}
