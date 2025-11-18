using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000422 RID: 1058
	[__DynamicallyInvokable]
	public class EventListener : IDisposable
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060034F4 RID: 13556 RVA: 0x000CD91C File Offset: 0x000CBB1C
		// (remove) Token: 0x060034F5 RID: 13557 RVA: 0x000CD954 File Offset: 0x000CBB54
		private event EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventSourceCreatedEventArgs> eventHandler = this._EventSourceCreated;
				EventHandler<EventSourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventSourceCreatedEventArgs> eventHandler3 = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventSourceCreatedEventArgs>>(ref this._EventSourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventSourceCreatedEventArgs> eventHandler = this._EventSourceCreated;
				EventHandler<EventSourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventSourceCreatedEventArgs> eventHandler3 = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventSourceCreatedEventArgs>>(ref this._EventSourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060034F6 RID: 13558 RVA: 0x000CD98C File Offset: 0x000CBB8C
		// (remove) Token: 0x060034F7 RID: 13559 RVA: 0x000CD9E4 File Offset: 0x000CBBE4
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			add
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this.CallBackForExistingEventSources(false, value);
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(this._EventSourceCreated, value);
				}
			}
			remove
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(this._EventSourceCreated, value);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060034F8 RID: 13560 RVA: 0x000CDA34 File Offset: 0x000CBC34
		// (remove) Token: 0x060034F9 RID: 13561 RVA: 0x000CDA6C File Offset: 0x000CBC6C
		public event EventHandler<EventWrittenEventArgs> EventWritten
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventWrittenEventArgs> eventHandler = this.EventWritten;
				EventHandler<EventWrittenEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventWrittenEventArgs> eventHandler3 = (EventHandler<EventWrittenEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventWrittenEventArgs>>(ref this.EventWritten, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventWrittenEventArgs> eventHandler = this.EventWritten;
				EventHandler<EventWrittenEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventWrittenEventArgs> eventHandler3 = (EventHandler<EventWrittenEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventWrittenEventArgs>>(ref this.EventWritten, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CDAA1 File Offset: 0x000CBCA1
		[__DynamicallyInvokable]
		public EventListener()
		{
			this.CallBackForExistingEventSources(true, delegate(object obj, EventSourceCreatedEventArgs args)
			{
				args.EventSource.AddListener(this);
			});
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000CDABC File Offset: 0x000CBCBC
		[__DynamicallyInvokable]
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(eventListener);
					}
					else
					{
						EventListener eventListener2 = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener2.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener2 = next;
						}
						return;
						Block_6:
						eventListener2.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000CDB5C File Offset: 0x000CBD5C
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000CDB68 File Offset: 0x000CBD68
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000CDB74 File Offset: 0x000CBD74
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000CDBA0 File Offset: 0x000CBDA0
		[__DynamicallyInvokable]
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000CDBCA File Offset: 0x000CBDCA
		[__DynamicallyInvokable]
		public static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000CDBD4 File Offset: 0x000CBDD4
		[__DynamicallyInvokable]
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
			EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
			if (eventSourceCreated != null)
			{
				eventSourceCreated(this, new EventSourceCreatedEventArgs
				{
					EventSource = eventSource
				});
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000CDC00 File Offset: 0x000CBE00
		[__DynamicallyInvokable]
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
			EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
			if (eventWritten != null)
			{
				eventWritten(this, eventData);
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000CDC20 File Offset: 0x000CBE20
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppDomain.CurrentDomain.ProcessExit += EventListener.DisposeOnShutdown;
					AppDomain.CurrentDomain.DomainUnload += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference weakReference = EventListener.s_EventSources[num2];
						if (!weakReference.IsAlive)
						{
							num = num2;
							weakReference.Target = newEventSource;
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CDD34 File Offset: 0x000CBF34
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						eventSource.Dispose();
					}
				}
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CDDC0 File Offset: 0x000CBFC0
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
			{
				IL_7E:
				while (enumerator.MoveNext())
				{
					WeakReference weakReference = enumerator.Current;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
						{
							eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
						}
						else
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							EventDispatcher next;
							for (;;)
							{
								next = eventDispatcher.m_Next;
								if (next == null)
								{
									goto IL_7E;
								}
								if (next.m_Listener == listenerToRemove)
								{
									break;
								}
								eventDispatcher = next;
							}
							eventDispatcher.m_Next = next.m_Next;
						}
					}
				}
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CDE74 File Offset: 0x000CC074
		[Conditional("DEBUG")]
		internal static void Validate()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					dictionary.Add(next, true);
				}
				int num = -1;
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					num++;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						for (EventDispatcher eventDispatcher = eventSource.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
						{
						}
						foreach (EventListener eventListener in dictionary.Keys)
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							while (eventDispatcher.m_Listener != eventListener)
							{
								eventDispatcher = eventDispatcher.m_Next;
							}
						}
					}
				}
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000CDFC4 File Offset: 0x000CC1C4
		private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback"));
				}
				try
				{
					EventListener.s_CreatingListener = true;
					if (addToListenersList)
					{
						this.m_Next = EventListener.s_Listeners;
						EventListener.s_Listeners = this;
					}
					foreach (WeakReference weakReference in EventListener.s_EventSources.ToArray())
					{
						EventSource eventSource = weakReference.Target as EventSource;
						if (eventSource != null)
						{
							callback(this, new EventSourceCreatedEventArgs
							{
								EventSource = eventSource
							});
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CE090 File Offset: 0x000CC290
		// Note: this type is marked as 'beforefieldinit'.
		static EventListener()
		{
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000CE0A8 File Offset: 0x000CC2A8
		[CompilerGenerated]
		private void <.ctor>b__10_0(object obj, EventSourceCreatedEventArgs args)
		{
			args.EventSource.AddListener(this);
		}

		// Token: 0x04001776 RID: 6006
		private static readonly object s_EventSourceCreatedLock = new object();

		// Token: 0x04001777 RID: 6007
		[CompilerGenerated]
		private EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

		// Token: 0x04001778 RID: 6008
		[CompilerGenerated]
		private EventHandler<EventWrittenEventArgs> EventWritten;

		// Token: 0x04001779 RID: 6009
		internal volatile EventListener m_Next;

		// Token: 0x0400177A RID: 6010
		internal ActivityFilter m_activityFilter;

		// Token: 0x0400177B RID: 6011
		internal static EventListener s_Listeners;

		// Token: 0x0400177C RID: 6012
		internal static List<WeakReference> s_EventSources;

		// Token: 0x0400177D RID: 6013
		private static bool s_CreatingListener = false;

		// Token: 0x0400177E RID: 6014
		private static bool s_EventSourceShutdownRegistered = false;
	}
}
