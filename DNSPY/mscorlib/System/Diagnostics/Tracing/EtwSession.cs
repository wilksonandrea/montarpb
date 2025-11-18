using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042D RID: 1069
	internal class EtwSession
	{
		// Token: 0x0600355E RID: 13662 RVA: 0x000CECFC File Offset: 0x000CCEFC
		public static EtwSession GetEtwSession(int etwSessionId, bool bCreateIfNeeded = false)
		{
			if (etwSessionId < 0)
			{
				return null;
			}
			EtwSession etwSession;
			foreach (WeakReference<EtwSession> weakReference in EtwSession.s_etwSessions)
			{
				if (weakReference.TryGetTarget(out etwSession) && etwSession.m_etwSessionId == etwSessionId)
				{
					return etwSession;
				}
			}
			if (!bCreateIfNeeded)
			{
				return null;
			}
			if (EtwSession.s_etwSessions == null)
			{
				EtwSession.s_etwSessions = new List<WeakReference<EtwSession>>();
			}
			etwSession = new EtwSession(etwSessionId);
			EtwSession.s_etwSessions.Add(new WeakReference<EtwSession>(etwSession));
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
			return etwSession;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000CEDA8 File Offset: 0x000CCFA8
		public static void RemoveEtwSession(EtwSession etwSession)
		{
			if (EtwSession.s_etwSessions == null || etwSession == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession2;
				return wrEtwSession.TryGetTarget(out etwSession2) && etwSession2.m_etwSessionId == etwSession.m_etwSessionId;
			});
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000CEDFC File Offset: 0x000CCFFC
		private static void TrimGlobalList()
		{
			if (EtwSession.s_etwSessions == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return !wrEtwSession.TryGetTarget(out etwSession);
			});
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000CEE30 File Offset: 0x000CD030
		private EtwSession(int etwSessionId)
		{
			this.m_etwSessionId = etwSessionId;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000CEE3F File Offset: 0x000CD03F
		// Note: this type is marked as 'beforefieldinit'.
		static EtwSession()
		{
		}

		// Token: 0x040017BB RID: 6075
		public readonly int m_etwSessionId;

		// Token: 0x040017BC RID: 6076
		public ActivityFilter m_activityFilter;

		// Token: 0x040017BD RID: 6077
		private static List<WeakReference<EtwSession>> s_etwSessions = new List<WeakReference<EtwSession>>();

		// Token: 0x040017BE RID: 6078
		private const int s_thrSessionCount = 16;

		// Token: 0x02000B93 RID: 2963
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06006C90 RID: 27792 RVA: 0x00177C04 File Offset: 0x00175E04
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06006C91 RID: 27793 RVA: 0x00177C0C File Offset: 0x00175E0C
			internal bool <RemoveEtwSession>b__0(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return wrEtwSession.TryGetTarget(out etwSession) && etwSession.m_etwSessionId == this.etwSession.m_etwSessionId;
			}

			// Token: 0x0400351D RID: 13597
			public EtwSession etwSession;
		}

		// Token: 0x02000B94 RID: 2964
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006C92 RID: 27794 RVA: 0x00177C38 File Offset: 0x00175E38
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006C93 RID: 27795 RVA: 0x00177C44 File Offset: 0x00175E44
			public <>c()
			{
			}

			// Token: 0x06006C94 RID: 27796 RVA: 0x00177C4C File Offset: 0x00175E4C
			internal bool <TrimGlobalList>b__2_0(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return !wrEtwSession.TryGetTarget(out etwSession);
			}

			// Token: 0x0400351E RID: 13598
			public static readonly EtwSession.<>c <>9 = new EtwSession.<>c();

			// Token: 0x0400351F RID: 13599
			public static Predicate<WeakReference<EtwSession>> <>9__2_0;
		}
	}
}
