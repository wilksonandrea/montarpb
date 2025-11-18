using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Plugin.Core.Managers
{
	// Token: 0x020000A3 RID: 163
	public static class PortalManager
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		public static void Load()
		{
			foreach (EventBoostModel eventBoostModel in EventBoostXML.Events)
			{
				if (eventBoostModel != null && eventBoostModel.EventIsEnabled())
				{
					PortalManager.AllEvents.Add(string.Format("Boost_{0}", eventBoostModel.Id), PortalEvents.BoostEvent);
				}
			}
			foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
			{
				if (eventRankUpModel != null && eventRankUpModel.EventIsEnabled())
				{
					PortalManager.AllEvents.Add(string.Format("RankUp_{0}", eventRankUpModel.Id), PortalEvents.RankUpEvent);
				}
			}
			foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
			{
				if (eventLoginModel != null && eventLoginModel.EventIsEnabled())
				{
					PortalManager.AllEvents.Add(string.Format("Login_{0}", eventLoginModel.Id), PortalEvents.LoginEvent);
				}
			}
			foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
			{
				if (eventPlaytimeModel != null && eventPlaytimeModel.EventIsEnabled())
				{
					PortalManager.AllEvents.Add(string.Format("Playtime_{0}", eventPlaytimeModel.Id), PortalEvents.PlaytimeEvent);
				}
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Listed Event Portal", PortalManager.AllEvents.Count), LoggerType.Info, null);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001CF94 File Offset: 0x0001B194
		public static int GetInitialId(string Input)
		{
			Match match = Regex.Match(Input, "\\d+");
			int num;
			if (match.Success && int.TryParse(match.Value, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001CFC8 File Offset: 0x0001B1C8
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

		// Token: 0x060007CC RID: 1996 RVA: 0x0001D05C File Offset: 0x0001B25C
		public static byte[] InitRankUpData(EventRankUpModel RankUp)
		{
			byte[] array2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)RankUp.Ranks.Count);
				foreach (int[] array in RankUp.Ranks)
				{
					syncServerPacket.WriteD(array[0]);
					syncServerPacket.WriteD(ComDiv.Percentage(array[1], array[3]));
					syncServerPacket.WriteD(ComDiv.Percentage(array[2], array[3]));
				}
				array2 = syncServerPacket.ToArray();
			}
			return array2;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001D10C File Offset: 0x0001B30C
		public static byte[] InitPlaytimeData(EventPlaytimeModel Playtime)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteD(Playtime.Minutes1 * 60);
				syncServerPacket.WriteD(Playtime.Minutes2 * 60);
				syncServerPacket.WriteD(Playtime.Minutes3 * 60);
				foreach (int num in Playtime.Goods1)
				{
					syncServerPacket.WriteD(num);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods1.Count) * 4]);
				foreach (int num2 in Playtime.Goods2)
				{
					syncServerPacket.WriteD(num2);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods2.Count) * 4]);
				foreach (int num3 in Playtime.Goods3)
				{
					syncServerPacket.WriteD(num3);
				}
				syncServerPacket.WriteB(new byte[(20 - Playtime.Goods3.Count) * 4]);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
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

		// Token: 0x060007CF RID: 1999 RVA: 0x0001D340 File Offset: 0x0001B540
		public static byte[] InitLoginData(EventLoginModel Login)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC(1);
				syncServerPacket.WriteC((byte)((Login.Goods.Count > 4) ? 4 : Login.Goods.Count));
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
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000065DD File Offset: 0x000047DD
		// Note: this type is marked as 'beforefieldinit'.
		static PortalManager()
		{
		}

		// Token: 0x0400037D RID: 893
		public static SortedList<string, PortalEvents> AllEvents = new SortedList<string, PortalEvents>();
	}
}
