using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F2 RID: 498
	public class EventInfo
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0002F89C File Offset: 0x0002DA9C
		public static void LoadEventInfo(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			if (EventInfo.smethod_0(num))
			{
				CLogger.Print(string.Format("Refresh event; Type: {0};", num), LoggerType.Command, null);
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
		private static bool smethod_0(int int_0)
		{
			if (int_0 == 0)
			{
				EventVisitXML.Reload();
				return true;
			}
			if (int_0 == 1)
			{
				EventLoginXML.Reload();
				return true;
			}
			if (int_0 == 2)
			{
				EventBoostXML.Reload();
				return true;
			}
			if (int_0 == 3)
			{
				EventPlaytimeXML.Reload();
				return true;
			}
			if (int_0 == 4)
			{
				EventQuestXML.Reload();
				return true;
			}
			if (int_0 == 5)
			{
				EventRankUpXML.Reload();
				return true;
			}
			if (int_0 == 6)
			{
				EventXmasXML.Reload();
				return true;
			}
			return false;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000025DF File Offset: 0x000007DF
		public EventInfo()
		{
		}
	}
}
