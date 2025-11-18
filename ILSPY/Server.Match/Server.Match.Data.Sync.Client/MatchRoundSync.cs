using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;

namespace Server.Match.Data.Sync.Client;

public class MatchRoundSync
{
	public static void Load(SyncClientPacket C)
	{
		uint uniqueRoomId = C.ReadUD();
		uint seed = C.ReadUD();
		int serverRound = C.ReadC();
		bool ısTeamSwap = C.ReadC() == 1;
		RoomModel room = RoomsManager.GetRoom(uniqueRoomId, seed);
		if (room != null)
		{
			room.ServerRound = serverRound;
			room.IsTeamSwap = ısTeamSwap;
		}
	}
}
