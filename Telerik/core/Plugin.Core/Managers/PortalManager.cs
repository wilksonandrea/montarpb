using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Plugin.Core.Managers
{
	public static class PortalManager
	{
		public static SortedList<string, PortalEvents> AllEvents;

		static PortalManager()
		{
			PortalManager.AllEvents = new SortedList<string, PortalEvents>();
		}

		public static int GetInitialId(string Input)
		{
			int ınt32;
			Match match = Regex.Match(Input, "\\d+");
			if (match.Success && int.TryParse(match.Value, out ınt32))
			{
				return ınt32;
			}
			return -1;
		}

		public static byte[] InitBoostData(EventBoostModel Boost)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteH((ushort)Boost.BoostType);
				syncServerPacket.WriteD(Boost.BoostValue);
				syncServerPacket.WriteD(ComDiv.Percentage(Boost.BonusExp, Boost.Percent));
				syncServerPacket.WriteD(ComDiv.Percentage(Boost.BonusGold, Boost.Percent));
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static byte[] InitEventData(PortalEvents Portal, int Id, uint[] DateTime, string[] Info, byte[] Type, ushort Image)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)Portal);
				syncServerPacket.WriteD(Id);
				syncServerPacket.WriteC(Type[0]);
				syncServerPacket.WriteD(DateTime[0]);
				syncServerPacket.WriteD(DateTime[1]);
				syncServerPacket.WriteD(0);
				syncServerPacket.WriteC(Type[1]);
				syncServerPacket.WriteU(Info[0], 120);
				syncServerPacket.WriteU(Info[1], 200);
				syncServerPacket.WriteH(Image);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static byte[] InitLoginData(EventLoginModel Login)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC(1);
				syncServerPacket.WriteC((byte)((Login.Goods.Count > 4 ? 4 : Login.Goods.Count)));
				for (int i = 0; i < 4; i++)
				{
					if (Login.Goods.Count < i + 1)
					{
						syncServerPacket.WriteD(0);
					}
					else
					{
						syncServerPacket.WriteD(Login.Goods[i]);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static byte[] InitPlaytimeData(EventPlaytimeModel Playtime)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteD(Playtime.Minutes1 * 60);
				syncServerPacket.WriteD(Playtime.Minutes2 * 60);
				syncServerPacket.WriteD(Playtime.Minutes3 * 60);
				foreach (int goods1 in Playtime.Goods1)
				{
					syncServerPacket.WriteD(goods1);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods1.Count) * 4]);
				foreach (int goods2 in Playtime.Goods2)
				{
					syncServerPacket.WriteD(goods2);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods2.Count) * 4]);
				foreach (int goods3 in Playtime.Goods3)
				{
					syncServerPacket.WriteD(goods3);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods3.Count) * 4]);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static byte[] InitRankUpData(EventRankUpModel RankUp)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)RankUp.Ranks.Count);
				foreach (int[] rank in RankUp.Ranks)
				{
					syncServerPacket.WriteD(rank[0]);
					syncServerPacket.WriteD(ComDiv.Percentage(rank[1], rank[3]));
					syncServerPacket.WriteD(ComDiv.Percentage(rank[2], rank[3]));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static void Load()
		{
			foreach (EventBoostModel @event in EventBoostXML.Events)
			{
				if (@event == null || !@event.EventIsEnabled())
				{
					continue;
				}
				PortalManager.AllEvents.Add(string.Format("Boost_{0}", @event.Id), PortalEvents.BoostEvent);
			}
			foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
			{
				if (eventRankUpModel == null || !eventRankUpModel.EventIsEnabled())
				{
					continue;
				}
				PortalManager.AllEvents.Add(string.Format("RankUp_{0}", eventRankUpModel.Id), PortalEvents.RankUpEvent);
			}
			foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
			{
				if (eventLoginModel == null || !eventLoginModel.EventIsEnabled())
				{
					continue;
				}
				PortalManager.AllEvents.Add(string.Format("Login_{0}", eventLoginModel.Id), PortalEvents.LoginEvent);
			}
			foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
			{
				if (eventPlaytimeModel == null || !eventPlaytimeModel.EventIsEnabled())
				{
					continue;
				}
				PortalManager.AllEvents.Add(string.Format("Playtime_{0}", eventPlaytimeModel.Id), PortalEvents.PlaytimeEvent);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Listed Event Portal", PortalManager.AllEvents.Count), LoggerType.Info, null);
		}
	}
}