using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_CHANGE_SLOT_REQ : GameClientPacket
	{
		private int int_0;

		private uint uint_0;

		public PROTOCOL_ROOM_CHANGE_SLOT_REQ()
		{
		}

		private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
		{
			if (slotModel_0.State != SlotState.EMPTY)
			{
				return;
			}
			if (roomModel_0.Competitive && !AllUtils.CanCloseSlotCompetitive(roomModel_0, slotModel_0))
			{
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, true, Translation.GetLabel("CompetitiveSlotMin")));
			}
			roomModel_0.ChangeSlotState(slotModel_0, SlotState.CLOSE, true);
		}

		private void method_1(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
		{
			if ((this.int_0 & 268435456) == 268435456)
			{
				if (slotModel_0.State == SlotState.CLOSE)
				{
					MapMatch mapLimit = SystemMapXML.GetMapLimit((int)roomModel_0.MapId, (int)roomModel_0.Rule);
					if (mapLimit != null && slotModel_0.Id < mapLimit.Limit)
					{
						if (roomModel_0.Competitive && !AllUtils.CanOpenSlotCompetitive(roomModel_0, slotModel_0))
						{
							account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, true, Translation.GetLabel("CompetitiveSlotMax")));
						}
						roomModel_0.IsBotMode();
						roomModel_0.ChangeSlotState(slotModel_0, SlotState.EMPTY, true);
					}
					return;
				}
			}
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			SlotModel slotModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room == null || room.LeaderSlot != player.SlotId || !room.GetSlot(this.int_0 & 268435455, out slotModel))
					{
						this.uint_0 = -2147482623;
					}
					else
					{
						if ((this.int_0 & 268435456) != 268435456)
						{
							this.method_0(player, room, slotModel);
						}
						else
						{
							this.method_1(player, room, slotModel);
						}
						SlotState state = slotModel.State;
						if ((int)state - (int)SlotState.UNKNOWN <= (int)SlotState.GIFTSHOP)
						{
							Account playerBySlot = room.GetPlayerBySlot(slotModel);
							if (playerBySlot != null && !playerBySlot.AntiKickGM && (slotModel.State != SlotState.READY && (room.ChannelType == ChannelType.Clan && room.State != RoomState.COUNTDOWN || room.ChannelType != ChannelType.Clan) || slotModel.State == SlotState.READY && (room.ChannelType == ChannelType.Clan && room.State == RoomState.READY || room.ChannelType != ChannelType.Clan)))
							{
								playerBySlot.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK());
								if (room.KickedPlayersHost.ContainsKey(player.PlayerId))
								{
									room.KickedPlayersHost[player.PlayerId] = DateTimeUtil.Now();
								}
								else
								{
									room.KickedPlayersHost.Add(player.PlayerId, DateTimeUtil.Now());
								}
								room.RemovePlayer(playerBySlot, slotModel, false, 0);
							}
						}
						else if ((int)state - (int)SlotState.LOAD <= (int)SlotState.CLAN)
						{
							this.uint_0 = -2147482623;
						}
					}
					this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_SLOT_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_CHANGE_SLOT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}