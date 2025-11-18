using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;

namespace Plugin.Core.Managers;

public static class ShopManager
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class13<T>
	{
		public static readonly Class13<T> _003C_003E9 = new Class13<T>();

		public static Func<T, int, Class0<T, int>> _003C_003E9__16_0;

		public static Func<Class0<T, int>, T> _003C_003E9__16_3;

		public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> _003C_003E9__16_2;

		internal Class0<T, int> method_0(T gparam_0, int int_0)
		{
			return new Class0<T, int>(gparam_0, int_0);
		}

		internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
		{
			return igrouping_0.Select((Class0<T, int> class0_0) => class0_0.item);
		}

		internal T method_2(Class0<T, int> class0_0)
		{
			return class0_0.item;
		}
	}

	[CompilerGenerated]
	private sealed class Class14<T>
	{
		public int int_0;

		internal int method_0(Class0<T, int> class0_0)
		{
			return class0_0.inx / int_0;
		}
	}

	public static List<ItemsRepair> ItemRepairs = new List<ItemsRepair>();

	public static List<GoodsItem> ShopAllList = new List<GoodsItem>();

	public static List<GoodsItem> ShopBuyableList = new List<GoodsItem>();

	public static SortedList<int, GoodsItem> ShopUniqueList = new SortedList<int, GoodsItem>();

	public static List<ShopData> ShopDataMt1 = new List<ShopData>();

	public static List<ShopData> ShopDataMt2 = new List<ShopData>();

	public static List<ShopData> ShopDataGoods = new List<ShopData>();

	public static List<ShopData> ShopDataItems = new List<ShopData>();

	public static List<ShopData> ShopDataItemRepairs = new List<ShopData>();

	public static byte[] ShopTagData;

	public static int TotalGoods;

	public static int TotalItems;

	public static int TotalMatching1;

	public static int TotalMatching2;

	public static int TotalRepairs;

	public static int Set4p;

	public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
	{
		return from class0_0 in list.Select((T gparam_0, int int_0) => new Class0<T, int>(gparam_0, int_0))
			group class0_0 by class0_0.inx / limit into igrouping_0
			select from class0_0 in igrouping_0
				select class0_0.item;
	}

	public static void Load(int Type)
	{
		smethod_4(Type);
		smethod_0(Type);
		smethod_1(Type);
		smethod_2(Type);
		if (Type == 1)
		{
			try
			{
				smethod_5(0);
				smethod_6(1);
				smethod_7();
				smethod_8();
				smethod_17();
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			CLogger.Print($"Plugin Loaded: {ShopBuyableList.Count} Buyable Items", LoggerType.Info);
			CLogger.Print($"Plugin Loaded: {ItemRepairs.Count} Repairable Items", LoggerType.Info);
		}
	}

	private static void smethod_0(int int_0)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				((DbConnection)(object)val).Open();
				NpgsqlCommand val2 = val.CreateCommand();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_shop";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					int num = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["item_id"]));
					string[] array = (string.Format("{0}", ((DbDataReader)(object)val3)["item_count_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["item_count_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["item_count_list"]) });
					string[] array2 = (string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]) });
					string[] array3 = (string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]) });
					if (array.Length == array2.Length && array2.Length == array3.Length)
					{
						int num2 = 0;
						string[] array4 = array;
						foreach (string s in array4)
						{
							num2++;
							if (!uint.TryParse(s, out var result))
							{
								CLogger.Print($"Loading goods with count != UInt ({num})", LoggerType.Warning);
								continue;
							}
							if (!int.TryParse(array2[num2 - 1], out var result2))
							{
								CLogger.Print($"Loading goods with cash != Int ({num})", LoggerType.Warning);
								continue;
							}
							if (!int.TryParse(array3[num2 - 1], out var result3))
							{
								CLogger.Print($"Loading goods with gold != Int ({num})", LoggerType.Warning);
								continue;
							}
							int ıdStatics = ComDiv.GetIdStatics(num, 1);
							string text = string.Format("{0}", ((DbDataReader)(object)val3)["item_name"]);
							GoodsItem goodsItem = new GoodsItem
							{
								Id = int.Parse(string.Format("{0}{1}", num, (ıdStatics == 22 || ıdStatics == 26 || ıdStatics == 36 || ıdStatics == 37 || ıdStatics == 40) ? "00" : $"{num2:D2}")),
								PriceGold = result3,
								PriceCash = result2
							};
							int num3 = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["discount_percent"]));
							if (num3 > 0 && goodsItem.PriceCash > 0)
							{
								goodsItem.StarCash = goodsItem.PriceCash * 255;
								goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num3);
							}
							if (num3 > 0 && goodsItem.PriceGold > 0)
							{
								goodsItem.StarGold = goodsItem.PriceGold * 255;
								goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num3);
							}
							goodsItem.Tag = ((num3 > 0) ? ItemTag.Sale : ((ItemTag)int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["shop_tag"]))));
							goodsItem.Title = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["title_requi"]));
							goodsItem.AuthType = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["item_consume"]));
							goodsItem.BuyType2 = ((goodsItem.AuthType == 2) ? 1 : ((!IsRepairableItem(num)) ? 1 : 2));
							goodsItem.BuyType3 = ((goodsItem.AuthType != 1) ? 1 : 2);
							goodsItem.Visibility = ((!bool.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["item_visible"]))) ? 4 : 0);
							goodsItem.Item.SetItemId(num);
							goodsItem.Item.Name = ((goodsItem.AuthType == 1) ? $"{text} ({result} qty)" : ((goodsItem.AuthType == 2) ? $"{text} ({result / 3600u} hours)" : text));
							goodsItem.Item.Count = result;
							int ıdStatics2 = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
							if (int_0 != 1 && (int_0 != 2 || ıdStatics2 != 16))
							{
								continue;
							}
							ShopAllList.Add(goodsItem);
							if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
							{
								ShopBuyableList.Add(goodsItem);
							}
							if (!ShopUniqueList.ContainsKey(goodsItem.Item.Id) && goodsItem.AuthType > 0)
							{
								ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
								if (goodsItem.Visibility == 4)
								{
									Set4p++;
								}
							}
						}
					}
					else
					{
						CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({num})", LoggerType.Warning);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_1(int int_0)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				((DbConnection)(object)val).Open();
				NpgsqlCommand val2 = val.CreateCommand();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_shop_effects";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					int num = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["coupon_id"]));
					string[] array = (string.Format("{0}", ((DbDataReader)(object)val3)["coupon_count_day_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["coupon_count_day_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["coupon_count_day_list"]) });
					string[] array2 = (string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["price_cash_list"]) });
					string[] array3 = (string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]).Contains(",") ? string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]).Split(',') : new string[1] { string.Format("{0}", ((DbDataReader)(object)val3)["price_gold_list"]) });
					if (array.Length == array2.Length && array2.Length == array3.Length)
					{
						int num2 = 0;
						string[] array4 = array;
						foreach (string s in array4)
						{
							num2++;
							if (!int.TryParse(s, out var result))
							{
								CLogger.Print($"Loading effects with count != Int ({num})", LoggerType.Warning);
								continue;
							}
							if (!int.TryParse(array2[num2 - 1], out var result2))
							{
								CLogger.Print($"Loading effects with cash != Int ({num})", LoggerType.Warning);
								continue;
							}
							if (!int.TryParse(array3[num2 - 1], out var result3))
							{
								CLogger.Print($"Loading effects with gold != Int ({num})", LoggerType.Warning);
								continue;
							}
							if (result >= 100)
							{
								result = 100;
							}
							int ıtemId = int.Parse($"{$"{num}".Substring(0, 2)}{result:D2}{$"{num}".Substring(4, 3)}");
							GoodsItem goodsItem = new GoodsItem
							{
								Id = int.Parse($"{num}{num2:D2}"),
								PriceGold = result3,
								PriceCash = result2
							};
							int num3 = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["discount_percent"]));
							if (num3 > 0 && goodsItem.PriceCash > 0)
							{
								goodsItem.StarCash = goodsItem.PriceCash * 255;
								goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num3);
							}
							if (num3 > 0 && goodsItem.PriceGold > 0)
							{
								goodsItem.PriceGold *= 255;
								goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num3);
							}
							goodsItem.Tag = ((num3 > 0) ? ItemTag.Sale : ((ItemTag)int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["shop_tag"]))));
							goodsItem.Title = 0;
							goodsItem.AuthType = 1;
							goodsItem.BuyType2 = 1;
							goodsItem.BuyType3 = 2;
							goodsItem.Visibility = ((!bool.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["coupon_visible"]))) ? 4 : 0);
							goodsItem.Item.SetItemId(ıtemId);
							goodsItem.Item.Name = string.Format("{0} ({1} days)", ((DbDataReader)(object)val3)["coupon_name"], result);
							goodsItem.Item.Count = 1u;
							int ıdStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
							if (int_0 != 1 && (int_0 != 2 || ıdStatics != 16))
							{
								continue;
							}
							ShopAllList.Add(goodsItem);
							if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
							{
								ShopBuyableList.Add(goodsItem);
							}
							if (!ShopUniqueList.ContainsKey(goodsItem.Item.Id) && goodsItem.AuthType > 0)
							{
								ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
								if (goodsItem.Visibility == 4)
								{
									Set4p++;
								}
							}
						}
					}
					else
					{
						CLogger.Print($"Loading goods with invalid counts / moneys / points sizes. ({num})", LoggerType.Warning);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_2(int int_0)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				((DbConnection)(object)val).Open();
				NpgsqlCommand val2 = val.CreateCommand();
				((DbCommand)(object)val2).CommandText = $"SELECT * FROM system_shop_sets WHERE visible = '{true}';";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					int int_ = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["id"]));
					string string_ = string.Format("{0}", ((DbDataReader)(object)val3)["name"]);
					smethod_3(int_, string_, int_0);
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_3(int int_0, string string_0, int int_1)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				((DbConnection)(object)val).Open();
				NpgsqlCommand val2 = val.CreateCommand();
				((DbCommand)(object)val2).CommandText = $"SELECT * FROM system_shop_sets_items WHERE set_id = '{int_0}' AND set_name = '{string_0}';";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					int ıtemId = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["id"]));
					string name = string.Format("{0}", ((DbDataReader)(object)val3)["name"]);
					int num = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["consume"]));
					uint count = uint.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["count"]));
					int priceGold = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["price_gold"]));
					int priceCash = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["price_cash"]));
					GoodsItem goodsItem = new GoodsItem
					{
						Id = int_0,
						PriceGold = priceGold,
						PriceCash = priceCash,
						Tag = ItemTag.Hot,
						Title = 0,
						AuthType = 0,
						BuyType2 = 1,
						BuyType3 = ((num != 1) ? 1 : 2),
						Visibility = 4
					};
					goodsItem.Item.SetItemId(ıtemId);
					goodsItem.Item.Name = name;
					goodsItem.Item.Count = count;
					int ıdStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
					if (int_1 != 1 && (int_1 != 2 || ıdStatics != 16))
					{
						continue;
					}
					ShopAllList.Add(goodsItem);
					if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
					{
						ShopBuyableList.Add(goodsItem);
					}
					if (!ShopUniqueList.ContainsKey(goodsItem.Item.Id) && goodsItem.AuthType > 0)
					{
						ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
						if (goodsItem.Visibility == 4)
						{
							Set4p++;
						}
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_4(int int_0)
	{
		try
		{
			NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
			try
			{
				((DbConnection)(object)val).Open();
				NpgsqlCommand val2 = val.CreateCommand();
				((DbCommand)(object)val2).CommandText = "SELECT * FROM system_shop_repair";
				((DbCommand)(object)val2).CommandType = CommandType.Text;
				NpgsqlDataReader val3 = val2.ExecuteReader(CommandBehavior.Default);
				while (((DbDataReader)(object)val3).Read())
				{
					ItemsRepair ıtemsRepair = new ItemsRepair
					{
						Id = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["item_id"])),
						Point = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["price_gold"])),
						Cash = int.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["price_cash"])),
						Quantity = uint.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["quantity"])),
						Enable = bool.Parse(string.Format("{0}", ((DbDataReader)(object)val3)["repairable"]))
					};
					if (int_0 == 1 && ıtemsRepair.Enable && ıtemsRepair.Quantity <= 100)
					{
						ItemRepairs.Add(ıtemsRepair);
					}
				}
				((Component)(object)val2).Dispose();
				((DbDataReader)(object)val3).Close();
				((Component)(object)val).Dispose();
				((DbConnection)(object)val).Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void Reset()
	{
		Set4p = 0;
		ShopAllList.Clear();
		ShopBuyableList.Clear();
		ShopUniqueList.Clear();
		ShopDataMt1.Clear();
		ShopDataMt2.Clear();
		ShopDataGoods.Clear();
		ShopDataItems.Clear();
		ShopDataItemRepairs.Clear();
		ItemRepairs.Clear();
		TotalGoods = 0;
		TotalItems = 0;
		TotalMatching1 = 0;
		TotalMatching2 = 0;
		TotalRepairs = 0;
	}

	private static void smethod_5(int int_0)
	{
		List<GoodsItem> list = new List<GoodsItem>();
		List<GoodsItem> list2 = new List<GoodsItem>();
		lock (ShopAllList)
		{
			foreach (GoodsItem shopAll in ShopAllList)
			{
				if (shopAll.Item.Count != 0)
				{
					if ((shopAll.Tag != ItemTag.PcCafe || int_0 != 0) && ((shopAll.Tag == ItemTag.PcCafe && int_0 > 0) || shopAll.Visibility != 2))
					{
						list.Add(shopAll);
					}
					if (shopAll.Visibility < 2 || shopAll.Visibility == 4)
					{
						list2.Add(shopAll);
					}
				}
			}
		}
		TotalMatching1 = list.Count;
		TotalGoods = list2.Count;
		int num = (int)Math.Ceiling((double)list.Count / 500.0);
		int int_ = 0;
		for (int i = 0; i < num; i++)
		{
			byte[] buffer = smethod_12(500, i, ref int_, list);
			ShopData item = new ShopData
			{
				Buffer = buffer,
				ItemsCount = int_,
				Offset = i * 500
			};
			ShopDataMt1.Add(item);
		}
		num = (int)Math.Ceiling((double)list2.Count / 50.0);
		for (int j = 0; j < num; j++)
		{
			byte[] buffer2 = smethod_10(50, j, ref int_, list2);
			ShopData item2 = new ShopData
			{
				Buffer = buffer2,
				ItemsCount = int_,
				Offset = j * 50
			};
			ShopDataGoods.Add(item2);
		}
	}

	private static void smethod_6(int int_0)
	{
		List<GoodsItem> list = new List<GoodsItem>();
		lock (ShopAllList)
		{
			foreach (GoodsItem shopAll in ShopAllList)
			{
				if (shopAll.Item.Count != 0 && (shopAll.Tag != ItemTag.PcCafe || int_0 != 0) && ((shopAll.Tag == ItemTag.PcCafe && int_0 > 0) || shopAll.Visibility != 2))
				{
					list.Add(shopAll);
				}
			}
		}
		TotalMatching2 = list.Count;
		int num = (int)Math.Ceiling((double)list.Count / 500.0);
		int int_ = 0;
		for (int i = 0; i < num; i++)
		{
			byte[] buffer = smethod_12(500, i, ref int_, list);
			ShopData item = new ShopData
			{
				Buffer = buffer,
				ItemsCount = int_,
				Offset = i * 500
			};
			ShopDataMt2.Add(item);
		}
	}

	private static void smethod_7()
	{
		List<GoodsItem> list = new List<GoodsItem>();
		lock (ShopUniqueList)
		{
			foreach (GoodsItem value in ShopUniqueList.Values)
			{
				if (value.Visibility != 1 && value.Visibility != 3)
				{
					list.Add(value);
				}
			}
		}
		TotalItems = list.Count;
		int num = (int)Math.Ceiling((double)list.Count / 800.0);
		int int_ = 0;
		for (int i = 0; i < num; i++)
		{
			byte[] buffer = smethod_9(800, i, ref int_, list);
			ShopData item = new ShopData
			{
				Buffer = buffer,
				ItemsCount = int_,
				Offset = i * 800
			};
			ShopDataItems.Add(item);
		}
	}

	private static void smethod_8()
	{
		List<ItemsRepair> list = new List<ItemsRepair>();
		lock (ItemRepairs)
		{
			foreach (ItemsRepair ıtemRepair in ItemRepairs)
			{
				list.Add(ıtemRepair);
			}
		}
		TotalRepairs = list.Count;
		int num = (int)Math.Ceiling((double)list.Count / 100.0);
		int int_ = 0;
		for (int i = 0; i < num; i++)
		{
			byte[] buffer = smethod_11(100, i, ref int_, list);
			ShopData item = new ShopData
			{
				Buffer = buffer,
				ItemsCount = int_,
				Offset = i * 100
			};
			ShopDataItemRepairs.Add(item);
		}
	}

	private static byte[] smethod_9(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
	{
		int_2 = 0;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_1 * int_0; i < list_0.Count; i++)
		{
			smethod_13(list_0[i], syncServerPacket);
			if (++int_2 == int_0)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private static byte[] smethod_10(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
	{
		int_2 = 0;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_1 * int_0; i < list_0.Count; i++)
		{
			smethod_14(list_0[i], syncServerPacket);
			if (++int_2 == int_0)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private static byte[] smethod_11(int int_0, int int_1, ref int int_2, List<ItemsRepair> list_0)
	{
		int_2 = 0;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_1 * int_0; i < list_0.Count; i++)
		{
			smethod_15(list_0[i], syncServerPacket);
			if (++int_2 == int_0)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private static byte[] smethod_12(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
	{
		int_2 = 0;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_1 * int_0; i < list_0.Count; i++)
		{
			smethod_16(list_0[i], syncServerPacket);
			if (++int_2 == int_0)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private static void smethod_13(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
		syncServerPacket_0.WriteC((byte)goodsItem_0.AuthType);
		syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType2);
		syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType3);
		syncServerPacket_0.WriteC((byte)goodsItem_0.Title);
		syncServerPacket_0.WriteC((byte)((goodsItem_0.Title != 0) ? 2u : 0u));
		syncServerPacket_0.WriteH(0);
	}

	private static void smethod_14(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(goodsItem_0.Id);
		syncServerPacket_0.WriteC(1);
		syncServerPacket_0.WriteC((byte)((goodsItem_0.Visibility != 4) ? 1u : 4u));
		syncServerPacket_0.WriteD(goodsItem_0.PriceGold);
		syncServerPacket_0.WriteD(goodsItem_0.PriceCash);
		syncServerPacket_0.WriteD(0);
		syncServerPacket_0.WriteC((byte)goodsItem_0.Tag);
		syncServerPacket_0.WriteC(0);
		syncServerPacket_0.WriteC(0);
		syncServerPacket_0.WriteC(0);
		syncServerPacket_0.WriteD((goodsItem_0.StarCash > 0) ? goodsItem_0.StarCash : ((goodsItem_0.StarGold > 0) ? goodsItem_0.StarGold : 0));
		syncServerPacket_0.WriteD(0);
		syncServerPacket_0.WriteD(0);
		syncServerPacket_0.WriteD(0);
		syncServerPacket_0.WriteB(new byte[98]);
	}

	private static void smethod_15(ItemsRepair itemsRepair_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(itemsRepair_0.Id);
		syncServerPacket_0.WriteD((int)(double)((long)itemsRepair_0.Point / (long)itemsRepair_0.Quantity));
		syncServerPacket_0.WriteD((int)(double)((long)itemsRepair_0.Cash / (long)itemsRepair_0.Quantity));
		syncServerPacket_0.WriteD(itemsRepair_0.Quantity);
	}

	private static void smethod_16(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(goodsItem_0.Id);
		syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
		syncServerPacket_0.WriteD(goodsItem_0.Item.Count);
		syncServerPacket_0.WriteD(0);
	}

	public static bool IsRepairableItem(int ItemId)
	{
		return GetRepairItem(ItemId) != null;
	}

	public static ItemsRepair GetRepairItem(int ItemId)
	{
		if (ItemId == 0)
		{
			return null;
		}
		lock (ItemRepairs)
		{
			foreach (ItemsRepair ıtemRepair in ItemRepairs)
			{
				if (ıtemRepair.Id == ItemId)
				{
					return ıtemRepair;
				}
			}
		}
		return null;
	}

	public static bool IsBlocked(string Text, List<int> Items)
	{
		lock (ShopUniqueList)
		{
			foreach (GoodsItem value in ShopUniqueList.Values)
			{
				if (!Items.Contains(value.Item.Id) && value.Item.Name.Contains(Text))
				{
					Items.Add(value.Item.Id);
				}
			}
		}
		return false;
	}

	public static GoodsItem GetGood(int GoodId)
	{
		if (GoodId == 0)
		{
			return null;
		}
		lock (ShopAllList)
		{
			foreach (GoodsItem shopAll in ShopAllList)
			{
				if (shopAll.Id == GoodId)
				{
					return shopAll;
				}
			}
		}
		return null;
	}

	public static GoodsItem GetItemId(int ItemId)
	{
		if (ItemId == 0)
		{
			return null;
		}
		lock (ShopAllList)
		{
			foreach (GoodsItem shopAll in ShopAllList)
			{
				if (shopAll.Item.Id == ItemId)
				{
					return shopAll;
				}
			}
		}
		return null;
	}

	public static List<GoodsItem> GetGoods(List<CartGoods> ShopCart, out int GoldPrice, out int CashPrice, out int TagsPrice)
	{
		GoldPrice = 0;
		CashPrice = 0;
		TagsPrice = 0;
		List<GoodsItem> list = new List<GoodsItem>();
		if (ShopCart.Count == 0)
		{
			return list;
		}
		lock (ShopBuyableList)
		{
			foreach (GoodsItem shopBuyable in ShopBuyableList)
			{
				foreach (CartGoods item in ShopCart)
				{
					if (item.GoodId == shopBuyable.Id)
					{
						list.Add(shopBuyable);
						if (item.BuyType == 1)
						{
							GoldPrice += shopBuyable.PriceGold;
						}
						else if (item.BuyType == 2)
						{
							CashPrice += shopBuyable.PriceCash;
						}
					}
				}
			}
			return list;
		}
	}

	private static void smethod_17()
	{
		string text = "zOne";
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteS(text, text.Length + 1);
		ShopTagData = syncServerPacket.ToArray();
	}
}
