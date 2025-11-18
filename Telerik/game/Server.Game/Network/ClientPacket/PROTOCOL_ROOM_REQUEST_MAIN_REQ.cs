using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_REQUEST_MAIN_REQ : GameClientPacket
	{
		public PROTOCOL_ROOM_REQUEST_MAIN_REQ()
		{
		}

		private void method_0(RoomModel roomModel_0, List<Account> list_0, int int_0)
		{
			roomModel_0.SetNewLeader(int_0, SlotState.EMPTY, -1, false);
			using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK pROTOCOLROOMREQUESTMAINCHANGEWHOACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int_0))
			{
				this.method_1(pROTOCOLROOMREQUESTMAINCHANGEWHOACK, list_0);
			}
			roomModel_0.UpdateSlotsInfo();
			roomModel_0.RequestRoomMaster.Clear();
		}

		private void method_1(GameServerPacket gameServerPacket_0, List<Account> list_0)
		{
			byte[] completeBytes = gameServerPacket_0.GetCompleteBytes("PROTOCOL_ROOM_REQUEST_MAIN_REQ");
			foreach (Account list0 in list_0)
			{
				list0.SendCompletePacket(completeBytes, gameServerPacket_0.GetType().Name);
			}
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room == null)
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_ACK((uint)-2147483648));
					}
					else
					{
						if (room.State == RoomState.READY)
						{
							if (room.LeaderSlot == player.SlotId)
							{
								return;
							}
							List<Account> allPlayers = room.GetAllPlayers();
							if (allPlayers.Count == 0)
							{
								return;
							}
							else if (!player.IsGM())
							{
								if (!room.RequestRoomMaster.Contains(player.PlayerId))
								{
									room.RequestRoomMaster.Add(player.PlayerId);
									if (room.RequestRoomMaster.Count() < allPlayers.Count / 2 + 1)
									{
										using (PROTOCOL_ROOM_REQUEST_MAIN_ACK pROTOCOLROOMREQUESTMAINACK = new PROTOCOL_ROOM_REQUEST_MAIN_ACK(player.SlotId))
										{
											this.method_1(pROTOCOLROOMREQUESTMAINACK, allPlayers);
										}
									}
								}
								if (room.RequestRoomMaster.Count() >= allPlayers.Count / 2 + 1)
								{
									this.method_0(room, allPlayers, player.SlotId);
									goto Label1;
								}
								else
								{
									goto Label1;
								}
							}
							else
							{
								this.method_0(room, allPlayers, player.SlotId);
								goto Label1;
							}
						}
						return;
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_REQUEST_MAIN_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}