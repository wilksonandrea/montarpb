namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using System;

    public class ReloadPermn
    {
        public static void Load(SyncClientPacket C)
        {
            int num = C.ReadC();
            if (num != 1)
            {
                if (num == 2)
                {
                    PermissionXML.Load();
                    CLogger.Print("Permission Successfully Reloaded!", LoggerType.Command, null);
                }
            }
            else
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
            CLogger.Print($"Updating null part: {num}", LoggerType.Command, null);
        }
    }
}

