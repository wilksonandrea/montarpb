using System;
using System.Threading;

namespace Plugin.Core.Utility
{
	// Token: 0x02000039 RID: 57
	public class TimerState
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00002C95 File Offset: 0x00000E95
		public TimerState()
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public bool IsTimer()
		{
			return this.Timer != null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00018D50 File Offset: 0x00016F50
		public void StartJob(int Period, TimerCallback Callback)
		{
			object sync = this.Sync;
			lock (sync)
			{
				this.Timer = new Timer(Callback, this, Period, -1);
				this.EndDate = DateTimeUtil.Now().AddMilliseconds((double)Period);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00018DB0 File Offset: 0x00016FB0
		public void StopJob()
		{
			object sync = this.Sync;
			lock (sync)
			{
				if (this.Timer != null)
				{
					this.Timer.Dispose();
					this.Timer = null;
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00018E04 File Offset: 0x00017004
		public void StartTimer(TimeSpan Period, TimerCallback Callback)
		{
			object sync = this.Sync;
			lock (sync)
			{
				this.Timer = new Timer(Callback, this, Period, TimeSpan.Zero);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00018E54 File Offset: 0x00017054
		public int GetTimeLeft()
		{
			if (this.Timer == null)
			{
				return 0;
			}
			int num = (int)ComDiv.GetDuration(this.EndDate);
			if (num >= 0)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x040000A2 RID: 162
		protected Timer Timer;

		// Token: 0x040000A3 RID: 163
		protected DateTime EndDate;

		// Token: 0x040000A4 RID: 164
		protected object Sync = new object();
	}
}
