using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CHAR_CREATE_CHARA_REQ : GameClientPacket
	{
		private string string_0;

		private List<CartGoods> list_0 = new List<CartGoods>();

		public PROTOCOL_CHAR_CREATE_CHARA_REQ()
		{
		}

		private CharacterModel method_0(Account account_0, List<GoodsItem> list_1)
		{
			CharacterModel characterModel;
			List<GoodsItem>.Enumerator enumerator = list_1.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GoodsItem current = enumerator.Current;
					if (current == null || current.Item.Id == 0)
					{
						continue;
					}
					characterModel = new CharacterModel()
					{
						Id = current.Item.Id,
						Slot = account_0.Character.GenSlotId(current.Item.Id),
						Name = this.string_0,
						CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
						PlayTime = 0
					};
					return characterModel;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return characterModel;
		}

		public override void Read()
		{
			base.ReadC();
			this.string_0 = base.ReadU(base.ReadC() * 2);
			base.ReadC();
			CartGoods cartGood = new CartGoods()
			{
				GoodId = base.ReadD(),
				BuyType = base.ReadC()
			};
			this.list_0.Add(cartGood);
			base.ReadC();
		}

		public override void Run()
		{
			int ınt32;
			int ınt321;
			int ınt322;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count >= 500 || player.Character.Characters.Count >= 64)
					{
						this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(-2147479367, 255, null, null));
					}
					else
					{
						List<GoodsItem> goods = ShopManager.GetGoods(this.list_0, out ınt32, out ınt321, out ınt322);
						if (goods.Count == 0)
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(-2147479529, 255, null, null));
						}
						else if (0 > player.Gold - ınt32 || 0 > player.Cash - ınt321 || 0 > player.Tags - ınt322)
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(-2147479528, 255, null, null));
						}
						else if (!DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - ınt32, player.Cash - ınt321, player.Tags - ınt322))
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(-2147479527, 255, null, null));
						}
						else
						{
							player.Gold -= ınt32;
							player.Cash -= ınt321;
							player.Tags -= ınt322;
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
							this.Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0, 1, characterModel, player));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}