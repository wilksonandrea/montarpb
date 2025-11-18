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
	// Token: 0x020001D9 RID: 473
	public class PROTOCOL_SHOP_GET_SAILLIST_REQ : GameClientPacket
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00005863 File Offset: 0x00003A63
		public override void Read()
		{
			this.string_0 = base.ReadS(32);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00026CCC File Offset: 0x00024ECC
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
					this.Client.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
					if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/Shop.dat") == this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(false));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_SHOP_GET_SAILLIST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_SHOP_GET_SAILLIST_REQ()
		{
		}

		// Token: 0x04000384 RID: 900
		private string string_0;
	}
}
