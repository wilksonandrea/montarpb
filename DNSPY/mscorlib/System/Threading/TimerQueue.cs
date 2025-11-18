using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.NetCore;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x0200052E RID: 1326
	internal class TimerQueue
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000E7A72 File Offset: 0x000E5C72
		public static TimerQueue Instance
		{
			get
			{
				return TimerQueue.s_queue;
			}
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x000E7A79 File Offset: 0x000E5C79
		private TimerQueue()
		{
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000E7A84 File Offset: 0x000E5C84
		private static int TickCount
		{
			[SecuritySafeCritical]
			get
			{
				if (!Environment.IsWindows8OrAbove)
				{
					return Environment.TickCount;
				}
				ulong num;
				if (!Win32Native.QueryUnbiasedInterruptTime(out num))
				{
					throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
				}
				return (int)((uint)(num / 10000UL));
			}
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000E7AC0 File Offset: 0x000E5CC0
		[SecuritySafeCritical]
		private bool EnsureAppDomainTimerFiresBy(uint requestedDuration)
		{
			uint num = Math.Min(requestedDuration, 268435455U);
			if (this.m_isAppDomainTimerScheduled)
			{
				uint num2 = (uint)(TimerQueue.TickCount - this.m_currentAppDomainTimerStartTicks);
				if (num2 >= this.m_currentAppDomainTimerDuration)
				{
					return true;
				}
				uint num3 = this.m_currentAppDomainTimerDuration - num2;
				if (num >= num3)
				{
					return true;
				}
			}
			if (this.m_pauseTicks != 0)
			{
				return true;
			}
			if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
			{
				this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(num, 0);
				if (!this.m_appDomainTimer.IsInvalid)
				{
					this.m_isAppDomainTimerScheduled = true;
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
					this.m_currentAppDomainTimerDuration = num;
					return true;
				}
				return false;
			}
			else
			{
				if (TimerQueue.ChangeAppDomainTimer(this.m_appDomainTimer, num))
				{
					this.m_isAppDomainTimerScheduled = true;
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
					this.m_currentAppDomainTimerDuration = num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000E7B8A File Offset: 0x000E5D8A
		[SecuritySafeCritical]
		internal static void AppDomainTimerCallback(int id)
		{
			if (Timer.UseNetCoreTimer)
			{
				TimerQueue.AppDomainTimerCallback(id);
				return;
			}
			TimerQueue.Instance.FireNextTimers();
		}

		// Token: 0x06003E3D RID: 15933
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern TimerQueue.AppDomainTimerSafeHandle CreateAppDomainTimer(uint dueTime, int id);

		// Token: 0x06003E3E RID: 15934
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool ChangeAppDomainTimer(TimerQueue.AppDomainTimerSafeHandle handle, uint dueTime);

		// Token: 0x06003E3F RID: 15935
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool DeleteAppDomainTimer(IntPtr handle);

		// Token: 0x06003E40 RID: 15936 RVA: 0x000E7BA4 File Offset: 0x000E5DA4
		[SecurityCritical]
		internal void Pause()
		{
			lock (this)
			{
				if (this.m_appDomainTimer != null && !this.m_appDomainTimer.IsInvalid)
				{
					this.m_appDomainTimer.Dispose();
					this.m_appDomainTimer = null;
					this.m_isAppDomainTimerScheduled = false;
					this.m_pauseTicks = TimerQueue.TickCount;
				}
			}
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000E7C14 File Offset: 0x000E5E14
		[SecurityCritical]
		internal void Resume()
		{
			lock (this)
			{
				try
				{
				}
				finally
				{
					int pauseTicks = this.m_pauseTicks;
					this.m_pauseTicks = 0;
					int tickCount = TimerQueue.TickCount;
					int num = tickCount - pauseTicks;
					bool flag2 = false;
					uint num2 = uint.MaxValue;
					for (TimerQueueTimer timerQueueTimer = this.m_timers; timerQueueTimer != null; timerQueueTimer = timerQueueTimer.m_next)
					{
						uint num3;
						if (timerQueueTimer.m_startTicks <= pauseTicks)
						{
							num3 = (uint)(pauseTicks - timerQueueTimer.m_startTicks);
						}
						else
						{
							num3 = (uint)(tickCount - timerQueueTimer.m_startTicks);
						}
						timerQueueTimer.m_dueTime = ((timerQueueTimer.m_dueTime > num3) ? (timerQueueTimer.m_dueTime - num3) : 0U);
						timerQueueTimer.m_startTicks = tickCount;
						if (timerQueueTimer.m_dueTime < num2)
						{
							flag2 = true;
							num2 = timerQueueTimer.m_dueTime;
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num2);
					}
				}
			}
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000E7D00 File Offset: 0x000E5F00
		private void FireNextTimers()
		{
			TimerQueueTimer timerQueueTimer = null;
			lock (this)
			{
				try
				{
				}
				finally
				{
					this.m_isAppDomainTimerScheduled = false;
					bool flag2 = false;
					uint num = uint.MaxValue;
					int tickCount = TimerQueue.TickCount;
					TimerQueueTimer timerQueueTimer2 = this.m_timers;
					while (timerQueueTimer2 != null)
					{
						uint num2 = (uint)(tickCount - timerQueueTimer2.m_startTicks);
						if (num2 >= timerQueueTimer2.m_dueTime)
						{
							TimerQueueTimer next = timerQueueTimer2.m_next;
							if (timerQueueTimer2.m_period != 4294967295U)
							{
								timerQueueTimer2.m_startTicks = tickCount;
								timerQueueTimer2.m_dueTime = timerQueueTimer2.m_period;
								if (timerQueueTimer2.m_dueTime < num)
								{
									flag2 = true;
									num = timerQueueTimer2.m_dueTime;
								}
							}
							else
							{
								this.DeleteTimer(timerQueueTimer2);
							}
							if (timerQueueTimer == null)
							{
								timerQueueTimer = timerQueueTimer2;
							}
							else
							{
								TimerQueue.QueueTimerCompletion(timerQueueTimer2);
							}
							timerQueueTimer2 = next;
						}
						else
						{
							uint num3 = timerQueueTimer2.m_dueTime - num2;
							if (num3 < num)
							{
								flag2 = true;
								num = num3;
							}
							timerQueueTimer2 = timerQueueTimer2.m_next;
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num);
					}
				}
			}
			if (timerQueueTimer != null)
			{
				timerQueueTimer.Fire();
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000E7E1C File Offset: 0x000E601C
		[SecuritySafeCritical]
		private static void QueueTimerCompletion(TimerQueueTimer timer)
		{
			WaitCallback waitCallback = TimerQueue.s_fireQueuedTimerCompletion;
			if (waitCallback == null)
			{
				waitCallback = (TimerQueue.s_fireQueuedTimerCompletion = new WaitCallback(TimerQueue.FireQueuedTimerCompletion));
			}
			ThreadPool.UnsafeQueueUserWorkItem(waitCallback, timer);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000E7E4D File Offset: 0x000E604D
		private static void FireQueuedTimerCompletion(object state)
		{
			((TimerQueueTimer)state).Fire();
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000E7E5C File Offset: 0x000E605C
		public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
		{
			if (timer.m_dueTime == 4294967295U)
			{
				timer.m_next = this.m_timers;
				timer.m_prev = null;
				if (timer.m_next != null)
				{
					timer.m_next.m_prev = timer;
				}
				this.m_timers = timer;
			}
			timer.m_dueTime = dueTime;
			timer.m_period = ((period == 0U) ? uint.MaxValue : period);
			timer.m_startTicks = TimerQueue.TickCount;
			return this.EnsureAppDomainTimerFiresBy(dueTime);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000E7EC8 File Offset: 0x000E60C8
		public void DeleteTimer(TimerQueueTimer timer)
		{
			if (timer.m_dueTime != 4294967295U)
			{
				if (timer.m_next != null)
				{
					timer.m_next.m_prev = timer.m_prev;
				}
				if (timer.m_prev != null)
				{
					timer.m_prev.m_next = timer.m_next;
				}
				if (this.m_timers == timer)
				{
					this.m_timers = timer.m_next;
				}
				timer.m_dueTime = uint.MaxValue;
				timer.m_period = uint.MaxValue;
				timer.m_startTicks = 0;
				timer.m_prev = null;
				timer.m_next = null;
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000E7F48 File Offset: 0x000E6148
		// Note: this type is marked as 'beforefieldinit'.
		static TimerQueue()
		{
		}

		// Token: 0x04001A32 RID: 6706
		private static TimerQueue s_queue = new TimerQueue();

		// Token: 0x04001A33 RID: 6707
		[SecurityCritical]
		private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;

		// Token: 0x04001A34 RID: 6708
		private bool m_isAppDomainTimerScheduled;

		// Token: 0x04001A35 RID: 6709
		private int m_currentAppDomainTimerStartTicks;

		// Token: 0x04001A36 RID: 6710
		private uint m_currentAppDomainTimerDuration;

		// Token: 0x04001A37 RID: 6711
		private TimerQueueTimer m_timers;

		// Token: 0x04001A38 RID: 6712
		private volatile int m_pauseTicks;

		// Token: 0x04001A39 RID: 6713
		private static WaitCallback s_fireQueuedTimerCompletion;

		// Token: 0x02000BFB RID: 3067
		[SecurityCritical]
		internal class AppDomainTimerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06006F8A RID: 28554 RVA: 0x001804F7 File Offset: 0x0017E6F7
			public AppDomainTimerSafeHandle()
				: base(true)
			{
			}

			// Token: 0x06006F8B RID: 28555 RVA: 0x00180500 File Offset: 0x0017E700
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			protected override bool ReleaseHandle()
			{
				return TimerQueue.DeleteAppDomainTimer(this.handle);
			}
		}
	}
}
