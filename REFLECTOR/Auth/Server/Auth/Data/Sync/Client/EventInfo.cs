namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using System;

    public class EventInfo
    {
        public static void LoadEventInfo(SyncClientPacket C)
        {
            int num = C.ReadC();
            if (smethod_0(num))
            {
                CLogger.Print($"Refresh event; Type: {num};", LoggerType.Command, null);
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

