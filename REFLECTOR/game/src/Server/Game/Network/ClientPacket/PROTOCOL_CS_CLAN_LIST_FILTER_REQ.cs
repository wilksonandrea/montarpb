namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_CLAN_LIST_FILTER_REQ : GameClientPacket
    {
        private byte byte_0;
        private byte byte_1;
        private ClanSearchType clanSearchType_0;
        private string string_0;

        private void method_0(ClanModel clanModel_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(clanModel_0.Id);
            syncServerPacket_0.WriteC((byte) (clanModel_0.Name.Length + 1));
            syncServerPacket_0.WriteN(clanModel_0.Name, clanModel_0.Name.Length + 2, "UTF-16LE");
            syncServerPacket_0.WriteD(clanModel_0.Logo);
            syncServerPacket_0.WriteH((ushort) (clanModel_0.Info.Length + 1));
            syncServerPacket_0.WriteN(clanModel_0.Info, clanModel_0.Info.Length + 2, "UTF-16LE");
            syncServerPacket_0.WriteC(0);
        }

        public override void Read()
        {
            this.byte_0 = base.ReadC();
            this.string_0 = base.ReadU(base.ReadC() * 2);
            this.byte_1 = base.ReadC();
            this.clanSearchType_0 = (ClanSearchType) base.ReadC();
        }

        public override void Run()
        {
            try
            {
                int num = 0;
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    List<ClanModel> clans = ClanManager.Clans;
                    lock (clans)
                    {
                        for (byte i = this.byte_0; i < ClanManager.Clans.Count; i = (byte) (i + 1))
                        {
                            ClanModel model = ClanManager.Clans[i];
                            this.method_0(model, packet);
                            if (++num == 15)
                            {
                                break;
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_FILTER_ACK(this.byte_0, num, packet.ToArray()));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

