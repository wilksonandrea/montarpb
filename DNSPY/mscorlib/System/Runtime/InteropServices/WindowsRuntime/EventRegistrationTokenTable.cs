using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E3 RID: 2531
	[__DynamicallyInvokable]
	public sealed class EventRegistrationTokenTable<T> where T : class
	{
		// Token: 0x0600647C RID: 25724 RVA: 0x001564F8 File Offset: 0x001546F8
		[__DynamicallyInvokable]
		public EventRegistrationTokenTable()
		{
			if (!typeof(Delegate).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EventTokenTableRequiresDelegate", new object[] { typeof(T) }));
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x00156554 File Offset: 0x00154754
		// (set) Token: 0x0600647E RID: 25726 RVA: 0x00156560 File Offset: 0x00154760
		[__DynamicallyInvokable]
		public T InvocationList
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_invokeList;
			}
			[__DynamicallyInvokable]
			set
			{
				Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
				lock (tokens)
				{
					this.m_tokens.Clear();
					this.m_invokeList = default(T);
					if (value != null)
					{
						this.AddEventHandlerNoLock(value);
					}
				}
			}
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x001565C8 File Offset: 0x001547C8
		[__DynamicallyInvokable]
		public EventRegistrationToken AddEventHandler(T handler)
		{
			if (handler == null)
			{
				return new EventRegistrationToken(0UL);
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			EventRegistrationToken eventRegistrationToken;
			lock (tokens)
			{
				eventRegistrationToken = this.AddEventHandlerNoLock(handler);
			}
			return eventRegistrationToken;
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x0015661C File Offset: 0x0015481C
		private EventRegistrationToken AddEventHandlerNoLock(T handler)
		{
			EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
			while (this.m_tokens.ContainsKey(preferredToken))
			{
				preferredToken = new EventRegistrationToken(preferredToken.Value + 1UL);
			}
			this.m_tokens[preferredToken] = handler;
			Delegate @delegate = (Delegate)((object)this.m_invokeList);
			@delegate = Delegate.Combine(@delegate, (Delegate)((object)handler));
			this.m_invokeList = (T)((object)@delegate);
			return preferredToken;
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x00156694 File Offset: 0x00154894
		[FriendAccessAllowed]
		internal T ExtractHandler(EventRegistrationToken token)
		{
			T t = default(T);
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				if (this.m_tokens.TryGetValue(token, out t))
				{
					this.RemoveEventHandlerNoLock(token);
				}
			}
			return t;
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001566F0 File Offset: 0x001548F0
		private static EventRegistrationToken GetPreferredToken(T handler)
		{
			Delegate[] invocationList = ((Delegate)((object)handler)).GetInvocationList();
			uint num;
			if (invocationList.Length == 1)
			{
				num = (uint)invocationList[0].Method.GetHashCode();
			}
			else
			{
				num = (uint)handler.GetHashCode();
			}
			ulong num2 = ((ulong)typeof(T).MetadataToken << 32) | (ulong)num;
			return new EventRegistrationToken(num2);
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x00156750 File Offset: 0x00154950
		[__DynamicallyInvokable]
		public void RemoveEventHandler(EventRegistrationToken token)
		{
			if (token.Value == 0UL)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				this.RemoveEventHandlerNoLock(token);
			}
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x0015679C File Offset: 0x0015499C
		[__DynamicallyInvokable]
		public void RemoveEventHandler(T handler)
		{
			if (handler == null)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
				T t;
				if (this.m_tokens.TryGetValue(preferredToken, out t) && t == handler)
				{
					this.RemoveEventHandlerNoLock(preferredToken);
				}
				else
				{
					foreach (KeyValuePair<EventRegistrationToken, T> keyValuePair in this.m_tokens)
					{
						if (keyValuePair.Value == (T)((object)handler))
						{
							this.RemoveEventHandlerNoLock(keyValuePair.Key);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00156878 File Offset: 0x00154A78
		private void RemoveEventHandlerNoLock(EventRegistrationToken token)
		{
			T t;
			if (this.m_tokens.TryGetValue(token, out t))
			{
				this.m_tokens.Remove(token);
				Delegate @delegate = (Delegate)((object)this.m_invokeList);
				@delegate = Delegate.Remove(@delegate, (Delegate)((object)t));
				this.m_invokeList = (T)((object)@delegate);
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x001568D5 File Offset: 0x00154AD5
		[__DynamicallyInvokable]
		public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
		{
			if (refEventTable == null)
			{
				Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), null);
			}
			return refEventTable;
		}

		// Token: 0x04002CF8 RID: 11512
		private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();

		// Token: 0x04002CF9 RID: 11513
		private volatile T m_invokeList;
	}
}
