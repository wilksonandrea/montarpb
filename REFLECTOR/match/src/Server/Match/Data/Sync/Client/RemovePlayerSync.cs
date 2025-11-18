namespace Server.Match.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Match.Data.Managers;
    using Server.Match.Data.Models;
    using System;

    public class RemovePlayerSync
    {
        public static void Load(SyncClientPacket C)
        {
            int slot = C.ReadC();
            int num3 = C.ReadC();
            RoomModel room = RoomsManager.GetRoom(C.ReadUD(), C.ReadUD());
            if (room != null)
            {
                if (num3 == 0)
                {
                    RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
                }
                else
                {
                    PlayerModel player = room.GetPlayer(slot, false);
                    if (player != null)
                    {
                        player.ResetAllInfos();
                    }
                }
            }
        }
    }
}

