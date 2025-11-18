using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000820 RID: 2080
	[ComVisible(true)]
	public interface ILease
	{
		// Token: 0x06005923 RID: 22819
		[SecurityCritical]
		void Register(ISponsor obj, TimeSpan renewalTime);

		// Token: 0x06005924 RID: 22820
		[SecurityCritical]
		void Register(ISponsor obj);

		// Token: 0x06005925 RID: 22821
		[SecurityCritical]
		void Unregister(ISponsor obj);

		// Token: 0x06005926 RID: 22822
		[SecurityCritical]
		TimeSpan Renew(TimeSpan renewalTime);

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005927 RID: 22823
		// (set) Token: 0x06005928 RID: 22824
		TimeSpan RenewOnCallTime
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005929 RID: 22825
		// (set) Token: 0x0600592A RID: 22826
		TimeSpan SponsorshipTimeout
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600592B RID: 22827
		// (set) Token: 0x0600592C RID: 22828
		TimeSpan InitialLeaseTime
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600592D RID: 22829
		TimeSpan CurrentLeaseTime
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x0600592E RID: 22830
		LeaseState CurrentState
		{
			[SecurityCritical]
			get;
		}
	}
}
