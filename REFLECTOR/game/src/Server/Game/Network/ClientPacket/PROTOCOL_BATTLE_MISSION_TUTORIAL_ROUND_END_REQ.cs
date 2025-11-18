namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ : GameClientPacket
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
                        room.State = RoomState.BATTLE_END;
                        base.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(room));
                        base.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room, 0, RoundEndType.Tutorial));
                        base.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
                        if (room.State == RoomState.BATTLE_END)
                        {
                            room.State = RoomState.READY;
                            base.Client.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(player));
                            base.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
                        }
                        AllUtils.ResetBattleInfo(room);
                        base.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(room));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

