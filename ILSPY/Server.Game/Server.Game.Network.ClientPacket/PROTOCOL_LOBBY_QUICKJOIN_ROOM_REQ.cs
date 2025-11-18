using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : GameClientPacket
{
	private int int_0;

	private List<RoomModel> list_0 = new List<RoomModel>();

	private List<QuickstartModel> list_1 = new List<QuickstartModel>();

	private QuickstartModel quickstartModel_0;

	public override void Read()
	{
		int_0 = ReadC();
		for (int i = 0; i < 3; i++)
		{
			QuickstartModel item = new QuickstartModel
			{
				MapId = ReadC(),
				Rule = ReadC(),
				StageOptions = ReadC(),
				Type = ReadC()
			};
			list_1.Add(item);
		}
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			player.Quickstart.Quickjoins[int_0] = list_1[int_0];
			ComDiv.UpdateDB("player_quickstarts", "owner_id", player.PlayerId, new string[4]
			{
				$"list{int_0}_map_id",
				$"list{int_0}_map_rule",
				$"list{int_0}_map_stage",
				$"list{int_0}_map_type"
			}, list_1[int_0].MapId, list_1[int_0].Rule, list_1[int_0].StageOptions, list_1[int_0].Type);
			if (player.Nickname.Length > 0 && player.Room == null && player.Match == null && player.GetChannel(out var Channel))
			{
				lock (Channel.Rooms)
				{
					foreach (RoomModel room in Channel.Rooms)
					{
						if (room.RoomType == RoomCondition.Tutorial)
						{
							continue;
						}
						quickstartModel_0 = list_1[int_0];
						if (quickstartModel_0.MapId != (int)room.MapId || quickstartModel_0.Rule != (int)room.Rule || quickstartModel_0.StageOptions != (int)room.Stage || quickstartModel_0.Type != (int)room.RoomType || room.Password.Length != 0 || room.Limit != 0 || (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM()))
						{
							continue;
						}
						SlotModel[] slots = room.Slots;
						foreach (SlotModel slotModel in slots)
						{
							if (slotModel.PlayerId == 0L && slotModel.State == SlotState.EMPTY)
							{
								list_0.Add(room);
								break;
							}
						}
					}
				}
			}
			if (list_0.Count == 0)
			{
				Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(2147483648u, list_1, null, null));
			}
			else
			{
				RoomModel roomModel = list_0[new Random().Next(list_0.Count)];
				if (roomModel != null && roomModel.GetLeader() != null && roomModel.AddPlayer(player) >= 0)
				{
					player.ResetPages();
					using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK packet = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
					{
						roomModel.SendPacketToPlayers(packet, player.PlayerId);
					}
					roomModel.UpdateSlotsInfo();
					Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0u, player));
					Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0u, list_1, roomModel, quickstartModel_0));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(2147483648u, null, null, null));
				}
			}
			list_0 = null;
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
