namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : GameClientPacket
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
                    RoomModel room = player.Room;
                    if (((room != null) && (room.State == RoomState.BATTLE)) && (player.SlotId == room.LeaderSlot))
                    {
                        SlotModel slot = room.GetSlot(this.int_0);
                        if (slot != null)
                        {
                            slot.AiLevel = room.IngameAiLevel;
                            room.SpawnsCount++;
                        }
                        using (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK protocol_battle_respawn_for_ai_ack = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(this.int_0))
                        {
                            room.SendPacketToPlayers(protocol_battle_respawn_for_ai_ack, SlotState.BATTLE, 0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

