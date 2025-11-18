using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.NetCore;

namespace System.Threading
{
	// Token: 0x02000531 RID: 1329
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class Timer : MarshalByRefObject, IDisposable
	{
		// Token: 0x06003E57 RID: 15959 RVA: 0x000E8428 File Offset: 0x000E6628
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			if (dueTime < -1)
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (period < -1)
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.TimerSetup(callback, state, (uint)dueTime, (uint)period, ref stackCrawlMark);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000E8480 File Offset: 0x000E6680
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			long num = (long)dueTime.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (num > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTm", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
			}
			long num2 = (long)period.TotalMilliseconds;
			if (num2 < -1L)
			{
				throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (num2 > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("periodTm", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.TimerSetup(callback, state, (uint)num, (uint)num2, ref stackCrawlMark);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000E8520 File Offset: 0x000E6720
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.TimerSetup(callback, state, dueTime, period, ref stackCrawlMark);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000E8544 File Offset: 0x000E6744
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (dueTime > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
			}
			if (period > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.TimerSetup(callback, state, (uint)dueTime, (uint)period, ref stackCrawlMark);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000E85D4 File Offset: 0x000E67D4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Timer(TimerCallback callback)
		{
			int num = -1;
			int num2 = -1;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.TimerSetup(callback, this, (uint)num, (uint)num2, ref stackCrawlMark);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000E85FC File Offset: 0x000E67FC
		[SecurityCritical]
		private void TimerSetup(TimerCallback callback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("TimerCallback");
			}
			object obj;
			if (Timer.UseNetCoreTimer)
			{
				obj = new TimerQueueTimer(callback, state, dueTime, period, true, ref stackMark);
			}
			else
			{
				obj = new TimerQueueTimer(callback, state, dueTime, period, ref stackMark);
			}
			this.m_timer = new TimerHolder(obj);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000E8647 File Offset: 0x000E6847
		[SecurityCritical]
		internal static void Pause()
		{
			if (Timer.UseNetCoreTimer)
			{
				TimerQueue.PauseAll();
				return;
			}
			TimerQueue.Instance.Pause();
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000E8660 File Offset: 0x000E6860
		[SecurityCritical]
		internal static void Resume()
		{
			if (Timer.UseNetCoreTimer)
			{
				TimerQueue.ResumeAll();
				return;
			}
			TimerQueue.Instance.Resume();
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000E867C File Offset: 0x000E687C
		[__DynamicallyInvokable]
		public bool Change(int dueTime, int period)
		{
			if (dueTime < -1)
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (period < -1)
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.m_timer.Change((uint)dueTime, (uint)period);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000E86C8 File Offset: 0x000E68C8
		[__DynamicallyInvokable]
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000E86E0 File Offset: 0x000E68E0
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			return this.m_timer.Change(dueTime, period);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000E86F0 File Offset: 0x000E68F0
		public bool Change(long dueTime, long period)
		{
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			if (dueTime > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTime", Environment.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge"));
			}
			if (period > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("period", Environment.GetResourceString("ArgumentOutOfRange_PeriodTooLarge"));
			}
			return this.m_timer.Change((uint)dueTime, (uint)period);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000E8776 File Offset: 0x000E6976
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			return this.m_timer.Close(notifyObject);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000E8792 File Offset: 0x000E6992
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.m_timer.Close();
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000E879F File Offset: 0x000E699F
		internal void KeepRootedWhileScheduled()
		{
			GC.SuppressFinalize(this.m_timer);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000E87AC File Offset: 0x000E69AC
		// Note: this type is marked as 'beforefieldinit'.
		static Timer()
		{
		}

		// Token: 0x04001A47 RID: 6727
		internal static readonly bool UseNetCoreTimer = AppContextSwitches.UseNetCoreTimer;

		// Token: 0x04001A48 RID: 6728
		private const uint MAX_SUPPORTED_TIMEOUT = 4294967294U;

		// Token: 0x04001A49 RID: 6729
		private TimerHolder m_timer;
	}
}
