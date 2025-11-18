namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_LOBBY_LEAVE_REQ : GameClientPacket
    {
        private uint uint_0;

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel channel = player.GetChannel();
                    if ((player.Room == null) && (player.Match == null))
                    {
                        if ((channel == null) || ((player.Session == null) || !channel.RemovePlayer(player)))
                        {
                            this.uint_0 = 0x80000000;
                        }
                        base.Client.SendPacket(new PROTOCOL_LOBBY_LEAVE_ACK(this.uint_0));
                        if (this.uint_0 != 0)
                        {
                            base.Client.Close(0x3e8, true, false);
                        }
                        else
                        {
                            player.ResetPages();
                            player.Status.UpdateChannel(0xff);
                            AllUtils.SyncPlayerToFriends(player, false);
                            AllUtils.SyncPlayerToClanMembers(player);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_LEAVE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

