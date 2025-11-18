using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200009C RID: 156
	[SecurityCritical]
	internal class AppDomainPauseManager
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		[SecurityCritical]
		public AppDomainPauseManager()
		{
			AppDomainPauseManager.isPaused = false;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		[SecurityCritical]
		static AppDomainPauseManager()
		{
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001D708 File Offset: 0x0001B908
		internal static AppDomainPauseManager Instance
		{
			[SecurityCritical]
			get
			{
				return AppDomainPauseManager.instance;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001D70F File Offset: 0x0001B90F
		[SecurityCritical]
		public void Pausing()
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001D711 File Offset: 0x0001B911
		[SecurityCritical]
		public void Paused()
		{
			if (AppDomainPauseManager.ResumeEvent == null)
			{
				AppDomainPauseManager.ResumeEvent = new ManualResetEvent(false);
			}
			else
			{
				AppDomainPauseManager.ResumeEvent.Reset();
			}
			Timer.Pause();
			AppDomainPauseManager.isPaused = true;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001D73F File Offset: 0x0001B93F
		[SecurityCritical]
		public void Resuming()
		{
			AppDomainPauseManager.isPaused = false;
			AppDomainPauseManager.ResumeEvent.Set();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001D754 File Offset: 0x0001B954
		[SecurityCritical]
		public void Resumed()
		{
			Timer.Resume();
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001D75B File Offset: 0x0001B95B
		internal static bool IsPaused
		{
			[SecurityCritical]
			get
			{
				return AppDomainPauseManager.isPaused;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001D764 File Offset: 0x0001B964
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001D76B File Offset: 0x0001B96B
		internal static ManualResetEvent ResumeEvent
		{
			[CompilerGenerated]
			[SecurityCritical]
			get
			{
				return AppDomainPauseManager.<ResumeEvent>k__BackingField;
			}
			[CompilerGenerated]
			[SecurityCritical]
			set
			{
				AppDomainPauseManager.<ResumeEvent>k__BackingField = value;
			}
		}

		// Token: 0x040003A3 RID: 931
		private static readonly AppDomainPauseManager instance = new AppDomainPauseManager();

		// Token: 0x040003A4 RID: 932
		private static volatile bool isPaused;

		// Token: 0x040003A5 RID: 933
		[CompilerGenerated]
		private static ManualResetEvent <ResumeEvent>k__BackingField;
	}
}
