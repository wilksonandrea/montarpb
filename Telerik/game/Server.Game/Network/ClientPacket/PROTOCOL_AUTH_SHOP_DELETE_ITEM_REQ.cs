using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ : GameClientPacket
	{
		private long long_0;

		private uint uint_0 = 1;

		public PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = (long)base.ReadUD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel ıtem = player.Inventory.GetItem(this.long_0);
					PlayerBonus bonus = player.Bonus;
					if (ıtem == null)
					{
						this.uint_0 = -2147483648;
					}
					else if (ComDiv.GetIdStatics(ıtem.Id, 1) == 16)
					{
						if (bonus != null)
						{
							if (bonus.RemoveBonuses(ıtem.Id))
							{
								DaoManagerSQL.UpdatePlayerBonus(player.PlayerId, bonus.Bonuses, bonus.FreePass);
							}
							else if (ıtem.Id == 1600014)
							{
								if (!ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", player.PlayerId))
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									bonus.CrosshairColor = 4;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
								}
							}
							else if (ıtem.Id == 1600010)
							{
								if (bonus.FakeNick.Length == 0)
								{
									this.uint_0 = -2147483648;
								}
								else if (!ComDiv.UpdateDB("accounts", "nickname", bonus.FakeNick, "player_id", player.PlayerId) || !ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", player.PlayerId))
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									player.Nickname = bonus.FakeNick;
									bonus.FakeNick = "";
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(player.Nickname));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel room = player.Room;
									if (room != null)
									{
										using (PROTOCOL_ROOM_GET_NICKNAME_ACK pROTOCOLROOMGETNICKNAMEACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(player.SlotId, player.Nickname))
										{
											room.SendPacketToPlayers(pROTOCOLROOMGETNICKNAMEACK);
										}
										room.UpdateSlotsInfo();
									}
								}
							}
							else if (ıtem.Id == 1600009)
							{
								if (!ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", player.PlayerId))
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									bonus.FakeRank = 55;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel roomModel = player.Room;
									if (roomModel != null)
									{
										using (PROTOCOL_ROOM_GET_RANK_ACK pROTOCOLROOMGETRANKACK = new PROTOCOL_ROOM_GET_RANK_ACK(player.SlotId, bonus.MuzzleColor))
										{
											roomModel.SendPacketToPlayers(pROTOCOLROOMGETRANKACK);
										}
										roomModel.UpdateSlotsInfo();
									}
								}
							}
							else if (ıtem.Id == 1600187)
							{
								if (!ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", player.PlayerId))
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									bonus.MuzzleColor = 0;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									RoomModel room1 = player.Room;
									if (room1 != null)
									{
										using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK pROTOCOLROOMGETCOLORMUZZLEFLASHACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(player.SlotId, bonus.MuzzleColor))
										{
											room1.SendPacketToPlayers(pROTOCOLROOMGETCOLORMUZZLEFLASHACK);
										}
										room1.UpdateSlotsInfo();
									}
								}
							}
							else if (ıtem.Id == 1600006)
							{
								if (!ComDiv.UpdateDB("accounts", "nick_color", 0, "owner_id", player.PlayerId))
								{
									this.uint_0 = -2147483648;
								}
								else
								{
									player.NickColor = 0;
									this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
									this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
									RoomModel roomModel1 = player.Room;
									if (roomModel1 != null)
									{
										using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK pROTOCOLROOMGETCOLORNICKACK = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(player.SlotId, player.NickColor))
										{
											roomModel1.SendPacketToPlayers(pROTOCOLROOMGETCOLORNICKACK);
										}
										roomModel1.UpdateSlotsInfo();
									}
								}
							}
							CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
							if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && player.Effects.HasFlag(couponEffect.EffectFlag))
							{
								player.Effects -= couponEffect.EffectFlag;
								DaoManagerSQL.UpdateCouponEffect(player.PlayerId, player.Effects);
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(-2147483648, 0L));
							return;
						}
					}
					if (this.uint_0 == 1 && ıtem != null)
					{
						if (!DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
						{
							this.uint_0 = -2147483648;
						}
						else
						{
							player.Inventory.RemoveItem(ıtem);
						}
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(this.uint_0, this.long_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}