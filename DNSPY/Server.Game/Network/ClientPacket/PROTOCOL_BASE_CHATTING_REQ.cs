using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000139 RID: 313
	public class PROTOCOL_BASE_CHATTING_REQ : GameClientPacket
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00004F08 File Offset: 0x00003108
		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadU((int)(base.ReadH() * 2));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000185C0 File Offset: 0x000167C0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && !string.IsNullOrEmpty(this.string_0) && this.string_0.Length <= 60 && player.Nickname.Length != 0)
				{
					RoomModel room = player.Room;
					switch (this.chattingType_0)
					{
					case ChattingType.None:
					case ChattingType.Whisper:
					case ChattingType.Reply:
					case ChattingType.Clan:
					case ChattingType.Match:
					case ChattingType.ClanMemberPage:
						goto IL_272;
					case ChattingType.All:
					case ChattingType.Lobby:
						break;
					case ChattingType.Team:
					{
						if (room == null)
						{
							return;
						}
						SlotModel slotModel = room.Slots[player.SlotId];
						int[] teamArray = room.GetTeamArray(slotModel.Team);
						using (PROTOCOL_ROOM_CHATTING_ACK protocol_ROOM_CHATTING_ACK = new PROTOCOL_ROOM_CHATTING_ACK((int)this.chattingType_0, slotModel.Id, player.UseChatGM(), this.string_0))
						{
							byte[] completeBytes = protocol_ROOM_CHATTING_ACK.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-1");
							SlotModel[] array = room.Slots;
							lock (array)
							{
								foreach (int num in teamArray)
								{
									SlotModel slotModel2 = room.Slots[num];
									if (slotModel2 != null)
									{
										Account playerBySlot = room.GetPlayerBySlot(slotModel2);
										if (playerBySlot != null && AllUtils.SlotValidMessage(slotModel, slotModel2))
										{
											playerBySlot.SendCompletePacket(completeBytes, protocol_ROOM_CHATTING_ACK.GetType().Name);
										}
									}
								}
							}
							goto IL_272;
						}
						break;
					}
					default:
						goto IL_272;
					}
					if (room != null)
					{
						if (AllUtils.ServerCommands(player, this.string_0))
						{
							goto IL_272;
						}
						SlotModel slotModel = room.Slots[player.SlotId];
						using (PROTOCOL_ROOM_CHATTING_ACK protocol_ROOM_CHATTING_ACK2 = new PROTOCOL_ROOM_CHATTING_ACK((int)this.chattingType_0, slotModel.Id, player.UseChatGM(), this.string_0))
						{
							byte[] completeBytes2 = protocol_ROOM_CHATTING_ACK2.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-2");
							SlotModel[] array = room.Slots;
							lock (array)
							{
								foreach (SlotModel slotModel3 in room.Slots)
								{
									Account playerBySlot2 = room.GetPlayerBySlot(slotModel3);
									if (playerBySlot2 != null && AllUtils.SlotValidMessage(slotModel, slotModel3))
									{
										playerBySlot2.SendCompletePacket(completeBytes2, protocol_ROOM_CHATTING_ACK2.GetType().Name);
									}
								}
							}
							goto IL_272;
						}
					}
					ChannelModel channel = player.GetChannel();
					if (channel != null)
					{
						if (!AllUtils.ServerCommands(player, this.string_0))
						{
							using (PROTOCOL_LOBBY_CHATTING_ACK protocol_LOBBY_CHATTING_ACK = new PROTOCOL_LOBBY_CHATTING_ACK(player, this.string_0, false))
							{
								channel.SendPacketToWaitPlayers(protocol_LOBBY_CHATTING_ACK);
							}
						}
					}
					IL_272:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_CHATTING_REQ()
		{
		}

		// Token: 0x0400023A RID: 570
		private string string_0;

		// Token: 0x0400023B RID: 571
		private ChattingType chattingType_0;
	}
}
