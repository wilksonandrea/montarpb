using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_CHATTING_REQ : GameClientPacket
	{
		private string string_0;

		private ChattingType chattingType_0;

		public PROTOCOL_BASE_CHATTING_REQ()
		{
		}

		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadU(base.ReadH() * 2);
		}

		public override void Run()
		{
			SlotModel slots;
			int i;
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
						{
							break;
						}
						case ChattingType.All:
						case ChattingType.Lobby:
						{
							if (room == null)
							{
								ChannelModel channel = player.GetChannel();
								if (channel != null)
								{
									if (AllUtils.ServerCommands(player, this.string_0))
									{
										goto case ChattingType.ClanMemberPage;
									}
									using (PROTOCOL_LOBBY_CHATTING_ACK pROTOCOLLOBBYCHATTINGACK = new PROTOCOL_LOBBY_CHATTING_ACK(player, this.string_0, false))
									{
										channel.SendPacketToWaitPlayers(pROTOCOLLOBBYCHATTINGACK);
										goto case ChattingType.ClanMemberPage;
									}
								}
								else
								{
									break;
								}
							}
							else
							{
								if (AllUtils.ServerCommands(player, this.string_0))
								{
									goto case ChattingType.ClanMemberPage;
								}
								slots = room.Slots[player.SlotId];
								using (PROTOCOL_ROOM_CHATTING_ACK pROTOCOLROOMCHATTINGACK = new PROTOCOL_ROOM_CHATTING_ACK((int)this.chattingType_0, slots.Id, player.UseChatGM(), this.string_0))
								{
									byte[] completeBytes = pROTOCOLROOMCHATTINGACK.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-2");
									lock (room.Slots)
									{
										SlotModel[] slotModelArray = room.Slots;
										for (i = 0; i < (int)slotModelArray.Length; i++)
										{
											SlotModel slotModel = slotModelArray[i];
											Account playerBySlot = room.GetPlayerBySlot(slotModel);
											if (playerBySlot != null && AllUtils.SlotValidMessage(slots, slotModel))
											{
												playerBySlot.SendCompletePacket(completeBytes, pROTOCOLROOMCHATTINGACK.GetType().Name);
											}
										}
									}
									goto case ChattingType.ClanMemberPage;
								}
							}
							break;
						}
						case ChattingType.Team:
						{
							if (room != null)
							{
								slots = room.Slots[player.SlotId];
								int[] teamArray = room.GetTeamArray(slots.Team);
								using (PROTOCOL_ROOM_CHATTING_ACK pROTOCOLROOMCHATTINGACK1 = new PROTOCOL_ROOM_CHATTING_ACK((int)this.chattingType_0, slots.Id, player.UseChatGM(), this.string_0))
								{
									byte[] numArray = pROTOCOLROOMCHATTINGACK1.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-1");
									lock (room.Slots)
									{
										int[] ınt32Array = teamArray;
										for (i = 0; i < (int)ınt32Array.Length; i++)
										{
											int ınt32 = ınt32Array[i];
											SlotModel slots1 = room.Slots[ınt32];
											if (slots1 != null)
											{
												Account account = room.GetPlayerBySlot(slots1);
												if (account != null && AllUtils.SlotValidMessage(slots, slots1))
												{
													account.SendCompletePacket(numArray, pROTOCOLROOMCHATTINGACK1.GetType().Name);
												}
											}
										}
									}
									goto case ChattingType.ClanMemberPage;
								}
							}
							else
							{
								break;
							}
							break;
						}
						default:
						{
							goto case ChattingType.ClanMemberPage;
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}