using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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

namespace Plugin.Core.Utility;

public static class ComDiv
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class5
	{
		public static readonly Class5 _003C_003E9 = new Class5();

		public static Func<ItemsModel, bool> _003C_003E9__1_0;

		public static Func<ItemsModel, int> _003C_003E9__1_1;

		internal bool method_0(ItemsModel itemsModel_0)
		{
			return itemsModel_0.Count != 0;
		}

		internal int method_1(ItemsModel itemsModel_0)
		{
			return itemsModel_0.Id;
		}
	}

	public static int CheckEquipedItems(PlayerEquipment Equip, List<ItemsModel> Inventory, bool BattleRules)
	{
		int num = 0;
		(bool, bool, bool, bool, bool) valueTuple_ = (false, false, false, false, false);
		(bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple_2 = (false, false, false, false, false, false, false, false, false, false, false, false);
		(bool, bool, bool) valueTuple_3 = (false, false, false);
		if (Equip.BeretItem == 0)
		{
			valueTuple_2.Item11 = true;
		}
		if (Equip.AccessoryId == 0)
		{
			valueTuple_3.Item1 = true;
		}
		if (Equip.SprayId == 0)
		{
			valueTuple_3.Item2 = true;
		}
		if (Equip.NameCardId == 0)
		{
			valueTuple_3.Item3 = true;
		}
		if (Equip.WeaponPrimary == 103004)
		{
			valueTuple_.Item1 = true;
		}
		if (BattleRules)
		{
			if (!valueTuple_.Item1 && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
			{
				valueTuple_.Item1 = true;
			}
			if (!valueTuple_.Item3 && Equip.WeaponMelee == 323001)
			{
				valueTuple_.Item3 = true;
			}
		}
		((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool), (bool, bool, bool)) tuple = smethod_0(Equip, Inventory, valueTuple_, valueTuple_2, valueTuple_3);
		valueTuple_ = tuple.Item1;
		valueTuple_2 = tuple.Item2;
		valueTuple_3 = tuple.Item3;
		bool flag = !valueTuple_.Item1 || !valueTuple_.Item2 || !valueTuple_.Item3 || !valueTuple_.Item4 || !valueTuple_.Item5;
		bool num2 = !valueTuple_2.Item1 || !valueTuple_2.Item2 || !valueTuple_2.Item3 || !valueTuple_2.Item4 || !valueTuple_2.Item5 || !valueTuple_2.Item6 || !valueTuple_2.Item7 || !valueTuple_2.Rest.Item1 || !valueTuple_2.Rest.Item2 || !valueTuple_2.Rest.Item3 || !valueTuple_2.Rest.Item4 || !valueTuple_2.Rest.Item5;
		bool flag2 = !valueTuple_3.Item1 || !valueTuple_3.Item2 || !valueTuple_3.Item3;
		if (flag)
		{
			num += 2;
		}
		if (num2)
		{
			num++;
		}
		if (flag2)
		{
			num += 3;
		}
		smethod_1(Equip, ref valueTuple_, ref valueTuple_2, ref valueTuple_3);
		return num;
	}

	private static ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool), (bool, bool, bool)) smethod_0(PlayerEquipment playerEquipment_0, List<ItemsModel> list_0, (bool, bool, bool, bool, bool) valueTuple_0, (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple_1, (bool, bool, bool) valueTuple_2)
	{
		lock (list_0)
		{
			HashSet<int> hashSet = new HashSet<int>(from itemsModel_0 in list_0
				where itemsModel_0.Count != 0
				select itemsModel_0.Id);
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
				valueTuple_1.Item8 = true;
			}
			if (hashSet.Contains(playerEquipment_0.PartHolster))
			{
				valueTuple_1.Item9 = true;
			}
			if (hashSet.Contains(playerEquipment_0.PartSkin))
			{
				valueTuple_1.Item10 = true;
			}
			if (playerEquipment_0.BeretItem != 0 && hashSet.Contains(playerEquipment_0.BeretItem))
			{
				valueTuple_1.Item11 = true;
			}
			if (hashSet.Contains(playerEquipment_0.DinoItem))
			{
				valueTuple_1.Item12 = true;
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
		return (valueTuple_0, valueTuple_1, valueTuple_2);
	}

	private static void smethod_1(PlayerEquipment playerEquipment_0, ref (bool, bool, bool, bool, bool) valueTuple_0, ref (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) valueTuple_1, ref (bool, bool, bool) valueTuple_2)
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
		if (!valueTuple_1.Item8)
		{
			playerEquipment_0.PartBelt = 1001200000;
		}
		if (!valueTuple_1.Item9)
		{
			playerEquipment_0.PartHolster = 1001300000;
		}
		if (!valueTuple_1.Item10)
		{
			playerEquipment_0.PartSkin = 1001400000;
		}
		if (!valueTuple_1.Item11)
		{
			playerEquipment_0.BeretItem = 0;
		}
		if (!valueTuple_1.Item12)
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

	public static void UpdateWeapons(PlayerEquipment Equip, DBQuery Query)
	{
		Query.AddQuery("weapon_primary", Equip.WeaponPrimary);
		Query.AddQuery("weapon_secondary", Equip.WeaponSecondary);
		Query.AddQuery("weapon_melee", Equip.WeaponMelee);
		Query.AddQuery("weapon_explosive", Equip.WeaponExplosive);
		Query.AddQuery("weapon_special", Equip.WeaponSpecial);
	}

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

	public static void UpdateItems(PlayerEquipment Equip, DBQuery Query)
	{
		Query.AddQuery("accesory_id", Equip.AccessoryId);
		Query.AddQuery("spray_id", Equip.SprayId);
		Query.AddQuery("namecard_id", Equip.NameCardId);
	}

	public static void TryCreateItem(ItemsModel Model, PlayerInventory Inventory, long OwnerId)
	{
		try
		{
			ItemsModel ıtem = Inventory.GetItem(Model.Id);
			if (ıtem == null)
			{
				if (DaoManagerSQL.CreatePlayerInventoryItem(Model, OwnerId))
				{
					Inventory.AddItem(Model);
				}
				return;
			}
			Model.ObjectId = ıtem.ObjectId;
			if (ıtem.Equip == ItemEquipType.Durable)
			{
				if (ShopManager.IsRepairableItem(Model.Id))
				{
					Model.Count = 100u;
					UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
				}
				else
				{
					Model.Count += ıtem.Count;
					UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
				}
			}
			else if (ıtem.Equip == ItemEquipType.Temporary)
			{
				DateTime dateTime = DateTime.ParseExact(ıtem.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
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
				UpdateDB("player_items", "count", (long)Model.Count, "owner_id", OwnerId, "id", Model.Id);
			}
			ıtem.Equip = Model.Equip;
			ıtem.Count = Model.Count;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static ItemCategory GetItemCategory(int ItemId)
	{
		int ıdStatics = GetIdStatics(ItemId, 1);
		int ıdStatics2 = GetIdStatics(ItemId, 4);
		if (ıdStatics >= 1 && ıdStatics <= 5)
		{
			return ItemCategory.Weapon;
		}
		if ((ıdStatics < 6 || ıdStatics > 14) && ıdStatics != 27 && (ıdStatics2 < 7 || ıdStatics2 > 14))
		{
			if (ıdStatics < 16 || ıdStatics > 20)
			{
				switch (ıdStatics)
				{
				default:
					if (ıdStatics < 36 || ıdStatics > 40)
					{
						switch (ıdStatics)
						{
						default:
							CLogger.Print($"Invalid Category [{ıdStatics}]: {ItemId}", LoggerType.Warning);
							return ItemCategory.None;
						case 15:
						case 30:
						case 31:
						case 32:
						case 33:
						case 34:
						case 35:
							return ItemCategory.NewItem;
						}
					}
					break;
				case 22:
				case 26:
				case 28:
				case 29:
					break;
				}
			}
			return ItemCategory.Coupon;
		}
		return ItemCategory.Character;
	}

	public static uint ValidateStockId(int ItemId)
	{
		int ıdStatics = GetIdStatics(ItemId, 4);
		return GenStockId((ıdStatics < 7 || ıdStatics > 14) ? ItemId : ıdStatics);
	}

	public static int GetIdStatics(int Id, int Type)
	{
		return Type switch
		{
			1 => Id / 100000, 
			2 => Id % 100000 / 1000, 
			3 => Id % 1000, 
			4 => Id % 10000000 / 100000, 
			5 => Id / 1000, 
			_ => 0, 
		};
	}

	public static double GetDuration(DateTime Date)
	{
		return (DateTimeUtil.Now() - Date).TotalSeconds;
	}

	public static byte[] AddressBytes(string Host)
	{
		return IPAddress.Parse(Host).GetAddressBytes();
	}

	public static int CreateItemId(int ItemClass, int ClassType, int Number)
	{
		return ItemClass * 100000 + ClassType * 1000 + Number;
	}

	public static int Percentage(int Total, int Percent)
	{
		return Total * Percent / 100;
	}

	public static float Percentage(float Total, int Percent)
	{
		return Total * (float)Percent / 100f;
	}

	public static char[] SubArray(this char[] Input, int StartIndex, int Length)
	{
		List<char> list = new List<char>();
		for (int i = StartIndex; i < Length; i++)
		{
			list.Add(Input[i]);
		}
		return list.ToArray();
	}

	public static bool UpdateDB(string TABEL, string[] COLUMNS, params object[] VALUES)
	{
		if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
		{
			CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
			return false;
		}
		if (COLUMNS.Length != 0 && VALUES.Length != 0)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					try
					{
						((DbConnection)(object)val).Open();
						((DbCommand)(object)val2).CommandType = CommandType.Text;
						string text = "";
						List<string> list = new List<string>();
						for (int i = 0; i < VALUES.Length; i++)
						{
							object obj = VALUES[i];
							string text2 = COLUMNS[i];
							string text3 = "@Value" + i;
							val2.Parameters.AddWithValue(text3, obj);
							list.Add(text2 + "=" + text3);
						}
						text = string.Join(",", list.ToArray());
						((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text;
						((DbCommand)(object)val2).ExecuteNonQuery();
						((Component)(object)val2).Dispose();
						((DbConnection)(object)val).Close();
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB1] " + ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string[] COLUMNS, params object[] VALUES)
	{
		if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
		{
			CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
			return false;
		}
		if (COLUMNS.Length != 0 && VALUES.Length != 0)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					try
					{
						((DbConnection)(object)val).Open();
						((DbCommand)(object)val2).CommandType = CommandType.Text;
						string text = "";
						List<string> list = new List<string>();
						for (int i = 0; i < VALUES.Length; i++)
						{
							object obj = VALUES[i];
							string text2 = COLUMNS[i];
							string text3 = "@Value" + i;
							val2.Parameters.AddWithValue(text3, obj);
							list.Add(text2 + "=" + text3);
						}
						text = string.Join(",", list.ToArray());
						val2.Parameters.AddWithValue("@Req1", ValueReq1);
						((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req1 + "=@Req1";
						((DbCommand)(object)val2).ExecuteNonQuery();
						((Component)(object)val2).Dispose();
						((DbConnection)(object)val).Close();
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB2] " + ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					val2.Parameters.AddWithValue("@Value", VALUE);
					val2.Parameters.AddWithValue("@Req1", ValueReq1);
					((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + COLUMN + "=@Value WHERE " + Req1 + "=@Req1";
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print("[AllUtils.UpdateDB3] " + ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string Req2, object valueReq2, string[] COLUMNS, params object[] VALUES)
	{
		if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
		{
			CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
			return false;
		}
		if (COLUMNS.Length != 0 && VALUES.Length != 0)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					try
					{
						((DbConnection)(object)val).Open();
						((DbCommand)(object)val2).CommandType = CommandType.Text;
						string text = "";
						List<string> list = new List<string>();
						for (int i = 0; i < VALUES.Length; i++)
						{
							object obj = VALUES[i];
							string text2 = COLUMNS[i];
							string text3 = "@Value" + i;
							val2.Parameters.AddWithValue(text3, obj);
							list.Add(text2 + "=" + text3);
						}
						text = string.Join(",", list.ToArray());
						if (Req1 != null)
						{
							val2.Parameters.AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							val2.Parameters.AddWithValue("@Req2", valueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req1 + "=@Req1";
						}
						else if (Req2 != null && Req1 == null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req2 + "=@Req2";
						}
						else if (Req2 != null && Req1 != null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req1 + "=@Req1 AND " + Req2 + "=@Req2";
						}
						((DbCommand)(object)val2).ExecuteNonQuery();
						((Component)(object)val2).Dispose();
						((DbConnection)(object)val).Close();
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB4] " + ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateDB(string TABEL, string Req1, int[] ValueReq1, string Req2, object ValueReq2, string[] COLUMNS, params object[] VALUES)
	{
		if (COLUMNS.Length != 0 && VALUES.Length != 0 && COLUMNS.Length != VALUES.Length)
		{
			CLogger.Print("[updateDB5] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning);
			return false;
		}
		if (COLUMNS.Length != 0 && VALUES.Length != 0)
		{
			try
			{
				NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
				try
				{
					NpgsqlCommand val2 = val.CreateCommand();
					try
					{
						((DbConnection)(object)val).Open();
						((DbCommand)(object)val2).CommandType = CommandType.Text;
						string text = "";
						List<string> list = new List<string>();
						for (int i = 0; i < VALUES.Length; i++)
						{
							object obj = VALUES[i];
							string text2 = COLUMNS[i];
							string text3 = "@Value" + i;
							val2.Parameters.AddWithValue(text3, obj);
							list.Add(text2 + "=" + text3);
						}
						text = string.Join(",", list.ToArray());
						if (Req1 != null)
						{
							val2.Parameters.AddWithValue("@Req1", (object)ValueReq1);
						}
						if (Req2 != null)
						{
							val2.Parameters.AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req1 + " = ANY (@Req1)";
						}
						else if (Req2 != null && Req1 == null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req2 + "=@Req2";
						}
						else if (Req2 != null && Req1 != null)
						{
							((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + text + " WHERE " + Req1 + " = ANY (@Req1) AND " + Req2 + "=@Req2";
						}
						((DbCommand)(object)val2).ExecuteNonQuery();
						((Component)(object)val2).Dispose();
						((DbConnection)(object)val).Close();
					}
					finally
					{
						((IDisposable)val2)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				return true;
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.UpdateDB5] " + ex.Message, LoggerType.Error, ex);
				return false;
			}
		}
		return false;
	}

	public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1, string Req2, object ValueReq2)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					val2.Parameters.AddWithValue("@Value", VALUE);
					if (Req1 != null)
					{
						val2.Parameters.AddWithValue("@Req1", ValueReq1);
					}
					if (Req2 != null)
					{
						val2.Parameters.AddWithValue("@Req2", ValueReq2);
					}
					if (Req1 != null && Req2 == null)
					{
						((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + COLUMN + "=@Value WHERE " + Req1 + "=@Req1";
					}
					else if (Req2 != null && Req1 == null)
					{
						((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + COLUMN + "=@Value WHERE " + Req2 + "=@Req2";
					}
					else if (Req2 != null && Req1 != null)
					{
						((DbCommand)(object)val2).CommandText = "UPDATE " + TABEL + " SET " + COLUMN + "=@Value WHERE " + Req1 + "=@Req1 AND " + Req2 + "=@Req2";
					}
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print("[AllUtils.UpdateDB6] " + ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool DeleteDB(string TABEL, string Req1, object ValueReq1)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					val2.Parameters.AddWithValue("@Req1", ValueReq1);
					((DbCommand)(object)val2).CommandText = "DELETE FROM " + TABEL + " WHERE " + Req1 + "=@Req1";
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool DeleteDB(string TABEL, string Req1, object ValueReq1, string Req2, object ValueReq2)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					if (Req1 != null)
					{
						val2.Parameters.AddWithValue("@Req1", ValueReq1);
					}
					if (Req2 != null)
					{
						val2.Parameters.AddWithValue("@Req2", ValueReq2);
					}
					if (Req1 != null && Req2 == null)
					{
						((DbCommand)(object)val2).CommandText = "DELETE FROM " + TABEL + " WHERE " + Req1 + "=@Req1";
					}
					else if (Req2 != null && Req1 == null)
					{
						((DbCommand)(object)val2).CommandText = "DELETE FROM " + TABEL + " WHERE " + Req2 + "=@Req2";
					}
					else if (Req2 != null && Req1 != null)
					{
						((DbCommand)(object)val2).CommandText = "DELETE FROM " + TABEL + " WHERE " + Req1 + "=@Req1 AND " + Req2 + "=@Req2";
					}
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool DeleteDB(string TABEL, string Req1, object[] ValueReq1, string Req2, object ValueReq2)
	{
		if (ValueReq1.Length == 0)
		{
			return false;
		}
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand val2 = val.CreateCommand();
				try
				{
					((DbConnection)(object)val).Open();
					((DbCommand)(object)val2).CommandType = CommandType.Text;
					string text = "";
					List<string> list = new List<string>();
					for (int i = 0; i < ValueReq1.Length; i++)
					{
						object obj = ValueReq1[i];
						string text2 = "@Value" + i;
						val2.Parameters.AddWithValue(text2, obj);
						list.Add(text2);
					}
					text = string.Join(",", list.ToArray());
					val2.Parameters.AddWithValue("@Req2", ValueReq2);
					((DbCommand)(object)val2).CommandText = "DELETE FROM " + TABEL + " WHERE " + Req1 + " in (" + text + ") AND " + Req2 + "=@Req2";
					((DbCommand)(object)val2).ExecuteNonQuery();
					((Component)(object)val2).Dispose();
					((Component)(object)val).Dispose();
					((DbConnection)(object)val).Close();
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static uint GetPlayerStatus(AccountStatus status, bool isOnline)
	{
		GetPlayerLocation(status, isOnline, out var state, out var roomId, out var channelId, out var serverId);
		return GetPlayerStatus(roomId, channelId, serverId, (int)state);
	}

	public static uint GetPlayerStatus(int roomId, int channelId, int serverId, int stateId)
	{
		int num = (stateId & 0xFF) << 28;
		int num2 = (serverId & 0xFF) << 20;
		int num3 = (channelId & 0xFF) << 12;
		int num4 = roomId & 0xFFF;
		return (uint)(num | num2 | num3 | num4);
	}

	public static ulong GetPlayerStatus(int clanFId, int roomId, int channelId, int serverId, int stateId)
	{
		long num = (clanFId & 0xFFFFFFFFL) << 32;
		long num2 = (stateId & 0xFF) << 28;
		long num3 = (serverId & 0xFF) << 20;
		long num4 = (channelId & 0xFF) << 12;
		long num5 = roomId & 0xFFF;
		return (ulong)(num | num2 | num3 | num4 | num5);
	}

	public static ulong GetClanStatus(AccountStatus status, bool isOnline)
	{
		GetPlayerLocation(status, isOnline, out var state, out var roomId, out var channelId, out var serverId, out var clanFId);
		return GetPlayerStatus(clanFId, roomId, channelId, serverId, (int)state);
	}

	public static ulong GetClanStatus(FriendState state)
	{
		return GetPlayerStatus(0, 0, 0, 0, (int)state);
	}

	public static uint GetFriendStatus(FriendModel f)
	{
		PlayerInfo ınfo = f.Info;
		if (ınfo == null)
		{
			return 0u;
		}
		FriendState state = FriendState.None;
		int serverId = 0;
		int channelId = 0;
		int roomId = 0;
		if (f.Removed)
		{
			state = FriendState.Offline;
		}
		else if (f.State > 0)
		{
			state = (FriendState)f.State;
		}
		else
		{
			GetPlayerLocation(ınfo.Status, ınfo.IsOnline, out state, out roomId, out channelId, out serverId);
		}
		return GetPlayerStatus(roomId, channelId, serverId, (int)state);
	}

	public static uint GetFriendStatus(FriendModel f, FriendState stateN)
	{
		PlayerInfo ınfo = f.Info;
		if (ınfo == null)
		{
			return 0u;
		}
		FriendState state = stateN;
		int serverId = 0;
		int channelId = 0;
		int roomId = 0;
		if (f.Removed)
		{
			state = FriendState.Offline;
		}
		else if (f.State > 0)
		{
			state = (FriendState)f.State;
		}
		else if (stateN == FriendState.None)
		{
			GetPlayerLocation(ınfo.Status, ınfo.IsOnline, out state, out roomId, out channelId, out serverId);
		}
		return GetPlayerStatus(roomId, channelId, serverId, (int)state);
	}

	public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId)
	{
		roomId = 0;
		channelId = 0;
		serverId = 0;
		if (isOnline)
		{
			if (status.RoomId != byte.MaxValue)
			{
				roomId = status.RoomId;
				channelId = status.ChannelId;
				state = FriendState.Room;
			}
			else if (status.RoomId == byte.MaxValue && status.ChannelId != byte.MaxValue)
			{
				channelId = status.ChannelId;
				state = FriendState.Lobby;
			}
			else if (status.RoomId == byte.MaxValue && status.ChannelId == byte.MaxValue)
			{
				state = FriendState.Online;
			}
			else
			{
				state = FriendState.Offline;
			}
			if (status.ServerId != byte.MaxValue)
			{
				serverId = status.ServerId;
			}
		}
		else
		{
			state = FriendState.Offline;
		}
	}

	public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId, out int clanFId)
	{
		roomId = 0;
		channelId = 0;
		serverId = 0;
		clanFId = 0;
		if (isOnline)
		{
			if (status.RoomId != byte.MaxValue)
			{
				roomId = status.RoomId;
				channelId = status.ChannelId;
				state = FriendState.Room;
			}
			else if ((status.ClanMatchId != byte.MaxValue || status.RoomId == byte.MaxValue) && status.ChannelId != byte.MaxValue)
			{
				channelId = status.ChannelId;
				state = FriendState.Lobby;
			}
			else if (status.RoomId == byte.MaxValue && status.ChannelId == byte.MaxValue)
			{
				state = FriendState.Online;
			}
			else
			{
				state = FriendState.Offline;
			}
			if (status.ServerId != byte.MaxValue)
			{
				serverId = status.ServerId;
			}
			if (status.ClanMatchId != byte.MaxValue)
			{
				clanFId = status.ClanMatchId + 1;
			}
		}
		else
		{
			state = FriendState.Offline;
		}
	}

	public static ushort GetMissionCardFlags(int missionId, int cardIdx, byte[] arrayList)
	{
		if (missionId == 0)
		{
			return 0;
		}
		int num = 0;
		foreach (MissionCardModel card in MissionCardRAW.GetCards(missionId, cardIdx))
		{
			if (arrayList[card.ArrayIdx] >= card.MissionLimit)
			{
				num |= card.Flag;
			}
		}
		return (ushort)num;
	}

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
		using SyncServerPacket syncServerPacket = new SyncServerPacket(20L);
		int num = 0;
		for (int i = 0; i < 10; i++)
		{
			foreach (MissionCardModel card in MissionCardRAW.GetCards(cards, i))
			{
				if (arrayList[card.ArrayIdx] >= card.MissionLimit)
				{
					num |= card.Flag;
				}
			}
			syncServerPacket.WriteH((ushort)num);
			num = 0;
		}
		return syncServerPacket.ToArray();
	}

	public static int CountDB(string CommandArgument)
	{
		int result = 0;
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandText = CommandArgument;
				result = Convert.ToInt32(((DbCommand)(object)obj).ExecuteScalar());
				((Component)(object)obj).Dispose();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("[QuerySQL.CountDB] " + ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static bool ValidateAllPlayersAccount()
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				NpgsqlCommand obj = val.CreateCommand();
				((DbConnection)(object)val).Open();
				((DbCommand)(object)obj).CommandType = CommandType.Text;
				((DbCommand)(object)obj).CommandText = $"UPDATE accounts SET online = {false} WHERE online = {true}";
				((DbCommand)(object)obj).ExecuteNonQuery();
				((Component)(object)obj).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static uint GenStockId(int ItemId)
	{
		return BitConverter.ToUInt32(smethod_2(ItemId), 0);
	}

	private static byte[] smethod_2(int int_0)
	{
		byte[] bytes = BitConverter.GetBytes(int_0);
		bytes[3] = 64;
		return bytes;
	}

	public static T NextOf<T>(IList<T> List, T Item)
	{
		int num = List.IndexOf(Item);
		return List[(num != List.Count - 1) ? (num + 1) : 0];
	}

	public static T ParseEnum<T>(string Value)
	{
		return (T)Enum.Parse(typeof(T), Value, ignoreCase: true);
	}

	public static string[] SplitObjects(string Input, string Delimiter)
	{
		return Input.Split(new string[1] { Delimiter }, StringSplitOptions.None);
	}

	public static string ToTitleCase(string Text)
	{
		string newValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text.Split(' ')[0].ToLower());
		Text = Text.Replace(Text.Split(' ')[0], newValue);
		return Text;
	}

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

	public static string AspectRatio(int X, int Y)
	{
		return $"{X / smethod_3(X, Y)}:{Y / smethod_3(X, Y)}";
	}

	public static uint Verificate(byte A, byte B, byte C, byte D)
	{
		byte[] array = new byte[4] { A, B, C, D };
		if (!array.Any())
		{
			return 0u;
		}
		if (B < 60)
		{
			CLogger.Print($"Refresh Rate is below the minimum limit ({B})", LoggerType.Warning);
			return 0u;
		}
		if (C >= 0 && C <= 1)
		{
			return BitConverter.ToUInt32(array, 0);
		}
		CLogger.Print($"Unknown Window State ({C})", LoggerType.Warning);
		return 0u;
	}
}
