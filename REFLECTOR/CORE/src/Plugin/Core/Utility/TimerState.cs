namespace Plugin.Core.Utility
{
    using System;
    using System.Threading;

    public class TimerState
    {
        protected System.Threading.Timer Timer;
        protected DateTime EndDate;
        protected object Sync = new object();

        public int GetTimeLeft()
        {
            if (this.Timer == null)
            {
                return 0;
            }
            int duration = (int) ComDiv.GetDuration(this.EndDate);
            return ((duration < 0) ? 0 : duration);
        }

        public bool IsTimer() => 
            this.Timer != null;

        public void StartJob(int Period, TimerCallback Callback)
        {
            object sync = this.Sync;
            lock (sync)
            {
                this.Timer = new System.Threading.Timer(Callback, this, Period, -1);
                this.EndDate = DateTimeUtil.Now().AddMilliseconds((double) Period);
            }
        }

        public void StartTimer(TimeSpan Period, TimerCallback Callback)
        {
            object sync = this.Sync;
            lock (sync)
            {
                this.Timer = new System.Threading.Timer(Callback, this, Period, TimeSpan.Zero);
            }
        }

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
    }
}

