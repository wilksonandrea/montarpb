namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_SHOP_LEAVE_REQ : GameClientPacket
    {
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
                    RoomModel room = player.Room;
                    if (room != null)
                    {
                        room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
                    }
                    base.Client.SendPacket(new PROTOCOL_SHOP_LEAVE_ACK());
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_SHOP_LEAVE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

