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

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ : GameClientPacket
{
	private long long_0;

	private int int_0;

	private long long_1;

	private uint uint_0 = 1u;

	private readonly Random random_0 = new Random();

	private readonly object object_0 = new object();

	public override void Read()
	{
		long_0 = ReadUD();
		ReadC();
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
			ItemsModel ıtem = player.Inventory.GetItem(long_0);
			if (ıtem != null)
			{
				int_0 = ıtem.Id;
				long_1 = ıtem.Count;
				if (ıtem.Category == ItemCategory.Coupon && player.Inventory.Items.Count >= 500)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487785u));
					return;
				}
				if (int_0 == 1800049)
				{
					if (DaoManagerSQL.UpdatePlayerKD(player.PlayerId, 0, 0, player.Statistic.Season.HeadshotsCount, player.Statistic.Season.TotalKillsCount))
					{
						player.Statistic.Season.KillsCount = 0;
						player.Statistic.Season.DeathsCount = 0;
						Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (int_0 == 1800048)
				{
					if (DaoManagerSQL.UpdatePlayerMatches(0, 0, 0, 0, player.Statistic.Season.TotalMatchesCount, player.PlayerId))
					{
						player.Statistic.Season.Matches = 0;
						player.Statistic.Season.MatchWins = 0;
						player.Statistic.Season.MatchLoses = 0;
						player.Statistic.Season.MatchDraws = 0;
						Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (int_0 == 1800050)
				{
					if (ComDiv.UpdateDB("player_stat_seasons", "escapes_count", 0, "owner_id", player.PlayerId))
					{
						player.Statistic.Season.EscapesCount = 0;
						Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (int_0 == 1800053)
				{
					if (DaoManagerSQL.UpdateClanBattles(player.ClanId, 0, 0, 0))
					{
						ClanModel clan = ClanManager.GetClan(player.ClanId);
						if (clan.Id > 0 && clan.OwnerId == Client.PlayerId)
						{
							clan.Matches = 0;
							clan.MatchWins = 0;
							clan.MatchLoses = 0;
							Client.SendPacket(new PROTOCOL_CS_RECORD_RESET_RESULT_ACK());
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (int_0 == 1800055)
				{
					ClanModel clan2 = ClanManager.GetClan(player.ClanId);
					if (clan2.Id > 0 && clan2.OwnerId == Client.PlayerId)
					{
						if (clan2.MaxPlayers + 50 <= 250 && ComDiv.UpdateDB("system_clan", "max_players", clan2.MaxPlayers + 50, "id", player.ClanId))
						{
							clan2.MaxPlayers += 50;
							Client.SendPacket(new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(clan2.MaxPlayers));
						}
						else
						{
							uint_0 = 2147487830u;
						}
					}
					else
					{
						uint_0 = 2147487830u;
					}
				}
				else if (int_0 == 1800056)
				{
					ClanModel clan3 = ClanManager.GetClan(player.ClanId);
					if (clan3.Id > 0 && clan3.Points != 1000f)
					{
						if (ComDiv.UpdateDB("system_clan", "points", 1000f, "id", player.ClanId))
						{
							clan3.Points = 1000f;
							Client.SendPacket(new PROTOCOL_CS_POINT_RESET_RESULT_ACK());
						}
						else
						{
							uint_0 = 2147487830u;
						}
					}
					else
					{
						uint_0 = 2147487830u;
					}
				}
				else if (int_0 > 1800113 && int_0 < 1800119)
				{
					int num = ((int_0 == 1800114) ? 500 : ((int_0 == 1800115) ? 1000 : ((int_0 == 1800116) ? 5000 : ((int_0 == 1800117) ? 10000 : 30000))));
					if (ComDiv.UpdateDB("accounts", "gold", player.Gold + num, "player_id", player.PlayerId))
					{
						player.Gold += num;
						Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, player.Gold, 0));
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (int_0 == 1801145)
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
							Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, player));
							Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(new ItemsModel(), int_0, num3));
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else if (ıtem.Category == ItemCategory.Coupon && RandomBoxXML.ContainsBox(int_0))
				{
					RandomBoxModel box = RandomBoxXML.GetBox(int_0);
					if (box != null)
					{
						List<RandomBoxItem> sortedList = box.GetSortedList(method_0(1, 100));
						List<RandomBoxItem> rewardList = box.GetRewardList(sortedList, method_0(0, sortedList.Count));
						if (rewardList.Count > 0)
						{
							int ındex = rewardList[0].Index;
							List<ItemsModel> list = new List<ItemsModel>();
							foreach (RandomBoxItem item in rewardList)
							{
								GoodsItem good = ShopManager.GetGood(item.GoodsId);
								if (good != null)
								{
									list.Add(good.Item);
								}
								else
								{
									if (!DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold + ındex))
									{
										uint_0 = 2147483648u;
										break;
									}
									player.Gold += ındex;
									Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(ındex, player.Gold, 0));
								}
								if (item.Special)
								{
									using PROTOCOL_AUTH_SHOP_JACKPOT_ACK packet = new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(player.Nickname, int_0, ındex);
									GameXender.Client.SendPacketToAllClients(packet);
								}
							}
							Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(list, int_0, ındex));
							if (list.Count > 0)
							{
								Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, list));
							}
						}
						else
						{
							uint_0 = 2147483648u;
						}
						sortedList = null;
						rewardList = null;
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
				else
				{
					int ıdStatics = ComDiv.GetIdStatics(ıtem.Id, 1);
					if ((ıdStatics < 1 || ıdStatics > 8) && ıdStatics != 15 && ıdStatics != 27 && (ıdStatics < 30 || ıdStatics > 35))
					{
						switch (ıdStatics)
						{
						case 17:
							method_1(player, ıtem.Name);
							break;
						case 20:
							method_2(player, ıtem.Id);
							break;
						case 37:
							method_3(player, ıtem.Id);
							break;
						default:
							uint_0 = 2147483648u;
							break;
						}
					}
					else if (ıtem.Equip == ItemEquipType.Durable)
					{
						ıtem.Equip = ItemEquipType.Temporary;
						ıtem.Count = Convert.ToUInt32(DateTimeUtil.Now().AddSeconds(ıtem.Count).ToString("yyMMddHHmm"));
						ComDiv.UpdateDB("player_items", "object_id", long_0, "owner_id", player.PlayerId, new string[2] { "count", "equip" }, (long)ıtem.Count, (int)ıtem.Equip);
						if (ıdStatics == 6)
						{
							CharacterModel character = player.Character.GetCharacter(ıtem.Id);
							if (character != null)
							{
								Client.SendPacket(new PROTOCOL_CHAR_CHANGE_STATE_ACK(character));
							}
						}
					}
					else
					{
						uint_0 = 2147483648u;
					}
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint_0, ıtem, player));
		}
		catch (OverflowException ex)
		{
			CLogger.Print($"Obj: {long_0} ItemId: {int_0} Count: {long_1} PlayerId: {Client.Player} Name: '{Client.Player.Nickname}' {ex.Message}", LoggerType.Error, ex);
		}
		catch (Exception ex2)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ: " + ex2.Message, LoggerType.Error, ex2);
		}
	}

	private int method_0(int int_1, int int_2)
	{
		lock (object_0)
		{
			return random_0.Next(int_1, int_2);
		}
	}

	private void method_1(Account account_0, string string_0)
	{
		int num = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(int_0, 3));
		int ıdStatics = ComDiv.GetIdStatics(int_0, 2);
		if (AllUtils.CheckDuplicateCouponEffects(account_0, num))
		{
			uint_0 = 2147483648u;
			return;
		}
		ItemsModel ıtem = account_0.Inventory.GetItem(num);
		if (ıtem == null)
		{
			bool num2 = account_0.Bonus.AddBonuses(num);
			CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(num);
			if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && !account_0.Effects.HasFlag(couponEffect.EffectFlag))
			{
				account_0.Effects |= couponEffect.EffectFlag;
				DaoManagerSQL.UpdateCouponEffect(account_0.PlayerId, account_0.Effects);
			}
			if (num2)
			{
				DaoManagerSQL.UpdatePlayerBonus(account_0.PlayerId, account_0.Bonus.Bonuses, account_0.Bonus.FreePass);
			}
			Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(num, string_0 + " [Active]", ItemEquipType.Temporary, Convert.ToUInt32(DateTimeUtil.Now().AddDays(ıdStatics).ToString("yyMMddHHmm")))));
		}
		else
		{
			ıtem.Count = Convert.ToUInt32(DateTime.ParseExact(ıtem.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture).AddDays(ıdStatics).ToString("yyMMddHHmm"));
			ComDiv.UpdateDB("player_items", "count", (long)ıtem.Count, "object_id", ıtem.ObjectId, "owner_id", account_0.PlayerId);
			Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, account_0, ıtem));
		}
	}

	private void method_2(Account account_0, int int_1)
	{
		int num = ComDiv.GetIdStatics(int_1, 3) * 100;
		num += ComDiv.GetIdStatics(int_1, 2) * 100000;
		if (DaoManagerSQL.UpdateAccountGold(account_0.PlayerId, account_0.Gold + num))
		{
			account_0.Gold += num;
			Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, account_0.Gold, 0));
		}
		else
		{
			uint_0 = 2147483648u;
		}
	}

	private void method_3(Account account_0, int int_1)
	{
		PlayerBattlepass battlepass = account_0.Battlepass;
		if (battlepass != null)
		{
			int num = ComDiv.GetIdStatics(int_1, 3) * 10;
			num += ComDiv.GetIdStatics(int_1, 2) * 100000;
			battlepass.TotalPoints += num;
			if (ComDiv.UpdateDB("player_battlepass", "total_points", battlepass.TotalPoints, "owner_id", account_0.PlayerId))
			{
				account_0.UpdateSeasonpass = true;
				AllUtils.UpdateSeasonPass(account_0);
			}
			else
			{
				uint_0 = 2147483648u;
			}
		}
		else
		{
			uint_0 = 2147483648u;
		}
	}
}
