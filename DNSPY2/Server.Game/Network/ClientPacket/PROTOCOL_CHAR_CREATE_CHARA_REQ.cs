using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000179 RID: 377
	public class PROTOCOL_CHAR_CREATE_CHARA_REQ : GameClientPacket
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0001E464 File Offset: 0x0001C664
		public override void Read()
		{
			base.ReadC();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			base.ReadC();
			CartGoods cartGoods = new CartGoods
			{
				GoodId = base.ReadD(),
				BuyType = (int)base.ReadC()
			};
			this.list_0.Add(cartGoods);
			base.ReadC();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count < 500 && player.Character.Characters.Count < 64)
					{
						int num;
						int num2;
						int num3;
						List<GoodsItem> goods = ShopManager.GetGoods(this.list_0, out num, out num2, out num3);
						if (goods.Count == 0)
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487767U, byte.MaxValue, null, null));
						}
						else if (0 <= player.Gold - num && 0 <= player.Cash - num2 && 0 <= player.Tags - num3)
						{
							if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num, player.Cash - num2, player.Tags - num3))
							{
								player.Gold -= num;
								player.Cash -= num2;
								player.Tags -= num3;
								CharacterModel characterModel = this.method_0(player, goods);
								if (characterModel != null)
								{
									player.Character.AddCharacter(characterModel);
									if (player.Character.GetCharacter(characterModel.Id) != null)
									{
										DaoManagerSQL.CreatePlayerCharacter(characterModel, player.PlayerId);
									}
								}
								this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, goods));
								this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0U, 1, characterModel, player));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487769U, byte.MaxValue, null, null));
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487768U, byte.MaxValue, null, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487929U, byte.MaxValue, null, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		private CharacterModel method_0(Account account_0, List<GoodsItem> list_1)
		{
			foreach (GoodsItem goodsItem in list_1)
			{
				if (goodsItem != null && goodsItem.Item.Id != 0)
				{
					return new CharacterModel
					{
						Id = goodsItem.Item.Id,
						Slot = account_0.Character.GenSlotId(goodsItem.Item.Id),
						Name = this.string_0,
						CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
						PlayTime = 0U
					};
				}
			}
			return null;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000526B File Offset: 0x0000346B
		public PROTOCOL_CHAR_CREATE_CHARA_REQ()
		{
		}

		// Token: 0x040002BC RID: 700
		private string string_0;

		// Token: 0x040002BD RID: 701
		private List<CartGoods> list_0 = new List<CartGoods>();
	}
}
