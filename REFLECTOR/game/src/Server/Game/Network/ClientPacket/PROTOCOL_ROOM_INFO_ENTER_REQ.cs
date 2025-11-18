namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_INFO_ENTER_REQ : GameClientPacket
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
                        room.ChangeSlotState(player.SlotId, SlotState.INFO, false);
                        room.StopCountDown(player.SlotId);
                        room.UpdateSlotsInfo();
                    }
                    base.Client.SendPacket(new PROTOCOL_ROOM_INFO_ENTER_ACK());
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

