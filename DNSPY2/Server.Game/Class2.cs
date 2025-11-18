using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

// Token: 0x020001C7 RID: 455
internal class Class2 : GameClientPacket
{
	// Token: 0x060004D1 RID: 1233 RVA: 0x00004D24 File Offset: 0x00002F24
	public virtual void Read()
	{
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00025464 File Offset: 0x00023664
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
						int num = new Random().Next(this.list_0.Count);
						SlotModel slotModel2 = this.list_0[num];
						this.uint_0 = (uint)((room.GetPlayerBySlot(slotModel2) != null) ? slotModel2.Id : int.MinValue);
						this.list_0 = null;
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
				}
				else
				{
					this.uint_0 = 2147483648U;
				}
				this.Client.SendPacket(new PROTOCOL_ROOM_CHECK_MAIN_ACK(this.uint_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_CHECK_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00005784 File Offset: 0x00003984
	public Class2()
	{
	}

	// Token: 0x0400035A RID: 858
	private uint uint_0;

	// Token: 0x0400035B RID: 859
	private List<SlotModel> list_0 = new List<SlotModel>();
}
