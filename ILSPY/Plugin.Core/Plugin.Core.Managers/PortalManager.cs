using System.Collections.Generic;
using System.Text.RegularExpressions;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Plugin.Core.Managers;

public static class PortalManager
{
	public static SortedList<string, PortalEvents> AllEvents = new SortedList<string, PortalEvents>();

	public static void Load()
	{
		foreach (EventBoostModel @event in EventBoostXML.Events)
		{
			if (@event != null && @event.EventIsEnabled())
			{
				AllEvents.Add($"Boost_{@event.Id}", PortalEvents.BoostEvent);
			}
		}
		foreach (EventRankUpModel event2 in EventRankUpXML.Events)
		{
			if (event2 != null && event2.EventIsEnabled())
			{
				AllEvents.Add($"RankUp_{event2.Id}", PortalEvents.RankUpEvent);
			}
		}
		foreach (EventLoginModel event3 in EventLoginXML.Events)
		{
			if (event3 != null && event3.EventIsEnabled())
			{
				AllEvents.Add($"Login_{event3.Id}", PortalEvents.LoginEvent);
			}
		}
		foreach (EventPlaytimeModel event4 in EventPlaytimeXML.Events)
		{
			if (event4 != null && event4.EventIsEnabled())
			{
				AllEvents.Add($"Playtime_{event4.Id}", PortalEvents.PlaytimeEvent);
			}
		}
		CLogger.Print($"Plugin Loaded: {AllEvents.Count} Listed Event Portal", LoggerType.Info);
	}

	public static int GetInitialId(string Input)
	{
		Match match = Regex.Match(Input, "\\d+");
		if (match.Success && int.TryParse(match.Value, out var result))
		{
			return result;
		}
		return -1;
	}

	public static byte[] InitEventData(PortalEvents Portal, int Id, uint[] DateTime, string[] Info, byte[] Type, ushort Image)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
		return syncServerPacket.ToArray();
	}

	public static byte[] InitRankUpData(EventRankUpModel RankUp)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)RankUp.Ranks.Count);
		foreach (int[] rank in RankUp.Ranks)
		{
			syncServerPacket.WriteD(rank[0]);
			syncServerPacket.WriteD(ComDiv.Percentage(rank[1], rank[3]));
			syncServerPacket.WriteD(ComDiv.Percentage(rank[2], rank[3]));
		}
		return syncServerPacket.ToArray();
	}

	public static byte[] InitPlaytimeData(EventPlaytimeModel Playtime)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteD(Playtime.Minutes1 * 60);
		syncServerPacket.WriteD(Playtime.Minutes2 * 60);
		syncServerPacket.WriteD(Playtime.Minutes3 * 60);
		foreach (int item in Playtime.Goods1)
		{
			syncServerPacket.WriteD(item);
		}
		syncServerPacket.WriteB(new byte[(20 - Playtime.Goods1.Count) * 4]);
		foreach (int item2 in Playtime.Goods2)
		{
			syncServerPacket.WriteD(item2);
		}
		syncServerPacket.WriteB(new byte[(20 - Playtime.Goods2.Count) * 4]);
		foreach (int item3 in Playtime.Goods3)
		{
			syncServerPacket.WriteD(item3);
		}
		syncServerPacket.WriteB(new byte[(20 - Playtime.Goods3.Count) * 4]);
		return syncServerPacket.ToArray();
	}

	public static byte[] InitBoostData(EventBoostModel Boost)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteH((ushort)Boost.BoostType);
		syncServerPacket.WriteD(Boost.BoostValue);
		syncServerPacket.WriteD(ComDiv.Percentage(Boost.BonusExp, Boost.Percent));
		syncServerPacket.WriteD(ComDiv.Percentage(Boost.BonusGold, Boost.Percent));
		return syncServerPacket.ToArray();
	}

	public static byte[] InitLoginData(EventLoginModel Login)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC(1);
		syncServerPacket.WriteC((byte)((Login.Goods.Count > 4) ? 4u : ((uint)Login.Goods.Count)));
		for (int i = 0; i < 4; i++)
		{
			if (Login.Goods.Count >= i + 1)
			{
				syncServerPacket.WriteD(Login.Goods[i]);
			}
			else
			{
				syncServerPacket.WriteD(0);
			}
		}
		return syncServerPacket.ToArray();
	}
}
