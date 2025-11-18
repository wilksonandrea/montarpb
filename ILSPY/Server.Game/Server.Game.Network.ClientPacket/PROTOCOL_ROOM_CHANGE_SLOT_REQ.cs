using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_SLOT_REQ : GameClientPacket
{
	private int int_0;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadD();
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
			RoomModel room = player.Room;
			if (room != null && room.LeaderSlot == player.SlotId && room.GetSlot(int_0 & 0xFFFFFFF, out var Slot))
			{
				if ((int_0 & 0x10000000) == 268435456)
				{
					method_1(player, room, Slot);
				}
				else
				{
					method_0(player, room, Slot);
				}
				switch (Slot.State)
				{
				case SlotState.LOAD:
				case SlotState.RENDEZVOUS:
				case SlotState.PRESTART:
				case SlotState.BATTLE_LOAD:
				case SlotState.BATTLE_READY:
				case SlotState.BATTLE:
					uint_0 = 2147484673u;
					break;
				case SlotState.UNKNOWN:
				case SlotState.SHOP:
				case SlotState.INFO:
				case SlotState.CLAN:
				case SlotState.INVENTORY:
				case SlotState.GACHA:
				case SlotState.GIFTSHOP:
				case SlotState.NORMAL:
				case SlotState.READY:
				{
					Account playerBySlot = room.GetPlayerBySlot(Slot);
					if (playerBySlot != null && !playerBySlot.AntiKickGM && ((Slot.State != SlotState.READY && ((room.ChannelType == ChannelType.Clan && room.State != RoomState.COUNTDOWN) || room.ChannelType != ChannelType.Clan)) || (Slot.State == SlotState.READY && ((room.ChannelType == ChannelType.Clan && room.State == RoomState.READY) || room.ChannelType != ChannelType.Clan))))
					{
						playerBySlot.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK());
						if (!room.KickedPlayersHost.ContainsKey(player.PlayerId))
						{
							room.KickedPlayersHost.Add(player.PlayerId, DateTimeUtil.Now());
						}
						else
						{
							room.KickedPlayersHost[player.PlayerId] = DateTimeUtil.Now();
						}
						room.RemovePlayer(playerBySlot, Slot, WarnAllPlayers: false);
					}
					break;
				}
				}
			}
			else
			{
				uint_0 = 2147484673u;
			}
			Client.SendPacket(new PROTOCOL_ROOM_CHANGE_SLOT_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_CHANGE_SLOT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
	{
		if (slotModel_0.State == SlotState.EMPTY)
		{
			if (roomModel_0.Competitive && !AllUtils.CanCloseSlotCompetitive(roomModel_0, slotModel_0))
			{
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, bool_1: true, Translation.GetLabel("CompetitiveSlotMin")));
			}
			roomModel_0.ChangeSlotState(slotModel_0, SlotState.CLOSE, SendInfo: true);
		}
	}

	private void method_1(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
	{
		if ((int_0 & 0x10000000) != 268435456 || slotModel_0.State != SlotState.CLOSE)
		{
			return;
		}
		MapMatch mapLimit = SystemMapXML.GetMapLimit((int)roomModel_0.MapId, (int)roomModel_0.Rule);
		if (mapLimit != null && slotModel_0.Id < mapLimit.Limit)
		{
			if (roomModel_0.Competitive && !AllUtils.CanOpenSlotCompetitive(roomModel_0, slotModel_0))
			{
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, bool_1: true, Translation.GetLabel("CompetitiveSlotMax")));
			}
			if (!roomModel_0.IsBotMode())
			{
			}
			roomModel_0.ChangeSlotState(slotModel_0, SlotState.EMPTY, SendInfo: true);
		}
	}
}
