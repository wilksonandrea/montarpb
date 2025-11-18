using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;

namespace Server.Match.Data.Sync.Client;

public class RemovePlayerSync
{
	public static void Load(SyncClientPacket C)
	{
		uint uniqueRoomId = C.ReadUD();
		uint seed = C.ReadUD();
		int slot = C.ReadC();
		int num = C.ReadC();
		RoomModel room = RoomsManager.GetRoom(uniqueRoomId, seed);
		if (room != null)
		{
			if (num == 0)
			{
				RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
			}
			else
			{
				room.GetPlayer(slot, Active: false)?.ResetAllInfos();
			}
		}
	}
}
