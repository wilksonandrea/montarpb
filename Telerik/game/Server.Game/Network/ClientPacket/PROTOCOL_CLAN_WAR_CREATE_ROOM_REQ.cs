using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ : GameClientPacket
	{
		private int int_0 = -1;

		private int int_1;

		private int int_2;

		private int int_3;

		private string string_0;

		private StageOptions stageOptions_0;

		private MapIdEnum mapIdEnum_0;

		private MapRules mapRules_0;

		private RoomCondition roomCondition_0;

		private RoomWeaponsFlag roomWeaponsFlag_0;

		private RoomStageFlag roomStageFlag_0;

		public PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ()
		{
		}

		public override void Read()
		{
			this.int_3 = base.ReadH();
			base.ReadD();
			base.ReadD();
			base.ReadH();
			this.string_0 = base.ReadU(46);
			this.mapIdEnum_0 = (MapIdEnum)base.ReadC();
			this.mapRules_0 = (MapRules)base.ReadC();
			this.stageOptions_0 = (StageOptions)base.ReadC();
			this.roomCondition_0 = (RoomCondition)base.ReadC();
			base.ReadC();
			base.ReadC();
			this.int_1 = base.ReadC();
			this.int_2 = base.ReadC();
			this.roomWeaponsFlag_0 = (RoomWeaponsFlag)base.ReadH();
			this.roomStageFlag_0 = (RoomStageFlag)base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0)
				{
					ChannelModel channel = player.GetChannel();
					MatchModel match = player.Match;
					if (channel != null && match != null)
					{
						MatchModel matchModel = channel.GetMatch(this.int_3);
						if (matchModel != null)
						{
							lock (channel.Rooms)
							{
								for (int i = 0; i < channel.MaxRooms; i++)
								{
									if (channel.GetRoom(i) == null)
									{
										RoomModel roomModel = new RoomModel(i, channel)
										{
											Name = this.string_0,
											MapId = this.mapIdEnum_0,
											Rule = this.mapRules_0,
											Stage = this.stageOptions_0,
											RoomType = this.roomCondition_0
										};
										roomModel.SetSlotCount(this.int_1, true, false);
										roomModel.Ping = this.int_2;
										roomModel.WeaponsFlag = this.roomWeaponsFlag_0;
										roomModel.Flag = this.roomStageFlag_0;
										roomModel.Password = "";
										roomModel.KillTime = 3;
										if (roomModel.AddPlayer(player) >= 0)
										{
											channel.AddRoom(roomModel);
											this.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(0, roomModel));
											this.int_0 = i;
											return;
										}
									}
								}
							}
							using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK pROTOCOLCLANWARENEMYINFOACK = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(matchModel))
							{
								using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK pROTOCOLCLANWARJOINROOMACK = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(matchModel, this.int_0, 0))
								{
									byte[] completeBytes = pROTOCOLCLANWARENEMYINFOACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-1");
									byte[] numArray = pROTOCOLCLANWARJOINROOMACK.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-2");
									foreach (Account allPlayer in match.GetAllPlayers(match.Leader))
									{
										allPlayer.SendCompletePacket(completeBytes, pROTOCOLCLANWARENEMYINFOACK.GetType().Name);
										allPlayer.SendCompletePacket(numArray, pROTOCOLCLANWARJOINROOMACK.GetType().Name);
										if (allPlayer.Match == null)
										{
											continue;
										}
										match.Slots[allPlayer.MatchSlot].State = SlotMatchState.Ready;
									}
								}
							}
							using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK pROTOCOLCLANWARENEMYINFOACK1 = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match))
							{
								using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK pROTOCOLCLANWARJOINROOMACK1 = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match, this.int_0, 1))
								{
									byte[] completeBytes1 = pROTOCOLCLANWARENEMYINFOACK1.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-3");
									byte[] numArray1 = pROTOCOLCLANWARJOINROOMACK1.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-4");
									foreach (Account account in matchModel.GetAllPlayers())
									{
										account.SendCompletePacket(completeBytes1, pROTOCOLCLANWARENEMYINFOACK1.GetType().Name);
										account.SendCompletePacket(numArray1, pROTOCOLCLANWARJOINROOMACK1.GetType().Name);
										if (account.Match == null)
										{
											continue;
										}
										match.Slots[account.MatchSlot].State = SlotMatchState.Ready;
									}
								}
							}
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