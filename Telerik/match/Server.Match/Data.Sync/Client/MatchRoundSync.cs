using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using System;

namespace Server.Match.Data.Sync.Client
{
	public class MatchRoundSync
	{
		public MatchRoundSync()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			uint uInt32 = C.ReadUD();
			uint uInt321 = C.ReadUD();
			int ınt32 = C.ReadC();
			bool flag = C.ReadC() == 1;
			RoomModel room = RoomsManager.GetRoom(uInt32, uInt321);
			if (room != null)
			{
				room.ServerRound = ınt32;
				room.IsTeamSwap = flag;
			}
		}
	}
}