using System;
using System.Threading;

namespace Plugin.Core.Utility
{
	public class TimerState
	{
		protected System.Threading.Timer Timer;

		protected DateTime EndDate;

		protected object Sync = new object();

		public TimerState()
		{
		}

		public int GetTimeLeft()
		{
			if (this.Timer == null)
			{
				return 0;
			}
			int duration = (int)ComDiv.GetDuration(this.EndDate);
			if (duration >= 0)
			{
				return duration;
			}
			return 0;
		}

		public bool IsTimer()
		{
			return this.Timer != null;
		}

		public void StartJob(int Period, TimerCallback Callback)
		{
			lock (this.Sync)
			{
				this.Timer = new System.Threading.Timer(Callback, this, Period, -1);
				DateTime dateTime = DateTimeUtil.Now();
				this.EndDate = dateTime.AddMilliseconds((double)Period);
			}
		}

		public void StartTimer(TimeSpan Period, TimerCallback Callback)
		{
			lock (this.Sync)
			{
				this.Timer = new System.Threading.Timer(Callback, this, Period, TimeSpan.Zero);
			}
		}

		public void StopJob()
		{
			lock (this.Sync)
			{
				if (this.Timer != null)
				{
					this.Timer.Dispose();
					this.Timer = null;
				}
			}
		}
	}
}