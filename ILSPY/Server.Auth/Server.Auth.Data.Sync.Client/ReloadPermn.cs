using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Client;

public class ReloadPermn
{
	public static void Load(SyncClientPacket C)
	{
		int num = C.ReadC();
		switch (num)
		{
		case 1:
			EventVisitXML.Reload();
			EventLoginXML.Reload();
			EventBoostXML.Reload();
			EventPlaytimeXML.Reload();
			EventQuestXML.Reload();
			EventRankUpXML.Reload();
			EventXmasXML.Reload();
			CLogger.Print("All Events Successfully Reloaded!", LoggerType.Command);
			break;
		case 2:
			PermissionXML.Load();
			CLogger.Print("Permission Successfully Reloaded!", LoggerType.Command);
			break;
		}
		CLogger.Print($"Updating null part: {num}", LoggerType.Command);
	}
}
