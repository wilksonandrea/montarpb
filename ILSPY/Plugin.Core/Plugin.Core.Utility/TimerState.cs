using System;
using System.Threading;

namespace Plugin.Core.Utility;

public class TimerState
{
	protected Timer Timer;

	protected DateTime EndDate;

	protected object Sync = new object();

	public bool IsTimer()
	{
		return Timer != null;
	}

	public void StartJob(int Period, TimerCallback Callback)
	{
		lock (Sync)
		{
			Timer = new Timer(Callback, this, Period, -1);
			EndDate = DateTimeUtil.Now().AddMilliseconds(Period);
		}
	}

	public void StopJob()
	{
		lock (Sync)
		{
			if (Timer != null)
			{
				Timer.Dispose();
				Timer = null;
			}
		}
	}

	public void StartTimer(TimeSpan Period, TimerCallback Callback)
	{
		lock (Sync)
		{
			Timer = new Timer(Callback, this, Period, TimeSpan.Zero);
		}
	}

	public int GetTimeLeft()
	{
		if (Timer == null)
		{
			return 0;
		}
		int num = (int)ComDiv.GetDuration(EndDate);
		if (num >= 0)
		{
			return num;
		}
		return 0;
	}
}
