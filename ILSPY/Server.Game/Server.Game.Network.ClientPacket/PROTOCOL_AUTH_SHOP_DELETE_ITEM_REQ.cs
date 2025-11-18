using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ : GameClientPacket
{
	private long long_0;

	private uint uint_0 = 1u;

	public override void Read()
	{
		long_0 = ReadUD();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			ItemsModel ıtem = player.Inventory.GetItem(long_0);
			PlayerBonus bonus = player.Bonus;
			if (ıtem == null)
			{
				uint_0 = 2147483648u;
			}
			else if (ComDiv.GetIdStatics(ıtem.Id, 1) == 16)
			{
				if (bonus == null)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(2147483648u));
					return;
				}
				if (!bonus.RemoveBonuses(ıtem.Id))
				{
					if (ıtem.Id == 1600014)
					{
						if (ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", player.PlayerId))
						{
							bonus.CrosshairColor = 4;
							Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
							Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else if (ıtem.Id == 1600010)
					{
						if (bonus.FakeNick.Length == 0)
						{
							uint_0 = 2147483648u;
						}
						else if (ComDiv.UpdateDB("accounts", "nickname", bonus.FakeNick, "player_id", player.PlayerId) && ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", player.PlayerId))
						{
							player.Nickname = bonus.FakeNick;
							bonus.FakeNick = "";
							Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
							Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(player.Nickname));
							Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
							RoomModel room = player.Room;
							if (room != null)
							{
								using (PROTOCOL_ROOM_GET_NICKNAME_ACK packet = new PROTOCOL_ROOM_GET_NICKNAME_ACK(player.SlotId, player.Nickname))
								{
									room.SendPacketToPlayers(packet);
								}
								room.UpdateSlotsInfo();
							}
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else if (ıtem.Id == 1600009)
					{
						if (ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", player.PlayerId))
						{
							bonus.FakeRank = 55;
							Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
							Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
							RoomModel room2 = player.Room;
							if (room2 != null)
							{
								using (PROTOCOL_ROOM_GET_RANK_ACK packet2 = new PROTOCOL_ROOM_GET_RANK_ACK(player.SlotId, bonus.MuzzleColor))
								{
									room2.SendPacketToPlayers(packet2);
								}
								room2.UpdateSlotsInfo();
							}
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else if (ıtem.Id == 1600187)
					{
						if (ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", player.PlayerId))
						{
							bonus.MuzzleColor = 0;
							Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
							RoomModel room3 = player.Room;
							if (room3 != null)
							{
								using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK packet3 = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(player.SlotId, bonus.MuzzleColor))
								{
									room3.SendPacketToPlayers(packet3);
								}
								room3.UpdateSlotsInfo();
							}
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
					else if (ıtem.Id == 1600006)
					{
						if (ComDiv.UpdateDB("accounts", "nick_color", 0, "owner_id", player.PlayerId))
						{
							player.NickColor = 0;
							Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
							Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
							RoomModel room4 = player.Room;
							if (room4 != null)
							{
								using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK packet4 = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(player.SlotId, player.NickColor))
								{
									room4.SendPacketToPlayers(packet4);
								}
								room4.UpdateSlotsInfo();
							}
						}
						else
						{
							uint_0 = 2147483648u;
						}
					}
				}
				else
				{
					DaoManagerSQL.UpdatePlayerBonus(player.PlayerId, bonus.Bonuses, bonus.FreePass);
				}
				CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
				if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && player.Effects.HasFlag(couponEffect.EffectFlag))
				{
					player.Effects -= (long)couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(player.PlayerId, player.Effects);
				}
			}
			if (uint_0 == 1 && ıtem != null)
			{
				if (DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
				{
					player.Inventory.RemoveItem(ıtem);
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(uint_0, long_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
