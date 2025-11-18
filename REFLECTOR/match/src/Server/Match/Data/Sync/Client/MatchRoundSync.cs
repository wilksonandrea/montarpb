namespace Server.Match.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Match.Data.Managers;
    using Server.Match.Data.Models;
    using System;

    public class MatchRoundSync
    {
        public static void Load(SyncClientPacket C)
        {
            int num2 = C.ReadC();
            bool flag = C.ReadC() == 1;
            RoomModel room = RoomsManager.GetRoom(C.ReadUD(), C.ReadUD());
            if (room != null)
            {
                room.ServerRound = num2;
                room.IsTeamSwap = flag;
            }
        }
    }
}

