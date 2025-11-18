using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

// Token: 0x020001D2 RID: 466
internal class Class3 : GameClientPacket
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x00004D24 File Offset: 0x00002F24
	public virtual void Read()
	{
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000263F0 File Offset: 0x000245F0
	public virtual void Run()
	{
		try
		{
			Account player = this.Client.Player;
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY)
				{
					SlotModel[] slots = room.Slots;
					lock (slots)
					{
						for (int i = 0; i < 18; i++)
						{
							SlotModel slotModel = room.Slots[i];
							if (slotModel.PlayerId > 0L && i != room.LeaderSlot)
							{
								this.list_0.Add(slotModel);
							}
						}
					}
					if (this.list_0.Count > 0)
					{
						SlotModel slotModel2 = this.list_0[new Random().Next(this.list_0.Count)];
						if (room.GetPlayerBySlot(slotModel2) != null)
						{
							room.SetNewLeader(slotModel2.Id, SlotState.EMPTY, room.LeaderSlot, false);
							using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK protocol_ROOM_REQUEST_MAIN_CHANGE_ACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(slotModel2.Id))
							{
								room.SendPacketToPlayers(protocol_ROOM_REQUEST_MAIN_CHANGE_ACK);
							}
							room.UpdateSlotsInfo();
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(2147483648U));
						}
						this.list_0 = null;
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(2147483648U));
					}
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("ROOM_RANDOM_HOST2_REC: " + ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00005829 File Offset: 0x00003A29
	public Class3()
	{
	}

	// Token: 0x04000381 RID: 897
	private List<SlotModel> list_0 = new List<SlotModel>();
}
