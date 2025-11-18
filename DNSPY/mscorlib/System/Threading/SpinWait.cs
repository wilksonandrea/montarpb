using System;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000538 RID: 1336
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinWait
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000E961B File Offset: 0x000E781B
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x000E9623 File Offset: 0x000E7823
		[__DynamicallyInvokable]
		public bool NextSpinWillYield
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count > 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x000E9638 File Offset: 0x000E7838
		[__DynamicallyInvokable]
		public void SpinOnce()
		{
			if (this.NextSpinWillYield)
			{
				CdsSyncEtwBCLProvider.Log.SpinWait_NextSpinWillYield();
				int num = ((this.m_count >= 10) ? (this.m_count - 10) : this.m_count);
				if (num % 20 == 19)
				{
					Thread.Sleep(1);
				}
				else if (num % 5 == 4)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
			}
			else
			{
				Thread.SpinWait(4 << this.m_count);
			}
			this.m_count = ((this.m_count == int.MaxValue) ? 10 : (this.m_count + 1));
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x000E96C8 File Offset: 0x000E78C8
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.m_count = 0;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000E96D1 File Offset: 0x000E78D1
		[__DynamicallyInvokable]
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000E96DC File Offset: 0x000E78DC
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			return SpinWait.SpinUntil(condition, (int)timeout.TotalMilliseconds);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000E972C File Offset: 0x000E792C
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", Environment.GetResourceString("SpinWait_SpinUntil_ArgumentNull"));
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001A63 RID: 6755
		internal const int YIELD_THRESHOLD = 10;

		// Token: 0x04001A64 RID: 6756
		internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;

		// Token: 0x04001A65 RID: 6757
		internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;

		// Token: 0x04001A66 RID: 6758
		private int m_count;
	}
}
