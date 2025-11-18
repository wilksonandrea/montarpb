using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D4 RID: 468
	public class PROTOCOL_ROOM_REQUEST_MAIN_REQ : GameClientPacket
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000266A8 File Offset: 0x000248A8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						if (room.State == RoomState.READY)
						{
							if (room.LeaderSlot != player.SlotId)
							{
								List<Account> allPlayers = room.GetAllPlayers();
								if (allPlayers.Count != 0)
								{
									if (player.IsGM())
									{
										this.method_0(room, allPlayers, player.SlotId);
									}
									else
									{
										if (!room.RequestRoomMaster.Contains(player.PlayerId))
										{
											room.RequestRoomMaster.Add(player.PlayerId);
											if (room.RequestRoomMaster.Count() < allPlayers.Count / 2 + 1)
											{
												using (PROTOCOL_ROOM_REQUEST_MAIN_ACK protocol_ROOM_REQUEST_MAIN_ACK = new PROTOCOL_ROOM_REQUEST_MAIN_ACK(player.SlotId))
												{
													this.method_1(protocol_ROOM_REQUEST_MAIN_ACK, allPlayers);
												}
											}
										}
										if (room.RequestRoomMaster.Count() >= allPlayers.Count / 2 + 1)
										{
											this.method_0(room, allPlayers, player.SlotId);
										}
									}
								}
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_ACK(2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00026810 File Offset: 0x00024A10
		private void method_0(RoomModel roomModel_0, List<Account> list_0, int int_0)
		{
			roomModel_0.SetNewLeader(int_0, SlotState.EMPTY, -1, false);
			using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK protocol_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int_0))
			{
				this.method_1(protocol_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK, list_0);
			}
			roomModel_0.UpdateSlotsInfo();
			roomModel_0.RequestRoomMaster.Clear();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00026864 File Offset: 0x00024A64
		private void method_1(GameServerPacket gameServerPacket_0, List<Account> list_0)
		{
			byte[] completeBytes = gameServerPacket_0.GetCompleteBytes("PROTOCOL_ROOM_REQUEST_MAIN_REQ");
			foreach (Account account in list_0)
			{
				account.SendCompletePacket(completeBytes, gameServerPacket_0.GetType().Name);
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_REQUEST_MAIN_REQ()
		{
		}
	}
}
