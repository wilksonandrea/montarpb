using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Managers
{
	public static class ShopManager
	{
		public static List<ItemsRepair> ItemRepairs;

		public static List<GoodsItem> ShopAllList;

		public static List<GoodsItem> ShopBuyableList;

		public static SortedList<int, GoodsItem> ShopUniqueList;

		public static List<ShopData> ShopDataMt1;

		public static List<ShopData> ShopDataMt2;

		public static List<ShopData> ShopDataGoods;

		public static List<ShopData> ShopDataItems;

		public static List<ShopData> ShopDataItemRepairs;

		public static byte[] ShopTagData;

		public static int TotalGoods;

		public static int TotalItems;

		public static int TotalMatching1;

		public static int TotalMatching2;

		public static int TotalRepairs;

		public static int Set4p;

		static ShopManager()
		{
			ShopManager.ItemRepairs = new List<ItemsRepair>();
			ShopManager.ShopAllList = new List<GoodsItem>();
			ShopManager.ShopBuyableList = new List<GoodsItem>();
			ShopManager.ShopUniqueList = new SortedList<int, GoodsItem>();
			ShopManager.ShopDataMt1 = new List<ShopData>();
			ShopManager.ShopDataMt2 = new List<ShopData>();
			ShopManager.ShopDataGoods = new List<ShopData>();
			ShopManager.ShopDataItems = new List<ShopData>();
			ShopManager.ShopDataItemRepairs = new List<ShopData>();
		}

		public static GoodsItem GetGood(int GoodId)
		{
			GoodsItem goodsItem;
			if (GoodId == 0)
			{
				return null;
			}
			lock (ShopManager.ShopAllList)
			{
				List<GoodsItem>.Enumerator enumerator = ShopManager.ShopAllList.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						GoodsItem current = enumerator.Current;
						if (current.Id != GoodId)
						{
							continue;
						}
						goodsItem = current;
						return goodsItem;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return goodsItem;
		}

		public static List<GoodsItem> GetGoods(List<CartGoods> ShopCart, out int GoldPrice, out int CashPrice, out int TagsPrice)
		{
			GoldPrice = 0;
			CashPrice = 0;
			TagsPrice = 0;
			List<GoodsItem> goodsItems = new List<GoodsItem>();
			if (ShopCart.Count == 0)
			{
				return goodsItems;
			}
			lock (ShopManager.ShopBuyableList)
			{
				foreach (GoodsItem shopBuyableList in ShopManager.ShopBuyableList)
				{
					foreach (CartGoods shopCart in ShopCart)
					{
						if (shopCart.GoodId != shopBuyableList.Id)
						{
							continue;
						}
						goodsItems.Add(shopBuyableList);
						if (shopCart.BuyType != 1)
						{
							if (shopCart.BuyType != 2)
							{
								continue;
							}
							CashPrice += shopBuyableList.PriceCash;
						}
						else
						{
							GoldPrice += shopBuyableList.PriceGold;
						}
					}
				}
			}
			return goodsItems;
		}

		public static GoodsItem GetItemId(int ItemId)
		{
			GoodsItem goodsItem;
			if (ItemId == 0)
			{
				return null;
			}
			lock (ShopManager.ShopAllList)
			{
				List<GoodsItem>.Enumerator enumerator = ShopManager.ShopAllList.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						GoodsItem current = enumerator.Current;
						if (current.Item.Id != ItemId)
						{
							continue;
						}
						goodsItem = current;
						return goodsItem;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return goodsItem;
		}

		public static ItemsRepair GetRepairItem(int ItemId)
		{
			ItemsRepair ıtemsRepair;
			if (ItemId == 0)
			{
				return null;
			}
			lock (ShopManager.ItemRepairs)
			{
				List<ItemsRepair>.Enumerator enumerator = ShopManager.ItemRepairs.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ItemsRepair current = enumerator.Current;
						if (current.Id != ItemId)
						{
							continue;
						}
						ıtemsRepair = current;
						return ıtemsRepair;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return ıtemsRepair;
		}

		public static bool IsBlocked(string Text, List<int> Items)
		{
			lock (ShopManager.ShopUniqueList)
			{
				foreach (GoodsItem value in ShopManager.ShopUniqueList.Values)
				{
					if (Items.Contains(value.Item.Id) || !value.Item.Name.Contains(Text))
					{
						continue;
					}
					Items.Add(value.Item.Id);
				}
			}
			return false;
		}

		public static bool IsRepairableItem(int ItemId)
		{
			return ShopManager.GetRepairItem(ItemId) != null;
		}

		public static void Load(int Type)
		{
			ShopManager.smethod_4(Type);
			ShopManager.smethod_0(Type);
			ShopManager.smethod_1(Type);
			ShopManager.smethod_2(Type);
			if (Type == 1)
			{
				try
				{
					ShopManager.smethod_5(0);
					ShopManager.smethod_6(1);
					ShopManager.smethod_7();
					ShopManager.smethod_8();
					ShopManager.smethod_17();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(exception.Message, LoggerType.Error, exception);
				}
				CLogger.Print(string.Format("Plugin Loaded: {0} Buyable Items", ShopManager.ShopBuyableList.Count), LoggerType.Info, null);
				CLogger.Print(string.Format("Plugin Loaded: {0} Repairable Items", ShopManager.ItemRepairs.Count), LoggerType.Info, null);
			}
		}

		public static void Reset()
		{
			ShopManager.Set4p = 0;
			ShopManager.ShopAllList.Clear();
			ShopManager.ShopBuyableList.Clear();
			ShopManager.ShopUniqueList.Clear();
			ShopManager.ShopDataMt1.Clear();
			ShopManager.ShopDataMt2.Clear();
			ShopManager.ShopDataGoods.Clear();
			ShopManager.ShopDataItems.Clear();
			ShopManager.ShopDataItemRepairs.Clear();
			ShopManager.ItemRepairs.Clear();
			ShopManager.TotalGoods = 0;
			ShopManager.TotalItems = 0;
			ShopManager.TotalMatching1 = 0;
			ShopManager.TotalMatching2 = 0;
			ShopManager.TotalRepairs = 0;
		}

		private static void smethod_0(int int_0)
		{
			uint uInt32;
			int ınt32;
			int ınt321;
			ItemTag ıtemTag;
			int ınt322;
			string str;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					npgsqlConnection.Open();
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlCommand.CommandText = "SELECT * FROM system_shop";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						int ınt323 = int.Parse(string.Format("{0}", npgsqlDataReader["item_id"]));
						string[] strArrays = (string.Format("{0}", npgsqlDataReader["item_count_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["item_count_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["item_count_list"]) });
						string[] strArrays1 = (string.Format("{0}", npgsqlDataReader["price_cash_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["price_cash_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["price_cash_list"]) });
						string[] strArrays2 = (string.Format("{0}", npgsqlDataReader["price_gold_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["price_gold_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["price_gold_list"]) });
						if ((int)strArrays.Length == (int)strArrays1.Length)
						{
							if ((int)strArrays1.Length == (int)strArrays2.Length)
							{
								int ınt324 = 0;
								string[] strArrays3 = strArrays;
								for (int i = 0; i < (int)strArrays3.Length; i++)
								{
									ınt324++;
									if (!uint.TryParse(strArrays3[i], out uInt32))
									{
										CLogger.Print(string.Format("Loading goods with count != UInt ({0})", ınt323), LoggerType.Warning, null);
									}
									else if (!int.TryParse(strArrays1[ınt324 - 1], out ınt32))
									{
										CLogger.Print(string.Format("Loading goods with cash != Int ({0})", ınt323), LoggerType.Warning, null);
									}
									else if (int.TryParse(strArrays2[ınt324 - 1], out ınt321))
									{
										int ıdStatics = ComDiv.GetIdStatics(ınt323, 1);
										string str1 = string.Format("{0}", npgsqlDataReader["item_name"]);
										GoodsItem goodsItem = new GoodsItem()
										{
											Id = int.Parse(string.Format("{0}{1}", ınt323, (ıdStatics == 22 || ıdStatics == 26 || ıdStatics == 36 || ıdStatics == 37 || ıdStatics == 40 ? "00" : string.Format("{0:D2}", ınt324)))),
											PriceGold = ınt321,
											PriceCash = ınt32
										};
										int ınt325 = int.Parse(string.Format("{0}", npgsqlDataReader["discount_percent"]));
										if (ınt325 > 0 && goodsItem.PriceCash > 0)
										{
											goodsItem.StarCash = goodsItem.PriceCash * 255;
											goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, ınt325);
										}
										if (ınt325 > 0 && goodsItem.PriceGold > 0)
										{
											goodsItem.StarGold = goodsItem.PriceGold * 255;
											goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, ınt325);
										}
										GoodsItem goodsItem1 = goodsItem;
										if (ınt325 > 0)
										{
											ıtemTag = ItemTag.Sale;
										}
										else
										{
											ıtemTag = (ItemTag)int.Parse(string.Format("{0}", npgsqlDataReader["shop_tag"]));
										}
										goodsItem1.Tag = ıtemTag;
										goodsItem.Title = int.Parse(string.Format("{0}", npgsqlDataReader["title_requi"]));
										goodsItem.AuthType = int.Parse(string.Format("{0}", npgsqlDataReader["item_consume"]));
										GoodsItem goodsItem2 = goodsItem;
										if (goodsItem.AuthType == 2)
										{
											ınt322 = 1;
										}
										else
										{
											ınt322 = (ShopManager.IsRepairableItem(ınt323) ? 2 : 1);
										}
										goodsItem2.BuyType2 = ınt322;
										goodsItem.BuyType3 = (goodsItem.AuthType == 1 ? 2 : 1);
										goodsItem.Visibility = (bool.Parse(string.Format("{0}", npgsqlDataReader["item_visible"])) ? 0 : 4);
										goodsItem.Item.SetItemId(ınt323);
										ItemsModel ıtem = goodsItem.Item;
										if (goodsItem.AuthType == 1)
										{
											str = string.Format("{0} ({1} qty)", str1, uInt32);
										}
										else
										{
											str = (goodsItem.AuthType == 2 ? string.Format("{0} ({1} hours)", str1, uInt32 / 3600) : str1);
										}
										ıtem.Name = str;
										goodsItem.Item.Count = uInt32;
										int ıdStatics1 = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
										if (int_0 == 1 || int_0 == 2 && ıdStatics1 == 16)
										{
											ShopManager.ShopAllList.Add(goodsItem);
											if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
											{
												ShopManager.ShopBuyableList.Add(goodsItem);
											}
											if (!ShopManager.ShopUniqueList.ContainsKey(goodsItem.Item.Id) && goodsItem.AuthType > 0)
											{
												ShopManager.ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
												if (goodsItem.Visibility == 4)
												{
													ShopManager.Set4p++;
												}
											}
										}
									}
									else
									{
										CLogger.Print(string.Format("Loading goods with gold != Int ({0})", ınt323), LoggerType.Warning, null);
									}
								}
								continue;
							}
						}
						CLogger.Print(string.Format("Loading goods with invalid counts / moneys / points sizes. ({0})", ınt323), LoggerType.Warning, null);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static void smethod_1(int int_0)
		{
			int ınt32;
			int ınt321;
			int ınt322;
			ItemTag ıtemTag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					npgsqlConnection.Open();
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlCommand.CommandText = "SELECT * FROM system_shop_effects";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						int ınt323 = int.Parse(string.Format("{0}", npgsqlDataReader["coupon_id"]));
						string[] strArrays = (string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]) });
						string[] strArrays1 = (string.Format("{0}", npgsqlDataReader["price_cash_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["price_cash_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["price_cash_list"]) });
						string[] strArrays2 = (string.Format("{0}", npgsqlDataReader["price_gold_list"]).Contains(",") ? string.Format("{0}", npgsqlDataReader["price_gold_list"]).Split(new char[] { ',' }) : new string[] { string.Format("{0}", npgsqlDataReader["price_gold_list"]) });
						if ((int)strArrays.Length == (int)strArrays1.Length)
						{
							if ((int)strArrays1.Length == (int)strArrays2.Length)
							{
								int ınt324 = 0;
								string[] strArrays3 = strArrays;
								for (int i = 0; i < (int)strArrays3.Length; i++)
								{
									ınt324++;
									if (!int.TryParse(strArrays3[i], out ınt32))
									{
										CLogger.Print(string.Format("Loading effects with count != Int ({0})", ınt323), LoggerType.Warning, null);
									}
									else if (!int.TryParse(strArrays1[ınt324 - 1], out ınt321))
									{
										CLogger.Print(string.Format("Loading effects with cash != Int ({0})", ınt323), LoggerType.Warning, null);
									}
									else if (int.TryParse(strArrays2[ınt324 - 1], out ınt322))
									{
										if (ınt32 >= 100)
										{
											ınt32 = 100;
										}
										int ınt325 = int.Parse(string.Format("{0}{1:D2}{2}", string.Format("{0}", ınt323).Substring(0, 2), ınt32, string.Format("{0}", ınt323).Substring(4, 3)));
										GoodsItem goodsItem = new GoodsItem()
										{
											Id = int.Parse(string.Format("{0}{1:D2}", ınt323, ınt324)),
											PriceGold = ınt322,
											PriceCash = ınt321
										};
										int ınt326 = int.Parse(string.Format("{0}", npgsqlDataReader["discount_percent"]));
										if (ınt326 > 0 && goodsItem.PriceCash > 0)
										{
											goodsItem.StarCash = goodsItem.PriceCash * 255;
											goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, ınt326);
										}
										if (ınt326 > 0 && goodsItem.PriceGold > 0)
										{
											goodsItem.PriceGold = goodsItem.PriceGold * 255;
											goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, ınt326);
										}
										GoodsItem goodsItem1 = goodsItem;
										if (ınt326 > 0)
										{
											ıtemTag = ItemTag.Sale;
										}
										else
										{
											ıtemTag = (ItemTag)int.Parse(string.Format("{0}", npgsqlDataReader["shop_tag"]));
										}
										goodsItem1.Tag = ıtemTag;
										goodsItem.Title = 0;
										goodsItem.AuthType = 1;
										goodsItem.BuyType2 = 1;
										goodsItem.BuyType3 = 2;
										goodsItem.Visibility = (bool.Parse(string.Format("{0}", npgsqlDataReader["coupon_visible"])) ? 0 : 4);
										goodsItem.Item.SetItemId(ınt325);
										goodsItem.Item.Name = string.Format("{0} ({1} days)", npgsqlDataReader["coupon_name"], ınt32);
										goodsItem.Item.Count = 1;
										int ıdStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
										if (int_0 == 1 || int_0 == 2 && ıdStatics == 16)
										{
											ShopManager.ShopAllList.Add(goodsItem);
											if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
											{
												ShopManager.ShopBuyableList.Add(goodsItem);
											}
											if (!ShopManager.ShopUniqueList.ContainsKey(goodsItem.Item.Id) && goodsItem.AuthType > 0)
											{
												ShopManager.ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
												if (goodsItem.Visibility == 4)
												{
													ShopManager.Set4p++;
												}
											}
										}
									}
									else
									{
										CLogger.Print(string.Format("Loading effects with gold != Int ({0})", ınt323), LoggerType.Warning, null);
									}
								}
								continue;
							}
						}
						CLogger.Print(string.Format("Loading goods with invalid counts / moneys / points sizes. ({0})", ınt323), LoggerType.Warning, null);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static byte[] smethod_10(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			byte[] array;
			int_2 = 0;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_14(list_0[i], syncServerPacket);
					int int2 = int_2 + 1;
					int_2 = int2;
					if (int2 == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private static byte[] smethod_11(int int_0, int int_1, ref int int_2, List<ItemsRepair> list_0)
		{
			byte[] array;
			int_2 = 0;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_15(list_0[i], syncServerPacket);
					int int2 = int_2 + 1;
					int_2 = int2;
					if (int2 == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private static byte[] smethod_12(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			byte[] array;
			int_2 = 0;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_16(list_0[i], syncServerPacket);
					int int2 = int_2 + 1;
					int_2 = int2;
					if (int2 == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private static void smethod_13(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			object obj;
			syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
			syncServerPacket_0.WriteC((byte)goodsItem_0.AuthType);
			syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType2);
			syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType3);
			syncServerPacket_0.WriteC((byte)goodsItem_0.Title);
			SyncServerPacket syncServerPacket0 = syncServerPacket_0;
			if (goodsItem_0.Title != 0)
			{
				obj = 2;
			}
			else
			{
				obj = null;
			}
			syncServerPacket0.WriteC((byte)obj);
			syncServerPacket_0.WriteH(0);
		}

		private static void smethod_14(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			int starCash;
			syncServerPacket_0.WriteD(goodsItem_0.Id);
			syncServerPacket_0.WriteC(1);
			syncServerPacket_0.WriteC((byte)((goodsItem_0.Visibility == 4 ? 4 : 1)));
			syncServerPacket_0.WriteD(goodsItem_0.PriceGold);
			syncServerPacket_0.WriteD(goodsItem_0.PriceCash);
			syncServerPacket_0.WriteD(0);
			syncServerPacket_0.WriteC((byte)goodsItem_0.Tag);
			syncServerPacket_0.WriteC(0);
			syncServerPacket_0.WriteC(0);
			syncServerPacket_0.WriteC(0);
			SyncServerPacket syncServerPacket0 = syncServerPacket_0;
			if (goodsItem_0.StarCash > 0)
			{
				starCash = goodsItem_0.StarCash;
			}
			else
			{
				starCash = (goodsItem_0.StarGold > 0 ? goodsItem_0.StarGold : 0);
			}
			syncServerPacket0.WriteD(starCash);
			syncServerPacket_0.WriteD(0);
			syncServerPacket_0.WriteD(0);
			syncServerPacket_0.WriteD(0);
			syncServerPacket_0.WriteB(new byte[98]);
		}

		private static void smethod_15(ItemsRepair itemsRepair_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(itemsRepair_0.Id);
			syncServerPacket_0.WriteD((int)((double)((long)itemsRepair_0.Point / (ulong)itemsRepair_0.Quantity)));
			syncServerPacket_0.WriteD((int)((double)((long)itemsRepair_0.Cash / (ulong)itemsRepair_0.Quantity)));
			syncServerPacket_0.WriteD(itemsRepair_0.Quantity);
		}

		private static void smethod_16(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(goodsItem_0.Id);
			syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
			syncServerPacket_0.WriteD(goodsItem_0.Item.Count);
			syncServerPacket_0.WriteD(0);
		}

		private static void smethod_17()
		{
			string str = "zOne";
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteS(str, str.Length + 1);
				ShopManager.ShopTagData = syncServerPacket.ToArray();
			}
		}

		private static void smethod_2(int int_0)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					npgsqlConnection.Open();
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlCommand.CommandText = string.Format("SELECT * FROM system_shop_sets WHERE visible = '{0}';", true);
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						int ınt32 = int.Parse(string.Format("{0}", npgsqlDataReader["id"]));
						string str = string.Format("{0}", npgsqlDataReader["name"]);
						ShopManager.smethod_3(ınt32, str, int_0);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static void smethod_3(int int_0, string string_0, int int_1)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					npgsqlConnection.Open();
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlCommand.CommandText = string.Format("SELECT * FROM system_shop_sets_items WHERE set_id = '{0}' AND set_name = '{1}';", int_0, string_0);
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						int ınt32 = int.Parse(string.Format("{0}", npgsqlDataReader["id"]));
						string str = string.Format("{0}", npgsqlDataReader["name"]);
						int ınt321 = int.Parse(string.Format("{0}", npgsqlDataReader["consume"]));
						uint uInt32 = uint.Parse(string.Format("{0}", npgsqlDataReader["count"]));
						int ınt322 = int.Parse(string.Format("{0}", npgsqlDataReader["price_gold"]));
						int ınt323 = int.Parse(string.Format("{0}", npgsqlDataReader["price_cash"]));
						GoodsItem goodsItem = new GoodsItem()
						{
							Id = int_0,
							PriceGold = ınt322,
							PriceCash = ınt323,
							Tag = ItemTag.Hot,
							Title = 0,
							AuthType = 0,
							BuyType2 = 1,
							BuyType3 = (ınt321 == 1 ? 2 : 1),
							Visibility = 4
						};
						goodsItem.Item.SetItemId(ınt32);
						goodsItem.Item.Name = str;
						goodsItem.Item.Count = uInt32;
						int ıdStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
						if (int_1 != 1 && (int_1 != 2 || ıdStatics != 16))
						{
							continue;
						}
						ShopManager.ShopAllList.Add(goodsItem);
						if (goodsItem.Visibility != 2 && goodsItem.Visibility != 4)
						{
							ShopManager.ShopBuyableList.Add(goodsItem);
						}
						if (ShopManager.ShopUniqueList.ContainsKey(goodsItem.Item.Id) || goodsItem.AuthType <= 0)
						{
							continue;
						}
						ShopManager.ShopUniqueList.Add(goodsItem.Item.Id, goodsItem);
						if (goodsItem.Visibility != 4)
						{
							continue;
						}
						ShopManager.Set4p++;
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static void smethod_4(int int_0)
		{
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					npgsqlConnection.Open();
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlCommand.CommandText = "SELECT * FROM system_shop_repair";
					npgsqlCommand.CommandType = CommandType.Text;
					NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader(CommandBehavior.Default);
					while (npgsqlDataReader.Read())
					{
						ItemsRepair ıtemsRepair = new ItemsRepair()
						{
							Id = int.Parse(string.Format("{0}", npgsqlDataReader["item_id"])),
							Point = int.Parse(string.Format("{0}", npgsqlDataReader["price_gold"])),
							Cash = int.Parse(string.Format("{0}", npgsqlDataReader["price_cash"])),
							Quantity = uint.Parse(string.Format("{0}", npgsqlDataReader["quantity"])),
							Enable = bool.Parse(string.Format("{0}", npgsqlDataReader["repairable"]))
						};
						if (int_0 != 1 || !ıtemsRepair.Enable || ıtemsRepair.Quantity > 100)
						{
							continue;
						}
						ShopManager.ItemRepairs.Add(ıtemsRepair);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static void smethod_5(int int_0)
		{
			List<GoodsItem> goodsItems = new List<GoodsItem>();
			List<GoodsItem> goodsItems1 = new List<GoodsItem>();
			lock (ShopManager.ShopAllList)
			{
				foreach (GoodsItem shopAllList in ShopManager.ShopAllList)
				{
					if (shopAllList.Item.Count == 0)
					{
						continue;
					}
					if ((shopAllList.Tag != ItemTag.PcCafe || int_0 != 0) && (shopAllList.Tag == ItemTag.PcCafe && int_0 > 0 || shopAllList.Visibility != 2))
					{
						goodsItems.Add(shopAllList);
					}
					if (shopAllList.Visibility >= 2 && shopAllList.Visibility != 4)
					{
						continue;
					}
					goodsItems1.Add(shopAllList);
				}
			}
			ShopManager.TotalMatching1 = goodsItems.Count;
			ShopManager.TotalGoods = goodsItems1.Count;
			int ınt32 = (int)Math.Ceiling((double)goodsItems.Count / 500);
			int ınt321 = 0;
			for (int i = 0; i < ınt32; i++)
			{
				byte[] numArray = ShopManager.smethod_12(500, i, ref ınt321, goodsItems);
				ShopData shopDatum = new ShopData()
				{
					Buffer = numArray,
					ItemsCount = ınt321,
					Offset = i * 500
				};
				ShopManager.ShopDataMt1.Add(shopDatum);
			}
			ınt32 = (int)Math.Ceiling((double)goodsItems1.Count / 50);
			for (int j = 0; j < ınt32; j++)
			{
				byte[] numArray1 = ShopManager.smethod_10(50, j, ref ınt321, goodsItems1);
				ShopData shopDatum1 = new ShopData()
				{
					Buffer = numArray1,
					ItemsCount = ınt321,
					Offset = j * 50
				};
				ShopManager.ShopDataGoods.Add(shopDatum1);
			}
		}

		private static void smethod_6(int int_0)
		{
			List<GoodsItem> goodsItems = new List<GoodsItem>();
			lock (ShopManager.ShopAllList)
			{
				foreach (GoodsItem shopAllList in ShopManager.ShopAllList)
				{
					if (shopAllList.Item.Count == 0 || shopAllList.Tag == ItemTag.PcCafe && int_0 == 0 || (shopAllList.Tag != ItemTag.PcCafe || int_0 <= 0) && shopAllList.Visibility == 2)
					{
						continue;
					}
					goodsItems.Add(shopAllList);
				}
			}
			ShopManager.TotalMatching2 = goodsItems.Count;
			int ınt32 = (int)Math.Ceiling((double)goodsItems.Count / 500);
			int ınt321 = 0;
			for (int i = 0; i < ınt32; i++)
			{
				byte[] numArray = ShopManager.smethod_12(500, i, ref ınt321, goodsItems);
				ShopData shopDatum = new ShopData()
				{
					Buffer = numArray,
					ItemsCount = ınt321,
					Offset = i * 500
				};
				ShopManager.ShopDataMt2.Add(shopDatum);
			}
		}

		private static void smethod_7()
		{
			List<GoodsItem> goodsItems = new List<GoodsItem>();
			lock (ShopManager.ShopUniqueList)
			{
				foreach (GoodsItem value in ShopManager.ShopUniqueList.Values)
				{
					if (value.Visibility == 1 || value.Visibility == 3)
					{
						continue;
					}
					goodsItems.Add(value);
				}
			}
			ShopManager.TotalItems = goodsItems.Count;
			int ınt32 = (int)Math.Ceiling((double)goodsItems.Count / 800);
			int ınt321 = 0;
			for (int i = 0; i < ınt32; i++)
			{
				byte[] numArray = ShopManager.smethod_9(800, i, ref ınt321, goodsItems);
				ShopData shopDatum = new ShopData()
				{
					Buffer = numArray,
					ItemsCount = ınt321,
					Offset = i * 800
				};
				ShopManager.ShopDataItems.Add(shopDatum);
			}
		}

		private static void smethod_8()
		{
			List<ItemsRepair> ıtemsRepairs = new List<ItemsRepair>();
			lock (ShopManager.ItemRepairs)
			{
				foreach (ItemsRepair ıtemRepair in ShopManager.ItemRepairs)
				{
					ıtemsRepairs.Add(ıtemRepair);
				}
			}
			ShopManager.TotalRepairs = ıtemsRepairs.Count;
			int ınt32 = (int)Math.Ceiling((double)ıtemsRepairs.Count / 100);
			int ınt321 = 0;
			for (int i = 0; i < ınt32; i++)
			{
				byte[] numArray = ShopManager.smethod_11(100, i, ref ınt321, ıtemsRepairs);
				ShopData shopDatum = new ShopData()
				{
					Buffer = numArray,
					ItemsCount = ınt321,
					Offset = i * 100
				};
				ShopManager.ShopDataItemRepairs.Add(shopDatum);
			}
		}

		private static byte[] smethod_9(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			byte[] array;
			int_2 = 0;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_13(list_0[i], syncServerPacket);
					int int2 = int_2 + 1;
					int_2 = int2;
					if (int2 == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			return list.Select((T gparam_0, int int_0) => new { item = gparam_0, inx = int_0 }).GroupBy((class0_0) => class0_0.inx / limit).Select((igrouping_0) => 
				from  in igrouping_0
				select class0_0.item);
		}
	}
}