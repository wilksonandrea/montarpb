namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_USER_SOPETYPE_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (room != null)
                    {
                        player.Sight = this.int_0;
                        using (new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player))
                        {
                            room.SendPacketToPlayers(new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_USER_SOPETYPE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

