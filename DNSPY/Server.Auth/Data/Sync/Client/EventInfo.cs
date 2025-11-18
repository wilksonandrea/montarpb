using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000054 RID: 84
	public class EventInfo
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000B6DC File Offset: 0x000098DC
		public static void LoadEventInfo(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			if (EventInfo.smethod_0(num))
			{
				CLogger.Print(string.Format("Refresh event; Type: {0};", num), LoggerType.Command, null);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000B710 File Offset: 0x00009910
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

		// Token: 0x06000137 RID: 311 RVA: 0x00002409 File Offset: 0x00000609
		public EventInfo()
		{
		}
	}
}
