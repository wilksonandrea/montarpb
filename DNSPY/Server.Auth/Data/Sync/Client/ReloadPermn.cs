using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x0200005B RID: 91
	public class ReloadPermn
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			if (num == 1)
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
			else if (num == 2)
			{
				PermissionXML.Load();
				CLogger.Print("Permission Successfully Reloaded!", LoggerType.Command, null);
			}
			CLogger.Print(string.Format("Updating null part: {0}", num), LoggerType.Command, null);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002409 File Offset: 0x00000609
		public ReloadPermn()
		{
		}
	}
}
