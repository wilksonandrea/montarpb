using System;
using System.Collections.Generic;
using System.Globalization;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000132 RID: 306
	public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ : GameClientPacket
	{
		// Token: 0x060002EC RID: 748 RVA: 0x00004E04 File Offset: 0x00003004
		public override void Read()
		{
			this.long_0 = (long)((ulong)base.ReadUD());
			base.ReadC();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001637C File Offset: 0x0001457C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel item = player.Inventory.GetItem(this.long_0);
					if (item != null)
					{
						this.int_0 = item.Id;
						this.long_1 = (long)((ulong)item.Count);
						if (item.Category == ItemCategory.Coupon && player.Inventory.Items.Count >= 500)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487785U, null, null));
							return;
						}
						if (this.int_0 == 1800049)
						{
							if (DaoManagerSQL.UpdatePlayerKD(player.PlayerId, 0, 0, player.Statistic.Season.HeadshotsCount, player.Statistic.Season.TotalKillsCount))
							{
								player.Statistic.Season.KillsCount = 0;
								player.Statistic.Season.DeathsCount = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (this.int_0 == 1800048)
						{
							if (DaoManagerSQL.UpdatePlayerMatches(0, 0, 0, 0, player.Statistic.Season.TotalMatchesCount, player.PlayerId))
							{
								player.Statistic.Season.Matches = 0;
								player.Statistic.Season.MatchWins = 0;
								player.Statistic.Season.MatchLoses = 0;
								player.Statistic.Season.MatchDraws = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (this.int_0 == 1800050)
						{
							if (ComDiv.UpdateDB("player_stat_seasons", "escapes_count", 0, "owner_id", player.PlayerId))
							{
								player.Statistic.Season.EscapesCount = 0;
								this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (this.int_0 == 1800053)
						{
							if (DaoManagerSQL.UpdateClanBattles(player.ClanId, 0, 0, 0))
							{
								ClanModel clan = ClanManager.GetClan(player.ClanId);
								if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
								{
									clan.Matches = 0;
									clan.MatchWins = 0;
									clan.MatchLoses = 0;
									this.Client.SendPacket(new PROTOCOL_CS_RECORD_RESET_RESULT_ACK());
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (this.int_0 == 1800055)
						{
							ClanModel clan2 = ClanManager.GetClan(player.ClanId);
							if (clan2.Id > 0 && clan2.OwnerId == this.Client.PlayerId)
							{
								if (clan2.MaxPlayers + 50 <= 250 && ComDiv.UpdateDB("system_clan", "max_players", clan2.MaxPlayers + 50, "id", player.ClanId))
								{
									clan2.MaxPlayers += 50;
									this.Client.SendPacket(new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(clan2.MaxPlayers));
								}
								else
								{
									this.uint_0 = 2147487830U;
								}
							}
							else
							{
								this.uint_0 = 2147487830U;
							}
						}
						else if (this.int_0 == 1800056)
						{
							ClanModel clan3 = ClanManager.GetClan(player.ClanId);
							if (clan3.Id > 0 && clan3.Points != 1000f)
							{
								if (ComDiv.UpdateDB("system_clan", "points", 1000f, "id", player.ClanId))
								{
									clan3.Points = 1000f;
									this.Client.SendPacket(new PROTOCOL_CS_POINT_RESET_RESULT_ACK());
								}
								else
								{
									this.uint_0 = 2147487830U;
								}
							}
							else
							{
								this.uint_0 = 2147487830U;
							}
						}
						else if (this.int_0 > 1800113 && this.int_0 < 1800119)
						{
							int num = ((this.int_0 == 1800114) ? 500 : ((this.int_0 == 1800115) ? 1000 : ((this.int_0 == 1800116) ? 5000 : ((this.int_0 == 1800117) ? 10000 : 30000))));
							if (ComDiv.UpdateDB("accounts", "gold", player.Gold + num, "player_id", player.PlayerId))
							{
								player.Gold += num;
								this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, player.Gold, 0));
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (this.int_0 == 1801145)
						{
							int num2 = 0;
							int num3 = new Random().Next(0, 9);
							switch (num3)
							{
							case 0:
								num2 = 1;
								break;
							case 1:
								num2 = 2;
								break;
							case 2:
								num2 = 3;
								break;
							case 3:
								num2 = 4;
								break;
							case 4:
								num2 = 5;
								break;
							case 5:
								num2 = 10;
								break;
							case 6:
								num2 = 15;
								break;
							case 7:
								num2 = 25;
								break;
							case 8:
								num2 = 30;
								break;
							case 9:
								num2 = 50;
								break;
							}
							if (num2 > 0)
							{
								if (DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags + num2))
								{
									player.Tags += num2;
									this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, player));
									this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(new ItemsModel(), this.int_0, num3));
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else if (item.Category == ItemCategory.Coupon && RandomBoxXML.ContainsBox(this.int_0))
						{
							RandomBoxModel box = RandomBoxXML.GetBox(this.int_0);
							if (box != null)
							{
								List<RandomBoxItem> sortedList = box.GetSortedList(this.method_0(1, 100));
								List<RandomBoxItem> rewardList = box.GetRewardList(sortedList, this.method_0(0, sortedList.Count));
								if (rewardList.Count > 0)
								{
									int index = rewardList[0].Index;
									List<ItemsModel> list = new List<ItemsModel>();
									foreach (RandomBoxItem randomBoxItem in rewardList)
									{
										GoodsItem good = ShopManager.GetGood(randomBoxItem.GoodsId);
										if (good != null)
										{
											list.Add(good.Item);
										}
										else
										{
											if (!DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold + index))
											{
												this.uint_0 = 2147483648U;
												break;
											}
											player.Gold += index;
											this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(index, player.Gold, 0));
										}
										if (randomBoxItem.Special)
										{
											using (PROTOCOL_AUTH_SHOP_JACKPOT_ACK protocol_AUTH_SHOP_JACKPOT_ACK = new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(player.Nickname, this.int_0, index))
											{
												GameXender.Client.SendPacketToAllClients(protocol_AUTH_SHOP_JACKPOT_ACK);
											}
										}
									}
									this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(list, this.int_0, index));
									if (list.Count > 0)
									{
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, list));
									}
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else
						{
							int idStatics = ComDiv.GetIdStatics(item.Id, 1);
							if ((idStatics < 1 || idStatics > 8) && idStatics != 15 && idStatics != 27 && (idStatics < 30 || idStatics > 35))
							{
								if (idStatics == 17)
								{
									this.method_1(player, item.Name);
								}
								else if (idStatics == 20)
								{
									this.method_2(player, item.Id);
								}
								else if (idStatics == 37)
								{
									this.method_3(player, item.Id);
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else if (item.Equip == ItemEquipType.Durable)
							{
								item.Equip = ItemEquipType.Temporary;
								item.Count = Convert.ToUInt32(DateTimeUtil.Now().AddSeconds(item.Count).ToString("yyMMddHHmm"));
								ComDiv.UpdateDB("player_items", "object_id", this.long_0, "owner_id", player.PlayerId, new string[] { "count", "equip" }, new object[]
								{
									(long)((ulong)item.Count),
									(int)item.Equip
								});
								if (idStatics == 6)
								{
									CharacterModel character = player.Character.GetCharacter(item.Id);
									if (character != null)
									{
										this.Client.SendPacket(new PROTOCOL_CHAR_CHANGE_STATE_ACK(character));
									}
								}
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(this.uint_0, item, player));
				}
			}
			catch (OverflowException ex)
			{
				CLogger.Print(string.Format("Obj: {0} ItemId: {1} Count: {2} PlayerId: {3} Name: '{4}' {5}", new object[]
				{
					this.long_0,
					this.int_0,
					this.long_1,
					this.Client.Player,
					this.Client.Player.Nickname,
					ex.Message
				}), LoggerType.Error, ex);
			}
			catch (Exception ex2)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ: " + ex2.Message, LoggerType.Error, ex2);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00016DC0 File Offset: 0x00014FC0
		private int method_0(int int_1, int int_2)
		{
			object obj = this.object_0;
			int num;
			lock (obj)
			{
				num = this.random_0.Next(int_1, int_2);
			}
			return num;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00016E0C File Offset: 0x0001500C
		private void method_1(Account account_0, string string_0)
		{
			int num = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(this.int_0, 3));
			int idStatics = ComDiv.GetIdStatics(this.int_0, 2);
			if (AllUtils.CheckDuplicateCouponEffects(account_0, num))
			{
				this.uint_0 = 2147483648U;
				return;
			}
			ItemsModel item = account_0.Inventory.GetItem(num);
			if (item == null)
			{
				bool flag = account_0.Bonus.AddBonuses(num);
				CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(num);
				if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && !account_0.Effects.HasFlag(couponEffect.EffectFlag))
				{
					account_0.Effects |= couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(account_0.PlayerId, account_0.Effects);
				}
				if (flag)
				{
					DaoManagerSQL.UpdatePlayerBonus(account_0.PlayerId, account_0.Bonus.Bonuses, account_0.Bonus.FreePass);
				}
				this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(num, string_0 + " [Active]", ItemEquipType.Temporary, Convert.ToUInt32(DateTimeUtil.Now().AddDays((double)idStatics).ToString("yyMMddHHmm")))));
				return;
			}
			item.Count = Convert.ToUInt32(DateTime.ParseExact(item.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture).AddDays((double)idStatics).ToString("yyMMddHHmm"));
			ComDiv.UpdateDB("player_items", "count", (long)((ulong)item.Count), "object_id", item.ObjectId, "owner_id", account_0.PlayerId);
			this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, account_0, item));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00016FC8 File Offset: 0x000151C8
		private void method_2(Account account_0, int int_1)
		{
			int num = ComDiv.GetIdStatics(int_1, 3) * 100;
			num += ComDiv.GetIdStatics(int_1, 2) * 100000;
			if (DaoManagerSQL.UpdateAccountGold(account_0.PlayerId, account_0.Gold + num))
			{
				account_0.Gold += num;
				this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, account_0.Gold, 0));
				return;
			}
			this.uint_0 = 2147483648U;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00017038 File Offset: 0x00015238
		private void method_3(Account account_0, int int_1)
		{
			PlayerBattlepass battlepass = account_0.Battlepass;
			if (battlepass == null)
			{
				this.uint_0 = 2147483648U;
				return;
			}
			int num = ComDiv.GetIdStatics(int_1, 3) * 10;
			num += ComDiv.GetIdStatics(int_1, 2) * 100000;
			battlepass.TotalPoints += num;
			if (ComDiv.UpdateDB("player_battlepass", "total_points", battlepass.TotalPoints, "owner_id", account_0.PlayerId))
			{
				account_0.UpdateSeasonpass = true;
				AllUtils.UpdateSeasonPass(account_0);
				return;
			}
			this.uint_0 = 2147483648U;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00004E1A File Offset: 0x0000301A
		public PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ()
		{
		}

		// Token: 0x04000223 RID: 547
		private long long_0;

		// Token: 0x04000224 RID: 548
		private int int_0;

		// Token: 0x04000225 RID: 549
		private long long_1;

		// Token: 0x04000226 RID: 550
		private uint uint_0 = 1U;

		// Token: 0x04000227 RID: 551
		private readonly Random random_0 = new Random();

		// Token: 0x04000228 RID: 552
		private readonly object object_0 = new object();
	}
}
