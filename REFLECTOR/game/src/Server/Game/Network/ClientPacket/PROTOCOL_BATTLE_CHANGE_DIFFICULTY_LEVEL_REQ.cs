namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ : GameClientPacket
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
                    if (((room != null) && (room.State == RoomState.BATTLE)) && (room.IngameAiLevel < 10))
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if ((slot != null) && (slot.State == SlotState.BATTLE))
                        {
                            if (room.IngameAiLevel <= 9)
                            {
                                room.IngameAiLevel = (byte) (room.IngameAiLevel + 1);
                            }
                            using (PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK protocol_battle_change_difficulty_level_ack = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room))
                            {
                                room.SendPacketToPlayers(protocol_battle_change_difficulty_level_ack, SlotState.READY, 1);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

