namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Game;
    using Server.Game.Data.Models;
    using System;

    public class BattleLeaveSync
    {
        public static void SendUDPPlayerLeave(RoomModel Room, int SlotId)
        {
            try
            {
                if (Room != null)
                {
                    int num = Room.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.BATTLE, 0, SlotId);
                    using (SyncServerPacket packet = new SyncServerPacket())
                    {
                        packet.WriteH((short) 2);
                        packet.WriteD(Room.UniqueRoomId);
                        packet.WriteD(Room.Seed);
                        packet.WriteC((byte) SlotId);
                        packet.WriteC((byte) num);
                        GameXender.Sync.SendPacket(packet.ToArray(), Room.UdpServer.Connection);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

