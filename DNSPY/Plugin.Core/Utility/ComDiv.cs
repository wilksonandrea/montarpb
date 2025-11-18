using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;

namespace Plugin.Core.Utility
{
	// Token: 0x0200002D RID: 45
	public static class ComDiv
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0001651C File Offset: 0x0001471C
		public static int CheckEquipedItems(PlayerEquipment Equip, List<ItemsModel> Inventory, bool BattleRules)
		{
			int num = 0;
			ValueTuple<bool, bool, bool, bool, bool> item = new ValueTuple<bool, bool, bool, bool, bool>(false, false, false, false, false);
			ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> item2 = new ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>(false, false, false, false, false, false, false, new ValueTuple<bool, bool, bool, bool, bool>(false, false, false, false, false));
			ValueTuple<bool, bool, bool> item3 = new ValueTuple<bool, bool, bool>(false, false, false);
			if (Equip.BeretItem == 0)
			{
				item2.Rest.Item4 = true;
			}
			if (Equip.AccessoryId == 0)
			{
				item3.Item1 = true;
			}
			if (Equip.SprayId == 0)
			{
				item3.Item2 = true;
			}
			if (Equip.NameCardId == 0)
			{
				item3.Item3 = true;
			}
			if (Equip.WeaponPrimary == 103004)
			{
				item.Item1 = true;
			}
			if (BattleRules)
			{
				if (!item.Item1 && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
				{
					item.Item1 = true;
				}
				if (!item.Item3 && Equip.WeaponMelee == 323001)
				{
					item.Item3 = true;
				}
			}
			ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>> valueTuple = ComDiv.smethod_0(Equip, Inventory, item, item2, item3);
			item = valueTuple.Item1;
			item2 = valueTuple.Item2;
			item3 = valueTuple.Item3;
			bool flag = !item.Item1 || !item.Item2 || !item.Item3 || !item.Item4 || !item.Item5;
			bool flag2 = !item2.Item1 || !item2.Item2 || !item2.Item3 || !item2.Item4 || !item2.Item5 || !item2.Item6 || !item2.Item7 || !item2.Rest.Item1 || !item2.Rest.Item2 || !item2.Rest.Item3 || !item2.Rest.Item4 || !item2.Rest.Item5;
			bool flag3 = !item3.Item1 || !item3.Item2 || !item3.Item3;
			if (flag)
			{
				num += 2;
			}
			if (flag2)
			{
				num++;
			}
			if (flag3)
			{
				num += 3;
			}
			ComDiv.smethod_1(Equip, ref item, ref item2, ref item3);
			return num;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00016708 File Offset: 0x00014908
		private static ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>> smethod_0(PlayerEquipment playerEquipment_0, List<ItemsModel> list_0, ValueTuple<bool, bool, bool, bool, bool> valueTuple_0, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> valueTuple_1, ValueTuple<bool, bool, bool> valueTuple_2)
		{
			lock (list_0)
			{
				HashSet<int> hashSet = new HashSet<int>(list_0.Where(new Func<ItemsModel, bool>(ComDiv.Class5.<>9.method_0)).Select(new Func<ItemsModel, int>(ComDiv.Class5.<>9.method_1)));
				if (hashSet.Contains(playerEquipment_0.WeaponPrimary))
				{
					valueTuple_0.Item1 = true;
				}
				if (hashSet.Contains(playerEquipment_0.WeaponSecondary))
				{
					valueTuple_0.Item2 = true;
				}
				if (hashSet.Contains(playerEquipment_0.WeaponMelee))
				{
					valueTuple_0.Item3 = true;
				}
				if (hashSet.Contains(playerEquipment_0.WeaponExplosive))
				{
					valueTuple_0.Item4 = true;
				}
				if (hashSet.Contains(playerEquipment_0.WeaponSpecial))
				{
					valueTuple_0.Item5 = true;
				}
				if (hashSet.Contains(playerEquipment_0.CharaRedId))
				{
					valueTuple_1.Item1 = true;
				}
				if (hashSet.Contains(playerEquipment_0.CharaBlueId))
				{
					valueTuple_1.Item2 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartHead))
				{
					valueTuple_1.Item3 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartFace))
				{
					valueTuple_1.Item4 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartJacket))
				{
					valueTuple_1.Item5 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartPocket))
				{
					valueTuple_1.Item6 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartGlove))
				{
					valueTuple_1.Item7 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartBelt))
				{
					valueTuple_1.Rest.Item1 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartHolster))
				{
					valueTuple_1.Rest.Item2 = true;
				}
				if (hashSet.Contains(playerEquipment_0.PartSkin))
				{
					valueTuple_1.Rest.Item3 = true;
				}
				if (playerEquipment_0.BeretItem != 0 && hashSet.Contains(playerEquipment_0.BeretItem))
				{
					valueTuple_1.Rest.Item4 = true;
				}
				if (hashSet.Contains(playerEquipment_0.DinoItem))
				{
					valueTuple_1.Rest.Item5 = true;
				}
				if (playerEquipment_0.AccessoryId != 0 && hashSet.Contains(playerEquipment_0.AccessoryId))
				{
					valueTuple_2.Item1 = true;
				}
				if (playerEquipment_0.SprayId != 0 && hashSet.Contains(playerEquipment_0.SprayId))
				{
					valueTuple_2.Item2 = true;
				}
				if (playerEquipment_0.NameCardId != 0 && hashSet.Contains(playerEquipment_0.NameCardId))
				{
					valueTuple_2.Item3 = true;
				}
			}
			return new ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>>(valueTuple_0, valueTuple_1, valueTuple_2);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00016994 File Offset: 0x00014B94
		private static void smethod_1(PlayerEquipment playerEquipment_0, ref ValueTuple<bool, bool, bool, bool, bool> valueTuple_0, ref ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> valueTuple_1, ref ValueTuple<bool, bool, bool> valueTuple_2)
		{
			if (!valueTuple_0.Item1)
			{
				playerEquipment_0.WeaponPrimary = 103004;
			}
			if (!valueTuple_0.Item2)
			{
				playerEquipment_0.WeaponSecondary = 202003;
			}
			if (!valueTuple_0.Item3)
			{
				playerEquipment_0.WeaponMelee = 301001;
			}
			if (!valueTuple_0.Item4)
			{
				playerEquipment_0.WeaponExplosive = 407001;
			}
			if (!valueTuple_0.Item5)
			{
				playerEquipment_0.WeaponSpecial = 508001;
			}
			if (!valueTuple_1.Item1)
			{
				playerEquipment_0.CharaRedId = 601001;
			}
			if (!valueTuple_1.Item2)
			{
				playerEquipment_0.CharaBlueId = 602002;
			}
			if (!valueTuple_1.Item3)
			{
				playerEquipment_0.PartHead = 1000700000;
			}
			if (!valueTuple_1.Item4)
			{
				playerEquipment_0.PartFace = 1000800000;
			}
			if (!valueTuple_1.Item5)
			{
				playerEquipment_0.PartJacket = 1000900000;
			}
			if (!valueTuple_1.Item6)
			{
				playerEquipment_0.PartPocket = 1001000000;
			}
			if (!valueTuple_1.Item7)
			{
				playerEquipment_0.PartGlove = 1001100000;
			}
			if (!valueTuple_1.Rest.Item1)
			{
				playerEquipment_0.PartBelt = 1001200000;
			}
			if (!valueTuple_1.Rest.Item2)
			{
				playerEquipment_0.PartHolster = 1001300000;
			}
			if (!valueTuple_1.Rest.Item3)
			{
				playerEquipment_0.PartSkin = 1001400000;
			}
			if (!valueTuple_1.Rest.Item4)
			{
				playerEquipment_0.BeretItem = 0;
			}
			if (!valueTuple_1.Rest.Item5)
			{
				playerEquipment_0.DinoItem = 1500511;
			}
			if (!valueTuple_2.Item1)
			{
				playerEquipment_0.AccessoryId = 0;
			}
			if (!valueTuple_2.Item2)
			{
				playerEquipment_0.SprayId = 0;
			}
			if (!valueTuple_2.Item3)
			{
				playerEquipment_0.NameCardId = 0;
			}
			if (playerEquipment_0.PartHead == 1000700000 && playerEquipment_0.PartFace != 1000800000)
			{
				playerEquipment_0.PartHead = 0;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00016B48 File Offset: 0x00014D48
		public static void UpdateWeapons(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("weapon_primary", Equip.WeaponPrimary);
			Query.AddQuery("weapon_secondary", Equip.WeaponSecondary);
			Query.AddQuery("weapon_melee", Equip.WeaponMelee);
			Query.AddQuery("weapon_explosive", Equip.WeaponExplosive);
			Query.AddQuery("weapon_special", Equip.WeaponSpecial);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00016BC4 File Offset: 0x00014DC4
		public static void UpdateChars(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("chara_red_side", Equip.CharaRedId);
			Query.AddQuery("chara_blue_side", Equip.CharaBlueId);
			Query.AddQuery("part_head", Equip.PartHead);
			Query.AddQuery("part_face", Equip.PartFace);
			Query.AddQuery("part_jacket", Equip.PartJacket);
			Query.AddQuery("part_pocket", Equip.PartPocket);
			Query.AddQuery("part_glove", Equip.PartGlove);
			Query.AddQuery("part_belt", Equip.PartBelt);
			Query.AddQuery("part_holster", Equip.PartHolster);
			Query.AddQuery("part_skin", Equip.PartSkin);
			Query.AddQuery("beret_item_part", Equip.BeretItem);
			Query.AddQuery("dino_item_chara", Equip.DinoItem);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00016CDC File Offset: 0x00014EDC
		public static void UpdateItems(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("accesory_id", Equip.AccessoryId);
			Query.AddQuery("spray_id", Equip.SprayId);
			Query.AddQuery("namecard_id", Equip.NameCardId);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00016D2C File Offset: 0x00014F2C
		public static void TryCreateItem(ItemsModel Model, PlayerInventory Inventory, long OwnerId)
		{
			try
			{
				ItemsModel item = Inventory.GetItem(Model.Id);
				if (item == null)
				{
					if (DaoManagerSQL.CreatePlayerInventoryItem(Model, OwnerId))
					{
						Inventory.AddItem(Model);
					}
				}
				else
				{
					Model.ObjectId = item.ObjectId;
					if (item.Equip == ItemEquipType.Durable)
					{
						if (ShopManager.IsRepairableItem(Model.Id))
						{
							Model.Count = 100U;
							ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
						}
						else
						{
							Model.Count += item.Count;
							ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
						}
					}
					else if (item.Equip == ItemEquipType.Temporary)
					{
						DateTime dateTime = DateTime.ParseExact(item.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
						if (Model.Category != ItemCategory.Coupon)
						{
							Model.Equip = ItemEquipType.Temporary;
							Model.Count = Convert.ToUInt32(dateTime.AddSeconds(Model.Count).ToString("yyMMddHHmm"));
						}
						else
						{
							TimeSpan timeSpan = DateTime.ParseExact(Model.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture) - DateTimeUtil.Now();
							Model.Equip = ItemEquipType.Temporary;
							Model.Count = Convert.ToUInt32(dateTime.AddDays(timeSpan.TotalDays).ToString("yyMMddHHmm"));
						}
						ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
					}
					item.Equip = Model.Equip;
					item.Count = Model.Count;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00016F54 File Offset: 0x00015154
		public static ItemCategory GetItemCategory(int ItemId)
		{
			int idStatics = ComDiv.GetIdStatics(ItemId, 1);
			int idStatics2 = ComDiv.GetIdStatics(ItemId, 4);
			if (idStatics >= 1 && idStatics <= 5)
			{
				return ItemCategory.Weapon;
			}
			if ((idStatics >= 6 && idStatics <= 14) || idStatics == 27 || (idStatics2 >= 7 && idStatics2 <= 14))
			{
				return ItemCategory.Character;
			}
			if ((idStatics >= 16 && idStatics <= 20) || idStatics == 22 || idStatics == 26 || (idStatics >= 28 && idStatics <= 29) || (idStatics >= 36 && idStatics <= 40))
			{
				return ItemCategory.Coupon;
			}
			if (idStatics != 15 && (idStatics < 30 || idStatics > 35))
			{
				CLogger.Print(string.Format("Invalid Category [{0}]: {1}", idStatics, ItemId), LoggerType.Warning, null);
				return ItemCategory.None;
			}
			return ItemCategory.NewItem;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00016FF0 File Offset: 0x000151F0
		public static uint ValidateStockId(int ItemId)
		{
			int idStatics = ComDiv.GetIdStatics(ItemId, 4);
			return ComDiv.GenStockId((idStatics < 7 || idStatics > 14) ? ItemId : idStatics);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00017018 File Offset: 0x00015218
		public static int GetIdStatics(int Id, int Type)
		{
			switch (Type)
			{
			case 1:
				return Id / 100000;
			case 2:
				return Id % 100000 / 1000;
			case 3:
				return Id % 1000;
			case 4:
				return Id % 10000000 / 100000;
			case 5:
				return Id / 1000;
			default:
				return 0;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00017078 File Offset: 0x00015278
		public static double GetDuration(DateTime Date)
		{
			return (DateTimeUtil.Now() - Date).TotalSeconds;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00002A47 File Offset: 0x00000C47
		public static byte[] AddressBytes(string Host)
		{
			return IPAddress.Parse(Host).GetAddressBytes();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00002A54 File Offset: 0x00000C54
		public static int CreateItemId(int ItemClass, int ClassType, int Number)
		{
			return ItemClass * 100000 + ClassType * 1000 + Number;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00002A67 File Offset: 0x00000C67
		public static int Percentage(int Total, int Percent)
		{
			return Total * Percent / 100;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00002A6F File Offset: 0x00000C6F
		public static float Percentage(float Total, int Percent)
		{
			return Total * (float)Percent / 100f;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0001709C File Offset: 0x0001529C
		public static char[] SubArray(this char[] Input, int StartIndex, int Length)
		{
			List<char> list = new List<char>();
			for (int i = StartIndex; i < Length; i++)
			{
				list.Add(Input[i]);
			}
			return list.ToArray();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000170CC File Offset: 0x000152CC
		public static bool UpdateDB(string TABEL, string[] COLUMNS, params object[] VALUES)
		{
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
			{
				CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length != 0 && VALUES.Length != 0)
			{
				bool flag;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
						{
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							List<string> list = new List<string>();
							for (int i = 0; i < VALUES.Length; i++)
							{
								object obj = VALUES[i];
								string text = COLUMNS[i];
								string text2 = "@Value" + i.ToString();
								npgsqlCommand.Parameters.AddWithValue(text2, obj);
								list.Add(text + "=" + text2);
							}
							string text3 = string.Join(",", list.ToArray());
							npgsqlCommand.CommandText = "UPDATE " + TABEL + " SET " + text3;
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Close();
						}
					}
					flag = true;
				}
				catch (Exception ex)
				{
					CLogger.Print("[AllUtils.UpdateDB1] " + ex.Message, LoggerType.Error, ex);
					flag = false;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0001724C File Offset: 0x0001544C
		public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string[] COLUMNS, params object[] VALUES)
		{
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
			{
				CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length != 0 && VALUES.Length != 0)
			{
				bool flag;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
						{
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							List<string> list = new List<string>();
							for (int i = 0; i < VALUES.Length; i++)
							{
								object obj = VALUES[i];
								string text = COLUMNS[i];
								string text2 = "@Value" + i.ToString();
								npgsqlCommand.Parameters.AddWithValue(text2, obj);
								list.Add(text + "=" + text2);
							}
							string text3 = string.Join(",", list.ToArray());
							npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req1, "=@Req1" });
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Close();
						}
					}
					flag = true;
				}
				catch (Exception ex)
				{
					CLogger.Print("[AllUtils.UpdateDB2] " + ex.Message, LoggerType.Error, ex);
					flag = false;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0001742C File Offset: 0x0001562C
		public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.Parameters.AddWithValue("@Value", VALUE);
						npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
						npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB3] " + ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00017530 File Offset: 0x00015730
		public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string Req2, object valueReq2, string[] COLUMNS, params object[] VALUES)
		{
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
			{
				CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length != 0 && VALUES.Length != 0)
			{
				bool flag;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
						{
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							List<string> list = new List<string>();
							for (int i = 0; i < VALUES.Length; i++)
							{
								object obj = VALUES[i];
								string text = COLUMNS[i];
								string text2 = "@Value" + i.ToString();
								npgsqlCommand.Parameters.AddWithValue(text2, obj);
								list.Add(text + "=" + text2);
							}
							string text3 = string.Join(",", list.ToArray());
							if (Req1 != null)
							{
								npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
							}
							if (Req2 != null)
							{
								npgsqlCommand.Parameters.AddWithValue("@Req2", valueReq2);
							}
							if (Req1 != null && Req2 == null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req1, "=@Req1" });
							}
							else if (Req2 != null && Req1 == null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req2, "=@Req2" });
							}
							else if (Req2 != null && Req1 != null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
							}
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Close();
						}
					}
					flag = true;
				}
				catch (Exception ex)
				{
					CLogger.Print("[AllUtils.UpdateDB4] " + ex.Message, LoggerType.Error, ex);
					flag = false;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000177D0 File Offset: 0x000159D0
		public static bool UpdateDB(string TABEL, string Req1, int[] ValueReq1, string Req2, object ValueReq2, string[] COLUMNS, params object[] VALUES)
		{
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
			{
				CLogger.Print("[updateDB5] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length != 0 && VALUES.Length != 0)
			{
				bool flag;
				try
				{
					using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
					{
						using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
						{
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							List<string> list = new List<string>();
							for (int i = 0; i < VALUES.Length; i++)
							{
								object obj = VALUES[i];
								string text = COLUMNS[i];
								string text2 = "@Value" + i.ToString();
								npgsqlCommand.Parameters.AddWithValue(text2, obj);
								list.Add(text + "=" + text2);
							}
							string text3 = string.Join(",", list.ToArray());
							if (Req1 != null)
							{
								npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
							}
							if (Req2 != null)
							{
								npgsqlCommand.Parameters.AddWithValue("@Req2", ValueReq2);
							}
							if (Req1 != null && Req2 == null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req1, " = ANY (@Req1)" });
							}
							else if (Req2 != null && Req1 == null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req2, "=@Req2" });
							}
							else if (Req2 != null && Req1 != null)
							{
								npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", text3, " WHERE ", Req1, " = ANY (@Req1) AND ", Req2, "=@Req2" });
							}
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Close();
						}
					}
					flag = true;
				}
				catch (Exception ex)
				{
					CLogger.Print("[AllUtils.UpdateDB5] " + ex.Message, LoggerType.Error, ex);
					flag = false;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00017A70 File Offset: 0x00015C70
		public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1, string Req2, object ValueReq2)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.Parameters.AddWithValue("@Value", VALUE);
						if (Req1 != null)
						{
							npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.Parameters.AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB6] " + ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00017C58 File Offset: 0x00015E58
		public static bool DeleteDB(string TABEL, string Req1, object ValueReq1)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
						npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00017D38 File Offset: 0x00015F38
		public static bool DeleteDB(string TABEL, string Req1, object ValueReq1, string Req2, object ValueReq2)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						if (Req1 != null)
						{
							npgsqlCommand.Parameters.AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.Parameters.AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00017ED8 File Offset: 0x000160D8
		public static bool DeleteDB(string TABEL, string Req1, object[] ValueReq1, string Req2, object ValueReq2)
		{
			if (ValueReq1.Length == 0)
			{
				return false;
			}
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						List<string> list = new List<string>();
						for (int i = 0; i < ValueReq1.Length; i++)
						{
							object obj = ValueReq1[i];
							string text = "@Value" + i.ToString();
							npgsqlCommand.Parameters.AddWithValue(text, obj);
							list.Add(text);
						}
						string text2 = string.Join(",", list.ToArray());
						npgsqlCommand.Parameters.AddWithValue("@Req2", ValueReq2);
						npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, " in (", text2, ") AND ", Req2, "=@Req2" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00018064 File Offset: 0x00016264
		public static uint GetPlayerStatus(AccountStatus status, bool isOnline)
		{
			FriendState friendState;
			int num;
			int num2;
			int num3;
			ComDiv.GetPlayerLocation(status, isOnline, out friendState, out num, out num2, out num3);
			return ComDiv.GetPlayerStatus(num, num2, num3, (int)friendState);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001808C File Offset: 0x0001628C
		public static uint GetPlayerStatus(int roomId, int channelId, int serverId, int stateId)
		{
			uint num = (uint)((uint)(stateId & 255) << 28);
			int num2 = (serverId & 255) << 20;
			int num3 = (channelId & 255) << 12;
			int num4 = roomId & 4095;
			return num | (uint)num2 | (uint)num3 | (uint)num4;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000180C8 File Offset: 0x000162C8
		public static ulong GetPlayerStatus(int clanFId, int roomId, int channelId, int serverId, int stateId)
		{
			ulong num = (ulong)((ulong)((long)clanFId & 4294967295L) << 32);
			long num2 = (long)((long)(stateId & 255) << 28);
			long num3 = (long)((long)(serverId & 255) << 20);
			long num4 = (long)((long)(channelId & 255) << 12);
			long num5 = (long)(roomId & 4095);
			return num | (ulong)num2 | (ulong)num3 | (ulong)num4 | (ulong)num5;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001811C File Offset: 0x0001631C
		public static ulong GetClanStatus(AccountStatus status, bool isOnline)
		{
			FriendState friendState;
			int num;
			int num2;
			int num3;
			int num4;
			ComDiv.GetPlayerLocation(status, isOnline, out friendState, out num, out num2, out num3, out num4);
			return ComDiv.GetPlayerStatus(num4, num, num2, num3, (int)friendState);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00002A7B File Offset: 0x00000C7B
		public static ulong GetClanStatus(FriendState state)
		{
			return ComDiv.GetPlayerStatus(0, 0, 0, 0, (int)state);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00018148 File Offset: 0x00016348
		public static uint GetFriendStatus(FriendModel f)
		{
			PlayerInfo info = f.Info;
			if (info == null)
			{
				return 0U;
			}
			FriendState friendState = FriendState.None;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (f.Removed)
			{
				friendState = FriendState.Offline;
			}
			else if (f.State > 0)
			{
				friendState = (FriendState)f.State;
			}
			else
			{
				ComDiv.GetPlayerLocation(info.Status, info.IsOnline, out friendState, out num3, out num2, out num);
			}
			return ComDiv.GetPlayerStatus(num3, num2, num, (int)friendState);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000181AC File Offset: 0x000163AC
		public static uint GetFriendStatus(FriendModel f, FriendState stateN)
		{
			PlayerInfo info = f.Info;
			if (info == null)
			{
				return 0U;
			}
			FriendState friendState = stateN;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (f.Removed)
			{
				friendState = FriendState.Offline;
			}
			else if (f.State > 0)
			{
				friendState = (FriendState)f.State;
			}
			else if (stateN == FriendState.None)
			{
				ComDiv.GetPlayerLocation(info.Status, info.IsOnline, out friendState, out num3, out num2, out num);
			}
			return ComDiv.GetPlayerStatus(num3, num2, num, (int)friendState);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00018214 File Offset: 0x00016414
		public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId)
		{
			roomId = 0;
			channelId = 0;
			serverId = 0;
			if (isOnline)
			{
				if (status.RoomId != 255)
				{
					roomId = (int)status.RoomId;
					channelId = (int)status.ChannelId;
					state = FriendState.Room;
				}
				else if (status.RoomId == 255 && status.ChannelId != 255)
				{
					channelId = (int)status.ChannelId;
					state = FriendState.Lobby;
				}
				else if (status.RoomId == 255 && status.ChannelId == 255)
				{
					state = FriendState.Online;
				}
				else
				{
					state = FriendState.Offline;
				}
				if (status.ServerId != 255)
				{
					serverId = (int)status.ServerId;
					return;
				}
			}
			else
			{
				state = FriendState.Offline;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000182BC File Offset: 0x000164BC
		public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId, out int clanFId)
		{
			roomId = 0;
			channelId = 0;
			serverId = 0;
			clanFId = 0;
			if (isOnline)
			{
				if (status.RoomId != 255)
				{
					roomId = (int)status.RoomId;
					channelId = (int)status.ChannelId;
					state = FriendState.Room;
				}
				else if ((status.ClanMatchId != 255 || status.RoomId == 255) && status.ChannelId != 255)
				{
					channelId = (int)status.ChannelId;
					state = FriendState.Lobby;
				}
				else if (status.RoomId == 255 && status.ChannelId == 255)
				{
					state = FriendState.Online;
				}
				else
				{
					state = FriendState.Offline;
				}
				if (status.ServerId != 255)
				{
					serverId = (int)status.ServerId;
				}
				if (status.ClanMatchId != 255)
				{
					clanFId = (int)(status.ClanMatchId + 1);
					return;
				}
			}
			else
			{
				state = FriendState.Offline;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001838C File Offset: 0x0001658C
		public static ushort GetMissionCardFlags(int missionId, int cardIdx, byte[] arrayList)
		{
			if (missionId == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (MissionCardModel missionCardModel in MissionCardRAW.GetCards(missionId, cardIdx))
			{
				if ((int)arrayList[missionCardModel.ArrayIdx] >= missionCardModel.MissionLimit)
				{
					num |= missionCardModel.Flag;
				}
			}
			return (ushort)num;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000183FC File Offset: 0x000165FC
		public static byte[] GetMissionCardFlags(int missionId, byte[] arrayList)
		{
			if (missionId == 0)
			{
				return new byte[20];
			}
			List<MissionCardModel> cards = MissionCardRAW.GetCards(missionId);
			if (cards.Count == 0)
			{
				return new byte[20];
			}
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket(20L))
			{
				int num = 0;
				for (int i = 0; i < 10; i++)
				{
					foreach (MissionCardModel missionCardModel in MissionCardRAW.GetCards(cards, i))
					{
						if ((int)arrayList[missionCardModel.ArrayIdx] >= missionCardModel.MissionLimit)
						{
							num |= missionCardModel.Flag;
						}
					}
					syncServerPacket.WriteH((ushort)num);
					num = 0;
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000184D4 File Offset: 0x000166D4
		public static int CountDB(string CommandArgument)
		{
			int num = 0;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandText = CommandArgument;
					num = Convert.ToInt32(npgsqlCommand.ExecuteScalar());
					npgsqlCommand.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("[QuerySQL.CountDB] " + ex.Message, LoggerType.Error, ex);
			}
			return num;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00018564 File Offset: 0x00016764
		public static bool ValidateAllPlayersAccount()
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = string.Format("UPDATE accounts SET online = {0} WHERE online = {1}", false, true);
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00002A87 File Offset: 0x00000C87
		public static uint GenStockId(int ItemId)
		{
			return BitConverter.ToUInt32(ComDiv.smethod_2(ItemId), 0);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00002A95 File Offset: 0x00000C95
		private static byte[] smethod_2(int int_0)
		{
			byte[] bytes = BitConverter.GetBytes(int_0);
			bytes[3] = 64;
			return bytes;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000185FC File Offset: 0x000167FC
		public static T NextOf<T>(IList<T> List, T Item)
		{
			int num = List.IndexOf(Item);
			return List[(num == List.Count - 1) ? 0 : (num + 1)];
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public static T ParseEnum<T>(string Value)
		{
			return (T)((object)Enum.Parse(typeof(T), Value, true));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00002ABA File Offset: 0x00000CBA
		public static string[] SplitObjects(string Input, string Delimiter)
		{
			return Input.Split(new string[] { Delimiter }, StringSplitOptions.None);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00018628 File Offset: 0x00016828
		public static string ToTitleCase(string Text)
		{
			string text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text.Split(new char[] { ' ' })[0].ToLower());
			Text = Text.Replace(Text.Split(new char[] { ' ' })[0], text);
			return Text;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00002ACD File Offset: 0x00000CCD
		private static int smethod_3(int int_0, int int_1)
		{
			while (int_1 != 0)
			{
				int num = int_0 % int_1;
				int_0 = int_1;
				int_1 = num;
			}
			return int_0;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00002ADE File Offset: 0x00000CDE
		public static string AspectRatio(int X, int Y)
		{
			return string.Format("{0}:{1}", X / ComDiv.smethod_3(X, Y), Y / ComDiv.smethod_3(X, Y));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001867C File Offset: 0x0001687C
		public static uint Verificate(byte A, byte B, byte C, byte D)
		{
			byte[] array = new byte[] { A, B, C, D };
			if (!array.Any<byte>())
			{
				return 0U;
			}
			if (B < 60)
			{
				CLogger.Print(string.Format("Refresh Rate is below the minimum limit ({0})", B), LoggerType.Warning, null);
				return 0U;
			}
			if (C >= 0 && C <= 1)
			{
				return BitConverter.ToUInt32(array, 0);
			}
			CLogger.Print(string.Format("Unknown Window State ({0})", C), LoggerType.Warning, null);
			return 0U;
		}

		// Token: 0x0200002E RID: 46
		[CompilerGenerated]
		[Serializable]
		private sealed class Class5
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x00002B06 File Offset: 0x00000D06
			// Note: this type is marked as 'beforefieldinit'.
			static Class5()
			{
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00002116 File Offset: 0x00000316
			public Class5()
			{
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00002B12 File Offset: 0x00000D12
			internal bool method_0(ItemsModel itemsModel_0)
			{
				return itemsModel_0.Count > 0U;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00002B1D File Offset: 0x00000D1D
			internal int method_1(ItemsModel itemsModel_0)
			{
				return itemsModel_0.Id;
			}

			// Token: 0x0400008E RID: 142
			public static readonly ComDiv.Class5 <>9 = new ComDiv.Class5();

			// Token: 0x0400008F RID: 143
			public static Func<ItemsModel, bool> <>9__1_0;

			// Token: 0x04000090 RID: 144
			public static Func<ItemsModel, int> <>9__1_1;
		}
	}
}
