using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Data.Utils
{
	public static class AllUtils
	{
		public static int AddFriend(Account Owner, Account Friend, int state)
		{
			int ınt32;
			if (Owner == null || Friend == null)
			{
				return -1;
			}
			try
			{
				FriendModel friend = Owner.Friend.GetFriend(Friend.PlayerId);
				if (friend != null)
				{
					if (friend.Removed)
					{
						friend.Removed = false;
						DaoManagerSQL.UpdatePlayerFriendBlock(Owner.PlayerId, friend);
						SendFriendInfo.Load(Owner, friend, 1);
					}
					ınt32 = 1;
				}
				else
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.get_Parameters().AddWithValue("@friend", Friend.PlayerId);
						npgsqlCommand.get_Parameters().AddWithValue("@owner", Owner.PlayerId);
						npgsqlCommand.get_Parameters().AddWithValue("@state", state);
						npgsqlCommand.CommandText = "INSERT INTO player_friends (id, owner_id, state) VALUES (@friend, @owner, @state)";
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
					lock (Owner.Friend.Friends)
					{
						FriendModel friendModel = new FriendModel(Friend.PlayerId, Friend.Rank, Friend.NickColor, Friend.Nickname, Friend.IsOnline, Friend.Status)
						{
							State = state,
							Removed = false
						};
						Owner.Friend.Friends.Add(friendModel);
						SendFriendInfo.Load(Owner, friendModel, 0);
					}
					ınt32 = 0;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("AllUtils.AddFriend: ", exception.Message), LoggerType.Error, exception);
				ınt32 = -1;
			}
			return ınt32;
		}

		public static void BattleEndKills(RoomModel room)
		{
			AllUtils.smethod_5(room, room.IsBotMode());
		}

		public static void BattleEndKills(RoomModel room, bool isBotMode)
		{
			AllUtils.smethod_5(room, isBotMode);
		}

		public static void BattleEndKillsFreeForAll(RoomModel room)
		{
			AllUtils.smethod_6(room);
		}

		public static void BattleEndPlayersCount(RoomModel Room, bool IsBotMode)
		{
			if (Room == null | IsBotMode || !Room.IsPreparing())
			{
				return;
			}
			int ınt32 = 0;
			int ınt321 = 0;
			int ınt322 = 0;
			int ınt323 = 0;
			SlotModel[] slots = Room.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.State == SlotState.BATTLE)
				{
					if (slotModel.Team != TeamEnum.FR_TEAM)
					{
						ınt32++;
					}
					else
					{
						ınt321++;
					}
				}
				else if (slotModel.State >= SlotState.LOAD)
				{
					if (slotModel.Team != TeamEnum.FR_TEAM)
					{
						ınt322++;
					}
					else
					{
						ınt323++;
					}
				}
			}
			if ((ınt321 == 0 || ınt32 == 0) && Room.State == RoomState.BATTLE || (ınt323 == 0 || ınt322 == 0) && Room.State <= RoomState.PRE_BATTLE)
			{
				AllUtils.EndBattle(Room, IsBotMode);
			}
		}

		public static void BattleEndRound(RoomModel Room, TeamEnum Winner, bool ForceRestart, FragInfos Kills, SlotModel Killer)
		{
			Room.MatchEndTime.StartJob(1250, (object object_0) => {
				AllUtils.smethod_2(Room, Winner, ForceRestart, Kills, Killer);
				lock (object_0)
				{
					Room.MatchEndTime.StopJob();
				}
			});
		}

		public static void BattleEndRound(RoomModel Room, TeamEnum Winner, RoundEndType Motive)
		{
			using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(Room, Winner, Motive))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDENDACK, SlotState.BATTLE, 0);
			}
			Room.StopBomb();
			int roundsByMask = Room.GetRoundsByMask();
			if (Room.FRRounds >= roundsByMask || Room.CTRounds >= roundsByMask)
			{
				AllUtils.EndBattle(Room, Room.IsBotMode(), Winner);
				return;
			}
			Room.ChangeRounds();
			RoundSync.SendUDPRoundSync(Room);
			Room.RoundRestart();
		}

		public static void BattleEndRoundPlayersCount(RoomModel Room)
		{
			int ınt32;
			int ınt321;
			int ınt322;
			int ınt323;
			if (!Room.RoundTime.IsTimer() && (Room.RoomType == RoomCondition.Bomb || Room.RoomType == RoomCondition.Annihilation || Room.RoomType == RoomCondition.Destroy || Room.RoomType == RoomCondition.Ace))
			{
				Room.GetPlayingPlayers(true, out ınt32, out ınt321, out ınt322, out ınt323);
				AllUtils.smethod_4(Room, ref ınt32, ref ınt321, ref ınt322, ref ınt323);
				if (ınt322 == ınt32)
				{
					if (!Room.ActiveC4)
					{
						if (!Room.SwapRound)
						{
							Room.CTRounds++;
						}
						else
						{
							Room.FRRounds++;
						}
					}
					AllUtils.BattleEndRound(Room, (Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM), false, null, null);
					return;
				}
				if (ınt323 == ınt321)
				{
					if (!Room.SwapRound)
					{
						Room.FRRounds++;
					}
					else
					{
						Room.CTRounds++;
					}
					AllUtils.BattleEndRound(Room, (Room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM), true, null, null);
				}
			}
		}

		public static void CalculateBattlePass(Account Player, SlotModel Slot, BattlePassModel CurrentSC)
		{
			PlayerBattlepass battlepass = Player.Battlepass;
			if (CurrentSC == null || battlepass == null)
			{
				return;
			}
			if (battlepass.Id == CurrentSC.Id)
			{
				if (battlepass.Level < CurrentSC.Cards.Count)
				{
					Slot.SeasonPoint += ComDiv.Percentage(Slot.Exp, 35);
					int seasonPoint = Slot.SeasonPoint + ComDiv.Percentage(Slot.SeasonPoint, Slot.BonusBattlePass);
					PlayerBattlepass totalPoints = battlepass;
					totalPoints.TotalPoints = totalPoints.TotalPoints + seasonPoint;
					PlayerBattlepass dailyPoints = battlepass;
					dailyPoints.DailyPoints = dailyPoints.DailyPoints + seasonPoint;
					uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
					if (ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "total_points", "daily_points", "last_record" }, new object[] { battlepass.TotalPoints, battlepass.DailyPoints, (long)((ulong)uInt32) }))
					{
						battlepass.LastRecord = uInt32;
					}
					Player.UpdateSeasonpass = true;
				}
				else
				{
					Player.UpdateSeasonpass = true;
				}
			}
			AllUtils.smethod_19(Player, battlepass, CurrentSC);
		}

		public static void CalculateCompetitive(RoomModel Room, Account Player, SlotModel Slot, bool HaveWin)
		{
			if (Room.Competitive)
			{
				int allKills = (HaveWin ? 50 : -30);
				allKills = allKills + 2 * Slot.AllKills;
				allKills += Slot.AllAssists;
				allKills -= Slot.AllDeaths;
				PlayerCompetitive competitive = Player.Competitive;
				competitive.Points = competitive.Points + allKills;
				if (Player.Competitive.Points < 0)
				{
					Player.Competitive.Points = 0;
				}
				AllUtils.smethod_20(Player.Competitive);
				string label = Translation.GetLabel("CompetitivePointsEarned", new object[] { allKills });
				string str = Translation.GetLabel("CompetitiveRank", new object[] { Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points });
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, string.Concat(label, "\n\r", str)));
			}
		}

		public static bool CanCloseSlotCompetitive(RoomModel Room, SlotModel Closing)
		{
			return Room.Slots.Where<SlotModel>((SlotModel slotModel_1) => {
				if (slotModel_1.Team != Closing.Team)
				{
					return false;
				}
				return slotModel_1.State != SlotState.CLOSE;
			}).Count<SlotModel>() > 3;
		}

		public static bool CanOpenSlotCompetitive(RoomModel Room, SlotModel Opening)
		{
			return Room.Slots.Where<SlotModel>((SlotModel slotModel_1) => {
				if (slotModel_1.Team != Opening.Team)
				{
					return false;
				}
				return slotModel_1.State != SlotState.CLOSE;
			}).Count<SlotModel>() < 5;
		}

		public static bool ChangeCostume(SlotModel Slot, TeamEnum CostumeTeam)
		{
			if (Slot.CostumeTeam != CostumeTeam)
			{
				Slot.CostumeTeam = CostumeTeam;
			}
			return Slot.CostumeTeam == CostumeTeam;
		}

		public static bool ChannelRequirementCheck(Account Player, ChannelModel Channel)
		{
			if (Player.IsGM())
			{
				return false;
			}
			if (Channel.Type == ChannelType.Clan && Player.ClanId == 0)
			{
				return true;
			}
			if (Channel.Type == ChannelType.Novice && Player.Statistic.GetKDRatio() > 40 && Player.Statistic.GetSeasonKDRatio() > 40)
			{
				return true;
			}
			if (Channel.Type == ChannelType.Training && Player.Rank >= 4)
			{
				return true;
			}
			if (Channel.Type == ChannelType.Special && Player.Rank <= 25)
			{
				return true;
			}
			if (Channel.Type == ChannelType.Blocked)
			{
				return true;
			}
			return false;
		}

		public static bool Check4vs4(RoomModel Room, bool IsLeader, ref int PlayerFR, ref int PlayerCT, ref int TotalEnemies)
		{
			if (!IsLeader)
			{
				if (PlayerFR + PlayerCT >= 8)
				{
					return true;
				}
				return false;
			}
			int playerFR = PlayerFR + PlayerCT + 1;
			if (playerFR > 8)
			{
				int ınt32 = playerFR - 8;
				if (ınt32 > 0)
				{
					for (int i = 15; i >= 0; i--)
					{
						if (i != Room.LeaderSlot)
						{
							SlotModel slot = Room.GetSlot(i);
							if (slot != null && slot.State == SlotState.READY)
							{
								Room.ChangeSlotState(i, SlotState.NORMAL, false);
								if (i % 2 != 0)
								{
									PlayerCT--;
								}
								else
								{
									PlayerFR--;
								}
								int ınt321 = ınt32 - 1;
								ınt32 = ınt321;
								if (ınt321 == 0)
								{
									break;
								}
							}
						}
					}
					Room.UpdateSlotsInfo();
					if (Room.LeaderSlot % 2 != 0)
					{
						TotalEnemies = PlayerFR;
					}
					else
					{
						TotalEnemies = PlayerCT;
					}
					return true;
				}
			}
			return false;
		}

		public static bool CheckClanMatchRestrict(RoomModel room)
		{
			bool flag;
			if (room.ChannelType == ChannelType.Clan)
			{
				using (IEnumerator<ClanTeam> enumerator = AllUtils.smethod_7(room).Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClanTeam current = enumerator.Current;
						if (current.PlayersFR < 1 || current.PlayersCT < 1)
						{
							continue;
						}
						room.BlockedClan = true;
						flag = true;
						return flag;
					}
					return false;
				}
				return flag;
			}
			return false;
		}

		public static bool CheckDuplicateCouponEffects(Account Player, int CouponId)
		{
			bool flag = false;
			foreach (ValueTuple<int, CouponEffects, bool> valueTuple in new List<ValueTuple<int, CouponEffects, bool>>()
			{
				new ValueTuple<int, CouponEffects, bool>(1600065, CouponEffects.Defense20 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600079, CouponEffects.Defense90 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600044, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600030, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense10, true),
				new ValueTuple<int, CouponEffects, bool>(1600078, CouponEffects.JackHollowPoint | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600032, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600031, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.HollowPoint, true),
				new ValueTuple<int, CouponEffects, bool>(1600036, CouponEffects.HollowPointPlus | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600028, CouponEffects.HP5, false),
				new ValueTuple<int, CouponEffects, bool>(1600040, CouponEffects.HP10, false),
				new ValueTuple<int, CouponEffects, bool>(1600209, CouponEffects.Camoflage50, false),
				new ValueTuple<int, CouponEffects, bool>(1600208, CouponEffects.Camoflage99, false)
			})
			{
				if (!AllUtils.smethod_17(CouponId, Player.Effects, valueTuple))
				{
					continue;
				}
				flag = true;
				return flag;
			}
			return flag;
		}

		public static bool ClanMatchCheck(RoomModel Room, ChannelType Type, int TotalEnemys, out uint Error)
		{
			if (!ConfigLoader.IsTestMode)
			{
				if (Type == ChannelType.Clan)
				{
					if (!AllUtils.Have2ClansToClanMatch(Room))
					{
						Error = -2147479439;
						return true;
					}
					if (TotalEnemys > 0 && !AllUtils.HavePlayersToClanMatch(Room))
					{
						Error = -2147479438;
						return true;
					}
					Error = 0;
					return false;
				}
			}
			Error = 0;
			return false;
		}

		public static void ClassicModeCheck(RoomModel Room, PlayerEquipment Equip)
		{
			if (!ConfigLoader.TournamentRule)
			{
				return;
			}
			TRuleModel tRuleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
			if (tRuleModel == null)
			{
				return;
			}
			if (tRuleModel.BanIndexes.Count > 0)
			{
				foreach (int banIndex in tRuleModel.BanIndexes)
				{
					if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponPrimary))
					{
						Equip.WeaponPrimary = 103004;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponSecondary))
					{
						Equip.WeaponSecondary = 202003;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponMelee))
					{
						Equip.WeaponMelee = 301001;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponExplosive))
					{
						Equip.WeaponExplosive = 407001;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponSpecial))
					{
						Equip.WeaponSpecial = 508001;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartHead))
					{
						Equip.PartHead = 1000700000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartFace))
					{
						Equip.PartFace = 1000800000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartJacket))
					{
						Equip.PartJacket = 1000900000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartPocket))
					{
						Equip.PartPocket = 1001000000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartGlove))
					{
						Equip.PartGlove = 1001100000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartBelt))
					{
						Equip.PartBelt = 1001200000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartHolster))
					{
						Equip.PartHolster = 1001300000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.PartSkin))
					{
						Equip.PartSkin = 1001400000;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.BeretItem))
					{
						Equip.BeretItem = 0;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.DinoItem))
					{
						Equip.DinoItem = 1500511;
					}
					else if (GameRuleXML.IsBlocked(banIndex, Equip.AccessoryId))
					{
						Equip.AccessoryId = 0;
					}
					else if (!GameRuleXML.IsBlocked(banIndex, Equip.SprayId))
					{
						if (!GameRuleXML.IsBlocked(banIndex, Equip.NameCardId))
						{
							continue;
						}
						Equip.NameCardId = 0;
					}
					else
					{
						Equip.SprayId = 0;
					}
				}
			}
		}

		public static bool ClassicModeCheck(Account Player, RoomModel Room)
		{
			TRuleModel tRuleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
			if (tRuleModel == null)
			{
				return false;
			}
			PlayerEquipment equipment = Player.Equipment;
			if (equipment == null)
			{
				string[] nickname = new string[] { "Player '", Player.Nickname, "' has invalid equipment (Error) on ", null, null };
				nickname[3] = (ConfigLoader.TournamentRule ? "Enabled" : "Disabled");
				nickname[4] = " Tournament Rules!";
				CLogger.Print(string.Concat(nickname), LoggerType.Warning, null);
				return false;
			}
			List<string> strs = new List<string>();
			if (tRuleModel.BanIndexes.Count > 0)
			{
				foreach (int banIndex in tRuleModel.BanIndexes)
				{
					if (GameRuleXML.IsBlocked(banIndex, equipment.WeaponPrimary, ref strs, Translation.GetLabel("Primary")) || GameRuleXML.IsBlocked(banIndex, equipment.WeaponSecondary, ref strs, Translation.GetLabel("Secondary")) || GameRuleXML.IsBlocked(banIndex, equipment.WeaponMelee, ref strs, Translation.GetLabel("Melee")) || GameRuleXML.IsBlocked(banIndex, equipment.WeaponExplosive, ref strs, Translation.GetLabel("Explosive")) || GameRuleXML.IsBlocked(banIndex, equipment.WeaponSpecial, ref strs, Translation.GetLabel("Special")) || GameRuleXML.IsBlocked(banIndex, equipment.CharaRedId, ref strs, Translation.GetLabel("Character")) || GameRuleXML.IsBlocked(banIndex, equipment.CharaBlueId, ref strs, Translation.GetLabel("Character")) || GameRuleXML.IsBlocked(banIndex, equipment.PartHead, ref strs, Translation.GetLabel("PartHead")) || GameRuleXML.IsBlocked(banIndex, equipment.PartFace, ref strs, Translation.GetLabel("PartFace")) || GameRuleXML.IsBlocked(banIndex, equipment.BeretItem, ref strs, Translation.GetLabel("BeretItem")))
					{
						continue;
					}
					GameRuleXML.IsBlocked(banIndex, equipment.AccessoryId, ref strs, Translation.GetLabel("Accessory"));
				}
			}
			if (strs.Count <= 0)
			{
				return false;
			}
			Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("ClassicModeWarn", new object[] { string.Join(", ", strs.ToArray()) })));
			return true;
		}

		public static bool CompetitiveMatchCheck(Account Player, RoomModel Room, out uint Error)
		{
			if (Room.Competitive)
			{
				SlotModel[] slots = Room.Slots;
				for (int i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel != null && slotModel.State != SlotState.CLOSE && slotModel.State < SlotState.READY)
					{
						Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveFullSlot")));
						Error = -2147479438;
						return true;
					}
				}
			}
			Error = 0;
			return false;
		}

		public static void CompleteMission(RoomModel Room, SlotModel Slot, FragInfos Kills, MissionType AutoComplete, int MoreInfo)
		{
			try
			{
				Account playerBySlot = Room.GetPlayerBySlot(Slot);
				if (playerBySlot != null)
				{
					AllUtils.smethod_8(Room, playerBySlot, Slot, Kills, AutoComplete, MoreInfo);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.CompleteMission1] ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static void CompleteMission(RoomModel room, SlotModel slot, MissionType autoComplete, int moreInfo)
		{
			try
			{
				Account playerBySlot = room.GetPlayerBySlot(slot);
				if (playerBySlot != null)
				{
					AllUtils.smethod_9(room, playerBySlot, slot, autoComplete, moreInfo);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.CompleteMission2] ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static void CompleteMission(RoomModel room, Account player, SlotModel slot, FragInfos kills, MissionType autoComplete, int moreInfo)
		{
			AllUtils.smethod_8(room, player, slot, kills, autoComplete, moreInfo);
		}

		public static void CompleteMission(RoomModel room, Account player, SlotModel slot, MissionType autoComplete, int moreInfo)
		{
			AllUtils.smethod_9(room, player, slot, autoComplete, moreInfo);
		}

		public static void CreateCharacter(Account Player, ItemsModel Item)
		{
			CharacterModel characterModel = new CharacterModel()
			{
				Id = Item.Id,
				Name = Item.Name,
				Slot = Player.Character.GenSlotId(Item.Id),
				CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				PlayTime = 0
			};
			Player.Character.AddCharacter(characterModel);
			Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, Item));
			if (DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
			{
				Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0, 3, characterModel, Player));
				return;
			}
			Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(-2147483648, 255, null, null));
		}

		public static bool DiscountPlayerItems(SlotModel Slot, Account Player)
		{
			bool flag;
			try
			{
				bool flag1 = false;
				bool flag2 = false;
				uint uInt32 = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
				List<ItemsModel> ıtemsModels = new List<ItemsModel>();
				List<object> objs = new List<object>();
				int ınt32 = (Player.Bonus != null ? Player.Bonus.Bonuses : 0);
				int ınt321 = (Player.Bonus != null ? Player.Bonus.FreePass : 0);
				lock (Player.Inventory.Items)
				{
					for (int i = 0; i < Player.Inventory.Items.Count; i++)
					{
						ItemsModel ıtem = Player.Inventory.Items[i];
						if (ıtem.Equip == ItemEquipType.Durable && Slot.ItemUsages.Contains(ıtem.Id) && !Slot.SpecGM)
						{
							if (ıtem.Count <= uInt32 && (ıtem.Id == 800216 || ıtem.Id == 2700013 || ıtem.Id == 800169))
							{
								DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, Player.PlayerId);
							}
							ItemsModel ıtemsModel = ıtem;
							uint count = ıtemsModel.Count - 1;
							ıtemsModel.Count = count;
							if (count >= 1)
							{
								ıtemsModels.Add(ıtem);
								ComDiv.UpdateDB("player_items", "count", (long)((ulong)ıtem.Count), "object_id", ıtem.ObjectId, "owner_id", Player.PlayerId);
							}
							else
							{
								objs.Add(ıtem.ObjectId);
								int ınt322 = i;
								i = ınt322 - 1;
								Player.Inventory.Items.RemoveAt(ınt322);
							}
						}
						else if (ıtem.Count <= uInt32 && ıtem.Equip == ItemEquipType.Temporary)
						{
							if (ıtem.Category == ItemCategory.Coupon)
							{
								if (Player.Bonus == null)
								{
									goto Label0;
								}
								if (!Player.Bonus.RemoveBonuses(ıtem.Id))
								{
									if (ıtem.Id == 1600014)
									{
										ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
										Player.Bonus.CrosshairColor = 4;
										flag1 = true;
									}
									else if (ıtem.Id == 1600006)
									{
										ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
										Player.NickColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK pROTOCOLROOMGETCOLORNICKACK = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(Player.SlotId, Player.NickColor))
											{
												Player.Room.SendPacketToPlayers(pROTOCOLROOMGETCOLORNICKACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag1 = true;
									}
									else if (ıtem.Id == 1600009)
									{
										ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
										Player.Bonus.FakeRank = 55;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_RANK_ACK pROTOCOLROOMGETRANKACK = new PROTOCOL_ROOM_GET_RANK_ACK(Player.SlotId, Player.Rank))
											{
												Player.Room.SendPacketToPlayers(pROTOCOLROOMGETRANKACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag1 = true;
									}
									else if (ıtem.Id == 1600010)
									{
										ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
										ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
										Player.Nickname = Player.Bonus.FakeNick;
										Player.Bonus.FakeNick = "";
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_NICKNAME_ACK pROTOCOLROOMGETNICKNAMEACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(Player.SlotId, Player.Nickname))
											{
												Player.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKNAMEACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag1 = true;
									}
									else if (ıtem.Id == 1600187)
									{
										ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.MuzzleColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK pROTOCOLROOMGETCOLORMUZZLEFLASHACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(Player.SlotId, Player.Bonus.MuzzleColor))
											{
												Player.Room.SendPacketToPlayers(pROTOCOLROOMGETCOLORMUZZLEFLASHACK);
											}
										}
										flag1 = true;
									}
									else if (ıtem.Id == 1600205)
									{
										ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.NickBorderColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK pROTOCOLROOMGETNICKOUTLINECOLORACK = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(Player.SlotId, Player.Bonus.NickBorderColor))
											{
												Player.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKOUTLINECOLORACK);
											}
										}
										flag1 = true;
									}
								}
								CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
								if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
								{
									Player.Effects -= couponEffect.EffectFlag;
									flag2 = true;
								}
							}
							objs.Add(ıtem.ObjectId);
							int ınt323 = i;
							i = ınt323 - 1;
							Player.Inventory.Items.RemoveAt(ınt323);
						}
						else if (ıtem.Count == 0)
						{
							objs.Add(ıtem.ObjectId);
							int ınt324 = i;
							i = ınt324 - 1;
							Player.Inventory.Items.RemoveAt(ınt324);
						}
					Label0:
					}
				}
				if (objs.Count > 0)
				{
					for (int j = 0; j < objs.Count; j++)
					{
						ItemsModel ıtem1 = Player.Inventory.GetItem((long)objs[j]);
						if (ıtem1 != null && ıtem1.Category == ItemCategory.Character && ComDiv.GetIdStatics(ıtem1.Id, 1) == 6)
						{
							AllUtils.smethod_3(Player, ıtem1.Id);
						}
						Player.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1, (long)objs[j]));
					}
					ComDiv.DeleteDB("player_items", "object_id", objs.ToArray(), "owner_id", Player.PlayerId);
				}
				objs.Clear();
				objs = null;
				if (Player.Bonus != null && (Player.Bonus.Bonuses != ınt32 || Player.Bonus.FreePass != ınt321))
				{
					DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
				}
				if ((long)Player.Effects < 0L)
				{
					Player.Effects = (CouponEffects)0L;
				}
				if (ıtemsModels.Count > 0)
				{
					Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, Player, ıtemsModels));
				}
				ıtemsModels.Clear();
				ıtemsModels = null;
				if (flag2)
				{
					ComDiv.UpdateDB("accounts", "coupon_effect", (long)Player.Effects, "player_id", Player.PlayerId);
				}
				if (flag1)
				{
					Player.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, Player));
				}
				int ınt325 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
				if (ınt325 > 0)
				{
					DBQuery dBQuery = new DBQuery();
					if ((ınt325 & 2) == 2)
					{
						ComDiv.UpdateWeapons(Player.Equipment, dBQuery);
					}
					if ((ınt325 & 1) == 1)
					{
						ComDiv.UpdateChars(Player.Equipment, dBQuery);
					}
					if ((ınt325 & 3) == 3)
					{
						ComDiv.UpdateItems(Player.Equipment, dBQuery);
					}
					ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Player, Slot));
					Slot.Equipment = Player.Equipment;
					dBQuery = null;
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static void EnableQuestMission(Account Player)
		{
			PlayerEvent @event = Player.Event;
			if (@event == null)
			{
				return;
			}
			if (@event.LastQuestFinish == 0 && EventQuestXML.GetRunningEvent() != null)
			{
				Player.Mission.Mission4 = 13;
			}
		}

		public static void EndBattle(RoomModel Room)
		{
			AllUtils.EndBattle(Room, Room.IsBotMode());
		}

		public static void EndBattle(RoomModel Room, bool isBotMode)
		{
			AllUtils.EndBattle(Room, isBotMode, AllUtils.GetWinnerTeam(Room));
		}

		public static void EndBattle(RoomModel Room, bool IsBotMode, TeamEnum WinnerTeam)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				Room.CalculateResult(WinnerTeam, IsBotMode);
				AllUtils.GetBattleResult(Room, out ınt32, out ınt321, out numArray);
				foreach (Account allPlayer in allPlayers)
				{
					allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, WinnerTeam, ınt321, ınt32, IsBotMode, numArray));
					AllUtils.UpdateSeasonPass(allPlayer);
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		public static void EndBattleNoPoints(RoomModel Room)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				AllUtils.GetBattleResult(Room, out ınt32, out ınt321, out numArray);
				bool flag = Room.IsBotMode();
				foreach (Account allPlayer in allPlayers)
				{
					allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, TeamEnum.TEAM_DRAW, ınt321, ınt32, flag, numArray));
					AllUtils.UpdateSeasonPass(allPlayer);
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		public static void EndMatchMission(RoomModel Room, Account Player, SlotModel Slot, TeamEnum WinnerTeam)
		{
			if (WinnerTeam != TeamEnum.TEAM_DRAW)
			{
				AllUtils.CompleteMission(Room, Player, Slot, (Slot.Team == WinnerTeam ? MissionType.WIN : MissionType.DEFEAT), 0);
			}
		}

		public static void FreepassEffect(Account Player, SlotModel Slot, RoomModel Room, bool IsBotMode)
		{
			bool flag;
			DBQuery dBQuery = new DBQuery();
			if (Player.Bonus.FreePass != 0)
			{
				if (Player.Bonus.FreePass == 1 && Room.ChannelType == ChannelType.Clan)
				{
					goto Label4;
				}
				if (Room.State != RoomState.BATTLE)
				{
					return;
				}
				int score = 0;
				int ınt32 = 0;
				if (!IsBotMode)
				{
					int ınt321 = (Slot.AllKills != 0 || Slot.AllDeaths != 0 ? (int)Slot.InBattleTime(DateTimeUtil.Now()) : 0);
					if (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.FreeForAll)
					{
						if (Room.RoomType == RoomCondition.Destroy)
						{
							goto Label5;
						}
						score = (int)((double)Slot.Score + (double)ınt321 / 2.5 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
						ınt32 = (int)((double)Slot.Score + (double)ınt321 / 3 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
						goto Label2;
					}
				Label5:
					score = (int)((double)Slot.Score + (double)ınt321 / 2.5 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
					ınt32 = (int)((double)Slot.Score + (double)ınt321 / 3 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
				}
				else
				{
					int ıngameAiLevel = Room.IngameAiLevel * (150 + Slot.AllDeaths);
					if (ıngameAiLevel == 0)
					{
						ıngameAiLevel++;
					}
					int score1 = Slot.Score / ıngameAiLevel;
					ınt32 += score1;
					score += score1;
				}
			Label2:
				Account player = Player;
				player.Exp = player.Exp + (ConfigLoader.MaxExpReward < score ? ConfigLoader.MaxExpReward : score);
				Account gold = Player;
				gold.Gold = gold.Gold + (ConfigLoader.MaxGoldReward < ınt32 ? ConfigLoader.MaxGoldReward : ınt32);
				if (ınt32 > 0)
				{
					dBQuery.AddQuery("gold", Player.Gold);
				}
				if (score > 0)
				{
					dBQuery.AddQuery("experience", Player.Exp);
					flag = ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					return;
				}
				else
				{
					flag = ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					return;
				}
			}
		Label4:
			if (IsBotMode || Slot.State < SlotState.BATTLE_READY)
			{
				return;
			}
			if (Player.Gold > 0)
			{
				Player.Gold -= 200;
				if (Player.Gold < 0)
				{
					Player.Gold = 0;
				}
				dBQuery.AddQuery("gold", Player.Gold);
			}
			object playerId = Player.PlayerId;
			StatisticTotal basic = Player.Statistic.Basic;
			int escapesCount = basic.EscapesCount + 1;
			basic.EscapesCount = escapesCount;
			ComDiv.UpdateDB("player_stat_basics", "owner_id", playerId, "escapes_count", escapesCount);
			object obj = Player.PlayerId;
			StatisticSeason season = Player.Statistic.Season;
			escapesCount = season.EscapesCount + 1;
			season.EscapesCount = escapesCount;
			ComDiv.UpdateDB("player_stat_seasons", "owner_id", obj, "escapes_count", escapesCount);
			flag = ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
		}

		public static void GenerateMissionAwards(Account Player, DBQuery query)
		{
			try
			{
				PlayerMissions mission = Player.Mission;
				int actualMission = mission.ActualMission;
				int currentMissionId = mission.GetCurrentMissionId();
				int currentCard = mission.GetCurrentCard();
				if (currentMissionId > 0 && !mission.SelectedCard)
				{
					int ınt32 = 0;
					int ınt321 = 0;
					byte[] currentMissionList = mission.GetCurrentMissionList();
					foreach (MissionCardModel card in MissionCardRAW.GetCards(currentMissionId, -1))
					{
						if (currentMissionList[card.ArrayIdx] < card.MissionLimit)
						{
							continue;
						}
						ınt321++;
						if (card.CardBasicId != currentCard)
						{
							continue;
						}
						ınt32++;
					}
					if (ınt321 >= 40)
					{
						int masterMedal = Player.MasterMedal;
						int ribbon = Player.Ribbon;
						int medal = Player.Medal;
						int ensign = Player.Ensign;
						MissionCardAwards award = MissionCardRAW.GetAward(currentMissionId, currentCard);
						if (award != null)
						{
							Player.Ribbon += award.Ribbon;
							Player.Medal += award.Medal;
							Player.Ensign += award.Ensign;
							Player.Gold += award.Gold;
							Player.Exp += award.Exp;
						}
						MissionAwards missionAward = MissionAwardXML.GetAward(currentMissionId);
						if (missionAward != null)
						{
							Player.MasterMedal += missionAward.MasterMedal;
							Player.Exp += missionAward.Exp;
							Player.Gold += missionAward.Gold;
						}
						List<ItemsModel> missionAwards = MissionCardRAW.GetMissionAwards(currentMissionId);
						if (missionAwards.Count > 0)
						{
							Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, missionAwards));
						}
						Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(273, 4, Player));
						if (Player.Ribbon != ribbon)
						{
							query.AddQuery("ribbon", Player.Ribbon);
						}
						if (Player.Ensign != ensign)
						{
							query.AddQuery("ensign", Player.Ensign);
						}
						if (Player.Medal != medal)
						{
							query.AddQuery("medal", Player.Medal);
						}
						if (Player.MasterMedal != masterMedal)
						{
							query.AddQuery("master_medal", Player.MasterMedal);
						}
						query.AddQuery(string.Format("mission_id{0}", actualMission + 1), 0);
						ComDiv.UpdateDB("player_missions", "owner_id", Player.PlayerId, new string[] { string.Format("card{0}", actualMission + 1), string.Format("mission{0}_raw", actualMission + 1) }, new object[] { 0, new byte[0] });
						if (actualMission == 0)
						{
							mission.Mission1 = 0;
							mission.Card1 = 0;
							mission.List1 = new byte[40];
						}
						else if (actualMission == 1)
						{
							mission.Mission2 = 0;
							mission.Card2 = 0;
							mission.List2 = new byte[40];
						}
						else if (actualMission == 2)
						{
							mission.Mission3 = 0;
							mission.Card3 = 0;
							mission.List3 = new byte[40];
						}
						else if (actualMission == 3)
						{
							mission.Mission4 = 0;
							mission.Card3 = 0;
							mission.List4 = new byte[40];
							if (Player.Event != null)
							{
								Player.Event.LastQuestFinish = 1;
								ComDiv.UpdateDB("player_events", "last_quest_finish", 1, "owner_id", Player.PlayerId);
							}
						}
					}
					else if (ınt32 == 4 && !mission.SelectedCard)
					{
						MissionCardAwards missionCardAward = MissionCardRAW.GetAward(currentMissionId, currentCard);
						if (missionCardAward != null)
						{
							int ribbon1 = Player.Ribbon;
							int medal1 = Player.Medal;
							int ensign1 = Player.Ensign;
							Player.Ribbon += missionCardAward.Ribbon;
							Player.Medal += missionCardAward.Medal;
							Player.Ensign += missionCardAward.Ensign;
							Player.Gold += missionCardAward.Gold;
							Player.Exp += missionCardAward.Exp;
							if (Player.Ribbon != ribbon1)
							{
								query.AddQuery("ribbon", Player.Ribbon);
							}
							if (Player.Ensign != ensign1)
							{
								query.AddQuery("ensign", Player.Ensign);
							}
							if (Player.Medal != medal1)
							{
								query.AddQuery("medal", Player.Medal);
							}
						}
						mission.SelectedCard = true;
						Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(1, 1, Player));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("AllUtils.GenerateMissionAwards: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static TeamEnum GetBalanceTeamIdx(RoomModel Room, bool InBattle, TeamEnum PlayerTeamIdx)
		{
			int ınt32;
			SlotModel[] slots;
			int i;
			SlotModel slotModel;
			int ınt321;
			int ınt322 = (!InBattle || PlayerTeamIdx != TeamEnum.FR_TEAM ? 0 : 1);
			if (InBattle)
			{
				if (PlayerTeamIdx != TeamEnum.CT_TEAM)
				{
					goto Label2;
				}
				ınt321 = 1;
				ınt32 = ınt321;
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					slotModel = slots[i];
					if (slotModel.State == SlotState.NORMAL && !InBattle || slotModel.State >= SlotState.LOAD & InBattle)
					{
						if (slotModel.Team != TeamEnum.FR_TEAM)
						{
							ınt32++;
						}
						else
						{
							ınt322++;
						}
					}
				}
				if (ınt322 + 1 < ınt32)
				{
					return TeamEnum.FR_TEAM;
				}
				if (ınt32 + 1 >= ınt322 + 1)
				{
					return TeamEnum.ALL_TEAM;
				}
				return TeamEnum.CT_TEAM;
			}
		Label2:
			ınt321 = 0;
			ınt32 = ınt321;
			slots = Room.Slots;
			for (i = 0; i < (int)slots.Length; i++)
			{
				slotModel = slots[i];
				if (slotModel.State == SlotState.NORMAL && !InBattle || slotModel.State >= SlotState.LOAD & InBattle)
				{
					if (slotModel.Team != TeamEnum.FR_TEAM)
					{
						ınt32++;
					}
					else
					{
						ınt322++;
					}
				}
			}
			if (ınt322 + 1 < ınt32)
			{
				return TeamEnum.FR_TEAM;
			}
			if (ınt32 + 1 >= ınt322 + 1)
			{
				return TeamEnum.ALL_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}

		public static void GetBattleResult(RoomModel Room, out int MissionFlag, out int SlotFlag, out byte[] Data)
		{
			int i;
			MissionFlag = 0;
			SlotFlag = 0;
			Data = new byte[306];
			if (Room == null)
			{
				return;
			}
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				SlotModel[] slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel = slots[i];
					if (slotModel.State >= SlotState.LOAD)
					{
						int flag = slotModel.Flag;
						if (slotModel.MissionsCompleted)
						{
							MissionFlag += flag;
						}
						SlotFlag += flag;
					}
				}
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					syncServerPacket.WriteH((ushort)slots[i].Exp);
				}
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					syncServerPacket.WriteH((ushort)slots[i].Gold);
				}
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					syncServerPacket.WriteC((byte)slots[i].BonusFlags);
				}
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel1 = slots[i];
					syncServerPacket.WriteH((ushort)slotModel1.BonusCafeExp);
					syncServerPacket.WriteH((ushort)slotModel1.BonusItemExp);
					syncServerPacket.WriteH((ushort)slotModel1.BonusEventExp);
				}
				slots = Room.Slots;
				for (i = 0; i < (int)slots.Length; i++)
				{
					SlotModel slotModel2 = slots[i];
					syncServerPacket.WriteH((ushort)slotModel2.BonusCafePoint);
					syncServerPacket.WriteH((ushort)slotModel2.BonusItemPoint);
					syncServerPacket.WriteH((ushort)slotModel2.BonusEventPoint);
				}
				Data = syncServerPacket.ToArray();
			}
		}

		public static List<int> GetDinossaurs(RoomModel Room, bool ForceNewTRex, int ForceRexIdx)
		{
			List<int> ınt32s = new List<int>();
			if (Room.IsDinoMode(""))
			{
				int[] teamArray = Room.GetTeamArray((Room.Rounds == 1 ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM));
				for (int i = 0; i < (int)teamArray.Length; i++)
				{
					int ınt32 = teamArray[i];
					SlotModel slots = Room.Slots[ınt32];
					if (slots.State == SlotState.BATTLE && !slots.SpecGM)
					{
						ınt32s.Add(ınt32);
					}
				}
				if ((Room.TRex == -1 ? true : Room.Slots[Room.TRex].State <= SlotState.BATTLE_LOAD) | ForceNewTRex && ınt32s.Count > 1 && Room.IsDinoMode("DE"))
				{
					if (ForceRexIdx >= 0 && ınt32s.Contains(ForceRexIdx))
					{
						Room.TRex = ForceRexIdx;
					}
					else if (ForceRexIdx == -2)
					{
						Room.TRex = ınt32s[(new Random()).Next(0, ınt32s.Count)];
					}
				}
			}
			return ınt32s;
		}

		public static int GetKillScore(KillingMessage msg)
		{
			int ınt32 = 0;
			if (msg != KillingMessage.MassKill)
			{
				if (msg == KillingMessage.PiercingShot)
				{
					ınt32 += 6;
					return ınt32;
				}
				if (msg == KillingMessage.ChainStopper)
				{
					ınt32 += 8;
				}
				else if (msg == KillingMessage.Headshot)
				{
					ınt32 += 10;
				}
				else if (msg == KillingMessage.ChainHeadshot)
				{
					ınt32 += 14;
				}
				else if (msg == KillingMessage.ChainSlugger)
				{
					ınt32 += 6;
				}
				else if (msg == KillingMessage.ObjectDefense)
				{
					ınt32 += 7;
				}
				else if (msg != KillingMessage.Suicide)
				{
					ınt32 += 5;
				}
			}
			else
			{
				ınt32 += 6;
				return ınt32;
			}
			return ınt32;
		}

		public static int GetNewSlotId(int SlotIdx)
		{
			if (SlotIdx % 2 != 0)
			{
				return SlotIdx - 1;
			}
			return SlotIdx + 1;
		}

		public static void GetReadyPlayers(RoomModel Room, ref int FRPlayers, ref int CTPlayers, ref int TotalEnemys)
		{
			int ınt32 = 0;
			for (int i = 0; i < (int)Room.Slots.Length; i++)
			{
				SlotModel slots = Room.Slots[i];
				if (slots.State == SlotState.READY)
				{
					if (Room.RoomType == RoomCondition.FreeForAll && i > 0)
					{
						ınt32++;
					}
					else if (slots.Team != TeamEnum.FR_TEAM)
					{
						CTPlayers++;
					}
					else
					{
						FRPlayers++;
					}
				}
			}
			if (Room.RoomType == RoomCondition.FreeForAll)
			{
				TotalEnemys = ınt32;
				return;
			}
			if (Room.LeaderSlot % 2 == 0)
			{
				TotalEnemys = CTPlayers;
				return;
			}
			TotalEnemys = FRPlayers;
		}

		public static ValueTuple<byte[], int[]> GetRewardData(RoomModel Room, List<SlotModel> Slots)
		{
			byte[] numArray = new byte[5];
			int[] ınt32Array = new int[5];
			for (int i = 0; i < 5; i++)
			{
				numArray[i] = 255;
				ınt32Array[i] = 0;
			}
			int ınt32 = 0;
			if (!Room.IsBotMode() && Slots.Count > 0)
			{
				SlotModel slotModel = (
					from slotModel_0 in Slots
					where slotModel_0.Score > 0
					orderby slotModel_0.Score descending
					select slotModel_0).FirstOrDefault<SlotModel>();
				if (slotModel != null)
				{
					AllUtils.smethod_24(Room, slotModel, BattleRewardType.MVP, 5, ref ınt32, ref numArray, ref ınt32Array);
				}
				SlotModel slotModel1 = (
					from slotModel_0 in Slots
					where slotModel_0.AllAssists > 0
					orderby slotModel_0.AllAssists descending
					select slotModel_0).FirstOrDefault<SlotModel>();
				if (slotModel1 != null)
				{
					AllUtils.smethod_24(Room, slotModel1, BattleRewardType.AssistKing, 5, ref ınt32, ref numArray, ref ınt32Array);
				}
				SlotModel slotModel2 = (
					from slotModel_0 in Slots
					where slotModel_0.KillsOnLife > 0
					orderby slotModel_0.KillsOnLife descending
					select slotModel_0).FirstOrDefault<SlotModel>();
				if (slotModel2 != null)
				{
					AllUtils.smethod_24(Room, slotModel2, BattleRewardType.MultiKill, 5, ref ınt32, ref numArray, ref ınt32Array);
				}
			}
			return new ValueTuple<byte[], int[]>(numArray, ınt32Array);
		}

		public static int GetSlotsFlag(RoomModel Room, bool OnlyNoSpectators, bool MissionSuccess)
		{
			if (Room == null)
			{
				return 0;
			}
			int flag = 0;
			SlotModel[] slots = Room.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.State >= SlotState.LOAD && (MissionSuccess && slotModel.MissionsCompleted || !MissionSuccess && (!OnlyNoSpectators || !slotModel.Spectator)))
				{
					flag += slotModel.Flag;
				}
			}
			return flag;
		}

		public static TeamEnum GetWinnerTeam(RoomModel room)
		{
			if (room == null)
			{
				return TeamEnum.TEAM_DRAW;
			}
			TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
			if (room.RoomType != RoomCondition.Bomb && room.RoomType != RoomCondition.Destroy && room.RoomType != RoomCondition.Annihilation && room.RoomType != RoomCondition.Defense)
			{
				if (room.RoomType == RoomCondition.Destroy)
				{
					if (room.CTRounds == room.FRRounds)
					{
						teamEnum = TeamEnum.TEAM_DRAW;
					}
					else if (room.CTRounds > room.FRRounds)
					{
						teamEnum = TeamEnum.CT_TEAM;
					}
					else if (room.CTRounds < room.FRRounds)
					{
						teamEnum = TeamEnum.FR_TEAM;
					}
					return teamEnum;
				}
				if (room.IsDinoMode("DE"))
				{
					if (room.CTDino == room.FRDino)
					{
						teamEnum = TeamEnum.TEAM_DRAW;
						return teamEnum;
					}
					else if (room.CTDino > room.FRDino)
					{
						teamEnum = TeamEnum.CT_TEAM;
						return teamEnum;
					}
					else if (room.CTDino < room.FRDino)
					{
						teamEnum = TeamEnum.FR_TEAM;
						return teamEnum;
					}
					else
					{
						return teamEnum;
					}
				}
				else if (room.CTKills == room.FRKills)
				{
					teamEnum = TeamEnum.TEAM_DRAW;
					return teamEnum;
				}
				else if (room.CTKills > room.FRKills)
				{
					teamEnum = TeamEnum.CT_TEAM;
					return teamEnum;
				}
				else if (room.CTKills < room.FRKills)
				{
					teamEnum = TeamEnum.FR_TEAM;
					return teamEnum;
				}
				else
				{
					return teamEnum;
				}
			}
			if (room.CTRounds == room.FRRounds)
			{
				teamEnum = TeamEnum.TEAM_DRAW;
			}
			else if (room.CTRounds > room.FRRounds)
			{
				teamEnum = TeamEnum.CT_TEAM;
			}
			else if (room.CTRounds < room.FRRounds)
			{
				teamEnum = TeamEnum.FR_TEAM;
			}
			return teamEnum;
		}

		public static TeamEnum GetWinnerTeam(RoomModel room, int RedPlayers, int BluePlayers)
		{
			if (room == null)
			{
				return TeamEnum.TEAM_DRAW;
			}
			TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
			if (RedPlayers == 0)
			{
				teamEnum = TeamEnum.CT_TEAM;
			}
			else if (BluePlayers == 0)
			{
				teamEnum = TeamEnum.FR_TEAM;
			}
			return teamEnum;
		}

		public static void GetXmasReward(Account Player)
		{
			EventXmasModel runningEvent = EventXmasXML.GetRunningEvent();
			if (runningEvent == null)
			{
				return;
			}
			PlayerEvent @event = Player.Event;
			uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			if (@event != null && (@event.LastXmasDate <= runningEvent.BeginDate || @event.LastXmasDate > runningEvent.EndedDate) && ComDiv.UpdateDB("player_events", "last_xmas_date", (long)((ulong)uInt32), "owner_id", Player.PlayerId))
			{
				@event.LastXmasDate = uInt32;
				GoodsItem good = ShopManager.GetGood(runningEvent.GoodId);
				if (good != null)
				{
					if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || Player.Character.GetCharacter(good.Item.Id) != null)
					{
						Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
					}
					else
					{
						AllUtils.CreateCharacter(Player, good.Item);
					}
					Player.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, good.Item));
				}
			}
		}

		public static bool Have2ClansToClanMatch(RoomModel room)
		{
			return AllUtils.smethod_7(room).Count == 2;
		}

		public static bool HavePlayersToClanMatch(RoomModel room)
		{
			bool flag = false;
			bool flag1 = false;
			foreach (ClanTeam value in AllUtils.smethod_7(room).Values)
			{
				if (value.PlayersFR < 4)
				{
					if (value.PlayersCT < 4)
					{
						continue;
					}
					flag1 = true;
				}
				else
				{
					flag = true;
				}
			}
			return flag & flag1;
		}

		public static int InitBotRank(int BotLevel)
		{
			Random random = new Random();
			switch (BotLevel)
			{
				case 1:
				{
					return random.Next(0, 4);
				}
				case 2:
				{
					return random.Next(5, 9);
				}
				case 3:
				{
					return random.Next(10, 14);
				}
				case 4:
				{
					return random.Next(15, 19);
				}
				case 5:
				{
					return random.Next(20, 24);
				}
				case 6:
				{
					return random.Next(25, 29);
				}
				case 7:
				{
					return random.Next(30, 34);
				}
				case 8:
				{
					return random.Next(35, 39);
				}
				case 9:
				{
					return random.Next(40, 44);
				}
				case 10:
				{
					return random.Next(45, 49);
				}
				default:
				{
					return 52;
				}
			}
		}

		public static void InsertItem(int ItemId, SlotModel Slot)
		{
			lock (Slot.ItemUsages)
			{
				if (!Slot.ItemUsages.Contains(ItemId))
				{
					Slot.ItemUsages.Add(ItemId);
				}
			}
		}

		public static void LeaveHostEndBattlePVE(RoomModel Room, Account Player)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = pROTOCOLBATTLEGIVEUPBATTLEACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-3");
					TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room);
					AllUtils.GetBattleResult(Room, out ınt32, out ınt321, out numArray);
					foreach (Account allPlayer in allPlayers)
					{
						allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEGIVEUPBATTLEACK.GetType().Name);
						allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, winnerTeam, ınt321, ınt32, true, numArray));
						AllUtils.UpdateSeasonPass(allPlayer);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		public static void LeaveHostEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			IsFinished = true;
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room, TeamFR, TeamCT);
				if (Room.State == RoomState.BATTLE)
				{
					Room.CalculateResult(winnerTeam, false);
				}
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = pROTOCOLBATTLEGIVEUPBATTLEACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-4");
					AllUtils.GetBattleResult(Room, out ınt32, out ınt321, out numArray);
					foreach (Account allPlayer in allPlayers)
					{
						allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEGIVEUPBATTLEACK.GetType().Name);
						allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, winnerTeam, ınt321, ınt32, false, numArray));
						AllUtils.UpdateSeasonPass(allPlayer);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		public static void LeaveHostGiveBattlePVE(RoomModel Room, Account Player)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count == 0)
			{
				return;
			}
			Room.SetNewLeader(-1, SlotState.BATTLE_READY, Room.LeaderSlot, true);
			using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
			{
				using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK pROTOCOLBATTLELEAVEP2PSERVERACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
				{
					byte[] completeBytes = pROTOCOLBATTLEGIVEUPBATTLEACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-1");
					byte[] numArray = pROTOCOLBATTLELEAVEP2PSERVERACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-2");
					foreach (Account allPlayer in allPlayers)
					{
						SlotModel slot = Room.GetSlot(allPlayer.SlotId);
						if (slot == null)
						{
							continue;
						}
						if (slot.State >= SlotState.PRESTART)
						{
							allPlayer.SendCompletePacket(numArray, pROTOCOLBATTLELEAVEP2PSERVERACK.GetType().Name);
						}
						allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEGIVEUPBATTLEACK.GetType().Name);
					}
				}
			}
		}

		public static void LeaveHostGiveBattlePVP(RoomModel Room, Account Player)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count == 0)
			{
				return;
			}
			int leaderSlot = Room.LeaderSlot;
			Room.SetNewLeader(-1, (Room.State == RoomState.BATTLE ? SlotState.BATTLE_READY : SlotState.READY), leaderSlot, true);
			using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK pROTOCOLBATTLELEAVEP2PSERVERACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
			{
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = pROTOCOLBATTLELEAVEP2PSERVERACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-6");
					byte[] numArray = pROTOCOLBATTLEGIVEUPBATTLEACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-7");
					foreach (Account allPlayer in allPlayers)
					{
						if (Room.Slots[allPlayer.SlotId].State >= SlotState.PRESTART)
						{
							allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLELEAVEP2PSERVERACK.GetType().Name);
						}
						allPlayer.SendCompletePacket(numArray, pROTOCOLBATTLEGIVEUPBATTLEACK.GetType().Name);
					}
				}
			}
		}

		public static void LeavePlayerEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			IsFinished = true;
			TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room, TeamFR, TeamCT);
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				if (Room.State == RoomState.BATTLE)
				{
					Room.CalculateResult(winnerTeam, false);
				}
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = pROTOCOLBATTLEGIVEUPBATTLEACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-8");
					AllUtils.GetBattleResult(Room, out ınt32, out ınt321, out numArray);
					foreach (Account allPlayer in allPlayers)
					{
						allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEGIVEUPBATTLEACK.GetType().Name);
						allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, winnerTeam, ınt321, ınt32, false, numArray));
						AllUtils.UpdateSeasonPass(allPlayer);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		public static void LeavePlayerQuitBattle(RoomModel Room, Account Player)
		{
			using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOLBATTLEGIVEUPBATTLEACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLEGIVEUPBATTLEACK, SlotState.READY, 1);
			}
		}

		public static void LoadPlayerBattlepass(Account Player)
		{
			PlayerBattlepass playerBattlepassDB = DaoManagerSQL.GetPlayerBattlepassDB(Player.PlayerId);
			if (playerBattlepassDB != null)
			{
				Player.Battlepass = playerBattlepassDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerBattlepassDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Battlepass!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerBonus(Account Player)
		{
			PlayerBonus playerBonusDB = DaoManagerSQL.GetPlayerBonusDB(Player.PlayerId);
			if (playerBonusDB != null)
			{
				Player.Bonus = playerBonusDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerBonusDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Bonus!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerCharacters(Account Player)
		{
			List<CharacterModel> playerCharactersDB = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
			if (playerCharactersDB.Count > 0)
			{
				Player.Character.Characters = playerCharactersDB;
			}
		}

		public static void LoadPlayerCompetitive(Account Player)
		{
			PlayerCompetitive playerCompetitiveDB = DaoManagerSQL.GetPlayerCompetitiveDB(Player.PlayerId);
			if (playerCompetitiveDB != null)
			{
				Player.Competitive = playerCompetitiveDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerCompetitiveDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Competitive!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerConfig(Account Player)
		{
			PlayerConfig playerConfigDB = DaoManagerSQL.GetPlayerConfigDB(Player.PlayerId);
			if (playerConfigDB != null)
			{
				Player.Config = playerConfigDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerConfigDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Config!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerEquipments(Account Player)
		{
			PlayerEquipment playerEquipmentsDB = DaoManagerSQL.GetPlayerEquipmentsDB(Player.PlayerId);
			if (playerEquipmentsDB != null)
			{
				Player.Equipment = playerEquipmentsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerEquipmentsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Equipment!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerEvent(Account Player)
		{
			PlayerEvent playerEventDB = DaoManagerSQL.GetPlayerEventDB(Player.PlayerId);
			if (playerEventDB != null)
			{
				Player.Event = playerEventDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerEventDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Event!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerFriend(Account Player, bool LoadFulLDatabase)
		{
			List<FriendModel> playerFriendsDB = DaoManagerSQL.GetPlayerFriendsDB(Player.PlayerId);
			if (playerFriendsDB.Count > 0)
			{
				Player.Friend.Friends = playerFriendsDB;
				if (LoadFulLDatabase)
				{
					AccountManager.GetFriendlyAccounts(Player.Friend);
				}
			}
		}

		public static void LoadPlayerInventory(Account Player)
		{
			lock (Player.Inventory.Items)
			{
				Player.Inventory.Items.AddRange(DaoManagerSQL.GetPlayerInventoryItems(Player.PlayerId));
			}
		}

		public static void LoadPlayerMissions(Account Player)
		{
			PlayerMissions playerMissionsDB = DaoManagerSQL.GetPlayerMissionsDB(Player.PlayerId, Player.Mission.Mission1, Player.Mission.Mission2, Player.Mission.Mission3, Player.Mission.Mission4);
			if (playerMissionsDB != null)
			{
				Player.Mission = playerMissionsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerMissionsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Missions!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerQuickstarts(Account Player)
		{
			List<QuickstartModel> playerQuickstartsDB = DaoManagerSQL.GetPlayerQuickstartsDB(Player.PlayerId);
			if (playerQuickstartsDB.Count > 0)
			{
				Player.Quickstart.Quickjoins = playerQuickstartsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerQuickstartsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Quickstarts!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerReport(Account Player)
		{
			PlayerReport playerReportDB = DaoManagerSQL.GetPlayerReportDB(Player.PlayerId);
			if (playerReportDB != null)
			{
				Player.Report = playerReportDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerReportDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Report!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerStatistic(Account Player)
		{
			StatisticTotal playerStatBasicDB = DaoManagerSQL.GetPlayerStatBasicDB(Player.PlayerId);
			if (playerStatBasicDB != null)
			{
				Player.Statistic.Basic = playerStatBasicDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatBasicDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Basic Statistic!", LoggerType.Warning, null);
			}
			StatisticSeason playerStatSeasonDB = DaoManagerSQL.GetPlayerStatSeasonDB(Player.PlayerId);
			if (playerStatSeasonDB != null)
			{
				Player.Statistic.Season = playerStatSeasonDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatSeasonDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Season Statistic!", LoggerType.Warning, null);
			}
			StatisticClan playerStatClanDB = DaoManagerSQL.GetPlayerStatClanDB(Player.PlayerId);
			if (playerStatClanDB != null)
			{
				Player.Statistic.Clan = playerStatClanDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatClanDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Clan Statistic!", LoggerType.Warning, null);
			}
			StatisticDaily playerStatDailiesDB = DaoManagerSQL.GetPlayerStatDailiesDB(Player.PlayerId);
			if (playerStatDailiesDB != null)
			{
				Player.Statistic.Daily = playerStatDailiesDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatDailiesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Daily Statistic!", LoggerType.Warning, null);
			}
			StatisticWeapon playerStatWeaponsDB = DaoManagerSQL.GetPlayerStatWeaponsDB(Player.PlayerId);
			if (playerStatWeaponsDB != null)
			{
				Player.Statistic.Weapon = playerStatWeaponsDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatWeaponsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Weapon Statistic!", LoggerType.Warning, null);
			}
			StatisticAcemode playerStatAcemodesDB = DaoManagerSQL.GetPlayerStatAcemodesDB(Player.PlayerId);
			if (playerStatAcemodesDB != null)
			{
				Player.Statistic.Acemode = playerStatAcemodesDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatAcemodesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Acemode Statistic!", LoggerType.Warning, null);
			}
			StatisticBattlecup playerStatBattlecupDB = DaoManagerSQL.GetPlayerStatBattlecupDB(Player.PlayerId);
			if (playerStatBattlecupDB != null)
			{
				Player.Statistic.Battlecup = playerStatBattlecupDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerStatBattlecupsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Battlecup Statistic!", LoggerType.Warning, null);
			}
		}

		public static void LoadPlayerTitles(Account Player)
		{
			PlayerTitles playerTitlesDB = DaoManagerSQL.GetPlayerTitlesDB(Player.PlayerId);
			if (playerTitlesDB != null)
			{
				Player.Title = playerTitlesDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Title!", LoggerType.Warning, null);
			}
		}

		public static bool PlayerIsBattle(Account Player)
		{
			SlotModel slotModel;
			RoomModel room = Player.Room;
			if (room == null || !room.GetSlot(Player.SlotId, out slotModel))
			{
				return false;
			}
			return slotModel.State >= SlotState.READY;
		}

		public static void PlayTimeEvent(Account Player, EventPlaytimeModel EvPlaytime, bool IsBotMode, SlotModel Slot, long PlayedTime)
		{
			int minutes1;
			List<int> goods1;
			try
			{
				RoomModel room = Player.Room;
				PlayerEvent @event = Player.Event;
				if (room != null && @event != null)
				{
					int ınt32 = EvPlaytime.Minutes1;
					int minutes2 = EvPlaytime.Minutes2;
					int minutes3 = EvPlaytime.Minutes3;
					if (ınt32 != 0 || minutes2 != 0 || minutes3 != 0)
					{
						long lastPlaytimeValue = @event.LastPlaytimeValue;
						long lastPlaytimeFinish = (long)@event.LastPlaytimeFinish;
						long lastPlaytimeDate = (long)@event.LastPlaytimeDate;
						if (@event.LastPlaytimeFinish >= 0 && @event.LastPlaytimeFinish <= 2)
						{
							PlayerEvent playerEvent = @event;
							playerEvent.LastPlaytimeValue = playerEvent.LastPlaytimeValue + PlayedTime;
							if (@event.LastPlaytimeFinish == 0)
							{
								minutes1 = EvPlaytime.Minutes1;
							}
							else if (@event.LastPlaytimeFinish == 1)
							{
								minutes1 = EvPlaytime.Minutes2;
							}
							else
							{
								minutes1 = (@event.LastPlaytimeFinish == 2 ? EvPlaytime.Minutes3 : 0);
							}
							int ınt321 = minutes1;
							if (ınt321 != 0)
							{
								if (@event.LastPlaytimeValue >= (long)(ınt321 * 60))
								{
									Random random = new Random();
									if (@event.LastPlaytimeFinish == 0)
									{
										goods1 = EvPlaytime.Goods1;
									}
									else if (@event.LastPlaytimeFinish == 1)
									{
										goods1 = EvPlaytime.Goods2;
									}
									else
									{
										goods1 = (@event.LastPlaytimeFinish == 2 ? EvPlaytime.Goods3 : new List<int>());
									}
									List<int> ınt32s = goods1;
									if (ınt32s.Count > 0)
									{
										GoodsItem good = ShopManager.GetGood(ınt32s[random.Next(0, ınt32s.Count)]);
										if (good != null)
										{
											if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || Player.Character.GetCharacter(good.Item.Id) != null)
											{
												Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
											}
											else
											{
												AllUtils.CreateCharacter(Player, good.Item);
											}
											Player.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, good.Item));
										}
									}
									PlayerEvent lastPlaytimeFinish1 = @event;
									lastPlaytimeFinish1.LastPlaytimeFinish = lastPlaytimeFinish1.LastPlaytimeFinish + 1;
									@event.LastPlaytimeValue = 0L;
								}
								@event.LastPlaytimeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
							}
							else
							{
								return;
							}
						}
						if (@event.LastPlaytimeValue != lastPlaytimeValue || (long)@event.LastPlaytimeFinish != lastPlaytimeFinish || (ulong)@event.LastPlaytimeDate != lastPlaytimeDate)
						{
							EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
						}
					}
					else
					{
						CLogger.Print(string.Format("Event Playtime Disabled Due To: 0 Value! (Minutes1: {0}; Minutes2: {1}; Minutes3: {2}", ınt32, minutes2, minutes3), LoggerType.Warning, null);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.PlayTimeEvent] ", exception.Message), LoggerType.Error, exception);
			}
		}

		public static void ProcessBattlepassPremiumBuy(Account Player)
		{
			int goodId;
			int ınt32;
			PlayerBattlepass battlepass = Player.Battlepass;
			if (battlepass != null)
			{
				BattlePassModel seasonPass = SeasonChallengeXML.GetSeasonPass(battlepass.Id);
				if (seasonPass == null)
				{
					return;
				}
				battlepass.IsPremium = true;
				for (int i = 0; i < battlepass.Level; i++)
				{
					PassBoxModel ıtem = seasonPass.Cards[i];
					int[] ınt32Array = new int[3];
					PassItemModel premiumA = ıtem.PremiumA;
					if (premiumA != null)
					{
						goodId = premiumA.GoodId;
					}
					else
					{
						goodId = 0;
					}
					ınt32Array[1] = goodId;
					PassItemModel premiumB = ıtem.PremiumB;
					if (premiumB != null)
					{
						ınt32 = premiumB.GoodId;
					}
					else
					{
						ınt32 = 0;
					}
					ınt32Array[2] = ınt32;
					AllUtils.smethod_22(Player, ınt32Array);
				}
				ComDiv.UpdateDB("player_battlepass", "premium", battlepass.IsPremium, "owner_id", Player.PlayerId);
			}
		}

		public static List<ItemsModel> RepairableItems(Account Player, List<long> ObjectIds, out int Gold, out int Cash, out uint Error)
		{
			Gold = 0;
			Cash = 0;
			Error = 0;
			List<ItemsModel> ıtemsModels = new List<ItemsModel>();
			if (ObjectIds.Count > 0)
			{
				foreach (long objectId in ObjectIds)
				{
					ItemsModel ıtem = Player.Inventory.GetItem(objectId);
					if (ıtem == null)
					{
						Error = -2147483376;
					}
					else
					{
						uint[] uInt32Array = AllUtils.smethod_18(Player, ıtem);
						Gold += uInt32Array[0];
						Cash += uInt32Array[1];
						Error = uInt32Array[2];
						ıtemsModels.Add(ıtem);
					}
				}
			}
			return ıtemsModels;
		}

		public static void ResetBattleInfo(RoomModel Room)
		{
			SlotModel[] slots = Room.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				if (slotModel.PlayerId > 0L && slotModel.State >= SlotState.LOAD)
				{
					slotModel.State = SlotState.NORMAL;
					slotModel.ResetSlot();
				}
				Room.CheckGhostSlot(slotModel);
			}
			Room.PreMatchCD = false;
			Room.BlockedClan = false;
			Room.SwapRound = false;
			Room.Rounds = 1;
			Room.SpawnsCount = 0;
			Room.FRKills = 0;
			Room.FRAssists = 0;
			Room.FRDeaths = 0;
			Room.CTKills = 0;
			Room.CTAssists = 0;
			Room.CTDeaths = 0;
			Room.FRDino = 0;
			Room.CTDino = 0;
			Room.FRRounds = 0;
			Room.CTRounds = 0;
			Room.BattleStart = new DateTime();
			Room.TimeRoom = 0;
			Room.Bar1 = 0;
			Room.Bar2 = 0;
			Room.IngameAiLevel = 0;
			Room.State = RoomState.READY;
			Room.UpdateRoomInfo();
			Room.VoteKick = null;
			Room.UdpServer = null;
			if (Room.RoundTime.IsTimer())
			{
				Room.RoundTime.StopJob();
			}
			if (Room.VoteTime.IsTimer())
			{
				Room.VoteTime.StopJob();
			}
			if (Room.BombTime.IsTimer())
			{
				Room.BombTime.StopJob();
			}
			Room.UpdateSlotsInfo();
		}

		public static void ResetSlotInfo(RoomModel Room, SlotModel Slot, bool UpdateInfo)
		{
			if (Slot.State >= SlotState.LOAD)
			{
				Room.ChangeSlotState(Slot, SlotState.NORMAL, UpdateInfo);
				Slot.ResetSlot();
			}
		}

		public static void RoomPingSync(RoomModel Room)
		{
			if (ComDiv.GetDuration(Room.LastPingSync) < (double)ConfigLoader.PingUpdateTimeSeconds)
			{
				return;
			}
			byte[] ping = new byte[18];
			for (int i = 0; i < 18; i++)
			{
				ping[i] = (byte)Room.Slots[i].Ping;
			}
			using (PROTOCOL_BATTLE_SENDPING_ACK pROTOCOLBATTLESENDPINGACK = new PROTOCOL_BATTLE_SENDPING_ACK(ping))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLESENDPINGACK, SlotState.BATTLE, 0);
			}
			Room.LastPingSync = DateTimeUtil.Now();
		}

		public static void SendCompetitiveInfo(Account Player)
		{
			try
			{
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveRank", new object[] { Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points })));
			}
			catch (Exception exception)
			{
				CLogger.Print(exception.ToString(), LoggerType.Error, null);
			}
		}

		public static bool ServerCommands(Account Player, string Text)
		{
			bool flag;
			try
			{
				bool flag1 = CommandManager.TryParse(Text, Player);
				if (flag1)
				{
					CLogger.Print(string.Format("Player '{0}' (UID: {1}) Running Command '{2}'", Player.Nickname, Player.PlayerId, Text), LoggerType.Command, null);
				}
				flag = flag1;
			}
			catch
			{
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, Translation.GetLabel("CommandsExceptionError")));
				flag = true;
			}
			return flag;
		}

		public static bool SlotValidMessage(SlotModel Sender, SlotModel Receiver)
		{
			if ((Sender.State == SlotState.NORMAL || Sender.State == SlotState.READY) && (Receiver.State == SlotState.NORMAL || Receiver.State == SlotState.READY))
			{
				return true;
			}
			if (Sender.State < SlotState.LOAD || Receiver.State < SlotState.LOAD)
			{
				return false;
			}
			if (Receiver.SpecGM || Sender.SpecGM || Sender.DeathState.HasFlag(DeadEnum.UseChat) || Sender.DeathState.HasFlag(DeadEnum.Dead) && Receiver.DeathState.HasFlag(DeadEnum.Dead) || Sender.Spectator && Receiver.Spectator)
			{
				return true;
			}
			if (!Sender.DeathState.HasFlag(DeadEnum.Alive) || !Receiver.DeathState.HasFlag(DeadEnum.Alive))
			{
				return false;
			}
			if (Sender.Spectator && Receiver.Spectator)
			{
				return true;
			}
			if (Sender.Spectator)
			{
				return false;
			}
			return !Receiver.Spectator;
		}

		private static bool smethod_0(Account account_0, out string string_0)
		{
			if (account_0.IsGM())
			{
				string_0 = "GM Special Access";
				return true;
			}
			PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(account_0.PlayerId);
			if (playerVIP == null)
			{
				string_0 = "Database Not Found!";
				return false;
			}
			if (playerVIP.Expirate < uint.Parse(DateTimeUtil.Now("yyMMddHHmm")))
			{
				string_0 = "The Time Has Expired!";
				return false;
			}
			if (!InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(account_0.PlayerId), playerVIP.Address) && ConfigLoader.ICafeSystem)
			{
				string_0 = "Invalid Configuration!";
				return false;
			}
			string str = string.Format("{0}", account_0.CafePC);
			if (!playerVIP.Benefit.Equals(str) && ComDiv.UpdateDB("player_vip", "last_benefit", str, "owner_id", account_0.PlayerId))
			{
				playerVIP.Benefit = str;
			}
			string_0 = "Valid Access";
			return true;
		}

		private static ClassType smethod_1(ClassType classType_0)
		{
			if (classType_0 == ClassType.DualSMG)
			{
				return ClassType.SMG;
			}
			if (classType_0 == ClassType.DualHandGun)
			{
				return ClassType.HandGun;
			}
			if (classType_0 != ClassType.DualKnife)
			{
				if (classType_0 != ClassType.Knuckle)
				{
					if (classType_0 == ClassType.DualShotgun)
					{
						return ClassType.Shotgun;
					}
					return classType_0;
				}
			}
			return ClassType.Knife;
		}

		private static int smethod_10(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, FragInfos fragInfos_0)
		{
			int ınt32 = 0;
			if ((missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0) && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == classType_0 || missionCardModel_0.WeaponReq == classType_1))
			{
				foreach (FragModel frag in fragInfos_0.Frags)
				{
					if (frag.VictimSlot % 2 == fragInfos_0.KillerSlot % 2)
					{
						continue;
					}
					ınt32++;
				}
			}
			return ınt32;
		}

		private static int smethod_11(MissionCardModel missionCardModel_0, FragInfos fragInfos_0)
		{
			int ınt32 = 0;
			foreach (FragModel frag in fragInfos_0.Frags)
			{
				if (frag.VictimSlot % 2 == fragInfos_0.KillerSlot % 2 || missionCardModel_0.WeaponReq != ClassType.Unknown && (byte)missionCardModel_0.WeaponReq != frag.WeaponClass && missionCardModel_0.WeaponReq != AllUtils.smethod_1((ClassType)frag.WeaponClass))
				{
					continue;
				}
				ınt32++;
			}
			return ınt32;
		}

		private static int smethod_12(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, int int_1, FragModel fragModel_0)
		{
			if (missionCardModel_0.WeaponReqId != 0 && missionCardModel_0.WeaponReqId != int_0 || missionCardModel_0.WeaponReq != ClassType.Unknown && missionCardModel_0.WeaponReq != classType_0 && missionCardModel_0.WeaponReq != classType_1 || fragModel_0.VictimSlot % 2 == int_1 % 2)
			{
				return 0;
			}
			return 1;
		}

		private static int smethod_13(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0)
		{
			if (missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0)
			{
				if (missionCardModel_0.WeaponReq != ClassType.Unknown && missionCardModel_0.WeaponReq != classType_0)
				{
					if (missionCardModel_0.WeaponReq != classType_1)
					{
						return 0;
					}
				}
				return 1;
			}
			return 0;
		}

		private static int smethod_14(int int_0, SortedList<int, int> sortedList_0)
		{
			int ınt32;
			if (sortedList_0.TryGetValue(int_0, out ınt32))
			{
				return ınt32;
			}
			return 0;
		}

		private static int smethod_15(int int_0, SortedList<int, int> sortedList_0)
		{
			int ınt32;
			if (sortedList_0.TryGetValue(int_0, out ınt32))
			{
				return ınt32;
			}
			return 0;
		}

		private static int smethod_16(Account account_0, int int_0)
		{
			ItemsModel ıtem = account_0.Inventory.GetItem(int_0);
			if (ıtem == null)
			{
				return 0;
			}
			return ıtem.Id;
		}

		private static bool smethod_17(int int_0, CouponEffects couponEffects_0, ValueTuple<int, CouponEffects, bool> valueTuple_0)
		{
			if (int_0 != valueTuple_0.Item1)
			{
				return false;
			}
			if (valueTuple_0.Item3 != null)
			{
				return (couponEffects_0 & valueTuple_0.Item2) > 0L;
			}
			return couponEffects_0.HasFlag((CouponEffects)valueTuple_0.Item2);
		}

		private static uint[] smethod_18(Account account_0, ItemsModel itemsModel_0)
		{
			uint[] uInt32Array = new uint[3];
			ItemsRepair repairItem = ShopManager.GetRepairItem(itemsModel_0.Id);
			if (repairItem == null)
			{
				uInt32Array[2] = -2147483376;
				return uInt32Array;
			}
			uint quantity = repairItem.Quantity - itemsModel_0.Count;
			if (repairItem.Point <= repairItem.Cash)
			{
				if (repairItem.Cash <= repairItem.Point)
				{
					uInt32Array[2] = -2147483376;
					return uInt32Array;
				}
				uint uInt32 = (uint)ComDiv.Percentage(repairItem.Cash, (int)quantity);
				if ((long)account_0.Cash < (ulong)uInt32)
				{
					uInt32Array[2] = -2147483376;
					return uInt32Array;
				}
				uInt32Array[1] = uInt32;
			}
			else
			{
				uint uInt321 = (uint)ComDiv.Percentage(repairItem.Point, (int)quantity);
				if ((long)account_0.Gold < (ulong)uInt321)
				{
					uInt32Array[2] = -2147483376;
					return uInt32Array;
				}
				uInt32Array[0] = uInt321;
			}
			itemsModel_0.Count = repairItem.Quantity;
			ComDiv.UpdateDB("player_items", "count", (long)((ulong)itemsModel_0.Count), "owner_id", account_0.PlayerId, "id", itemsModel_0.Id);
			uInt32Array[2] = 1;
			return uInt32Array;
		}

		private static void smethod_19(Account account_0, PlayerBattlepass playerBattlepass_0, BattlePassModel battlePassModel_0)
		{
			int goodId;
			int ınt32;
			int goodId1;
			PassBoxModel ıtem = battlePassModel_0.Cards[playerBattlepass_0.Level];
			if (battlePassModel_0.SeasonIsEnabled() && ıtem != null && playerBattlepass_0.TotalPoints >= ıtem.RequiredPoints)
			{
				int level = playerBattlepass_0.Level + 1;
				if (ComDiv.UpdateDB("player_battlepass", "level", level, "owner_id", account_0.PlayerId))
				{
					playerBattlepass_0.Level = level;
				}
				int[] ınt32Array = new int[3];
				PassItemModel normal = ıtem.Normal;
				if (normal != null)
				{
					goodId = normal.GoodId;
				}
				else
				{
					goodId = 0;
				}
				ınt32Array[0] = goodId;
				int[] ınt32Array1 = ınt32Array;
				if (playerBattlepass_0.IsPremium)
				{
					int[] ınt32Array2 = ınt32Array1;
					PassItemModel premiumA = ıtem.PremiumA;
					if (premiumA != null)
					{
						ınt32 = premiumA.GoodId;
					}
					else
					{
						ınt32 = 0;
					}
					ınt32Array2[1] = ınt32;
					int[] ınt32Array3 = ınt32Array1;
					PassItemModel premiumB = ıtem.PremiumB;
					if (premiumB != null)
					{
						goodId1 = premiumB.GoodId;
					}
					else
					{
						goodId1 = 0;
					}
					ınt32Array3[2] = goodId1;
				}
				AllUtils.smethod_22(account_0, ınt32Array1);
			}
		}

		private static void smethod_2(RoomModel roomModel_0, TeamEnum teamEnum_0, bool bool_0, FragInfos fragInfos_0, SlotModel slotModel_0)
		{
			int roundsByMask = roomModel_0.GetRoundsByMask();
			if (roomModel_0.FRRounds >= roundsByMask || roomModel_0.CTRounds >= roundsByMask)
			{
				roomModel_0.StopBomb();
				using (PROTOCOL_BATTLE_WINNING_CAM_ACK pROTOCOLBATTLEWINNINGCAMACK = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
				{
					using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
					{
						roomModel_0.SendPacketToPlayers(pROTOCOLBATTLEWINNINGCAMACK, pROTOCOLBATTLEMISSIONROUNDENDACK, SlotState.BATTLE, 0);
					}
				}
				AllUtils.EndBattle(roomModel_0, roomModel_0.IsBotMode(), teamEnum_0);
				return;
			}
			if (!roomModel_0.ActiveC4 | bool_0)
			{
				roomModel_0.StopBomb();
				roomModel_0.ChangeRounds();
				RoundSync.SendUDPRoundSync(roomModel_0);
				using (PROTOCOL_BATTLE_WINNING_CAM_ACK pROTOCOLBATTLEWINNINGCAMACK1 = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
				{
					using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK1 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
					{
						roomModel_0.SendPacketToPlayers(pROTOCOLBATTLEWINNINGCAMACK1, pROTOCOLBATTLEMISSIONROUNDENDACK1, SlotState.BATTLE, 0);
					}
				}
				roomModel_0.RoundRestart();
			}
		}

		private static void smethod_20(PlayerCompetitive playerCompetitive_0)
		{
			int ınt32 = 0;
			CompetitiveRank competitiveRank = CompetitiveXML.Ranks.FirstOrDefault<CompetitiveRank>((CompetitiveRank competitiveRank_0) => playerCompetitive_0.Points <= competitiveRank_0.Points);
			ınt32 = (competitiveRank == null ? playerCompetitive_0.Level : competitiveRank.Id);
			ComDiv.UpdateDB("player_competitive", "points", playerCompetitive_0.Points, "owner_id", playerCompetitive_0.OwnerId);
			if (ınt32 != playerCompetitive_0.Level && ComDiv.UpdateDB("player_competitive", "level", ınt32, "owner_id", playerCompetitive_0.OwnerId))
			{
				playerCompetitive_0.Level = ınt32;
			}
		}

		private static void smethod_21(Account account_0)
		{
			List<ItemsModel> ıtems = account_0.Inventory.Items;
			lock (ıtems)
			{
				foreach (ItemsModel ıtem in ıtems)
				{
					if (ComDiv.GetIdStatics(ıtem.Id, 1) != 6 || account_0.Character.GetCharacter(ıtem.Id) != null)
					{
						continue;
					}
					AllUtils.CreateCharacter(account_0, ıtem);
				}
			}
		}

		private static void smethod_22(Account account_0, int[] int_0)
		{
			int[] int0 = int_0;
			for (int i = 0; i < (int)int0.Length; i++)
			{
				int ınt32 = int0[i];
				if (ınt32 != 0)
				{
					GoodsItem good = ShopManager.GetGood(ınt32);
					if (good != null)
					{
						if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || account_0.Character.GetCharacter(good.Item.Id) != null)
						{
							account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
						}
						else
						{
							AllUtils.CreateCharacter(account_0, good.Item);
						}
					}
				}
			}
			account_0.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(0, int_0));
		}

		private static int smethod_23(Account account_0, BattleRewardType battleRewardType_0)
		{
			Random random = new Random();
			BattleRewardModel rewardType = BattleRewardXML.GetRewardType(battleRewardType_0);
			if (rewardType == null || !rewardType.Enable || random.Next(100) >= rewardType.Percentage)
			{
				return 0;
			}
			GoodsItem good = ShopManager.GetGood(rewardType.Rewards[random.Next((int)rewardType.Rewards.Length)]);
			if (good == null)
			{
				return 0;
			}
			account_0.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(account_0, good.Item));
			if (ComDiv.GetIdStatics(good.Item.Id, 1) != 29)
			{
				if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || account_0.Character.GetCharacter(good.Item.Id) != null)
				{
					account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
				}
				else
				{
					AllUtils.CreateCharacter(account_0, good.Item);
				}
				return good.Item.Id;
			}
			int ınt32 = 0;
			switch (good.Item.Id)
			{
				case 2900001:
				{
					ınt32 = 1;
					break;
				}
				case 2900002:
				{
					ınt32 = 2;
					break;
				}
				case 2900003:
				{
					ınt32 = 3;
					break;
				}
				case 2900004:
				{
					ınt32 = 4;
					break;
				}
				case 2900005:
				{
					ınt32 = 5;
					break;
				}
			}
			account_0.Tags += ınt32;
			ComDiv.UpdateDB("accounts", "tags", account_0.Tags, "player_id", account_0.PlayerId);
			return good.Item.Id;
		}

		private static void smethod_24(RoomModel roomModel_0, SlotModel slotModel_0, BattleRewardType battleRewardType_0, int int_0, ref int int_1, ref byte[] byte_0, ref int[] int_2)
		{
			Account account;
			if (int_1 < int_0 && roomModel_0.GetPlayerBySlot(slotModel_0, out account))
			{
				byte ıd = (byte)slotModel_0.Id;
				int ınt32 = AllUtils.smethod_23(account, battleRewardType_0);
				if (ıd == 255 || ınt32 == 0)
				{
					return;
				}
				byte_0[int_1] = ıd;
				int_2[int_1] = ınt32;
				int_1++;
			}
		}

		private static void smethod_3(Account account_0, int int_0)
		{
			CharacterModel character = account_0.Character.GetCharacter(int_0);
			if (character != null)
			{
				int ınt32 = 0;
				foreach (CharacterModel characterModel in account_0.Character.Characters)
				{
					if (characterModel.Slot == character.Slot)
					{
						continue;
					}
					characterModel.Slot = ınt32;
					DaoManagerSQL.UpdatePlayerCharacter(ınt32, characterModel.ObjectId, account_0.PlayerId);
					ınt32++;
				}
				if (DaoManagerSQL.DeletePlayerCharacter(character.ObjectId, account_0.PlayerId))
				{
					account_0.Character.RemoveCharacter(character);
				}
			}
		}

		private static void smethod_4(RoomModel roomModel_0, ref int int_0, ref int int_1, ref int int_2, ref int int_3)
		{
			if (roomModel_0.SwapRound)
			{
				int int0 = int_0;
				int int1 = int_1;
				int_1 = int0;
				int_0 = int1;
				int1 = int_2;
				int0 = int_3;
				int_3 = int1;
				int_2 = int0;
			}
		}

		private static void smethod_5(RoomModel roomModel_0, bool bool_0)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			int killsByMask = roomModel_0.GetKillsByMask();
			if (roomModel_0.FRKills < killsByMask && roomModel_0.CTKills < killsByMask)
			{
				return;
			}
			List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				TeamEnum winnerTeam = AllUtils.GetWinnerTeam(roomModel_0);
				roomModel_0.CalculateResult(winnerTeam, bool_0);
				AllUtils.GetBattleResult(roomModel_0, out ınt32, out ınt321, out numArray);
				using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut))
				{
					byte[] completeBytes = pROTOCOLBATTLEMISSIONROUNDENDACK.GetCompleteBytes("AllUtils.BaseEndByKills");
					foreach (Account allPlayer in allPlayers)
					{
						SlotModel slot = roomModel_0.GetSlot(allPlayer.SlotId);
						if (slot == null)
						{
							continue;
						}
						if (slot.State == SlotState.BATTLE)
						{
							allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEMISSIONROUNDENDACK.GetType().Name);
						}
						allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, winnerTeam, ınt321, ınt32, bool_0, numArray));
						AllUtils.UpdateSeasonPass(allPlayer);
					}
				}
			}
			AllUtils.ResetBattleInfo(roomModel_0);
		}

		private static void smethod_6(RoomModel roomModel_0)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			int killsByMask = roomModel_0.GetKillsByMask();
			int[] allKills = new int[18];
			for (int i = 0; i < (int)allKills.Length; i++)
			{
				SlotModel slots = roomModel_0.Slots[i];
				if (slots.PlayerId == 0)
				{
					allKills[i] = 0;
				}
				else
				{
					allKills[i] = slots.AllKills;
				}
			}
			int ınt322 = 0;
			for (int j = 0; j < (int)allKills.Length; j++)
			{
				if (allKills[j] > allKills[ınt322])
				{
					ınt322 = j;
				}
			}
			if (allKills[ınt322] < killsByMask)
			{
				return;
			}
			List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				roomModel_0.CalculateResultFreeForAll(ınt322);
				AllUtils.GetBattleResult(roomModel_0, out ınt32, out ınt321, out numArray);
				using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, ınt322, RoundEndType.FreeForAll))
				{
					byte[] completeBytes = pROTOCOLBATTLEMISSIONROUNDENDACK.GetCompleteBytes("AllUtils.BaseEndByKills");
					foreach (Account allPlayer in allPlayers)
					{
						SlotModel slot = roomModel_0.GetSlot(allPlayer.SlotId);
						if (slot == null)
						{
							continue;
						}
						if (slot.State == SlotState.BATTLE)
						{
							allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEMISSIONROUNDENDACK.GetType().Name);
						}
						allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, ınt322, ınt321, ınt32, false, numArray));
						AllUtils.UpdateSeasonPass(allPlayer);
					}
				}
			}
			AllUtils.ResetBattleInfo(roomModel_0);
		}

		private static SortedList<int, ClanTeam> smethod_7(RoomModel roomModel_0)
		{
			ClanTeam clanTeam;
			SortedList<int, ClanTeam> ınt32s = new SortedList<int, ClanTeam>();
			for (int i = 0; i < roomModel_0.GetAllPlayers().Count; i++)
			{
				Account ıtem = roomModel_0.GetAllPlayers()[i];
				if (ıtem.ClanId != 0)
				{
					if (!ınt32s.TryGetValue(ıtem.ClanId, out clanTeam) || clanTeam == null)
					{
						clanTeam = new ClanTeam()
						{
							ClanId = ıtem.ClanId
						};
						if (ıtem.SlotId % 2 != 0)
						{
							ClanTeam playersCT = clanTeam;
							playersCT.PlayersCT = playersCT.PlayersCT + 1;
						}
						else
						{
							ClanTeam playersFR = clanTeam;
							playersFR.PlayersFR = playersFR.PlayersFR + 1;
						}
						ınt32s.Add(ıtem.ClanId, clanTeam);
					}
					else if (ıtem.SlotId % 2 != 0)
					{
						ClanTeam playersCT1 = clanTeam;
						playersCT1.PlayersCT = playersCT1.PlayersCT + 1;
					}
					else
					{
						ClanTeam playersFR1 = clanTeam;
						playersFR1.PlayersFR = playersFR1.PlayersFR + 1;
					}
				}
			}
			return ınt32s;
		}

		private static void smethod_8(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, FragInfos fragInfos_0, MissionType missionType_0, int int_0)
		{
			ClassType ıdStatics;
			try
			{
				PlayerMissions missions = slotModel_0.Missions;
				if (missions != null)
				{
					int currentMissionId = missions.GetCurrentMissionId();
					int currentCard = missions.GetCurrentCard();
					if (currentMissionId > 0 && !missions.SelectedCard)
					{
						List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
						if (cards.Count != 0)
						{
							KillingMessage allKillFlags = fragInfos_0.GetAllKillFlags();
							byte[] currentMissionList = missions.GetCurrentMissionList();
							ClassType classType = (ClassType)ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2);
							ClassType classType1 = AllUtils.smethod_1(classType);
							int ınt32 = ComDiv.GetIdStatics(fragInfos_0.WeaponId, 3);
							if (int_0 > 0)
							{
								ıdStatics = (ClassType)ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2);
							}
							else
							{
								ıdStatics = ClassType.Unknown;
							}
							ClassType classType2 = ıdStatics;
							ClassType classType3 = (int_0 > 0 ? AllUtils.smethod_1(classType2) : ClassType.Unknown);
							int ınt321 = (int_0 > 0 ? ComDiv.GetIdStatics(int_0, 3) : 0);
							foreach (MissionCardModel card in cards)
							{
								int ınt322 = 0;
								if (card.MapId == 0 || card.MapId == (int)roomModel_0.MapId)
								{
									if (fragInfos_0.Frags.Count > 0)
									{
										if (card.MissionType != MissionType.KILL && (card.MissionType != MissionType.CHAINSTOPPER || !allKillFlags.HasFlag(KillingMessage.ChainStopper)) && (card.MissionType != MissionType.CHAINSLUGGER || !allKillFlags.HasFlag(KillingMessage.ChainSlugger)) && (card.MissionType != MissionType.CHAINKILLER || slotModel_0.KillsOnLife < 4) && (card.MissionType != MissionType.TRIPLE_KILL || slotModel_0.KillsOnLife != 3) && (card.MissionType != MissionType.DOUBLE_KILL || slotModel_0.KillsOnLife != 2) && (card.MissionType != MissionType.HEADSHOT || !allKillFlags.HasFlag(KillingMessage.Headshot) && !allKillFlags.HasFlag(KillingMessage.ChainHeadshot)) && (card.MissionType != MissionType.CHAINHEADSHOT || !allKillFlags.HasFlag(KillingMessage.ChainHeadshot)) && (card.MissionType != MissionType.PIERCING || !allKillFlags.HasFlag(KillingMessage.PiercingShot)) && (card.MissionType != MissionType.MASS_KILL || !allKillFlags.HasFlag(KillingMessage.MassKill)))
										{
											if (card.MissionType == MissionType.KILL_MAN && roomModel_0.IsDinoMode(""))
											{
												if (slotModel_0.Team != TeamEnum.CT_TEAM || roomModel_0.Rounds != 2)
												{
													if (slotModel_0.Team != TeamEnum.FR_TEAM || roomModel_0.Rounds != 1)
													{
														goto Label2;
													}
													goto Label1;
												}
												else
												{
													goto Label1;
												}
											}
										Label2:
											if (card.MissionType == MissionType.KILL_WEAPONCLASS || card.MissionType == MissionType.DOUBLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 2 || card.MissionType == MissionType.TRIPLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 3)
											{
												ınt322 = AllUtils.smethod_11(card, fragInfos_0);
												goto Label0;
											}
											else
											{
												goto Label0;
											}
										}
									Label1:
										ınt322 = AllUtils.smethod_10(card, classType, classType1, ınt32, fragInfos_0);
									}
									else if (card.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
									{
										ınt322 = AllUtils.smethod_13(card, classType2, classType3, ınt321);
									}
									else if (card.MissionType == missionType_0)
									{
										ınt322 = 1;
									}
								}
							Label0:
								if (ınt322 == 0)
								{
									continue;
								}
								int arrayIdx = card.ArrayIdx;
								if (currentMissionList[arrayIdx] + 1 > card.MissionLimit)
								{
									continue;
								}
								slotModel_0.MissionsCompleted = true;
								ref byte numPointer = ref currentMissionList[arrayIdx];
								numPointer = (byte)(numPointer + (byte)ınt322);
								if (currentMissionList[arrayIdx] > card.MissionLimit)
								{
									currentMissionList[arrayIdx] = (byte)card.MissionLimit;
								}
								account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK((int)currentMissionList[arrayIdx], card));
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

		private static void smethod_9(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, MissionType missionType_0, int int_0)
		{
			ClassType ıdStatics;
			try
			{
				PlayerMissions missions = slotModel_0.Missions;
				if (missions != null)
				{
					int currentMissionId = missions.GetCurrentMissionId();
					int currentCard = missions.GetCurrentCard();
					if (currentMissionId > 0 && !missions.SelectedCard)
					{
						List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
						if (cards.Count != 0)
						{
							byte[] currentMissionList = missions.GetCurrentMissionList();
							if (int_0 > 0)
							{
								ıdStatics = (ClassType)ComDiv.GetIdStatics(int_0, 2);
							}
							else
							{
								ıdStatics = ClassType.Unknown;
							}
							ClassType classType = ıdStatics;
							ClassType classType1 = (int_0 > 0 ? AllUtils.smethod_1(classType) : ClassType.Unknown);
							int ınt32 = (int_0 > 0 ? ComDiv.GetIdStatics(int_0, 3) : 0);
							foreach (MissionCardModel card in cards)
							{
								int ınt321 = 0;
								if (card.MapId == 0 || card.MapId == (int)roomModel_0.MapId)
								{
									if (card.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
									{
										ınt321 = AllUtils.smethod_13(card, classType, classType1, ınt32);
									}
									else if (card.MissionType == missionType_0)
									{
										ınt321 = 1;
									}
								}
								if (ınt321 == 0)
								{
									continue;
								}
								int arrayIdx = card.ArrayIdx;
								if (currentMissionList[arrayIdx] + 1 > card.MissionLimit)
								{
									continue;
								}
								slotModel_0.MissionsCompleted = true;
								ref byte numPointer = ref currentMissionList[arrayIdx];
								numPointer = (byte)(numPointer + (byte)ınt321);
								if (currentMissionList[arrayIdx] > card.MissionLimit)
								{
									currentMissionList[arrayIdx] = (byte)card.MissionLimit;
								}
								account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK((int)currentMissionList[arrayIdx], card));
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

		public static void SyncPlayerToClanMembers(Account player)
		{
			if (player == null || player.ClanId <= 0)
			{
				return;
			}
			using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK pROTOCOLCSMEMBERINFOCHANGEACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(player))
			{
				ClanManager.SendPacket(pROTOCOLCSMEMBERINFOCHANGEACK, player.ClanId, player.PlayerId, true, true);
			}
		}

		public static void SyncPlayerToFriends(Account p, bool all)
		{
			if (p == null || p.Friend.Friends.Count == 0)
			{
				return;
			}
			PlayerInfo playerInfo = new PlayerInfo(p.PlayerId, p.Rank, p.NickColor, p.Nickname, p.IsOnline, p.Status);
			for (int i = 0; i < p.Friend.Friends.Count; i++)
			{
				FriendModel ıtem = p.Friend.Friends[i];
				if (all || ıtem.State == 0 && !ıtem.Removed)
				{
					Account account = AccountManager.GetAccount(ıtem.PlayerId, 287);
					if (account != null)
					{
						int ınt32 = -1;
						FriendModel friend = account.Friend.GetFriend(p.PlayerId, out ınt32);
						if (friend != null)
						{
							friend.Info = playerInfo;
							account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, ınt32), false);
						}
					}
				}
			}
		}

		public static void TryBalancePlayer(RoomModel Room, Account Player, bool InBattle, ref SlotModel MySlot)
		{
			SlotModel slot = Room.GetSlot(Player.SlotId);
			if (slot == null)
			{
				return;
			}
			TeamEnum team = slot.Team;
			TeamEnum balanceTeamIdx = AllUtils.GetBalanceTeamIdx(Room, InBattle, team);
			if (team != balanceTeamIdx)
			{
				if (balanceTeamIdx != TeamEnum.ALL_TEAM)
				{
					SlotModel slotModel = null;
					int[] ınt32Array = (team == TeamEnum.CT_TEAM ? Room.FR_TEAM : Room.CT_TEAM);
					int ınt32 = 0;
					while (true)
					{
						if (ınt32 < (int)ınt32Array.Length)
						{
							SlotModel slots = Room.Slots[ınt32Array[ınt32]];
							if (slots.State == SlotState.CLOSE || slots.PlayerId != 0)
							{
								ınt32++;
							}
							else
							{
								slotModel = slots;
								break;
							}
						}
						else
						{
							break;
						}
					}
					if (slotModel != null)
					{
						List<SlotChange> slotChanges = new List<SlotChange>();
						lock (Room.Slots)
						{
							Room.SwitchSlots(slotChanges, slotModel.Id, slot.Id, false);
						}
						if (slotChanges.Count > 0)
						{
							Player.SlotId = slot.Id;
							MySlot = slot;
							using (PROTOCOL_ROOM_TEAM_BALANCE_ACK pROTOCOLROOMTEAMBALANCEACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotChanges, Room.LeaderSlot, 1))
							{
								Room.SendPacketToPlayers(pROTOCOLROOMTEAMBALANCEACK);
							}
							Room.UpdateSlotsInfo();
						}
					}
					return;
				}
			}
		}

		public static void TryBalanceTeams(RoomModel Room)
		{
			Account account;
			if (Room.BalanceType != TeamBalance.Count || Room.IsBotMode())
			{
				return;
			}
			TeamEnum balanceTeamIdx = AllUtils.GetBalanceTeamIdx(Room, false, TeamEnum.ALL_TEAM);
			if (balanceTeamIdx == TeamEnum.ALL_TEAM)
			{
				return;
			}
			int[] ınt32Array = (balanceTeamIdx == TeamEnum.CT_TEAM ? Room.FR_TEAM : Room.CT_TEAM);
			SlotModel slotModel = null;
			int length = (int)ınt32Array.Length - 1;
			while (true)
			{
				if (length >= 0)
				{
					SlotModel slots = Room.Slots[ınt32Array[length]];
					if (slots.State != SlotState.READY || Room.LeaderSlot == slots.Id)
					{
						length--;
					}
					else
					{
						slotModel = slots;
						break;
					}
				}
				else
				{
					break;
				}
			}
			if (slotModel != null && Room.GetPlayerBySlot(slotModel, out account))
			{
				AllUtils.TryBalancePlayer(Room, account, false, ref slotModel);
			}
		}

		public static void UpdateDailyRecord(bool WonTheMatch, Account Player, int winnerTeam, DBQuery query)
		{
			int matchDraws;
			if (winnerTeam == 2)
			{
				StatisticDaily daily = Player.Statistic.Daily;
				matchDraws = daily.MatchDraws + 1;
				daily.MatchDraws = matchDraws;
				query.AddQuery("match_draws", matchDraws);
			}
			else if (!WonTheMatch)
			{
				StatisticDaily statisticDaily = Player.Statistic.Daily;
				matchDraws = statisticDaily.MatchLoses + 1;
				statisticDaily.MatchLoses = matchDraws;
				query.AddQuery("match_loses", matchDraws);
			}
			else
			{
				StatisticDaily daily1 = Player.Statistic.Daily;
				matchDraws = daily1.MatchWins + 1;
				daily1.MatchWins = matchDraws;
				query.AddQuery("match_wins", matchDraws);
			}
			StatisticDaily statisticDaily1 = Player.Statistic.Daily;
			matchDraws = statisticDaily1.Matches + 1;
			statisticDaily1.Matches = matchDraws;
			query.AddQuery("matches", matchDraws);
		}

		public static void UpdateMatchCount(bool WonTheMatch, Account Player, int WinnerTeam, DBQuery TotalQuery, DBQuery SeasonQuery)
		{
			int matchDraws;
			if (WinnerTeam == 2)
			{
				StatisticTotal basic = Player.Statistic.Basic;
				matchDraws = basic.MatchDraws + 1;
				basic.MatchDraws = matchDraws;
				TotalQuery.AddQuery("match_draws", matchDraws);
				StatisticSeason season = Player.Statistic.Season;
				matchDraws = season.MatchDraws + 1;
				season.MatchDraws = matchDraws;
				SeasonQuery.AddQuery("match_draws", matchDraws);
			}
			else if (!WonTheMatch)
			{
				StatisticTotal statisticTotal = Player.Statistic.Basic;
				matchDraws = statisticTotal.MatchLoses + 1;
				statisticTotal.MatchLoses = matchDraws;
				TotalQuery.AddQuery("match_loses", matchDraws);
				StatisticSeason statisticSeason = Player.Statistic.Season;
				matchDraws = statisticSeason.MatchLoses + 1;
				statisticSeason.MatchLoses = matchDraws;
				SeasonQuery.AddQuery("match_loses", matchDraws);
			}
			else
			{
				StatisticTotal basic1 = Player.Statistic.Basic;
				matchDraws = basic1.MatchWins + 1;
				basic1.MatchWins = matchDraws;
				TotalQuery.AddQuery("match_wins", matchDraws);
				StatisticSeason season1 = Player.Statistic.Season;
				matchDraws = season1.MatchWins + 1;
				season1.MatchWins = matchDraws;
				SeasonQuery.AddQuery("match_wins", matchDraws);
			}
			StatisticTotal statisticTotal1 = Player.Statistic.Basic;
			matchDraws = statisticTotal1.Matches + 1;
			statisticTotal1.Matches = matchDraws;
			TotalQuery.AddQuery("matches", matchDraws);
			StatisticTotal basic2 = Player.Statistic.Basic;
			matchDraws = basic2.TotalMatchesCount + 1;
			basic2.TotalMatchesCount = matchDraws;
			TotalQuery.AddQuery("total_matches", matchDraws);
			StatisticSeason statisticSeason1 = Player.Statistic.Season;
			matchDraws = statisticSeason1.Matches + 1;
			statisticSeason1.Matches = matchDraws;
			SeasonQuery.AddQuery("matches", matchDraws);
			StatisticSeason season2 = Player.Statistic.Season;
			matchDraws = season2.TotalMatchesCount + 1;
			season2.TotalMatchesCount = matchDraws;
			SeasonQuery.AddQuery("total_matches", matchDraws);
		}

		public static void UpdateMatchCountFFA(RoomModel Room, Account Player, int SlotWin, DBQuery TotalQuery, DBQuery SeasonQuery)
		{
			int matchLoses;
			int[] allKills = new int[18];
			for (int i = 0; i < (int)allKills.Length; i++)
			{
				SlotModel slots = Room.Slots[i];
				if (slots.PlayerId == 0)
				{
					allKills[i] = 0;
				}
				else
				{
					allKills[i] = slots.AllKills;
				}
			}
			int ınt32 = 0;
			for (int j = 0; j < (int)allKills.Length; j++)
			{
				if (allKills[j] > allKills[ınt32])
				{
					ınt32 = j;
				}
			}
			if (allKills[ınt32] != SlotWin)
			{
				StatisticTotal basic = Player.Statistic.Basic;
				matchLoses = basic.MatchLoses + 1;
				basic.MatchLoses = matchLoses;
				TotalQuery.AddQuery("match_loses", matchLoses);
				StatisticSeason season = Player.Statistic.Season;
				matchLoses = season.MatchLoses + 1;
				season.MatchLoses = matchLoses;
				SeasonQuery.AddQuery("match_loses", matchLoses);
			}
			else
			{
				StatisticTotal statisticTotal = Player.Statistic.Basic;
				matchLoses = statisticTotal.MatchWins + 1;
				statisticTotal.MatchWins = matchLoses;
				TotalQuery.AddQuery("match_wins", matchLoses);
				StatisticSeason statisticSeason = Player.Statistic.Season;
				matchLoses = statisticSeason.MatchWins + 1;
				statisticSeason.MatchWins = matchLoses;
				SeasonQuery.AddQuery("match_wins", matchLoses);
			}
			StatisticTotal basic1 = Player.Statistic.Basic;
			matchLoses = basic1.Matches + 1;
			basic1.Matches = matchLoses;
			TotalQuery.AddQuery("matches", matchLoses);
			StatisticTotal statisticTotal1 = Player.Statistic.Basic;
			matchLoses = statisticTotal1.TotalMatchesCount + 1;
			statisticTotal1.TotalMatchesCount = matchLoses;
			TotalQuery.AddQuery("total_matches", matchLoses);
			StatisticSeason season1 = Player.Statistic.Season;
			matchLoses = season1.Matches + 1;
			season1.Matches = matchLoses;
			SeasonQuery.AddQuery("matches", matchLoses);
			StatisticSeason statisticSeason1 = Player.Statistic.Season;
			matchLoses = statisticSeason1.TotalMatchesCount + 1;
			statisticSeason1.TotalMatchesCount = matchLoses;
			SeasonQuery.AddQuery("total_matches", matchLoses);
		}

		public static void UpdateMatchDailyRecordFFA(RoomModel Room, Account Player, int SlotWin, DBQuery Query)
		{
			int matchLoses;
			int[] allKills = new int[18];
			for (int i = 0; i < (int)allKills.Length; i++)
			{
				SlotModel slots = Room.Slots[i];
				if (slots.PlayerId == 0)
				{
					allKills[i] = 0;
				}
				else
				{
					allKills[i] = slots.AllKills;
				}
			}
			int ınt32 = 0;
			for (int j = 0; j < (int)allKills.Length; j++)
			{
				if (allKills[j] > allKills[ınt32])
				{
					ınt32 = j;
				}
			}
			if (allKills[ınt32] != SlotWin)
			{
				StatisticDaily daily = Player.Statistic.Daily;
				matchLoses = daily.MatchLoses + 1;
				daily.MatchLoses = matchLoses;
				Query.AddQuery("match_loses", matchLoses);
			}
			else
			{
				StatisticDaily statisticDaily = Player.Statistic.Daily;
				matchLoses = statisticDaily.MatchWins + 1;
				statisticDaily.MatchWins = matchLoses;
				Query.AddQuery("match_wins", matchLoses);
			}
			StatisticDaily daily1 = Player.Statistic.Daily;
			matchLoses = daily1.Matches + 1;
			daily1.Matches = matchLoses;
			Query.AddQuery("matches", matchLoses);
		}

		public static void UpdateSeasonPass(Account Player)
		{
			if (SeasonChallengeXML.GetActiveSeasonPass() == null)
			{
				return;
			}
			if (Player.UpdateSeasonpass)
			{
				Player.UpdateSeasonpass = false;
				Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
				Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Player));
			}
		}

		public static void UpdateSlotEquips(Account Player)
		{
			RoomModel room = Player.Room;
			if (room != null)
			{
				AllUtils.UpdateSlotEquips(Player, room);
			}
		}

		public static void UpdateSlotEquips(Account Player, RoomModel Room)
		{
			SlotModel equipment;
			if (Room.GetSlot(Player.SlotId, out equipment))
			{
				equipment.Equipment = Player.Equipment;
			}
			Room.UpdateSlotsInfo();
		}

		public static void UpdateWeaponRecord(Account Player, SlotModel Slot, DBQuery Query)
		{
			int assaultKills;
			StatisticWeapon weapon = Player.Statistic.Weapon;
			if (Slot.AR[0] > 0)
			{
				StatisticWeapon statisticWeapon = weapon;
				assaultKills = statisticWeapon.AssaultKills + 1;
				statisticWeapon.AssaultKills = assaultKills;
				Query.AddQuery("assault_rifle_kills", assaultKills);
			}
			if (Slot.AR[1] > 0)
			{
				StatisticWeapon statisticWeapon1 = weapon;
				assaultKills = statisticWeapon1.AssaultDeaths + 1;
				statisticWeapon1.AssaultDeaths = assaultKills;
				Query.AddQuery("assault_rifle_deaths", assaultKills);
			}
			if (Slot.SMG[0] > 0)
			{
				StatisticWeapon statisticWeapon2 = weapon;
				assaultKills = statisticWeapon2.SmgKills + 1;
				statisticWeapon2.SmgKills = assaultKills;
				Query.AddQuery("sub_machine_gun_kills", assaultKills);
			}
			if (Slot.SMG[1] > 0)
			{
				StatisticWeapon statisticWeapon3 = weapon;
				assaultKills = statisticWeapon3.SmgDeaths + 1;
				statisticWeapon3.SmgDeaths = assaultKills;
				Query.AddQuery("sub_machine_gun_deaths", assaultKills);
			}
			if (Slot.SR[0] > 0)
			{
				StatisticWeapon statisticWeapon4 = weapon;
				assaultKills = statisticWeapon4.SniperKills + 1;
				statisticWeapon4.SniperKills = assaultKills;
				Query.AddQuery("sniper_rifle_kills", assaultKills);
			}
			if (Slot.SR[1] > 0)
			{
				StatisticWeapon statisticWeapon5 = weapon;
				assaultKills = statisticWeapon5.SniperDeaths + 1;
				statisticWeapon5.SniperDeaths = assaultKills;
				Query.AddQuery("sniper_rifle_deaths", assaultKills);
			}
			if (Slot.SG[0] > 0)
			{
				StatisticWeapon statisticWeapon6 = weapon;
				assaultKills = statisticWeapon6.ShotgunKills + 1;
				statisticWeapon6.ShotgunKills = assaultKills;
				Query.AddQuery("shot_gun_kills", assaultKills);
			}
			if (Slot.SG[1] > 0)
			{
				StatisticWeapon statisticWeapon7 = weapon;
				assaultKills = statisticWeapon7.ShotgunDeaths + 1;
				statisticWeapon7.ShotgunDeaths = assaultKills;
				Query.AddQuery("shot_gun_deaths", assaultKills);
			}
			if (Slot.MG[0] > 0)
			{
				StatisticWeapon statisticWeapon8 = weapon;
				assaultKills = statisticWeapon8.MachinegunKills + 1;
				statisticWeapon8.MachinegunKills = assaultKills;
				Query.AddQuery("machine_gun_kills", assaultKills);
			}
			if (Slot.MG[1] > 0)
			{
				StatisticWeapon statisticWeapon9 = weapon;
				assaultKills = statisticWeapon9.MachinegunDeaths + 1;
				statisticWeapon9.MachinegunDeaths = assaultKills;
				Query.AddQuery("machine_gun_deaths", assaultKills);
			}
			if (Slot.SHD[0] > 0)
			{
				StatisticWeapon statisticWeapon10 = weapon;
				assaultKills = statisticWeapon10.ShieldKills + 1;
				statisticWeapon10.ShieldKills = assaultKills;
				Query.AddQuery("shield_kills", assaultKills);
			}
			if (Slot.SHD[1] > 0)
			{
				StatisticWeapon statisticWeapon11 = weapon;
				assaultKills = statisticWeapon11.ShieldDeaths + 1;
				statisticWeapon11.ShieldDeaths = assaultKills;
				Query.AddQuery("shield_deaths", assaultKills);
			}
		}

		public static void ValidateAccesoryEquipment(Account Player, int AccessoryId)
		{
			if (Player.Equipment.AccessoryId != AccessoryId)
			{
				Player.Equipment.AccessoryId = AllUtils.smethod_16(Player, AccessoryId);
				ComDiv.UpdateDB("player_equipments", "accesory_id", Player.Equipment.AccessoryId, "owner_id", Player.PlayerId);
			}
		}

		public static void ValidateAuthLevel(Account Player)
		{
			if (!Enum.IsDefined(typeof(AccessLevel), Player.Access))
			{
				AccessLevel accessLevel = Player.AuthLevel();
				if (ComDiv.UpdateDB("accounts", "access_level", (int)accessLevel, "player_id", Player.PlayerId))
				{
					Player.Access = accessLevel;
				}
			}
		}

		public static void ValidateBanPlayer(Account Player, string Message)
		{
			if (ConfigLoader.AutoBan && DaoManagerSQL.SaveAutoBan(Player.PlayerId, Player.Username, Player.Nickname, string.Concat("Cheat ", Message, ")"), DateTimeUtil.Now("dd -MM-yyyy HH:mm:ss"), Player.PublicIP.ToString(), "Illegal Program"))
			{
				using (PROTOCOL_LOBBY_CHATTING_ACK pROTOCOLLOBBYCHATTINGACK = new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 1, false, string.Concat("Permanently ban player [", Player.Nickname, "], ", Message)))
				{
					GameXender.Client.SendPacketToAllClients(pROTOCOLLOBBYCHATTINGACK);
				}
				Player.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
				Player.Close(1000, true);
			}
			CLogger.Print(string.Format("Player: {0}; Id: {1}; User: {2}; Reason: {3}", new object[] { Player.Nickname, Player.PlayerId, Player.Username, Message }), LoggerType.Hack, null);
		}

		public static void ValidateCharacterEquipment(Account Player, PlayerEquipment Equip, int[] EquipmentList, int CharacterId, int[] CharaSlots)
		{
			DBQuery dBQuery = new DBQuery();
			CharacterModel character = Player.Character.GetCharacter(CharacterId);
			if (character != null)
			{
				int ıdStatics = ComDiv.GetIdStatics(character.Id, 1);
				int ınt32 = ComDiv.GetIdStatics(character.Id, 2);
				int ıdStatics1 = ComDiv.GetIdStatics(character.Id, 5);
				if (ıdStatics == 6 && (ınt32 == 1 || ıdStatics1 == 632) && CharaSlots[0] == character.Slot)
				{
					if (Equip.CharaRedId != character.Id)
					{
						Equip.CharaRedId = character.Id;
						dBQuery.AddQuery("chara_red_side", Equip.CharaRedId);
					}
				}
				else if (ıdStatics == 6 && (ınt32 == 2 || ıdStatics1 == 664) && CharaSlots[1] == character.Slot && Equip.CharaBlueId != character.Id)
				{
					Equip.CharaBlueId = character.Id;
					dBQuery.AddQuery("chara_blue_side", Equip.CharaBlueId);
				}
			}
			for (int i = 0; i < (int)EquipmentList.Length; i++)
			{
				int ınt321 = AllUtils.smethod_16(Player, EquipmentList[i]);
				switch (i)
				{
					case 0:
					{
						if (ınt321 == 0 || Equip.WeaponPrimary == ınt321)
						{
							break;
						}
						Equip.WeaponPrimary = ınt321;
						dBQuery.AddQuery("weapon_primary", Equip.WeaponPrimary);
						break;
					}
					case 1:
					{
						if (ınt321 == 0 || Equip.WeaponSecondary == ınt321)
						{
							break;
						}
						Equip.WeaponSecondary = ınt321;
						dBQuery.AddQuery("weapon_secondary", Equip.WeaponSecondary);
						break;
					}
					case 2:
					{
						if (ınt321 == 0 || Equip.WeaponMelee == ınt321)
						{
							break;
						}
						Equip.WeaponMelee = ınt321;
						dBQuery.AddQuery("weapon_melee", Equip.WeaponMelee);
						break;
					}
					case 3:
					{
						if (ınt321 == 0 || Equip.WeaponExplosive == ınt321)
						{
							break;
						}
						Equip.WeaponExplosive = ınt321;
						dBQuery.AddQuery("weapon_explosive", Equip.WeaponExplosive);
						break;
					}
					case 4:
					{
						if (ınt321 == 0 || Equip.WeaponSpecial == ınt321)
						{
							break;
						}
						Equip.WeaponSpecial = ınt321;
						dBQuery.AddQuery("weapon_special", Equip.WeaponSpecial);
						break;
					}
					case 5:
					{
						if (Equip.PartHead == ınt321)
						{
							break;
						}
						Equip.PartHead = ınt321;
						dBQuery.AddQuery("part_head", Equip.PartHead);
						break;
					}
					case 6:
					{
						if (ınt321 == 0 || Equip.PartFace == ınt321)
						{
							break;
						}
						Equip.PartFace = ınt321;
						dBQuery.AddQuery("part_face", Equip.PartFace);
						break;
					}
					case 7:
					{
						if (ınt321 == 0 || Equip.PartJacket == ınt321)
						{
							break;
						}
						Equip.PartJacket = ınt321;
						dBQuery.AddQuery("part_jacket", Equip.PartJacket);
						break;
					}
					case 8:
					{
						if (ınt321 == 0 || Equip.PartPocket == ınt321)
						{
							break;
						}
						Equip.PartPocket = ınt321;
						dBQuery.AddQuery("part_pocket", Equip.PartPocket);
						break;
					}
					case 9:
					{
						if (ınt321 == 0 || Equip.PartGlove == ınt321)
						{
							break;
						}
						Equip.PartGlove = ınt321;
						dBQuery.AddQuery("part_glove", Equip.PartGlove);
						break;
					}
					case 10:
					{
						if (ınt321 == 0 || Equip.PartBelt == ınt321)
						{
							break;
						}
						Equip.PartBelt = ınt321;
						dBQuery.AddQuery("part_belt", Equip.PartBelt);
						break;
					}
					case 11:
					{
						if (ınt321 == 0 || Equip.PartHolster == ınt321)
						{
							break;
						}
						Equip.PartHolster = ınt321;
						dBQuery.AddQuery("part_holster", Equip.PartHolster);
						break;
					}
					case 12:
					{
						if (ınt321 == 0 || Equip.PartSkin == ınt321)
						{
							break;
						}
						Equip.PartSkin = ınt321;
						dBQuery.AddQuery("part_skin", Equip.PartSkin);
						break;
					}
					case 13:
					{
						if (Equip.BeretItem == ınt321)
						{
							break;
						}
						Equip.BeretItem = ınt321;
						dBQuery.AddQuery("beret_item_part", Equip.BeretItem);
						break;
					}
				}
			}
			ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
		}

		public static void ValidateCharacterSlot(Account Player, PlayerEquipment Equip, int[] Slots)
		{
			DBQuery dBQuery = new DBQuery();
			CharacterModel characterSlot = Player.Character.GetCharacterSlot(Slots[0]);
			if (characterSlot != null && Equip.CharaRedId != characterSlot.Id)
			{
				Equip.CharaRedId = AllUtils.smethod_16(Player, characterSlot.Id);
				dBQuery.AddQuery("chara_red_side", Equip.CharaRedId);
			}
			CharacterModel characterModel = Player.Character.GetCharacterSlot(Slots[1]);
			if (characterModel != null && Equip.CharaBlueId != characterModel.Id)
			{
				Equip.CharaBlueId = AllUtils.smethod_16(Player, characterModel.Id);
				dBQuery.AddQuery("chara_blue_side", Equip.CharaBlueId);
			}
			ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
		}

		public static void ValidateDisabledCoupon(Account Player, SortedList<int, int> Coupons)
		{
			for (int i = 0; i < Coupons.Keys.Count; i++)
			{
				ItemsModel ıtem = Player.Inventory.GetItem(AllUtils.smethod_14(i, Coupons));
				if (ıtem != null)
				{
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
					if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						Player.Effects -= couponEffect.EffectFlag;
						DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
					}
				}
			}
		}

		public static void ValidateEnabledCoupon(Account Player, SortedList<int, int> Coupons)
		{
			for (int i = 0; i < Coupons.Keys.Count; i++)
			{
				ItemsModel ıtem = Player.Inventory.GetItem(AllUtils.smethod_14(i, Coupons));
				if (ıtem != null)
				{
					bool flag = Player.Bonus.AddBonuses(ıtem.Id);
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
					if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && !Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						Player.Effects |= couponEffect.EffectFlag;
						DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
					}
					if (flag)
					{
						DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
					}
				}
			}
		}

		public static void ValidateItemEquipment(Account Player, SortedList<int, int> Items)
		{
			for (int i = 0; i < Items.Keys.Count; i++)
			{
				int ınt32 = AllUtils.smethod_15(i, Items);
				switch (i)
				{
					case 0:
					{
						if (ınt32 == 0 || Player.Equipment.DinoItem == ınt32)
						{
							break;
						}
						Player.Equipment.DinoItem = AllUtils.smethod_16(Player, ınt32);
						ComDiv.UpdateDB("player_equipments", "dino_item_chara", Player.Equipment.DinoItem, "owner_id", Player.PlayerId);
						break;
					}
					case 1:
					{
						if (Player.Equipment.SprayId == ınt32)
						{
							break;
						}
						Player.Equipment.SprayId = AllUtils.smethod_16(Player, ınt32);
						ComDiv.UpdateDB("player_equipments", "spray_id", Player.Equipment.SprayId, "owner_id", Player.PlayerId);
						break;
					}
					case 2:
					{
						if (Player.Equipment.NameCardId == ınt32)
						{
							break;
						}
						Player.Equipment.NameCardId = AllUtils.smethod_16(Player, ınt32);
						ComDiv.UpdateDB("player_equipments", "namecard_id", Player.Equipment.NameCardId, "owner_id", Player.PlayerId);
						break;
					}
				}
			}
		}

		public static void ValidatePlayerInventoryStatus(Account Player)
		{
			string str;
			Player.Inventory.LoadBasicItems();
			if (Player.Rank >= 46)
			{
				Player.Inventory.LoadGeneralBeret();
			}
			if (Player.IsGM())
			{
				Player.Inventory.LoadHatForGM();
			}
			if (!string.IsNullOrEmpty(Player.Nickname))
			{
				AllUtils.smethod_21(Player);
			}
			if (!AllUtils.smethod_0(Player, out str))
			{
				foreach (ItemsModel pCCafeReward in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
				{
					if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) == 6 && Player.Character.GetCharacter(pCCafeReward.Id) != null)
					{
						AllUtils.smethod_3(Player, pCCafeReward.Id);
					}
					if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) != 16)
					{
						continue;
					}
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(pCCafeReward.Id);
					if (couponEffect == null || (long)couponEffect.EffectFlag <= 0L || !Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						continue;
					}
					Player.Effects -= couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
				if (Player.CafePC > CafeEnum.None && ComDiv.UpdateDB("accounts", "pc_cafe", 0, "player_id", Player.PlayerId))
				{
					Player.CafePC = CafeEnum.None;
					if (!string.IsNullOrEmpty(str) && ComDiv.DeleteDB("player_vip", "owner_id", Player.PlayerId))
					{
						CLogger.Print(string.Format("VIP for UID: {0} Nick: {1} Deleted Due To {2}", Player.PlayerId, Player.Nickname, str), LoggerType.Info, null);
					}
					CLogger.Print(string.Format("Player PC Cafe was resetted by default into '{0}'; (UID: {1} Nick: {2})", Player.CafePC, Player.PlayerId, Player.Nickname), LoggerType.Info, null);
				}
			}
			else
			{
				List<ItemsModel> pCCafeRewards = TemplatePackXML.GetPCCafeRewards(Player.CafePC);
				lock (Player.Inventory.Items)
				{
					Player.Inventory.Items.AddRange(pCCafeRewards);
				}
				foreach (ItemsModel ıtemsModel in pCCafeRewards)
				{
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) == 6 && Player.Character.GetCharacter(ıtemsModel.Id) == null)
					{
						AllUtils.CreateCharacter(Player, ıtemsModel);
					}
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) != 16)
					{
						continue;
					}
					CouponFlag couponFlag = CouponEffectXML.GetCouponEffect(ıtemsModel.Id);
					if (couponFlag == null || (long)couponFlag.EffectFlag <= 0L || Player.Effects.HasFlag(couponFlag.EffectFlag))
					{
						continue;
					}
					Player.Effects |= couponFlag.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
			}
		}

		public static PlayerEquipment ValidateRespawnEQ(SlotModel Slot, int[] ItemIds)
		{
			PlayerEquipment playerEquipment = new PlayerEquipment()
			{
				WeaponPrimary = ItemIds[0],
				WeaponSecondary = ItemIds[1],
				WeaponMelee = ItemIds[2],
				WeaponExplosive = ItemIds[3],
				WeaponSpecial = ItemIds[4],
				PartHead = ItemIds[6],
				PartFace = ItemIds[7],
				PartJacket = ItemIds[8],
				PartPocket = ItemIds[9],
				PartGlove = ItemIds[10],
				PartBelt = ItemIds[11],
				PartHolster = ItemIds[12],
				PartSkin = ItemIds[13],
				BeretItem = ItemIds[14],
				AccessoryId = ItemIds[15],
				CharaRedId = Slot.Equipment.CharaRedId,
				CharaBlueId = Slot.Equipment.CharaBlueId,
				DinoItem = Slot.Equipment.DinoItem
			};
			int ıdStatics = ComDiv.GetIdStatics(ItemIds[5], 1);
			int ınt32 = ComDiv.GetIdStatics(ItemIds[5], 2);
			int ıdStatics1 = ComDiv.GetIdStatics(ItemIds[5], 5);
			if (ıdStatics == 6)
			{
				if (ınt32 != 1)
				{
					if (ıdStatics1 == 632)
					{
						goto Label2;
					}
					if (ınt32 == 2 || ıdStatics1 == 664)
					{
						playerEquipment.CharaBlueId = ItemIds[5];
						return playerEquipment;
					}
					else
					{
						return playerEquipment;
					}
				}
			Label2:
				playerEquipment.CharaRedId = ItemIds[5];
			}
			else if (ıdStatics == 15)
			{
				playerEquipment.DinoItem = ItemIds[5];
			}
			return playerEquipment;
		}

		public static void VotekickResult(RoomModel Room)
		{
			VoteKickModel voteKick = Room.VoteKick;
			if (voteKick != null)
			{
				int ınGamePlayers = voteKick.GetInGamePlayers();
				if (voteKick.Accept > voteKick.Denie && voteKick.Enemies > 0 && voteKick.Allies > 0 && voteKick.Votes.Count >= ınGamePlayers / 2)
				{
					Account playerBySlot = Room.GetPlayerBySlot(voteKick.VictimIdx);
					if (playerBySlot != null)
					{
						playerBySlot.SendPacket(new PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK());
						Room.KickedPlayersVote.Add(playerBySlot.PlayerId);
						Room.RemovePlayer(playerBySlot, true, 2);
					}
				}
				uint uInt32 = 0;
				if (voteKick.Allies == 0)
				{
					uInt32 = -2147479295;
				}
				else if (voteKick.Enemies == 0)
				{
					uInt32 = -2147479294;
				}
				else if (voteKick.Denie < voteKick.Accept || voteKick.Votes.Count < ınGamePlayers / 2)
				{
					uInt32 = -2147479296;
				}
				using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK pROTOCOLBATTLENOTIFYKICKVOTERESULTACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(uInt32, voteKick))
				{
					Room.SendPacketToPlayers(pROTOCOLBATTLENOTIFYKICKVOTERESULTACK, SlotState.BATTLE, 0);
				}
			}
			Room.VoteKick = null;
		}
	}
}