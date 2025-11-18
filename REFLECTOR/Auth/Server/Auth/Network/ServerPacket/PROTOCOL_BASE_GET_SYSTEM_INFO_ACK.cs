namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.XML;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_SYSTEM_INFO_ACK : AuthServerPacket
    {
        private readonly ServerConfig serverConfig_0;
        private readonly List<SChannelModel> list_0;
        private readonly List<RankModel> list_1;
        private readonly EventPlaytimeModel eventPlaytimeModel_0;
        private readonly string[] string_0;

        public PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(ServerConfig serverConfig_1)
        {
            this.serverConfig_0 = serverConfig_1;
            if (serverConfig_1 != null)
            {
                this.list_0 = SChannelXML.Servers;
                this.list_1 = PlayerRankXML.Ranks;
                this.eventPlaytimeModel_0 = EventPlaytimeXML.GetRunningEvent();
            }
            this.string_0 = new string[] { "ded9a5bc68c44c6b885ac376be4f08c6", "5c67549f9ea01f1c7429d2a6bb121844" };
        }

        private byte[] method_0(List<SChannelModel> list_2)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) list_2.Count);
                foreach (SChannelModel model in list_2)
                {
                    packet.WriteD((int) model.State);
                    packet.WriteB(ComDiv.AddressBytes(model.Host));
                    packet.WriteB(ComDiv.AddressBytes(model.Host));
                    packet.WriteH(model.Port);
                    packet.WriteC((byte) model.Type);
                    packet.WriteH((ushort) model.MaxPlayers);
                    packet.WriteD(model.LastPlayers);
                    if (model.Id == 0)
                    {
                        packet.WriteB(Bitwise.HexStringToByteArray("01 01 01 01 01 01 01 01 01 01 0E 00 00 00 00"));
                        continue;
                    }
                    foreach (ChannelModel model2 in ChannelsXML.GetChannels(model.Id))
                    {
                        packet.WriteC((byte) model2.Type);
                    }
                    packet.WriteC((byte) model.Type);
                    packet.WriteC(0);
                    packet.WriteH((short) 0);
                }
                return packet.ToArray();
            }
        }

        private byte[] method_1(List<RankModel> list_2)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) list_2.Count);
                foreach (RankModel model in list_2)
                {
                    packet.WriteC((byte) model.Id);
                    List<int> rewards = PlayerRankXML.GetRewards(model.Id);
                    using (List<int>.Enumerator enumerator2 = rewards.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            GoodsItem good = ShopManager.GetGood(enumerator2.Current);
                            packet.WriteD((good == null) ? 0 : good.Id);
                        }
                    }
                    for (int i = rewards.Count; (4 - i) > 0; i++)
                    {
                        packet.WriteD(0);
                    }
                }
                return packet.ToArray();
            }
        }

        private byte[] method_2(EventPlaytimeModel eventPlaytimeModel_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC(3);
                if ((eventPlaytimeModel_1 == null) || !eventPlaytimeModel_1.EventIsEnabled())
                {
                    packet.WriteB(new byte[12]);
                }
                else
                {
                    packet.WriteD((int) (eventPlaytimeModel_1.Minutes1 * 60));
                    packet.WriteD((int) (eventPlaytimeModel_1.Minutes2 * 60));
                    packet.WriteD((int) (eventPlaytimeModel_1.Minutes3 * 60));
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x90b);
            base.WriteH((short) 0);
            base.WriteC((byte) this.string_0[0].Length);
            base.WriteS(this.string_0[0], this.string_0[0].Length);
            base.WriteC((byte) this.string_0[1].Length);
            base.WriteS(this.string_0[1], this.string_0[1].Length);
            base.WriteD(0);
            base.WriteD(0x10);
            base.WriteB(new byte[0x3d]);
            base.WriteH((short) 5);
            base.WriteH((short) 120);
            base.WriteH((short) 0x402);
            base.WriteC(0);
            base.WriteH((short) 770);
            base.WriteC(1);
            base.WriteH((short) 200);
            base.WriteH((short) 0);
            base.WriteD(50);
            base.WriteD(50);
            base.WriteC(1);
            base.WriteH((short) 0x3e8);
            base.WriteC(0);
            base.WriteD(0x25863);
            base.WriteC(0);
            base.WriteC(1);
            base.WriteB(new byte[0x175]);
            base.WriteC((byte) this.serverConfig_0.Showroom);
            base.WriteC(5);
            base.WriteC(4);
            base.WriteH((short) 0xdac);
            base.WriteH((short) 0);
            base.WriteH((short) 0x5aa);
            base.WriteH((short) 0);
            base.WriteD(0x31);
            base.WriteD(1);
            base.WriteH((short) 0x701);
            base.WriteC(1);
            base.WriteH((short) 0x2123);
            base.WriteH((short) 0);
            base.WriteB(new byte[0x34]);
            base.WriteH((short) 0xa05);
            base.WriteB(new byte[0xe5]);
            base.WriteB(this.method_2(this.eventPlaytimeModel_0));
            base.WriteC((byte) this.serverConfig_0.Missions);
            base.WriteH((ushort) MissionConfigXML.MissionPage1);
            base.WriteH((ushort) MissionConfigXML.MissionPage2);
            base.WriteH((ushort) base.SECURITY_KEY);
            base.WriteB(this.method_0(this.list_0));
            base.WriteC(1);
            base.WriteC((byte) base.NATIONS);
            base.WriteC(0);
            base.WriteH((short) this.serverConfig_0.ShopURL.Length);
            base.WriteS(this.serverConfig_0.ShopURL, this.serverConfig_0.ShopURL.Length);
            base.WriteB(this.method_1(this.list_1));
            base.WriteC(0);
            base.WriteC(6);
        }
    }
}

