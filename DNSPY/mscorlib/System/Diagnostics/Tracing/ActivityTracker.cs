using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041B RID: 1051
	internal class ActivityTracker
	{
		// Token: 0x0600343D RID: 13373 RVA: 0x000C6B60 File Offset: 0x000C4D60
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
		{
			if (this.m_current == null)
			{
				if (this.m_checkedForEnable)
				{
					return;
				}
				this.m_checkedForEnable = true;
				if (TplEtwProvider.Log.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
				{
					this.Enable();
				}
				if (this.m_current == null)
				{
					return;
				}
			}
			ActivityTracker.ActivityInfo activityInfo = this.m_current.Value;
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartEnter", text);
				log.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
			}
			if (activityInfo != null)
			{
				if (activityInfo.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (log.Debug)
					{
						log.DebugFacilityMessage("OnStartRET", "Fail");
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None)
				{
					ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, activityInfo);
					if (activityInfo2 != null)
					{
						this.OnStop(providerName, activityName, task, ref activityId);
						activityInfo = this.m_current.Value;
					}
				}
			}
			long num;
			if (activityInfo == null)
			{
				num = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				num = Interlocked.Increment(ref activityInfo.m_lastChildID);
			}
			relatedActivityId = EventSource.CurrentThreadActivityId;
			ActivityTracker.ActivityInfo activityInfo3 = new ActivityTracker.ActivityInfo(text, num, activityInfo, relatedActivityId, options);
			this.m_current.Value = activityInfo3;
			activityId = activityInfo3.ActivityId;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo3));
				log.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
			}
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000C6CE8 File Offset: 0x000C4EE8
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopEnter", text);
				log.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_9;
				}
			}
			activityId = Guid.Empty;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRET", "Fail");
			}
			return;
			Block_9:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
				log.DebugFacilityMessage("OnStopRet", activityId.ToString());
			}
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000C6E2C File Offset: 0x000C502C
		[SecuritySafeCritical]
		public void Enable()
		{
			if (this.m_current == null)
			{
				this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000C6E4D File Offset: 0x000C504D
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000C6E54 File Offset: 0x000C5054
		private Guid CurrentActivityId
		{
			get
			{
				return this.m_current.Value.ActivityId;
			}
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000C6E68 File Offset: 0x000C5068
		private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000C6E9C File Offset: 0x000C509C
		private string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Start".Length);
			}
			else if (activityName.EndsWith("Stop"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
			}
			else if (task != 0)
			{
				activityName = "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000C6F18 File Offset: 0x000C5118
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			ActivityTracker.ActivityInfo activityInfo = args.CurrentValue;
			ActivityTracker.ActivityInfo previousValue = args.PreviousValue;
			if (previousValue != null && previousValue.m_creator == activityInfo && (activityInfo == null || previousValue.m_activityIdToRestore != activityInfo.ActivityId))
			{
				EventSource.SetCurrentThreadActivityId(previousValue.m_activityIdToRestore);
				return;
			}
			while (activityInfo != null)
			{
				if (activityInfo.m_stopped == 0)
				{
					EventSource.SetCurrentThreadActivityId(activityInfo.ActivityId);
					return;
				}
				activityInfo = activityInfo.m_creator;
			}
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000C6F81 File Offset: 0x000C5181
		public ActivityTracker()
		{
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000C6F89 File Offset: 0x000C5189
		// Note: this type is marked as 'beforefieldinit'.
		static ActivityTracker()
		{
		}

		// Token: 0x04001728 RID: 5928
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x04001729 RID: 5929
		private bool m_checkedForEnable;

		// Token: 0x0400172A RID: 5930
		private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x0400172B RID: 5931
		private static long m_nextId = 0L;

		// Token: 0x0400172C RID: 5932
		private const ushort MAX_ACTIVITY_DEPTH = 100;

		// Token: 0x02000B87 RID: 2951
		private class ActivityInfo
		{
			// Token: 0x06006C68 RID: 27752 RVA: 0x00177294 File Offset: 0x00175494
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, Guid activityIDToRestore, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.m_activityIdToRestore = activityIDToRestore;
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x06006C69 RID: 27753 RVA: 0x001772F2 File Offset: 0x001754F2
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x06006C6A RID: 27754 RVA: 0x001772FC File Offset: 0x001754FC
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId.ToString();
			}

			// Token: 0x06006C6B RID: 27755 RVA: 0x00177338 File Offset: 0x00175538
			public override string ToString()
			{
				string text = "";
				if (this.m_stopped != 0)
				{
					text = ",DEAD";
				}
				return string.Concat(new string[]
				{
					this.m_name,
					"(",
					ActivityTracker.ActivityInfo.Path(this),
					text,
					")"
				});
			}

			// Token: 0x06006C6C RID: 27756 RVA: 0x0017738A File Offset: 0x0017558A
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x06006C6D RID: 27757 RVA: 0x001773B0 File Offset: 0x001755B0
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x06006C6E RID: 27758 RVA: 0x001773C0 File Offset: 0x001755C0
			[SecuritySafeCritical]
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					Guid* ptr2 = ptr;
					int num = 0;
					if (this.m_creator != null)
					{
						num = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int domainID = Thread.GetDomainID();
						num = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)domainID, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(ptr2);
					}
				}
			}

			// Token: 0x06006C6F RID: 27759 RVA: 0x00177430 File Offset: 0x00175630
			[SecurityCritical]
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo activityInfo = this.m_creator; activityInfo != null; activityInfo = activityInfo.m_creator)
				{
					if (activityInfo.m_activityPathGuidOffset <= 10)
					{
						uint num = (uint)Interlocked.Increment(ref activityInfo.m_lastChildID);
						*outPtr = activityInfo.m_guid;
						int num2 = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, activityInfo.m_activityPathGuidOffset, num, true);
						if (num2 <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x06006C70 RID: 27760 RVA: 0x00177488 File Offset: 0x00175688
			[SecurityCritical]
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x06006C71 RID: 27761 RVA: 0x00177578 File Offset: 0x00175778
			[SecurityCritical]
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x040034EB RID: 13547
			internal readonly string m_name;

			// Token: 0x040034EC RID: 13548
			private readonly long m_uniqueId;

			// Token: 0x040034ED RID: 13549
			internal readonly Guid m_guid;

			// Token: 0x040034EE RID: 13550
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x040034EF RID: 13551
			internal readonly int m_level;

			// Token: 0x040034F0 RID: 13552
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x040034F1 RID: 13553
			internal long m_lastChildID;

			// Token: 0x040034F2 RID: 13554
			internal int m_stopped;

			// Token: 0x040034F3 RID: 13555
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x040034F4 RID: 13556
			internal readonly Guid m_activityIdToRestore;

			// Token: 0x02000D06 RID: 3334
			private enum NumberListCodes : byte
			{
				// Token: 0x0400393C RID: 14652
				End,
				// Token: 0x0400393D RID: 14653
				LastImmediateValue = 10,
				// Token: 0x0400393E RID: 14654
				PrefixCode,
				// Token: 0x0400393F RID: 14655
				MultiByte1
			}
		}
	}
}
