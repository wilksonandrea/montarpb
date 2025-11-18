using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D6 RID: 470
	public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : GameClientPacket
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000269E8 File Offset: 0x00024BE8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY && ComDiv.GetDuration(room.LastChangeTeam) >= 1.5 && !room.ChangingSlots)
					{
						List<SlotChange> list = new List<SlotChange>();
						SlotModel[] slots = room.Slots;
						lock (slots)
						{
							room.ChangingSlots = true;
							foreach (int num in room.FR_TEAM)
							{
								int num2 = num + 1;
								if (num == room.LeaderSlot)
								{
									room.LeaderSlot = num2;
								}
								else if (num2 == room.LeaderSlot)
								{
									room.LeaderSlot = num;
								}
								room.SwitchSlots(list, num2, num, true);
							}
							if (list.Count > 0)
							{
								using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_ROOM_TEAM_BALANCE_ACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, room.LeaderSlot, 2))
								{
									byte[] completeBytes = protocol_ROOM_TEAM_BALANCE_ACK.GetCompleteBytes(base.GetType().Name);
									foreach (Account account in room.GetAllPlayers())
									{
										account.SlotId = AllUtils.GetNewSlotId(account.SlotId);
										account.SendCompletePacket(completeBytes, protocol_ROOM_TEAM_BALANCE_ACK.GetType().Name);
									}
								}
							}
							room.ChangingSlots = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_CHANGE_TEAM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ()
		{
		}
	}
}
