namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    Account account2;
                    RoomModel room = player.Room;
                    if ((room != null) && room.GetPlayerBySlot(this.int_0, out account2))
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(account2));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

