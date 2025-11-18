using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000826 RID: 2086
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x0600595D RID: 22877 RVA: 0x0013AB34 File Offset: 0x00138D34
		private static TimeSpan GetTimeSpan(ref long ticks)
		{
			return TimeSpan.FromTicks(Volatile.Read(ref ticks));
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x0013AB41 File Offset: 0x00138D41
		private static void SetTimeSpan(ref long ticks, TimeSpan value)
		{
			Volatile.Write(ref ticks, value.Ticks);
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600595F RID: 22879 RVA: 0x0013AB50 File Offset: 0x00138D50
		private static object LifetimeSyncObject
		{
			get
			{
				if (LifetimeServices.s_LifetimeSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref LifetimeServices.s_LifetimeSyncObject, obj, null);
				}
				return LifetimeServices.s_LifetimeSyncObject;
			}
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0013AB7C File Offset: 0x00138D7C
		[Obsolete("Do not create instances of the LifetimeServices class.  Call the static methods directly on this type instead", true)]
		public LifetimeServices()
		{
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005961 RID: 22881 RVA: 0x0013AB84 File Offset: 0x00138D84
		// (set) Token: 0x06005962 RID: 22882 RVA: 0x0013AB90 File Offset: 0x00138D90
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_leaseTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isLeaseTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "LeaseTime" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_leaseTimeTicks, value);
					LifetimeServices.s_isLeaseTime = true;
				}
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x0013AC00 File Offset: 0x00138E00
		// (set) Token: 0x06005964 RID: 22884 RVA: 0x0013AC0C File Offset: 0x00138E0C
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isRenewOnCallTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "RenewOnCallTime" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks, value);
					LifetimeServices.s_isRenewOnCallTime = true;
				}
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x0013AC7C File Offset: 0x00138E7C
		// (set) Token: 0x06005966 RID: 22886 RVA: 0x0013AC88 File Offset: 0x00138E88
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isSponsorshipTimeout)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "SponsorshipTimeout" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks, value);
					LifetimeServices.s_isSponsorshipTimeout = true;
				}
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06005967 RID: 22887 RVA: 0x0013ACF8 File Offset: 0x00138EF8
		// (set) Token: 0x06005968 RID: 22888 RVA: 0x0013AD04 File Offset: 0x00138F04
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_pollTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_pollTimeTicks, value);
					if (LeaseManager.IsInitialized())
					{
						LeaseManager.GetLeaseManager().ChangePollTime(value);
					}
				}
			}
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0013AD5C File Offset: 0x00138F5C
		[SecurityCritical]
		internal static ILease GetLeaseInitial(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			ILease lease = leaseManager.GetLease(obj);
			if (lease == null)
			{
				lease = LifetimeServices.CreateLease(obj);
			}
			return lease;
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0013AD8C File Offset: 0x00138F8C
		[SecurityCritical]
		internal static ILease GetLease(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return leaseManager.GetLease(obj);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0013ADAF File Offset: 0x00138FAF
		[SecurityCritical]
		internal static ILease CreateLease(MarshalByRefObject obj)
		{
			return LifetimeServices.CreateLease(LifetimeServices.LeaseTime, LifetimeServices.RenewOnCallTime, LifetimeServices.SponsorshipTimeout, obj);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0013ADC6 File Offset: 0x00138FC6
		[SecurityCritical]
		internal static ILease CreateLease(TimeSpan leaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject obj)
		{
			LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return new Lease(leaseTime, renewOnCallTime, sponsorshipTimeout, obj);
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0013ADDC File Offset: 0x00138FDC
		// Note: this type is marked as 'beforefieldinit'.
		static LifetimeServices()
		{
		}

		// Token: 0x040028BE RID: 10430
		private static bool s_isLeaseTime = false;

		// Token: 0x040028BF RID: 10431
		private static bool s_isRenewOnCallTime = false;

		// Token: 0x040028C0 RID: 10432
		private static bool s_isSponsorshipTimeout = false;

		// Token: 0x040028C1 RID: 10433
		private static long s_leaseTimeTicks = TimeSpan.FromMinutes(5.0).Ticks;

		// Token: 0x040028C2 RID: 10434
		private static long s_renewOnCallTimeTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x040028C3 RID: 10435
		private static long s_sponsorshipTimeoutTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x040028C4 RID: 10436
		private static long s_pollTimeTicks = TimeSpan.FromMilliseconds(10000.0).Ticks;

		// Token: 0x040028C5 RID: 10437
		private static object s_LifetimeSyncObject = null;
	}
}
