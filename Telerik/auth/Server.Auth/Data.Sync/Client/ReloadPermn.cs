using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public class ReloadPermn
	{
		public ReloadPermn()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			if (ınt32 == 1)
			{
				EventVisitXML.Reload();
				EventLoginXML.Reload();
				EventBoostXML.Reload();
				EventPlaytimeXML.Reload();
				EventQuestXML.Reload();
				EventRankUpXML.Reload();
				EventXmasXML.Reload();
				CLogger.Print("All Events Successfully Reloaded!", LoggerType.Command, null);
			}
			else if (ınt32 == 2)
			{
				PermissionXML.Load();
				CLogger.Print("Permission Successfully Reloaded!", LoggerType.Command, null);
			}
			CLogger.Print(string.Format("Updating null part: {0}", ınt32), LoggerType.Command, null);
		}
	}
}