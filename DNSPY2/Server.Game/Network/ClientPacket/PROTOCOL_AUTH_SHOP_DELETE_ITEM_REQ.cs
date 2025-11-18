using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200012F RID: 303
	public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ : GameClientPacket
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00004DD3 File Offset: 0x00002FD3
		public override void Read()
		{
			this.long_0 = (long)((ulong)base.ReadUD());
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00015B20 File Offset: 0x00013D20
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel item = player.Inventory.GetItem(this.long_0);
					PlayerBonus bonus = player.Bonus;
					if (item == null)
					{
						this.uint_0 = 2147483648U;
					}
					else if (ComDiv.GetIdStatics(item.Id, 1) == 16)
					{
						if (bonus == null)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(2147483648U, 0L));
							return;
						}
						if (!bonus.RemoveBonuses(item.Id))
						{
							if (item.Id == 1600014)
							{
								if (ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", player.PlayerId))
								{
									bonus.CrosshairColor = 4;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else if (item.Id == 1600010)
							{
								if (bonus.FakeNick.Length == 0)
								{
									this.uint_0 = 2147483648U;
								}
								else if (ComDiv.UpdateDB("accounts", "nickname", bonus.FakeNick, "player_id", player.PlayerId) && ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", player.PlayerId))
								{
									player.Nickname = bonus.FakeNick;
									bonus.FakeNick = "";
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(player.Nickname));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel room = player.Room;
									if (room != null)
									{
										using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_ROOM_GET_NICKNAME_ACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(player.SlotId, player.Nickname))
										{
											room.SendPacketToPlayers(protocol_ROOM_GET_NICKNAME_ACK);
										}
										room.UpdateSlotsInfo();
									}
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else if (item.Id == 1600009)
							{
								if (ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", player.PlayerId))
								{
									bonus.FakeRank = 55;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel room2 = player.Room;
									if (room2 != null)
									{
										using (PROTOCOL_ROOM_GET_RANK_ACK protocol_ROOM_GET_RANK_ACK = new PROTOCOL_ROOM_GET_RANK_ACK(player.SlotId, bonus.MuzzleColor))
										{
											room2.SendPacketToPlayers(protocol_ROOM_GET_RANK_ACK);
										}
										room2.UpdateSlotsInfo();
									}
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else if (item.Id == 1600187)
							{
								if (ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", player.PlayerId))
								{
									bonus.MuzzleColor = 0;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									RoomModel room3 = player.Room;
									if (room3 != null)
									{
										using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(player.SlotId, bonus.MuzzleColor))
										{
											room3.SendPacketToPlayers(protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK);
										}
										room3.UpdateSlotsInfo();
									}
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
							else if (item.Id == 1600006)
							{
								if (ComDiv.UpdateDB("accounts", "nick_color", 0, "owner_id", player.PlayerId))
								{
									player.NickColor = 0;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel room4 = player.Room;
									if (room4 != null)
									{
										using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_ROOM_GET_COLOR_NICK_ACK = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(player.SlotId, player.NickColor))
										{
											room4.SendPacketToPlayers(protocol_ROOM_GET_COLOR_NICK_ACK);
										}
										room4.UpdateSlotsInfo();
									}
								}
								else
								{
									this.uint_0 = 2147483648U;
								}
							}
						}
						else
						{
							DaoManagerSQL.UpdatePlayerBonus(player.PlayerId, bonus.Bonuses, bonus.FreePass);
						}
						CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(item.Id);
						if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && player.Effects.HasFlag(couponEffect.EffectFlag))
						{
							player.Effects -= (long)couponEffect.EffectFlag;
							DaoManagerSQL.UpdateCouponEffect(player.PlayerId, player.Effects);
						}
					}
					if (this.uint_0 == 1U && item != null)
					{
						if (DaoManagerSQL.DeletePlayerInventoryItem(item.ObjectId, player.PlayerId))
						{
							player.Inventory.RemoveItem(item);
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(this.uint_0, this.long_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00004DE2 File Offset: 0x00002FE2
		public PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ()
		{
		}

		// Token: 0x04000220 RID: 544
		private long long_0;

		// Token: 0x04000221 RID: 545
		private uint uint_0 = 1U;
	}
}
