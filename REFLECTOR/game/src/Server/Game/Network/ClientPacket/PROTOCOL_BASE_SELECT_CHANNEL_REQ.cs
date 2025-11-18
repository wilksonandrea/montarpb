namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_SELECT_CHANNEL_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            base.ReadB(4);
            this.int_0 = base.ReadH();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.ChannelId < 0))
                {
                    ChannelModel channel = ChannelsXML.GetChannel(base.Client.ServerId, this.int_0);
                    if (channel == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0x80000000, -1, -1));
                    }
                    else if (AllUtils.ChannelRequirementCheck(player, channel))
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0x80000202, -1, -1));
                    }
                    else if (channel.Players.Count >= SChannelXML.GetServer(base.Client.ServerId).ChannelPlayers)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0x80000201, -1, -1));
                    }
                    else
                    {
                        player.ServerId = channel.ServerId;
                        player.ChannelId = channel.Id;
                        base.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0, player.ServerId, player.ChannelId));
                        base.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
                        player.Status.UpdateServer((byte) player.ServerId);
                        player.Status.UpdateChannel((byte) player.ChannelId);
                        player.UpdateCacheInfo();
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_SELECT_CHANNEL_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

