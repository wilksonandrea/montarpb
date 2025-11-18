using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Npgsql;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;

namespace Plugin.Core.Managers
{
	// Token: 0x020000A4 RID: 164
	public static class ShopManager
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			ShopManager.Class14<T> @class = new ShopManager.Class14<T>();
			@class.int_0 = limit;
			return list.Select(new Func<T, int, Class0<T, int>>(ShopManager.Class13<T>.<>9.method_0)).GroupBy(new Func<Class0<T, int>, int>(@class.method_0)).Select(new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(ShopManager.Class13<T>.<>9.method_1));
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001D44C File Offset: 0x0001B64C
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
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
				CLogger.Print(string.Format("Plugin Loaded: {0} Buyable Items", ShopManager.ShopBuyableList.Count), LoggerType.Info, null);
				CLogger.Print(string.Format("Plugin Loaded: {0} Repairable Items", ShopManager.ItemRepairs.Count), LoggerType.Info, null);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		private static void smethod_0(int int_0)
		{
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
						int num = int.Parse(string.Format("{0}", npgsqlDataReader["item_id"]));
						string[] array;
						if (!string.Format("{0}", npgsqlDataReader["item_count_list"]).Contains(","))
						{
							(array = new string[1])[0] = string.Format("{0}", npgsqlDataReader["item_count_list"]);
						}
						else
						{
							array = string.Format("{0}", npgsqlDataReader["item_count_list"]).Split(new char[] { ',' });
						}
						string[] array2 = array;
						string[] array3;
						if (!string.Format("{0}", npgsqlDataReader["price_cash_list"]).Contains(","))
						{
							(array3 = new string[1])[0] = string.Format("{0}", npgsqlDataReader["price_cash_list"]);
						}
						else
						{
							array3 = string.Format("{0}", npgsqlDataReader["price_cash_list"]).Split(new char[] { ',' });
						}
						string[] array4 = array3;
						string[] array5;
						if (!string.Format("{0}", npgsqlDataReader["price_gold_list"]).Contains(","))
						{
							(array5 = new string[1])[0] = string.Format("{0}", npgsqlDataReader["price_gold_list"]);
						}
						else
						{
							array5 = string.Format("{0}", npgsqlDataReader["price_gold_list"]).Split(new char[] { ',' });
						}
						string[] array6 = array5;
						if (array2.Length == array4.Length)
						{
							if (array4.Length == array6.Length)
							{
								int num2 = 0;
								foreach (string text in array2)
								{
									num2++;
									uint num3;
									int num4;
									int num5;
									if (!uint.TryParse(text, out num3))
									{
										CLogger.Print(string.Format("Loading goods with count != UInt ({0})", num), LoggerType.Warning, null);
									}
									else if (!int.TryParse(array4[num2 - 1], out num4))
									{
										CLogger.Print(string.Format("Loading goods with cash != Int ({0})", num), LoggerType.Warning, null);
									}
									else if (!int.TryParse(array6[num2 - 1], out num5))
									{
										CLogger.Print(string.Format("Loading goods with gold != Int ({0})", num), LoggerType.Warning, null);
									}
									else
									{
										int idStatics = ComDiv.GetIdStatics(num, 1);
										string text2 = string.Format("{0}", npgsqlDataReader["item_name"]);
										GoodsItem goodsItem = new GoodsItem
										{
											Id = int.Parse(string.Format("{0}{1}", num, (idStatics == 22 || idStatics == 26 || idStatics == 36 || idStatics == 37 || idStatics == 40) ? "00" : string.Format("{0:D2}", num2))),
											PriceGold = num5,
											PriceCash = num4
										};
										int num6 = int.Parse(string.Format("{0}", npgsqlDataReader["discount_percent"]));
										if (num6 > 0 && goodsItem.PriceCash > 0)
										{
											goodsItem.StarCash = goodsItem.PriceCash * 255;
											goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num6);
										}
										if (num6 > 0 && goodsItem.PriceGold > 0)
										{
											goodsItem.StarGold = goodsItem.PriceGold * 255;
											goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num6);
										}
										goodsItem.Tag = (ItemTag)((num6 > 0) ? 5 : int.Parse(string.Format("{0}", npgsqlDataReader["shop_tag"])));
										goodsItem.Title = int.Parse(string.Format("{0}", npgsqlDataReader["title_requi"]));
										goodsItem.AuthType = int.Parse(string.Format("{0}", npgsqlDataReader["item_consume"]));
										goodsItem.BuyType2 = ((goodsItem.AuthType == 2) ? 1 : (ShopManager.IsRepairableItem(num) ? 2 : 1));
										goodsItem.BuyType3 = ((goodsItem.AuthType == 1) ? 2 : 1);
										goodsItem.Visibility = (bool.Parse(string.Format("{0}", npgsqlDataReader["item_visible"])) ? 0 : 4);
										goodsItem.Item.SetItemId(num);
										goodsItem.Item.Name = ((goodsItem.AuthType == 1) ? string.Format("{0} ({1} qty)", text2, num3) : ((goodsItem.AuthType == 2) ? string.Format("{0} ({1} hours)", text2, num3 / 3600U) : text2));
										goodsItem.Item.Count = num3;
										int idStatics2 = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
										if (int_0 == 1 || (int_0 == 2 && idStatics2 == 16))
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
								}
								continue;
							}
						}
						CLogger.Print(string.Format("Loading goods with invalid counts / moneys / points sizes. ({0})", num), LoggerType.Warning, null);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001DACC File Offset: 0x0001BCCC
		private static void smethod_1(int int_0)
		{
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
						int num = int.Parse(string.Format("{0}", npgsqlDataReader["coupon_id"]));
						string[] array;
						if (!string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]).Contains(","))
						{
							(array = new string[1])[0] = string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]);
						}
						else
						{
							array = string.Format("{0}", npgsqlDataReader["coupon_count_day_list"]).Split(new char[] { ',' });
						}
						string[] array2 = array;
						string[] array3;
						if (!string.Format("{0}", npgsqlDataReader["price_cash_list"]).Contains(","))
						{
							(array3 = new string[1])[0] = string.Format("{0}", npgsqlDataReader["price_cash_list"]);
						}
						else
						{
							array3 = string.Format("{0}", npgsqlDataReader["price_cash_list"]).Split(new char[] { ',' });
						}
						string[] array4 = array3;
						string[] array5;
						if (!string.Format("{0}", npgsqlDataReader["price_gold_list"]).Contains(","))
						{
							(array5 = new string[1])[0] = string.Format("{0}", npgsqlDataReader["price_gold_list"]);
						}
						else
						{
							array5 = string.Format("{0}", npgsqlDataReader["price_gold_list"]).Split(new char[] { ',' });
						}
						string[] array6 = array5;
						if (array2.Length == array4.Length)
						{
							if (array4.Length == array6.Length)
							{
								int num2 = 0;
								foreach (string text in array2)
								{
									num2++;
									int num3;
									int num4;
									int num5;
									if (!int.TryParse(text, out num3))
									{
										CLogger.Print(string.Format("Loading effects with count != Int ({0})", num), LoggerType.Warning, null);
									}
									else if (!int.TryParse(array4[num2 - 1], out num4))
									{
										CLogger.Print(string.Format("Loading effects with cash != Int ({0})", num), LoggerType.Warning, null);
									}
									else if (!int.TryParse(array6[num2 - 1], out num5))
									{
										CLogger.Print(string.Format("Loading effects with gold != Int ({0})", num), LoggerType.Warning, null);
									}
									else
									{
										if (num3 >= 100)
										{
											num3 = 100;
										}
										int num6 = int.Parse(string.Format("{0}{1:D2}{2}", string.Format("{0}", num).Substring(0, 2), num3, string.Format("{0}", num).Substring(4, 3)));
										GoodsItem goodsItem = new GoodsItem
										{
											Id = int.Parse(string.Format("{0}{1:D2}", num, num2)),
											PriceGold = num5,
											PriceCash = num4
										};
										int num7 = int.Parse(string.Format("{0}", npgsqlDataReader["discount_percent"]));
										if (num7 > 0 && goodsItem.PriceCash > 0)
										{
											goodsItem.StarCash = goodsItem.PriceCash * 255;
											goodsItem.PriceCash = ComDiv.Percentage(goodsItem.PriceCash, num7);
										}
										if (num7 > 0 && goodsItem.PriceGold > 0)
										{
											goodsItem.PriceGold *= 255;
											goodsItem.PriceGold = ComDiv.Percentage(goodsItem.PriceGold, num7);
										}
										goodsItem.Tag = (ItemTag)((num7 > 0) ? 5 : int.Parse(string.Format("{0}", npgsqlDataReader["shop_tag"])));
										goodsItem.Title = 0;
										goodsItem.AuthType = 1;
										goodsItem.BuyType2 = 1;
										goodsItem.BuyType3 = 2;
										goodsItem.Visibility = (bool.Parse(string.Format("{0}", npgsqlDataReader["coupon_visible"])) ? 0 : 4);
										goodsItem.Item.SetItemId(num6);
										goodsItem.Item.Name = string.Format("{0} ({1} days)", npgsqlDataReader["coupon_name"], num3);
										goodsItem.Item.Count = 1U;
										int idStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
										if (int_0 == 1 || (int_0 == 2 && idStatics == 16))
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
								}
								continue;
							}
						}
						CLogger.Print(string.Format("Loading goods with invalid counts / moneys / points sizes. ({0})", num), LoggerType.Warning, null);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001E024 File Offset: 0x0001C224
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
						int num = int.Parse(string.Format("{0}", npgsqlDataReader["id"]));
						string text = string.Format("{0}", npgsqlDataReader["name"]);
						ShopManager.smethod_3(num, text, int_0);
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001E104 File Offset: 0x0001C304
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
						int num = int.Parse(string.Format("{0}", npgsqlDataReader["id"]));
						string text = string.Format("{0}", npgsqlDataReader["name"]);
						int num2 = int.Parse(string.Format("{0}", npgsqlDataReader["consume"]));
						uint num3 = uint.Parse(string.Format("{0}", npgsqlDataReader["count"]));
						int num4 = int.Parse(string.Format("{0}", npgsqlDataReader["price_gold"]));
						int num5 = int.Parse(string.Format("{0}", npgsqlDataReader["price_cash"]));
						GoodsItem goodsItem = new GoodsItem
						{
							Id = int_0,
							PriceGold = num4,
							PriceCash = num5,
							Tag = ItemTag.Hot,
							Title = 0,
							AuthType = 0,
							BuyType2 = 1,
							BuyType3 = ((num2 == 1) ? 2 : 1),
							Visibility = 4
						};
						goodsItem.Item.SetItemId(num);
						goodsItem.Item.Name = text;
						goodsItem.Item.Count = num3;
						int idStatics = ComDiv.GetIdStatics(goodsItem.Item.Id, 1);
						if (int_1 == 1 || (int_1 == 2 && idStatics == 16))
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
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001E388 File Offset: 0x0001C588
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
						ItemsRepair itemsRepair = new ItemsRepair
						{
							Id = int.Parse(string.Format("{0}", npgsqlDataReader["item_id"])),
							Point = int.Parse(string.Format("{0}", npgsqlDataReader["price_gold"])),
							Cash = int.Parse(string.Format("{0}", npgsqlDataReader["price_cash"])),
							Quantity = uint.Parse(string.Format("{0}", npgsqlDataReader["quantity"])),
							Enable = bool.Parse(string.Format("{0}", npgsqlDataReader["repairable"]))
						};
						if (int_0 == 1 && itemsRepair.Enable && itemsRepair.Quantity <= 100U)
						{
							ShopManager.ItemRepairs.Add(itemsRepair);
						}
					}
					npgsqlCommand.Dispose();
					npgsqlDataReader.Close();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001E50C File Offset: 0x0001C70C
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

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001E598 File Offset: 0x0001C798
		private static void smethod_5(int int_0)
		{
			List<GoodsItem> list = new List<GoodsItem>();
			List<GoodsItem> list2 = new List<GoodsItem>();
			List<GoodsItem> shopAllList = ShopManager.ShopAllList;
			lock (shopAllList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopAllList)
				{
					if (goodsItem.Item.Count != 0U)
					{
						if ((goodsItem.Tag != ItemTag.PcCafe || int_0 != 0) && ((goodsItem.Tag == ItemTag.PcCafe && int_0 > 0) || goodsItem.Visibility != 2))
						{
							list.Add(goodsItem);
						}
						if (goodsItem.Visibility < 2 || goodsItem.Visibility == 4)
						{
							list2.Add(goodsItem);
						}
					}
				}
			}
			ShopManager.TotalMatching1 = list.Count;
			ShopManager.TotalGoods = list2.Count;
			int num = (int)Math.Ceiling((double)list.Count / 500.0);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte[] array = ShopManager.smethod_12(500, i, ref num2, list);
				ShopData shopData = new ShopData
				{
					Buffer = array,
					ItemsCount = num2,
					Offset = i * 500
				};
				ShopManager.ShopDataMt1.Add(shopData);
			}
			num = (int)Math.Ceiling((double)list2.Count / 50.0);
			for (int j = 0; j < num; j++)
			{
				byte[] array2 = ShopManager.smethod_10(50, j, ref num2, list2);
				ShopData shopData2 = new ShopData
				{
					Buffer = array2,
					ItemsCount = num2,
					Offset = j * 50
				};
				ShopManager.ShopDataGoods.Add(shopData2);
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001E758 File Offset: 0x0001C958
		private static void smethod_6(int int_0)
		{
			List<GoodsItem> list = new List<GoodsItem>();
			List<GoodsItem> shopAllList = ShopManager.ShopAllList;
			lock (shopAllList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopAllList)
				{
					if (goodsItem.Item.Count != 0U && (goodsItem.Tag != ItemTag.PcCafe || int_0 != 0) && ((goodsItem.Tag == ItemTag.PcCafe && int_0 > 0) || goodsItem.Visibility != 2))
					{
						list.Add(goodsItem);
					}
				}
			}
			ShopManager.TotalMatching2 = list.Count;
			int num = (int)Math.Ceiling((double)list.Count / 500.0);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte[] array = ShopManager.smethod_12(500, i, ref num2, list);
				ShopData shopData = new ShopData
				{
					Buffer = array,
					ItemsCount = num2,
					Offset = i * 500
				};
				ShopManager.ShopDataMt2.Add(shopData);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001E888 File Offset: 0x0001CA88
		private static void smethod_7()
		{
			List<GoodsItem> list = new List<GoodsItem>();
			SortedList<int, GoodsItem> shopUniqueList = ShopManager.ShopUniqueList;
			lock (shopUniqueList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopUniqueList.Values)
				{
					if (goodsItem.Visibility != 1 && goodsItem.Visibility != 3)
					{
						list.Add(goodsItem);
					}
				}
			}
			ShopManager.TotalItems = list.Count;
			int num = (int)Math.Ceiling((double)list.Count / 800.0);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte[] array = ShopManager.smethod_9(800, i, ref num2, list);
				ShopData shopData = new ShopData
				{
					Buffer = array,
					ItemsCount = num2,
					Offset = i * 800
				};
				ShopManager.ShopDataItems.Add(shopData);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E99C File Offset: 0x0001CB9C
		private static void smethod_8()
		{
			List<ItemsRepair> list = new List<ItemsRepair>();
			List<ItemsRepair> itemRepairs = ShopManager.ItemRepairs;
			lock (itemRepairs)
			{
				foreach (ItemsRepair itemsRepair in ShopManager.ItemRepairs)
				{
					list.Add(itemsRepair);
				}
			}
			ShopManager.TotalRepairs = list.Count;
			int num = (int)Math.Ceiling((double)list.Count / 100.0);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte[] array = ShopManager.smethod_11(100, i, ref num2, list);
				ShopData shopData = new ShopData
				{
					Buffer = array,
					ItemsCount = num2,
					Offset = i * 100
				};
				ShopManager.ShopDataItemRepairs.Add(shopData);
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001EA90 File Offset: 0x0001CC90
		private static byte[] smethod_9(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			int_2 = 0;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_13(list_0[i], syncServerPacket);
					int num = int_2 + 1;
					int_2 = num;
					if (num == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		private static byte[] smethod_10(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			int_2 = 0;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_14(list_0[i], syncServerPacket);
					int num = int_2 + 1;
					int_2 = num;
					if (num == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001EB60 File Offset: 0x0001CD60
		private static byte[] smethod_11(int int_0, int int_1, ref int int_2, List<ItemsRepair> list_0)
		{
			int_2 = 0;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_15(list_0[i], syncServerPacket);
					int num = int_2 + 1;
					int_2 = num;
					if (num == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001EBC8 File Offset: 0x0001CDC8
		private static byte[] smethod_12(int int_0, int int_1, ref int int_2, List<GoodsItem> list_0)
		{
			int_2 = 0;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					ShopManager.smethod_16(list_0[i], syncServerPacket);
					int num = int_2 + 1;
					int_2 = num;
					if (num == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001EC30 File Offset: 0x0001CE30
		private static void smethod_13(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
			syncServerPacket_0.WriteC((byte)goodsItem_0.AuthType);
			syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType2);
			syncServerPacket_0.WriteC((byte)goodsItem_0.BuyType3);
			syncServerPacket_0.WriteC((byte)goodsItem_0.Title);
			syncServerPacket_0.WriteC((goodsItem_0.Title != 0) ? 2 : 0);
			syncServerPacket_0.WriteH(0);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		private static void smethod_14(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(goodsItem_0.Id);
			syncServerPacket_0.WriteC(1);
			syncServerPacket_0.WriteC((goodsItem_0.Visibility == 4) ? 4 : 1);
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

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		private static void smethod_15(ItemsRepair itemsRepair_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(itemsRepair_0.Id);
			syncServerPacket_0.WriteD((int)((double)((long)itemsRepair_0.Point / (long)((ulong)itemsRepair_0.Quantity))));
			syncServerPacket_0.WriteD((int)((double)((long)itemsRepair_0.Cash / (long)((ulong)itemsRepair_0.Quantity))));
			syncServerPacket_0.WriteD(itemsRepair_0.Quantity);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000065E9 File Offset: 0x000047E9
		private static void smethod_16(GoodsItem goodsItem_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(goodsItem_0.Id);
			syncServerPacket_0.WriteD(goodsItem_0.Item.Id);
			syncServerPacket_0.WriteD(goodsItem_0.Item.Count);
			syncServerPacket_0.WriteD(0);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00006620 File Offset: 0x00004820
		public static bool IsRepairableItem(int ItemId)
		{
			return ShopManager.GetRepairItem(ItemId) != null;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		public static ItemsRepair GetRepairItem(int ItemId)
		{
			if (ItemId == 0)
			{
				return null;
			}
			List<ItemsRepair> itemRepairs = ShopManager.ItemRepairs;
			lock (itemRepairs)
			{
				foreach (ItemsRepair itemsRepair in ShopManager.ItemRepairs)
				{
					if (itemsRepair.Id == ItemId)
					{
						return itemsRepair;
					}
				}
			}
			return null;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public static bool IsBlocked(string Text, List<int> Items)
		{
			SortedList<int, GoodsItem> shopUniqueList = ShopManager.ShopUniqueList;
			lock (shopUniqueList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopUniqueList.Values)
				{
					if (!Items.Contains(goodsItem.Item.Id) && goodsItem.Item.Name.Contains(Text))
					{
						Items.Add(goodsItem.Item.Id);
					}
				}
			}
			return false;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public static GoodsItem GetGood(int GoodId)
		{
			if (GoodId == 0)
			{
				return null;
			}
			List<GoodsItem> shopAllList = ShopManager.ShopAllList;
			lock (shopAllList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopAllList)
				{
					if (goodsItem.Id == GoodId)
					{
						return goodsItem;
					}
				}
			}
			return null;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001EF74 File Offset: 0x0001D174
		public static GoodsItem GetItemId(int ItemId)
		{
			if (ItemId == 0)
			{
				return null;
			}
			List<GoodsItem> shopAllList = ShopManager.ShopAllList;
			lock (shopAllList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopAllList)
				{
					if (goodsItem.Item.Id == ItemId)
					{
						return goodsItem;
					}
				}
			}
			return null;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001F004 File Offset: 0x0001D204
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
			List<GoodsItem> shopBuyableList = ShopManager.ShopBuyableList;
			lock (shopBuyableList)
			{
				foreach (GoodsItem goodsItem in ShopManager.ShopBuyableList)
				{
					foreach (CartGoods cartGoods in ShopCart)
					{
						if (cartGoods.GoodId == goodsItem.Id)
						{
							list.Add(goodsItem);
							if (cartGoods.BuyType == 1)
							{
								GoldPrice += goodsItem.PriceGold;
							}
							else if (cartGoods.BuyType == 2)
							{
								CashPrice += goodsItem.PriceCash;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001F114 File Offset: 0x0001D314
		private static void smethod_17()
		{
			string text = "zOne";
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteS(text, text.Length + 1);
				ShopManager.ShopTagData = syncServerPacket.ToArray();
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001F164 File Offset: 0x0001D364
		// Note: this type is marked as 'beforefieldinit'.
		static ShopManager()
		{
		}

		// Token: 0x0400037E RID: 894
		public static List<ItemsRepair> ItemRepairs = new List<ItemsRepair>();

		// Token: 0x0400037F RID: 895
		public static List<GoodsItem> ShopAllList = new List<GoodsItem>();

		// Token: 0x04000380 RID: 896
		public static List<GoodsItem> ShopBuyableList = new List<GoodsItem>();

		// Token: 0x04000381 RID: 897
		public static SortedList<int, GoodsItem> ShopUniqueList = new SortedList<int, GoodsItem>();

		// Token: 0x04000382 RID: 898
		public static List<ShopData> ShopDataMt1 = new List<ShopData>();

		// Token: 0x04000383 RID: 899
		public static List<ShopData> ShopDataMt2 = new List<ShopData>();

		// Token: 0x04000384 RID: 900
		public static List<ShopData> ShopDataGoods = new List<ShopData>();

		// Token: 0x04000385 RID: 901
		public static List<ShopData> ShopDataItems = new List<ShopData>();

		// Token: 0x04000386 RID: 902
		public static List<ShopData> ShopDataItemRepairs = new List<ShopData>();

		// Token: 0x04000387 RID: 903
		public static byte[] ShopTagData;

		// Token: 0x04000388 RID: 904
		public static int TotalGoods;

		// Token: 0x04000389 RID: 905
		public static int TotalItems;

		// Token: 0x0400038A RID: 906
		public static int TotalMatching1;

		// Token: 0x0400038B RID: 907
		public static int TotalMatching2;

		// Token: 0x0400038C RID: 908
		public static int TotalRepairs;

		// Token: 0x0400038D RID: 909
		public static int Set4p;

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		[Serializable]
		private sealed class Class13<T>
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x0000662B File Offset: 0x0000482B
			// Note: this type is marked as 'beforefieldinit'.
			static Class13()
			{
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00002116 File Offset: 0x00000316
			public Class13()
			{
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x000028E7 File Offset: 0x00000AE7
			internal Class0<T, int> method_0(T gparam_0, int int_0)
			{
				return new Class0<T, int>(gparam_0, int_0);
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x00006637 File Offset: 0x00004837
			internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
			{
				return igrouping_0.Select(new Func<Class0<T, int>, T>(ShopManager.Class13<T>.<>9.method_2));
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00002917 File Offset: 0x00000B17
			internal T method_2(Class0<T, int> class0_0)
			{
				return class0_0.item;
			}

			// Token: 0x0400038E RID: 910
			public static readonly ShopManager.Class13<T> <>9 = new ShopManager.Class13<T>();

			// Token: 0x0400038F RID: 911
			public static Func<T, int, Class0<T, int>> <>9__16_0;

			// Token: 0x04000390 RID: 912
			public static Func<Class0<T, int>, T> <>9__16_3;

			// Token: 0x04000391 RID: 913
			public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__16_2;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		private sealed class Class14<T>
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x00002116 File Offset: 0x00000316
			public Class14()
			{
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x0000665E File Offset: 0x0000485E
			internal int method_0(Class0<T, int> class0_0)
			{
				return class0_0.inx / this.int_0;
			}

			// Token: 0x04000392 RID: 914
			public int int_0;
		}
	}
}
