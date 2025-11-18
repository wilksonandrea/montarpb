using System;
using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;

namespace Server.Match.Data.Sync.Client
{
	// Token: 0x0200002D RID: 45
	public class MatchRoundSync
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000A260 File Offset: 0x00008460
		public static void Load(SyncClientPacket C)
		{
			uint num = C.ReadUD();
			uint num2 = C.ReadUD();
			int num3 = (int)C.ReadC();
			bool flag = C.ReadC() == 1;
			RoomModel room = RoomsManager.GetRoom(num, num2);
			if (room != null)
			{
				room.ServerRound = num3;
				room.IsTeamSwap = flag;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000020A2 File Offset: 0x000002A2
		public MatchRoundSync()
		{
		}
	}
}
