using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D3 RID: 467
	public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ : GameClientPacket
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000583C File Offset: 0x00003A3C
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000265BC File Offset: 0x000247BC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.LeaderSlot != this.int_0 && room.Slots[this.int_0].PlayerId != 0L)
					{
						if (room.State == RoomState.READY && room.LeaderSlot == player.SlotId)
						{
							room.SetNewLeader(this.int_0, SlotState.EMPTY, room.LeaderSlot, false);
							using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK protocol_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(this.int_0))
							{
								room.SendPacketToPlayers(protocol_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK);
							}
							room.UpdateSlotsInfo();
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ()
		{
		}

		// Token: 0x04000382 RID: 898
		private int int_0;
	}
}
