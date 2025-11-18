using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_CHATTING_REQ : GameClientPacket
{
	private string string_0;

	private ChattingType chattingType_0;

	public override void Read()
	{
		chattingType_0 = (ChattingType)ReadH();
		string_0 = ReadU(ReadH() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || string.IsNullOrEmpty(string_0) || string_0.Length > 60 || player.Nickname.Length == 0)
			{
				return;
			}
			RoomModel room = player.Room;
			switch (chattingType_0)
			{
			case ChattingType.Team:
			{
				if (room == null)
				{
					break;
				}
				SlotModel slotModel = room.Slots[player.SlotId];
				int[] teamArray = room.GetTeamArray(slotModel.Team);
				using PROTOCOL_ROOM_CHATTING_ACK pROTOCOL_ROOM_CHATTING_ACK2 = new PROTOCOL_ROOM_CHATTING_ACK((int)chattingType_0, slotModel.Id, player.UseChatGM(), string_0);
				byte[] completeBytes2 = pROTOCOL_ROOM_CHATTING_ACK2.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-1");
				lock (room.Slots)
				{
					int[] array = teamArray;
					foreach (int num in array)
					{
						SlotModel slotModel3 = room.Slots[num];
						if (slotModel3 != null)
						{
							Account playerBySlot2 = room.GetPlayerBySlot(slotModel3);
							if (playerBySlot2 != null && AllUtils.SlotValidMessage(slotModel, slotModel3))
							{
								playerBySlot2.SendCompletePacket(completeBytes2, pROTOCOL_ROOM_CHATTING_ACK2.GetType().Name);
							}
						}
					}
					break;
				}
			}
			case ChattingType.All:
			case ChattingType.Lobby:
			{
				if (room != null)
				{
					if (AllUtils.ServerCommands(player, string_0))
					{
						break;
					}
					SlotModel slotModel = room.Slots[player.SlotId];
					using PROTOCOL_ROOM_CHATTING_ACK pROTOCOL_ROOM_CHATTING_ACK = new PROTOCOL_ROOM_CHATTING_ACK((int)chattingType_0, slotModel.Id, player.UseChatGM(), string_0);
					byte[] completeBytes = pROTOCOL_ROOM_CHATTING_ACK.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-2");
					lock (room.Slots)
					{
						SlotModel[] slots = room.Slots;
						foreach (SlotModel slotModel2 in slots)
						{
							Account playerBySlot = room.GetPlayerBySlot(slotModel2);
							if (playerBySlot != null && AllUtils.SlotValidMessage(slotModel, slotModel2))
							{
								playerBySlot.SendCompletePacket(completeBytes, pROTOCOL_ROOM_CHATTING_ACK.GetType().Name);
							}
						}
						break;
					}
				}
				ChannelModel channel = player.GetChannel();
				if (channel != null && !AllUtils.ServerCommands(player, string_0))
				{
					using (PROTOCOL_LOBBY_CHATTING_ACK packet = new PROTOCOL_LOBBY_CHATTING_ACK(player, string_0))
					{
						channel.SendPacketToWaitPlayers(packet);
						break;
					}
				}
				break;
			}
			case ChattingType.None:
			case ChattingType.Whisper:
			case ChattingType.Reply:
			case ChattingType.Clan:
			case ChattingType.Match:
			case ChattingType.ClanMemberPage:
				break;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
