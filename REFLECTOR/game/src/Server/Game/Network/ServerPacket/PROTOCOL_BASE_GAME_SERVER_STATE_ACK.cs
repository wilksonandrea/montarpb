namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GAME_SERVER_STATE_ACK : GameServerPacket
    {
        private byte[] method_0(List<SChannelModel> list_0)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) list_0.Count);
                foreach (SChannelModel model in list_0)
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

        public override void Write()
        {
            base.WriteH((short) 0x960);
            base.WriteB(this.method_0(SChannelXML.Servers));
            base.WriteC(0);
        }
    }
}

