using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ : GameClientPacket
	{
		private long long_0;

		private int int_0;

		private long long_1;

		private uint uint_0 = 1;

		private readonly Random random_0 = new Random();

		private readonly object object_0 = new object();

		public PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ()
		{
		}

		private int method_0(int int_1, int int_2)
		{
			int ınt32;
			lock (this.object_0)
			{
				ınt32 = this.random_0.Next(int_1, int_2);
			}
			return ınt32;
		}

		private void method_1(Account account_0, string string_0)
		{
			DateTime dateTime;
			int ınt32 = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(this.int_0, 3));
			int ıdStatics = ComDiv.GetIdStatics(this.int_0, 2);
			if (AllUtils.CheckDuplicateCouponEffects(account_0, ınt32))
			{
				this.uint_0 = -2147483648;
				return;
			}
			ItemsModel ıtem = account_0.Inventory.GetItem(ınt32);
			if (ıtem != null)
			{
				uint count = ıtem.Count;
				DateTime dateTime1 = DateTime.ParseExact(count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
				dateTime = dateTime1.AddDays((double)ıdStatics);
				ıtem.Count = Convert.ToUInt32(dateTime.ToString("yyMMddHHmm"));
				ComDiv.UpdateDB("player_items", "count", (long)((ulong)ıtem.Count), "object_id", ıtem.ObjectId, "owner_id", account_0.PlayerId);
				this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, account_0, ıtem));
				return;
			}
			bool flag = account_0.Bonus.AddBonuses(ınt32);
			CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ınt32);
			if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && !account_0.Effects.HasFlag(couponEffect.EffectFlag))
			{
				account_0.Effects |= couponEffect.EffectFlag;
				DaoManagerSQL.UpdateCouponEffect(account_0.PlayerId, account_0.Effects);
			}
			if (flag)
			{
				DaoManagerSQL.UpdatePlayerBonus(account_0.PlayerId, account_0.Bonus.Bonuses, account_0.Bonus.FreePass);
			}
			GameClient client = this.Client;
			string str = string.Concat(string_0, " [Active]");
			dateTime = DateTimeUtil.Now();
			dateTime = dateTime.AddDays((double)ıdStatics);
			client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(ınt32, str, ItemEquipType.Temporary, Convert.ToUInt32(dateTime.ToString("yyMMddHHmm")))));
		}

		private void method_2(Account account_0, int int_1)
		{
			int ıdStatics = ComDiv.GetIdStatics(int_1, 3) * 100;
			ıdStatics = ıdStatics + ComDiv.GetIdStatics(int_1, 2) * 100000;
			if (!DaoManagerSQL.UpdateAccountGold(account_0.PlayerId, account_0.Gold + ıdStatics))
			{
				this.uint_0 = -2147483648;
				return;
			}
			account_0.Gold += ıdStatics;
			this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(ıdStatics, account_0.Gold, 0));
		}

		private void method_3(Account account_0, int int_1)
		{
			PlayerBattlepass battlepass = account_0.Battlepass;
			if (battlepass == null)
			{
				this.uint_0 = -2147483648;
				return;
			}
			int ıdStatics = ComDiv.GetIdStatics(int_1, 3) * 10;
			ıdStatics = ıdStatics + ComDiv.GetIdStatics(int_1, 2) * 100000;
			PlayerBattlepass totalPoints = battlepass;
			totalPoints.TotalPoints = totalPoints.TotalPoints + ıdStatics;
			if (!ComDiv.UpdateDB("player_battlepass", "total_points", battlepass.TotalPoints, "owner_id", account_0.PlayerId))
			{
				this.uint_0 = -2147483648;
				return;
			}
			account_0.UpdateSeasonpass = true;
			AllUtils.UpdateSeasonPass(account_0);
		}

		public override void Read()
		{
			this.long_0 = (long)base.ReadUD();
			base.ReadC();
		}

		public override void Run()
		{
			int ınt32;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel ıtem = player.Inventory.GetItem(this.long_0);
					if (ıtem == null)
					{
						this.uint_0 = -2147483648;
					}
					else
					{
						this.int_0 = ıtem.Id;
						this.long_1 = (long)ıtem.Count;
						if (ıtem.Category == ItemCategory.Coupon && player.Inventory.Items.Count >= 500)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(-2147479511, null, null));
							return;
						}
						else if (this.int_0 == 1800049)
						{
							if (!DaoManagerSQL.UpdatePlayerKD(player.PlayerId, 0, 0, player.Statistic.Season.HeadshotsCount, player.Statistic.Season.TotalKillsCount))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								player.Statistic.Season.KillsCount = 0;
								player.Statistic.Season.DeathsCount = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
						}
						else if (this.int_0 == 1800048)
						{
							if (!DaoManagerSQL.UpdatePlayerMatches(0, 0, 0, 0, player.Statistic.Season.TotalMatchesCount, player.PlayerId))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								player.Statistic.Season.Matches = 0;
								player.Statistic.Season.MatchWins = 0;
								player.Statistic.Season.MatchLoses = 0;
								player.Statistic.Season.MatchDraws = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
						}
						else if (this.int_0 == 1800050)
						{
							if (!ComDiv.UpdateDB("player_stat_seasons", "escapes_count", 0, "owner_id", player.PlayerId))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								player.Statistic.Season.EscapesCount = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
						}
						else if (this.int_0 == 1800053)
						{
							if (!DaoManagerSQL.UpdateClanBattles(player.ClanId, 0, 0, 0))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								ClanModel clan = ClanManager.GetClan(player.ClanId);
								if (clan.Id <= 0 || clan.OwnerId != this.Client.PlayerId)
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									clan.Matches = 0;
									clan.MatchWins = 0;
									clan.MatchLoses = 0;
									this.Client.SendPacket(new PROTOCOL_CS_RECORD_RESET_RESULT_ACK());
								}
							}
						}
						else if (this.int_0 == 1800055)
						{
							ClanModel clanModel = ClanManager.GetClan(player.ClanId);
							if (clanModel.Id <= 0 || clanModel.OwnerId != this.Client.PlayerId)
							{
								this.uint_0 = -2147479466;
							}
							else if (clanModel.MaxPlayers + 50 > 250 || !ComDiv.UpdateDB("system_clan", "max_players", clanModel.MaxPlayers + 50, "id", player.ClanId))
							{
								this.uint_0 = -2147479466;
							}
							else
							{
								clanModel.MaxPlayers += 50;
								this.Client.SendPacket(new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(clanModel.MaxPlayers));
							}
						}
						else if (this.int_0 == 1800056)
						{
							ClanModel clan1 = ClanManager.GetClan(player.ClanId);
							if (clan1.Id <= 0 || clan1.Points == 1000f)
							{
								this.uint_0 = -2147479466;
							}
							else if (!ComDiv.UpdateDB("system_clan", "points", 1000f, "id", player.ClanId))
							{
								this.uint_0 = -2147479466;
							}
							else
							{
								clan1.Points = 1000f;
								this.Client.SendPacket(new PROTOCOL_CS_POINT_RESET_RESULT_ACK());
							}
						}
						else if (this.int_0 > 1800113 && this.int_0 < 1800119)
						{
							if (this.int_0 == 1800114)
							{
								ınt32 = 500;
							}
							else if (this.int_0 == 1800115)
							{
								ınt32 = 1000;
							}
							else if (this.int_0 == 1800116)
							{
								ınt32 = 5000;
							}
							else
							{
								ınt32 = (this.int_0 == 1800117 ? 10000 : 30000);
							}
							int ınt321 = ınt32;
							if (!ComDiv.UpdateDB("accounts", "gold", player.Gold + ınt321, "player_id", player.PlayerId))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								player.Gold += ınt321;
								this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(ınt321, player.Gold, 0));
							}
						}
						else if (this.int_0 == 1801145)
						{
							int ınt322 = 0;
							int ınt323 = (new Random()).Next(0, 9);
							switch (ınt323)
							{
								case 0:
								{
									ınt322 = 1;
									break;
								}
								case 1:
								{
									ınt322 = 2;
									break;
								}
								case 2:
								{
									ınt322 = 3;
									break;
								}
								case 3:
								{
									ınt322 = 4;
									break;
								}
								case 4:
								{
									ınt322 = 5;
									break;
								}
								case 5:
								{
									ınt322 = 10;
									break;
								}
								case 6:
								{
									ınt322 = 15;
									break;
								}
								case 7:
								{
									ınt322 = 25;
									break;
								}
								case 8:
								{
									ınt322 = 30;
									break;
								}
								case 9:
								{
									ınt322 = 50;
									break;
								}
							}
							if (ınt322 <= 0)
							{
								this.uint_0 = -2147483648;
							}
							else if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags + ınt322))
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								player.Tags += ınt322;
								this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(new ItemsModel(), this.int_0, ınt323));
							}
						}
						else if (ıtem.Category != ItemCategory.Coupon || !RandomBoxXML.ContainsBox(this.int_0))
						{
							int ıdStatics = ComDiv.GetIdStatics(ıtem.Id, 1);
							if (ıdStatics >= 1 && ıdStatics <= 8 || ıdStatics == 15 || ıdStatics == 27 || ıdStatics >= 30 && ıdStatics <= 35)
							{
								if (ıtem.Equip != ItemEquipType.Durable)
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									ıtem.Equip = ItemEquipType.Temporary;
									DateTime dateTime = DateTimeUtil.Now();
									dateTime = dateTime.AddSeconds((double)((float)ıtem.Count));
									ıtem.Count = Convert.ToUInt32(dateTime.ToString("yyMMddHHmm"));
									ComDiv.UpdateDB("player_items", "object_id", this.long_0, "owner_id", player.PlayerId, new string[] { "count", "equip" }, new object[] { (long)((ulong)ıtem.Count), (int)ıtem.Equip });
									if (ıdStatics == 6)
									{
										CharacterModel character = player.Character.GetCharacter(ıtem.Id);
										if (character != null)
										{
											this.Client.SendPacket(new PROTOCOL_CHAR_CHANGE_STATE_ACK(character));
										}
									}
								}
							}
							else if (ıdStatics == 17)
							{
								this.method_1(player, ıtem.Name);
							}
							else if (ıdStatics == 20)
							{
								this.method_2(player, ıtem.Id);
							}
							else if (ıdStatics != 37)
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								this.method_3(player, ıtem.Id);
							}
						}
						else
						{
							RandomBoxModel box = RandomBoxXML.GetBox(this.int_0);
							if (box == null)
							{
								this.uint_0 = -2147483648;
							}
							else
							{
								List<RandomBoxItem> sortedList = box.GetSortedList(this.method_0(1, 100));
								List<RandomBoxItem> rewardList = box.GetRewardList(sortedList, this.method_0(0, sortedList.Count));
								if (rewardList.Count <= 0)
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									int ındex = rewardList[0].Index;
									List<ItemsModel> ıtemsModels = new List<ItemsModel>();
									List<RandomBoxItem>.Enumerator enumerator = rewardList.GetEnumerator();
									try
									{
										while (true)
										{
											if (enumerator.MoveNext())
											{
												RandomBoxItem current = enumerator.Current;
												GoodsItem good = ShopManager.GetGood(current.GoodsId);
												if (good != null)
												{
													ıtemsModels.Add(good.Item);
												}
												else if (!DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold + ındex))
												{
													this.uint_0 = -2147483648;
													break;
												}
												else
												{
													player.Gold += ındex;
													this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(ındex, player.Gold, 0));
												}
												if (current.Special)
												{
													using (PROTOCOL_AUTH_SHOP_JACKPOT_ACK pROTOCOLAUTHSHOPJACKPOTACK = new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(player.Nickname, this.int_0, ındex))
													{
														GameXender.Client.SendPacketToAllClients(pROTOCOLAUTHSHOPJACKPOTACK);
													}
												}
											}
											else
											{
												break;
											}
										}
									}
									finally
									{
										((IDisposable)enumerator).Dispose();
									}
									this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(ıtemsModels, this.int_0, ındex));
									if (ıtemsModels.Count > 0)
									{
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, ıtemsModels));
									}
								}
								sortedList = null;
								rewardList = null;
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(this.uint_0, ıtem, player));
				}
			}
			catch (OverflowException overflowException1)
			{
				OverflowException overflowException = overflowException1;
				CLogger.Print(string.Format("Obj: {0} ItemId: {1} Count: {2} PlayerId: {3} Name: '{4}' {5}", new object[] { this.long_0, this.int_0, this.long_1, this.Client.Player, this.Client.Player.Nickname, overflowException.Message }), LoggerType.Error, overflowException);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}