namespace Plugin.Core.Managers
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class PortalManager
    {
        public static SortedList<string, PortalEvents> AllEvents = new SortedList<string, PortalEvents>();

        public static int GetInitialId(string Input)
        {
            int num;
            Match match = Regex.Match(Input, @"\d+");
            return ((!match.Success || !int.TryParse(match.Value, out num)) ? -1 : num);
        }

        public static byte[] InitBoostData(EventBoostModel Boost)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteH((ushort) Boost.BoostType);
                packet.WriteD(Boost.BoostValue);
                packet.WriteD(ComDiv.Percentage(Boost.BonusExp, Boost.Percent));
                packet.WriteD(ComDiv.Percentage(Boost.BonusGold, Boost.Percent));
                return packet.ToArray();
            }
        }

        public static byte[] InitEventData(PortalEvents Portal, int Id, uint[] DateTime, string[] Info, byte[] Type, ushort Image)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) Portal);
                packet.WriteD(Id);
                packet.WriteC(Type[0]);
                packet.WriteD(DateTime[0]);
                packet.WriteD(DateTime[1]);
                packet.WriteD(0);
                packet.WriteC(Type[1]);
                packet.WriteU(Info[0], 120);
                packet.WriteU(Info[1], 200);
                packet.WriteH(Image);
                return packet.ToArray();
            }
        }

        public static byte[] InitLoginData(EventLoginModel Login)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC(1);
                packet.WriteC((Login.Goods.Count > 4) ? ((byte) 4) : ((byte) Login.Goods.Count));
                int num = 0;
                while (true)
                {
                    if (num >= 4)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    if (Login.Goods.Count >= (num + 1))
                    {
                        packet.WriteD(Login.Goods[num]);
                    }
                    else
                    {
                        packet.WriteD(0);
                    }
                    num++;
                }
            }
            return buffer;
        }

        public static byte[] InitPlaytimeData(EventPlaytimeModel Playtime)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteD((int) (Playtime.Minutes1 * 60));
                packet.WriteD((int) (Playtime.Minutes2 * 60));
                packet.WriteD((int) (Playtime.Minutes3 * 60));
                foreach (int num in Playtime.Goods1)
                {
                    packet.WriteD(num);
                }
                packet.WriteB(new byte[(20 - Playtime.Goods1.Count) * 4]);
                foreach (int num2 in Playtime.Goods2)
                {
                    packet.WriteD(num2);
                }
                packet.WriteB(new byte[(20 - Playtime.Goods2.Count) * 4]);
                foreach (int num3 in Playtime.Goods3)
                {
                    packet.WriteD(num3);
                }
                packet.WriteB(new byte[(20 - Playtime.Goods3.Count) * 4]);
                return packet.ToArray();
            }
        }

        public static byte[] InitRankUpData(EventRankUpModel RankUp)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) RankUp.Ranks.Count);
                foreach (int[] numArray in RankUp.Ranks)
                {
                    packet.WriteD(numArray[0]);
                    packet.WriteD(ComDiv.Percentage(numArray[1], numArray[3]));
                    packet.WriteD(ComDiv.Percentage(numArray[2], numArray[3]));
                }
                return packet.ToArray();
            }
        }

        public static void Load()
        {
            foreach (EventBoostModel model in EventBoostXML.Events)
            {
                if ((model != null) && model.EventIsEnabled())
                {
                    AllEvents.Add($"Boost_{model.Id}", PortalEvents.BoostEvent);
                }
            }
            foreach (EventRankUpModel model2 in EventRankUpXML.Events)
            {
                if ((model2 != null) && model2.EventIsEnabled())
                {
                    AllEvents.Add($"RankUp_{model2.Id}", PortalEvents.RankUpEvent);
                }
            }
            foreach (EventLoginModel model3 in EventLoginXML.Events)
            {
                if ((model3 != null) && model3.EventIsEnabled())
                {
                    AllEvents.Add($"Login_{model3.Id}", PortalEvents.LoginEvent);
                }
            }
            foreach (EventPlaytimeModel model4 in EventPlaytimeXML.Events)
            {
                if ((model4 != null) && model4.EventIsEnabled())
                {
                    AllEvents.Add($"Playtime_{model4.Id}", PortalEvents.PlaytimeEvent);
                }
            }
            CLogger.Print($"Plugin Loaded: {AllEvents.Count} Listed Event Portal", LoggerType.Info, null);
        }
    }
}

