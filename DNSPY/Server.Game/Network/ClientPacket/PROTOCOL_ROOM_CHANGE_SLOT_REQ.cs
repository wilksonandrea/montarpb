using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C6 RID: 454
	public class PROTOCOL_ROOM_CHANGE_SLOT_REQ : GameClientPacket
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00005776 File Offset: 0x00003976
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00025190 File Offset: 0x00023390
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.LeaderSlot == player.SlotId && room.GetSlot(this.int_0 & 268435455, out slotModel))
					{
						if ((this.int_0 & 268435456) == 268435456)
						{
							this.method_1(player, room, slotModel);
						}
						else
						{
							this.method_0(player, room, slotModel);
						}
						SlotState state = slotModel.State;
						if (state - SlotState.UNKNOWN > 8)
						{
							if (state - SlotState.LOAD <= 5)
							{
								this.uint_0 = 2147484673U;
							}
						}
						else
						{
							Account playerBySlot = room.GetPlayerBySlot(slotModel);
							if (playerBySlot != null && !playerBySlot.AntiKickGM && ((slotModel.State != SlotState.READY && ((room.ChannelType == ChannelType.Clan && room.State != RoomState.COUNTDOWN) || room.ChannelType != ChannelType.Clan)) || (slotModel.State == SlotState.READY && ((room.ChannelType == ChannelType.Clan && room.State == RoomState.READY) || room.ChannelType != ChannelType.Clan))))
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
								room.RemovePlayer(playerBySlot, slotModel, false, 0);
							}
						}
					}
					else
					{
						this.uint_0 = 2147484673U;
					}
					this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_SLOT_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_CHANGE_SLOT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00025358 File Offset: 0x00023558
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

		// Token: 0x060004CF RID: 1231 RVA: 0x000253BC File Offset: 0x000235BC
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
						if (!roomModel_0.IsBotMode())
						{
						}
						roomModel_0.ChangeSlotState(slotModel_0, SlotState.EMPTY, true);
					}
					return;
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CHANGE_SLOT_REQ()
		{
		}

		// Token: 0x04000358 RID: 856
		private int int_0;

		// Token: 0x04000359 RID: 857
		private uint uint_0;
	}
}
