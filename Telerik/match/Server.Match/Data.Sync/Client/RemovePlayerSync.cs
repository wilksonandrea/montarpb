using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using System;

namespace Server.Match.Data.Sync.Client
{
	public class RemovePlayerSync
	{
		public RemovePlayerSync()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			uint uInt32 = C.ReadUD();
			uint uInt321 = C.ReadUD();
			int ınt32 = C.ReadC();
			int ınt321 = C.ReadC();
			RoomModel room = RoomsManager.GetRoom(uInt32, uInt321);
			if (room == null)
			{
				return;
			}
			if (ınt321 == 0)
			{
				RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
				return;
			}
			PlayerModel player = room.GetPlayer(ınt32, false);
			if (player != null)
			{
				player.ResetAllInfos();
			}
		}
	}
}