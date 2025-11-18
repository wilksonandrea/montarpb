namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_EVENT_PORTAL_ACK : GameServerPacket
    {
        private readonly bool bool_0;

        public PROTOCOL_BASE_EVENT_PORTAL_ACK(bool bool_1)
        {
            this.bool_0 = bool_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x9cf);
            base.WriteC((byte) this.bool_0);
            base.WriteC(1);
            base.WriteD(0x2000);
            base.WriteC((byte) PortalManager.AllEvents.Count);
            foreach (KeyValuePair<string, PortalEvents> pair in PortalManager.AllEvents)
            {
                if (pair.Key.Contains("Boost") && (((PortalEvents) pair.Value) == PortalEvents.BoostEvent))
                {
                    EventBoostModel boost = EventBoostXML.GetEvent(PortalManager.GetInitialId(pair.Key));
                    uint[] dateTime = new uint[] { boost.BeginDate, boost.EndedDate };
                    string[] textArray1 = new string[] { boost.Name, boost.Description };
                    byte[] type = new byte[] { (byte) !boost.Period, (byte) boost.Priority };
                    base.WriteB(PortalManager.InitEventData(pair.Value, boost.Id, dateTime, textArray1, type, 0));
                    base.WriteB(PortalManager.InitBoostData(boost));
                    continue;
                }
                if (pair.Key.Contains("RankUp") && (((PortalEvents) pair.Value) == PortalEvents.RankUpEvent))
                {
                    EventRankUpModel rankUp = EventRankUpXML.GetEvent(PortalManager.GetInitialId(pair.Key));
                    uint[] dateTime = new uint[] { rankUp.BeginDate, rankUp.EndedDate };
                    string[] textArray2 = new string[] { rankUp.Name, rankUp.Description };
                    byte[] type = new byte[] { (byte) !rankUp.Period, (byte) rankUp.Priority };
                    base.WriteB(PortalManager.InitEventData(pair.Value, rankUp.Id, dateTime, textArray2, type, 1));
                    base.WriteB(PortalManager.InitRankUpData(rankUp));
                    continue;
                }
                if (pair.Key.Contains("Login") && (((PortalEvents) pair.Value) == PortalEvents.LoginEvent))
                {
                    EventLoginModel login = EventLoginXML.GetEvent(PortalManager.GetInitialId(pair.Key));
                    uint[] dateTime = new uint[] { login.BeginDate, login.EndedDate };
                    string[] textArray3 = new string[] { login.Name, login.Description };
                    byte[] type = new byte[] { (byte) !login.Period, (byte) login.Priority };
                    base.WriteB(PortalManager.InitEventData(pair.Value, login.Id, dateTime, textArray3, type, 1));
                    base.WriteB(PortalManager.InitLoginData(login));
                    continue;
                }
                if (pair.Key.Contains("Playtime") && (((PortalEvents) pair.Value) == PortalEvents.PlaytimeEvent))
                {
                    EventPlaytimeModel playtime = EventPlaytimeXML.GetEvent(PortalManager.GetInitialId(pair.Key));
                    uint[] dateTime = new uint[] { playtime.BeginDate, playtime.EndedDate };
                    string[] textArray4 = new string[] { playtime.Name, playtime.Description };
                    byte[] type = new byte[] { (byte) !playtime.Period, (byte) playtime.Priority };
                    base.WriteB(PortalManager.InitEventData(pair.Value, playtime.Id, dateTime, textArray4, type, 0));
                    base.WriteB(PortalManager.InitPlaytimeData(playtime));
                }
            }
        }
    }
}

