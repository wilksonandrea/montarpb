using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000155 RID: 341
	public class PROTOCOL_BASE_RANDOMBOX_LIST_REQ : GameClientPacket
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00005081 File Offset: 0x00003281
		public override void Read()
		{
			this.string_0 = base.ReadS(32);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001A328 File Offset: 0x00018528
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!player.LoadedShop)
					{
						player.LoadedShop = true;
						foreach (ShopData shopData in ShopManager.ShopDataItems)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopData, ShopManager.TotalItems));
						}
						foreach (ShopData shopData2 in ShopManager.ShopDataGoods)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(shopData2, ShopManager.TotalGoods));
						}
						foreach (ShopData shopData3 in ShopManager.ShopDataItemRepairs)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopData3, ShopManager.TotalRepairs));
						}
						foreach (ShopData shopData4 in BattleBoxXML.ShopDataBattleBoxes)
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(shopData4, BattleBoxXML.TotalBoxes));
						}
						if (player.CafePC == CafeEnum.None)
						{
							using (List<ShopData>.Enumerator enumerator = ShopManager.ShopDataMt1.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ShopData shopData5 = enumerator.Current;
									this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(shopData5, ShopManager.TotalMatching1));
								}
								goto IL_1D4;
							}
						}
						foreach (ShopData shopData6 in ShopManager.ShopDataMt2)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(shopData6, ShopManager.TotalMatching2));
						}
					}
					IL_1D4:
					if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/RandomBox.dat") == this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_RANDOMBOX_LIST_ACK(false));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_RANDOMBOX_LIST_ACK(true));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_RANDOMBOX_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_RANDOMBOX_LIST_REQ()
		{
		}

		// Token: 0x04000275 RID: 629
		private string string_0;
	}
}
