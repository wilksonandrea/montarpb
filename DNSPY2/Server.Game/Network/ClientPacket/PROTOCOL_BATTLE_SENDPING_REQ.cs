using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000171 RID: 369
	public class PROTOCOL_BATTLE_SENDPING_REQ : GameClientPacket
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00005225 File Offset: 0x00003425
		public override void Read()
		{
			this.byte_0 = base.ReadB(16);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001D134 File Offset: 0x0001B334
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.GetSlot(player.SlotId, out slotModel))
					{
						int num = 0;
						if (slotModel != null && slotModel.State >= SlotState.BATTLE_READY)
						{
							if (room.State == RoomState.BATTLE)
							{
								room.Ping = (int)this.byte_0[room.LeaderSlot];
							}
							using (PROTOCOL_BATTLE_SENDPING_ACK protocol_BATTLE_SENDPING_ACK = new PROTOCOL_BATTLE_SENDPING_ACK(this.byte_0))
							{
								List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
								if (allPlayers.Count == 0)
								{
									return;
								}
								byte[] completeBytes = protocol_BATTLE_SENDPING_ACK.GetCompleteBytes(base.GetType().Name);
								foreach (Account account in allPlayers)
								{
									SlotModel slot = room.GetSlot(account.SlotId);
									if (slot != null && slot.State >= SlotState.BATTLE_READY)
									{
										account.SendCompletePacket(completeBytes, protocol_BATTLE_SENDPING_ACK.GetType().Name);
									}
									else
									{
										num++;
									}
								}
							}
							if (num == 0)
							{
								room.SpawnReadyPlayers();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_SENDPING_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_SENDPING_REQ()
		{
		}

		// Token: 0x040002A3 RID: 675
		private byte[] byte_0;
	}
}
