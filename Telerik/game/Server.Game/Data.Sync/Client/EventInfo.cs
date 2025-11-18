using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class EventInfo
	{
		public EventInfo()
		{
		}

		public static void LoadEventInfo(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			if (EventInfo.smethod_0(ınt32))
			{
				CLogger.Print(string.Format("Refresh event; Type: {0};", ınt32), LoggerType.Command, null);
			}
		}

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
			if (int_0 != 6)
			{
				return false;
			}
			EventXmasXML.Reload();
			return true;
		}
	}
}