using System;
using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;

namespace Server.Match.Data.Sync.Client
{
	// Token: 0x0200002E RID: 46
	public class RemovePlayerSync
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public static void Load(SyncClientPacket C)
		{
			uint num = C.ReadUD();
			uint num2 = C.ReadUD();
			int num3 = (int)C.ReadC();
			int num4 = (int)C.ReadC();
			RoomModel room = RoomsManager.GetRoom(num, num2);
			if (room == null)
			{
				return;
			}
			if (num4 == 0)
			{
				RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
				return;
			}
			PlayerModel player = room.GetPlayer(num3, false);
			if (player != null)
			{
				player.ResetAllInfos();
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000020A2 File Offset: 0x000002A2
		public RemovePlayerSync()
		{
		}
	}
}
