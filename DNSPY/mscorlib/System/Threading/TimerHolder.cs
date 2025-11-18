using System;
using System.Runtime.CompilerServices;
using System.Threading.NetCore;

namespace System.Threading
{
	// Token: 0x02000530 RID: 1328
	internal sealed class TimerHolder
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x000E831D File Offset: 0x000E651D
		public TimerHolder(object timer)
		{
			this.m_timer = timer;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000E832C File Offset: 0x000E652C
		~TimerHolder()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				if (Timer.UseNetCoreTimer)
				{
					this.NetCoreTimer.Close();
				}
				else
				{
					this.NetFxTimer.Close();
				}
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000E8388 File Offset: 0x000E6588
		private TimerQueueTimer NetFxTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000E8395 File Offset: 0x000E6595
		private TimerQueueTimer NetCoreTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000E83A2 File Offset: 0x000E65A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Change(uint dueTime, uint period)
		{
			if (!Timer.UseNetCoreTimer)
			{
				return this.NetFxTimer.Change(dueTime, period);
			}
			return this.NetCoreTimer.Change(dueTime, period);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000E83C6 File Offset: 0x000E65C6
		public void Close()
		{
			if (Timer.UseNetCoreTimer)
			{
				this.NetCoreTimer.Close();
			}
			else
			{
				this.NetFxTimer.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000E83F0 File Offset: 0x000E65F0
		public bool Close(WaitHandle notifyObject)
		{
			bool flag = (Timer.UseNetCoreTimer ? this.NetCoreTimer.Close(notifyObject) : this.NetFxTimer.Close(notifyObject));
			GC.SuppressFinalize(this);
			return flag;
		}

		// Token: 0x04001A46 RID: 6726
		private object m_timer;
	}
}
