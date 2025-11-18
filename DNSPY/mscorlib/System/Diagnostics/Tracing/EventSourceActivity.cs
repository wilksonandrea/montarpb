using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000447 RID: 1095
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x06003623 RID: 13859 RVA: 0x000D2444 File Offset: 0x000D0644
		public EventSourceActivity(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			this.eventSource = eventSource;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000D2461 File Offset: 0x000D0661
		public static implicit operator EventSourceActivity(EventSource eventSource)
		{
			return new EventSourceActivity(eventSource);
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000D2469 File Offset: 0x000D0669
		public EventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000D2471 File Offset: 0x000D0671
		public Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000D2479 File Offset: 0x000D0679
		public EventSourceActivity Start<T>(string eventName, EventSourceOptions options, T data)
		{
			return this.Start<T>(eventName, ref options, ref data);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000D2488 File Offset: 0x000D0688
		public EventSourceActivity Start(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000D24B0 File Offset: 0x000D06B0
		public EventSourceActivity Start(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref options, ref emptyStruct);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D24D0 File Offset: 0x000D06D0
		public EventSourceActivity Start<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			return this.Start<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000D24F0 File Offset: 0x000D06F0
		public void Stop<T>(T data)
		{
			this.Stop<T>(null, ref data);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000D24FC File Offset: 0x000D06FC
		public void Stop<T>(string eventName)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Stop<EmptyStruct>(eventName, ref emptyStruct);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D251A File Offset: 0x000D071A
		public void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D2525 File Offset: 0x000D0725
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(this.eventSource, eventName, ref options, ref data);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000D2538 File Offset: 0x000D0738
		public void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(this.eventSource, eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000D2560 File Offset: 0x000D0760
		public void Write(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref options, ref emptyStruct);
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000D2588 File Offset: 0x000D0788
		public void Write(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000D25B6 File Offset: 0x000D07B6
		public void Write<T>(EventSource source, string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(source, eventName, ref options, ref data);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000D25C4 File Offset: 0x000D07C4
		public void Dispose()
		{
			if (this.state == EventSourceActivity.State.Started)
			{
				EmptyStruct emptyStruct = default(EmptyStruct);
				this.Stop<EmptyStruct>(null, ref emptyStruct);
			}
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000D25EC File Offset: 0x000D07EC
		private EventSourceActivity Start<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.eventSource.IsEnabled())
			{
				return this;
			}
			EventSourceActivity eventSourceActivity = new EventSourceActivity(this.eventSource);
			if (!this.eventSource.IsEnabled(options.Level, options.Keywords))
			{
				Guid id = this.Id;
				eventSourceActivity.activityId = Guid.NewGuid();
				eventSourceActivity.startStopOptions = options;
				eventSourceActivity.eventName = eventName;
				eventSourceActivity.startStopOptions.Opcode = EventOpcode.Start;
				this.eventSource.Write<T>(eventName, ref eventSourceActivity.startStopOptions, ref eventSourceActivity.activityId, ref id, ref data);
			}
			else
			{
				eventSourceActivity.activityId = this.Id;
			}
			return eventSourceActivity;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000D2696 File Offset: 0x000D0896
		private void Write<T>(EventSource eventSource, string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (eventName == null)
			{
				throw new ArgumentNullException();
			}
			eventSource.Write<T>(eventName, ref options, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000D26C4 File Offset: 0x000D08C4
		private void Stop<T>(string eventName, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.StartEventWasFired)
			{
				return;
			}
			this.state = EventSourceActivity.State.Stopped;
			if (eventName == null)
			{
				eventName = this.eventName;
				if (eventName.EndsWith("Start"))
				{
					eventName = eventName.Substring(0, eventName.Length - 5);
				}
				eventName += "Stop";
			}
			this.startStopOptions.Opcode = EventOpcode.Stop;
			this.eventSource.Write<T>(eventName, ref this.startStopOptions, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000D274F File Offset: 0x000D094F
		private bool StartEventWasFired
		{
			get
			{
				return this.eventName != null;
			}
		}

		// Token: 0x04001832 RID: 6194
		private readonly EventSource eventSource;

		// Token: 0x04001833 RID: 6195
		private EventSourceOptions startStopOptions;

		// Token: 0x04001834 RID: 6196
		internal Guid activityId;

		// Token: 0x04001835 RID: 6197
		private EventSourceActivity.State state;

		// Token: 0x04001836 RID: 6198
		private string eventName;

		// Token: 0x04001837 RID: 6199
		internal static Guid s_empty;

		// Token: 0x02000B9F RID: 2975
		private enum State
		{
			// Token: 0x0400353B RID: 13627
			Started,
			// Token: 0x0400353C RID: 13628
			Stopped
		}
	}
}
