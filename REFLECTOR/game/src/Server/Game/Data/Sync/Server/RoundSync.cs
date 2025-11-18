namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Game;
    using Server.Game.Data.Models;
    using System;

    public class RoundSync
    {
        public static void SendUDPRoundSync(RoomModel Room)
        {
            try
            {
                if (Room != null)
                {
                    using (SyncServerPacket packet = new SyncServerPacket())
                    {
                        packet.WriteH((short) 3);
                        packet.WriteD(Room.UniqueRoomId);
                        packet.WriteD(Room.Seed);
                        packet.WriteC((byte) Room.Rounds);
                        packet.WriteC((byte) Room.SwapRound);
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

