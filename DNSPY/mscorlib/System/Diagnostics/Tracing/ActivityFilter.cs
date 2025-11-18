using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042C RID: 1068
	internal sealed class ActivityFilter : IDisposable
	{
		// Token: 0x0600354F RID: 13647 RVA: 0x000CE5AC File Offset: 0x000CC7AC
		public static void DisableFilter(ref ActivityFilter filterList, EventSource source)
		{
			if (filterList == null)
			{
				return;
			}
			ActivityFilter activityFilter = filterList;
			ActivityFilter activityFilter2 = activityFilter.m_next;
			while (activityFilter2 != null)
			{
				if (activityFilter2.m_providerGuid == source.Guid)
				{
					if (activityFilter2.m_eventId >= 0 && activityFilter2.m_eventId < source.m_eventData.Length)
					{
						EventSource.EventMetadata[] eventData = source.m_eventData;
						int eventId = activityFilter2.m_eventId;
						eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking - 1;
					}
					activityFilter.m_next = activityFilter2.m_next;
					activityFilter2.Dispose();
					activityFilter2 = activityFilter.m_next;
				}
				else
				{
					activityFilter = activityFilter2;
					activityFilter2 = activityFilter.m_next;
				}
			}
			if (filterList.m_providerGuid == source.Guid)
			{
				if (filterList.m_eventId >= 0 && filterList.m_eventId < source.m_eventData.Length)
				{
					EventSource.EventMetadata[] eventData2 = source.m_eventData;
					int eventId2 = filterList.m_eventId;
					eventData2[eventId2].TriggersActivityTracking = eventData2[eventId2].TriggersActivityTracking - 1;
				}
				ActivityFilter activityFilter3 = filterList;
				filterList = activityFilter3.m_next;
				activityFilter3.Dispose();
			}
			if (filterList != null)
			{
				ActivityFilter.EnsureActivityCleanupDelegate(filterList);
			}
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000CE6AC File Offset: 0x000CC8AC
		public static void UpdateFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, string startEvents)
		{
			ActivityFilter.DisableFilter(ref filterList, source);
			if (!string.IsNullOrEmpty(startEvents))
			{
				foreach (string text in startEvents.Split(new char[] { ' ' }))
				{
					int num = 1;
					int num2 = -1;
					int num3 = text.IndexOf(':');
					if (num3 < 0)
					{
						source.ReportOutOfBandMessage("ERROR: Invalid ActivitySamplingStartEvent specification: " + text, false);
					}
					else
					{
						string text2 = text.Substring(num3 + 1);
						if (!int.TryParse(text2, out num))
						{
							source.ReportOutOfBandMessage("ERROR: Invalid sampling frequency specification: " + text2, false);
						}
						else
						{
							text = text.Substring(0, num3);
							if (!int.TryParse(text, out num2))
							{
								num2 = -1;
								for (int j = 0; j < source.m_eventData.Length; j++)
								{
									EventSource.EventMetadata[] eventData = source.m_eventData;
									if (eventData[j].Name != null && eventData[j].Name.Length == text.Length && string.Compare(eventData[j].Name, text, StringComparison.OrdinalIgnoreCase) == 0)
									{
										num2 = eventData[j].Descriptor.EventId;
										break;
									}
								}
							}
							if (num2 < 0 || num2 >= source.m_eventData.Length)
							{
								source.ReportOutOfBandMessage("ERROR: Invalid eventId specification: " + text, false);
							}
							else
							{
								ActivityFilter.EnableFilter(ref filterList, source, perEventSourceSessionId, num2, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000CE818 File Offset: 0x000CCA18
		public static ActivityFilter GetFilter(ActivityFilter filterList, EventSource source)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_providerGuid == source.Guid && activityFilter.m_samplingFreq != -1)
				{
					return activityFilter;
				}
			}
			return null;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000CE854 File Offset: 0x000CCA54
		[SecurityCritical]
		public unsafe static bool PassesActivityFilter(ActivityFilter filterList, Guid* childActivityID, bool triggeringEvent, EventSource source, int eventId)
		{
			bool flag = false;
			if (triggeringEvent)
			{
				ActivityFilter activityFilter = filterList;
				while (activityFilter != null)
				{
					if (eventId == activityFilter.m_eventId && source.Guid == activityFilter.m_providerGuid)
					{
						int curSampleCount;
						int num;
						do
						{
							curSampleCount = activityFilter.m_curSampleCount;
							if (curSampleCount <= 1)
							{
								num = activityFilter.m_samplingFreq;
							}
							else
							{
								num = curSampleCount - 1;
							}
						}
						while (Interlocked.CompareExchange(ref activityFilter.m_curSampleCount, num, curSampleCount) != curSampleCount);
						if (curSampleCount <= 1)
						{
							Guid internalCurrentThreadActivityId = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple;
							if (!activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId, out tuple))
							{
								flag = true;
								activityFilter.m_activeActivities[internalCurrentThreadActivityId] = Environment.TickCount;
								activityFilter.m_rootActiveActivities[internalCurrentThreadActivityId] = Tuple.Create<Guid, int>(source.Guid, eventId);
								break;
							}
							break;
						}
						else
						{
							Guid internalCurrentThreadActivityId2 = EventSource.InternalCurrentThreadActivityId;
							Tuple<Guid, int> tuple2;
							if (activityFilter.m_rootActiveActivities.TryGetValue(internalCurrentThreadActivityId2, out tuple2) && tuple2.Item1 == source.Guid && tuple2.Item2 == eventId)
							{
								int num2;
								activityFilter.m_activeActivities.TryRemove(internalCurrentThreadActivityId2, out num2);
								break;
							}
							break;
						}
					}
					else
					{
						activityFilter = activityFilter.m_next;
					}
				}
			}
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (activeActivities != null)
			{
				if (!flag)
				{
					flag = !activeActivities.IsEmpty && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
				}
				if (flag && childActivityID != null && source.m_eventData[eventId].Descriptor.Opcode == 9)
				{
					ActivityFilter.FlowActivityIfNeeded(filterList, null, childActivityID);
				}
			}
			return flag;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000CE9B8 File Offset: 0x000CCBB8
		[SecuritySafeCritical]
		public static bool IsCurrentActivityActive(ActivityFilter filterList)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			return activeActivities != null && activeActivities.ContainsKey(EventSource.InternalCurrentThreadActivityId);
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000CE9E0 File Offset: 0x000CCBE0
		[SecurityCritical]
		public unsafe static void FlowActivityIfNeeded(ActivityFilter filterList, Guid* currentActivityId, Guid* childActivityID)
		{
			ConcurrentDictionary<Guid, int> activeActivities = ActivityFilter.GetActiveActivities(filterList);
			if (currentActivityId != null && !activeActivities.ContainsKey(*currentActivityId))
			{
				return;
			}
			if (activeActivities.Count > 100000)
			{
				ActivityFilter.TrimActiveActivityStore(activeActivities);
				activeActivities[EventSource.InternalCurrentThreadActivityId] = Environment.TickCount;
			}
			activeActivities[*childActivityID] = Environment.TickCount;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000CEA3C File Offset: 0x000CCC3C
		public static void UpdateKwdTriggers(ActivityFilter activityFilter, Guid sourceGuid, EventSource source, EventKeywords sessKeywords)
		{
			for (ActivityFilter activityFilter2 = activityFilter; activityFilter2 != null; activityFilter2 = activityFilter2.m_next)
			{
				if (sourceGuid == activityFilter2.m_providerGuid && (source.m_eventData[activityFilter2.m_eventId].TriggersActivityTracking > 0 || source.m_eventData[activityFilter2.m_eventId].Descriptor.Opcode == 9))
				{
					source.m_keywordTriggers |= source.m_eventData[activityFilter2.m_eventId].Descriptor.Keywords & (long)sessKeywords;
				}
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000CEACD File Offset: 0x000CCCCD
		public IEnumerable<Tuple<int, int>> GetFilterAsTuple(Guid sourceGuid)
		{
			ActivityFilter af;
			for (af = this; af != null; af = af.m_next)
			{
				if (af.m_providerGuid == sourceGuid)
				{
					yield return Tuple.Create<int, int>(af.m_eventId, af.m_samplingFreq);
				}
			}
			af = null;
			yield break;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000CEAE4 File Offset: 0x000CCCE4
		public void Dispose()
		{
			if (this.m_myActivityDelegate != null)
			{
				EventSource.s_activityDying = (Action<Guid>)Delegate.Remove(EventSource.s_activityDying, this.m_myActivityDelegate);
				this.m_myActivityDelegate = null;
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000CEB10 File Offset: 0x000CCD10
		private ActivityFilter(EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq, ActivityFilter existingFilter = null)
		{
			this.m_providerGuid = source.Guid;
			this.m_perEventSourceSessionId = perEventSourceSessionId;
			this.m_eventId = eventId;
			this.m_samplingFreq = samplingFreq;
			this.m_next = existingFilter;
			ConcurrentDictionary<Guid, int> activeActivities;
			if (existingFilter == null || (activeActivities = ActivityFilter.GetActiveActivities(existingFilter)) == null)
			{
				this.m_activeActivities = new ConcurrentDictionary<Guid, int>();
				this.m_rootActiveActivities = new ConcurrentDictionary<Guid, Tuple<Guid, int>>();
				this.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(this);
				EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, this.m_myActivityDelegate);
				return;
			}
			this.m_activeActivities = activeActivities;
			this.m_rootActiveActivities = existingFilter.m_rootActiveActivities;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000CEBB0 File Offset: 0x000CCDB0
		private static void EnsureActivityCleanupDelegate(ActivityFilter filterList)
		{
			if (filterList == null)
			{
				return;
			}
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_myActivityDelegate != null)
				{
					return;
				}
			}
			filterList.m_myActivityDelegate = ActivityFilter.GetActivityDyingDelegate(filterList);
			EventSource.s_activityDying = (Action<Guid>)Delegate.Combine(EventSource.s_activityDying, filterList.m_myActivityDelegate);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000CEC00 File Offset: 0x000CCE00
		private static Action<Guid> GetActivityDyingDelegate(ActivityFilter filterList)
		{
			return delegate(Guid oldActivity)
			{
				int num;
				filterList.m_activeActivities.TryRemove(oldActivity, out num);
				Tuple<Guid, int> tuple;
				filterList.m_rootActiveActivities.TryRemove(oldActivity, out tuple);
			};
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000CEC26 File Offset: 0x000CCE26
		private static bool EnableFilter(ref ActivityFilter filterList, EventSource source, int perEventSourceSessionId, int eventId, int samplingFreq)
		{
			filterList = new ActivityFilter(source, perEventSourceSessionId, eventId, samplingFreq, filterList);
			if (0 <= eventId && eventId < source.m_eventData.Length)
			{
				EventSource.EventMetadata[] eventData = source.m_eventData;
				eventData[eventId].TriggersActivityTracking = eventData[eventId].TriggersActivityTracking + 1;
			}
			return true;
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000CEC64 File Offset: 0x000CCE64
		private static void TrimActiveActivityStore(ConcurrentDictionary<Guid, int> activities)
		{
			if (activities.Count > 100000)
			{
				KeyValuePair<Guid, int>[] array = activities.ToArray();
				int tickNow = Environment.TickCount;
				Array.Sort<KeyValuePair<Guid, int>>(array, (KeyValuePair<Guid, int> x, KeyValuePair<Guid, int> y) => (int.MaxValue & (tickNow - y.Value)) - (int.MaxValue & (tickNow - x.Value)));
				for (int i = 0; i < array.Length / 2; i++)
				{
					int num;
					activities.TryRemove(array[i].Key, out num);
				}
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000CECD0 File Offset: 0x000CCED0
		private static ConcurrentDictionary<Guid, int> GetActiveActivities(ActivityFilter filterList)
		{
			for (ActivityFilter activityFilter = filterList; activityFilter != null; activityFilter = activityFilter.m_next)
			{
				if (activityFilter.m_activeActivities != null)
				{
					return activityFilter.m_activeActivities;
				}
			}
			return null;
		}

		// Token: 0x040017B1 RID: 6065
		private ConcurrentDictionary<Guid, int> m_activeActivities;

		// Token: 0x040017B2 RID: 6066
		private ConcurrentDictionary<Guid, Tuple<Guid, int>> m_rootActiveActivities;

		// Token: 0x040017B3 RID: 6067
		private Guid m_providerGuid;

		// Token: 0x040017B4 RID: 6068
		private int m_eventId;

		// Token: 0x040017B5 RID: 6069
		private int m_samplingFreq;

		// Token: 0x040017B6 RID: 6070
		private int m_curSampleCount;

		// Token: 0x040017B7 RID: 6071
		private int m_perEventSourceSessionId;

		// Token: 0x040017B8 RID: 6072
		private const int MaxActivityTrackCount = 100000;

		// Token: 0x040017B9 RID: 6073
		private ActivityFilter m_next;

		// Token: 0x040017BA RID: 6074
		private Action<Guid> m_myActivityDelegate;

		// Token: 0x02000B90 RID: 2960
		[CompilerGenerated]
		private sealed class <GetFilterAsTuple>d__7 : IEnumerable<Tuple<int, int>>, IEnumerable, IEnumerator<Tuple<int, int>>, IDisposable, IEnumerator
		{
			// Token: 0x06006C84 RID: 27780 RVA: 0x00177A66 File Offset: 0x00175C66
			[DebuggerHidden]
			public <GetFilterAsTuple>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006C85 RID: 27781 RVA: 0x00177A80 File Offset: 0x00175C80
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006C86 RID: 27782 RVA: 0x00177A84 File Offset: 0x00175C84
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ActivityFilter activityFilter = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					af = activityFilter;
					goto IL_81;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_70:
				af = af.m_next;
				IL_81:
				if (af == null)
				{
					af = null;
					return false;
				}
				if (af.m_providerGuid == sourceGuid)
				{
					this.<>2__current = Tuple.Create<int, int>(af.m_eventId, af.m_samplingFreq);
					this.<>1__state = 1;
					return true;
				}
				goto IL_70;
			}

			// Token: 0x1700125E RID: 4702
			// (get) Token: 0x06006C87 RID: 27783 RVA: 0x00177B22 File Offset: 0x00175D22
			Tuple<int, int> IEnumerator<Tuple<int, int>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006C88 RID: 27784 RVA: 0x00177B2A File Offset: 0x00175D2A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700125F RID: 4703
			// (get) Token: 0x06006C89 RID: 27785 RVA: 0x00177B31 File Offset: 0x00175D31
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006C8A RID: 27786 RVA: 0x00177B3C File Offset: 0x00175D3C
			[DebuggerHidden]
			IEnumerator<Tuple<int, int>> IEnumerable<Tuple<int, int>>.GetEnumerator()
			{
				ActivityFilter.<GetFilterAsTuple>d__7 <GetFilterAsTuple>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetFilterAsTuple>d__ = this;
				}
				else
				{
					<GetFilterAsTuple>d__ = new ActivityFilter.<GetFilterAsTuple>d__7(0);
					<GetFilterAsTuple>d__.<>4__this = this;
				}
				<GetFilterAsTuple>d__.sourceGuid = sourceGuid;
				return <GetFilterAsTuple>d__;
			}

			// Token: 0x06006C8B RID: 27787 RVA: 0x00177B8B File Offset: 0x00175D8B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Tuple<System.Int32,System.Int32>>.GetEnumerator();
			}

			// Token: 0x04003514 RID: 13588
			private int <>1__state;

			// Token: 0x04003515 RID: 13589
			private Tuple<int, int> <>2__current;

			// Token: 0x04003516 RID: 13590
			private int <>l__initialThreadId;

			// Token: 0x04003517 RID: 13591
			public ActivityFilter <>4__this;

			// Token: 0x04003518 RID: 13592
			private Guid sourceGuid;

			// Token: 0x04003519 RID: 13593
			public Guid <>3__sourceGuid;

			// Token: 0x0400351A RID: 13594
			private ActivityFilter <af>5__2;
		}

		// Token: 0x02000B91 RID: 2961
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06006C8C RID: 27788 RVA: 0x00177B93 File Offset: 0x00175D93
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06006C8D RID: 27789 RVA: 0x00177B9C File Offset: 0x00175D9C
			internal void <GetActivityDyingDelegate>b__0(Guid oldActivity)
			{
				int num;
				this.filterList.m_activeActivities.TryRemove(oldActivity, out num);
				Tuple<Guid, int> tuple;
				this.filterList.m_rootActiveActivities.TryRemove(oldActivity, out tuple);
			}

			// Token: 0x0400351B RID: 13595
			public ActivityFilter filterList;
		}

		// Token: 0x02000B92 RID: 2962
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06006C8E RID: 27790 RVA: 0x00177BD1 File Offset: 0x00175DD1
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06006C8F RID: 27791 RVA: 0x00177BD9 File Offset: 0x00175DD9
			internal int <TrimActiveActivityStore>b__0(KeyValuePair<Guid, int> x, KeyValuePair<Guid, int> y)
			{
				return (int.MaxValue & (this.tickNow - y.Value)) - (int.MaxValue & (this.tickNow - x.Value));
			}

			// Token: 0x0400351C RID: 13596
			public int tickNow;
		}
	}
}
