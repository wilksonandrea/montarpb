using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_INVENTORY_USE_ITEM_REQ : GameClientPacket
{
	private long long_0;

	private uint uint_0;

	private uint uint_1 = 1u;

	private byte[] byte_0;

	private string string_0;

	public override void Read()
	{
		long_0 = ReadD();
		byte_0 = ReadB(ReadC());
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
			if (ıtem != null && ıtem.Id > 1700000)
			{
				int num = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(ıtem.Id, 3));
				uint uint_ = Convert.ToUInt32(DateTimeUtil.Now().AddDays(ComDiv.GetIdStatics(ıtem.Id, 2)).ToString("yyMMddHHmm"));
				switch (num)
				{
				default:
					if (byte_0.Length != 0)
					{
						uint_0 = byte_0[0];
					}
					break;
				case 1600005:
				case 1600052:
					uint_0 = BitConverter.ToUInt32(byte_0, 0);
					break;
				case 1600010:
				case 1600047:
				case 1600051:
					string_0 = Bitwise.HexArrayToString(byte_0, byte_0.Length);
					break;
				}
				method_0(num, uint_, player);
			}
			else
			{
				uint_1 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint_1, ıtem, player));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_INVENTORY_USE_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(int int_0, uint uint_2, Account account_0)
	{
		switch (int_0)
		{
		case 1600051:
			if (!string.IsNullOrEmpty(string_0) && string_0.Length <= 16)
			{
				ClanModel clan2 = ClanManager.GetClan(account_0.ClanId);
				if (clan2.Id > 0 && clan2.OwnerId == Client.PlayerId)
				{
					if (!ClanManager.IsClanNameExist(string_0) && ComDiv.UpdateDB("system_clan", "name", string_0, "id", account_0.ClanId))
					{
						clan2.Name = string_0;
						using PROTOCOL_CS_REPLACE_NAME_RESULT_ACK packet7 = new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(string_0);
						ClanManager.SendPacket(packet7, account_0.ClanId, -1L, UseCache: true, IsOnline: true);
						break;
					}
					uint_1 = 2147483648u;
				}
				else
				{
					uint_1 = 2147483648u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600052:
		{
			ClanModel clan4 = ClanManager.GetClan(account_0.ClanId);
			if (clan4.Id > 0 && clan4.OwnerId == Client.PlayerId && !ClanManager.IsClanLogoExist(uint_0) && DaoManagerSQL.UpdateClanLogo(account_0.ClanId, uint_0))
			{
				clan4.Logo = uint_0;
				using PROTOCOL_CS_REPLACE_MARK_RESULT_ACK packet10 = new PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(uint_0);
				ClanManager.SendPacket(packet10, account_0.ClanId, -1L, UseCache: true, IsOnline: true);
				break;
			}
			uint_1 = 2147483648u;
			break;
		}
		case 1600047:
			if (!string.IsNullOrEmpty(string_0) && string_0.Length >= ConfigLoader.MinNickSize && string_0.Length <= ConfigLoader.MaxNickSize && account_0.Inventory.GetItem(1600010) == null)
			{
				if (!DaoManagerSQL.IsPlayerNameExist(string_0))
				{
					if (ComDiv.UpdateDB("accounts", "nickname", string_0, "player_id", account_0.PlayerId))
					{
						DaoManagerSQL.CreatePlayerNickHistory(account_0.PlayerId, account_0.Nickname, string_0, "Nickname changed (Item)");
						account_0.Nickname = string_0;
						Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
						Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						if (account_0.Room != null)
						{
							using (PROTOCOL_ROOM_GET_NICKNAME_ACK packet8 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
							{
								account_0.Room.SendPacketToPlayers(packet8);
							}
							account_0.Room.UpdateSlotsInfo();
						}
						if (account_0.ClanId > 0)
						{
							using PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK packet9 = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account_0);
							ClanManager.SendPacket(packet9, account_0.ClanId, -1L, UseCache: true, IsOnline: true);
						}
						AllUtils.SyncPlayerToFriends(account_0, all: true);
					}
					else
					{
						uint_1 = 2147483648u;
					}
				}
				else
				{
					uint_1 = 2147483923u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600006:
			if (ComDiv.UpdateDB("accounts", "nick_color", (int)uint_0, "player_id", account_0.PlayerId))
			{
				account_0.NickColor = (int)uint_0;
				Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
				Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
				Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Name Color [Active]", ItemEquipType.Temporary, uint_2)));
				if (account_0.Room != null)
				{
					using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK packet3 = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(account_0.SlotId, account_0.NickColor))
					{
						account_0.Room.SendPacketToPlayers(packet3);
					}
					account_0.Room.UpdateSlotsInfo();
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600187:
			if (ComDiv.UpdateDB("player_bonus", "muzzle_color", (int)uint_0, "owner_id", account_0.PlayerId))
			{
				account_0.Bonus.MuzzleColor = (int)uint_0;
				Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
				Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Muzzle Color [Active]", ItemEquipType.Temporary, uint_2)));
				if (account_0.Room != null)
				{
					using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK packet2 = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(account_0.SlotId, account_0.Bonus.MuzzleColor))
					{
						account_0.Room.SendPacketToPlayers(packet2);
					}
					account_0.Room.UpdateSlotsInfo();
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600205:
			if (ComDiv.UpdateDB("player_bonus", "nick_border_color", (int)uint_0, "owner_id", account_0.PlayerId))
			{
				account_0.Bonus.NickBorderColor = (int)uint_0;
				Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
				Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Nick Border Color [Active]", ItemEquipType.Temporary, uint_2)));
				if (account_0.Room != null)
				{
					using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK packet = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(account_0.SlotId, account_0.Bonus.NickBorderColor))
					{
						account_0.Room.SendPacketToPlayers(packet);
					}
					account_0.Room.UpdateSlotsInfo();
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600193:
		{
			ClanModel clan = ClanManager.GetClan(account_0.ClanId);
			if (clan.Id > 0 && clan.OwnerId == Client.PlayerId)
			{
				if (ComDiv.UpdateDB("system_clan", "effects", (int)uint_0, "id", account_0.ClanId))
				{
					clan.Effect = (int)uint_0;
					using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK packet6 = new PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK((int)uint_0))
					{
						ClanManager.SendPacket(packet6, account_0.ClanId, -1L, UseCache: true, IsOnline: true);
					}
					Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
				}
				else
				{
					uint_1 = 2147483648u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		}
		case 1600009:
			if ((int)uint_0 < 51 && (int)uint_0 >= account_0.Rank - 10 && (int)uint_0 <= account_0.Rank + 10)
			{
				if (ComDiv.UpdateDB("player_bonus", "fake_rank", (int)uint_0, "owner_id", account_0.PlayerId))
				{
					account_0.Bonus.FakeRank = (int)uint_0;
					Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
					Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Rank [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_RANK_ACK packet5 = new PROTOCOL_ROOM_GET_RANK_ACK(account_0.SlotId, account_0.GetRank()))
						{
							account_0.Room.SendPacketToPlayers(packet5);
						}
						account_0.Room.UpdateSlotsInfo();
					}
				}
				else
				{
					uint_1 = 2147483648u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600010:
			if (!string.IsNullOrEmpty(string_0) && string_0.Length >= ConfigLoader.MinNickSize && string_0.Length <= ConfigLoader.MaxNickSize)
			{
				if (ComDiv.UpdateDB("player_bonus", "fake_nick", account_0.Nickname, "owner_id", account_0.PlayerId) && ComDiv.UpdateDB("accounts", "nickname", string_0, "player_id", account_0.PlayerId))
				{
					account_0.Bonus.FakeNick = account_0.Nickname;
					account_0.Nickname = string_0;
					Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
					Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
					Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Nick [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_NICKNAME_ACK packet4 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
						{
							account_0.Room.SendPacketToPlayers(packet4);
						}
						account_0.Room.UpdateSlotsInfo();
					}
				}
				else
				{
					uint_1 = 2147483648u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600014:
			if (ComDiv.UpdateDB("player_bonus", "crosshair_color", (int)uint_0, "owner_id", account_0.PlayerId))
			{
				account_0.Bonus.CrosshairColor = (int)uint_0;
				Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(byte_0.Length, account_0));
				Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Crosshair Color [Active]", ItemEquipType.Temporary, uint_2)));
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600005:
		{
			ClanModel clan3 = ClanManager.GetClan(account_0.ClanId);
			if (clan3.Id > 0 && clan3.OwnerId == Client.PlayerId && ComDiv.UpdateDB("system_clan", "name_color", (int)uint_0, "id", clan3.Id))
			{
				clan3.NameColor = (int)uint_0;
				Client.SendPacket(new PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(clan3.NameColor));
				Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		}
		case 1600183:
			if (!string.IsNullOrWhiteSpace(string_0) && string_0.Length <= 60 && !string.IsNullOrWhiteSpace(account_0.Nickname))
			{
				GameXender.Client.SendPacketToAllClients(new PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(account_0.Nickname, string_0));
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		case 1600085:
			if (account_0.Room != null)
			{
				Account playerBySlot = account_0.Room.GetPlayerBySlot((int)uint_0);
				if (playerBySlot != null)
				{
					Client.SendPacket(new PROTOCOL_ROOM_GET_USER_ITEM_ACK(playerBySlot));
				}
				else
				{
					uint_1 = 2147483648u;
				}
			}
			else
			{
				uint_1 = 2147483648u;
			}
			break;
		default:
			CLogger.Print($"Coupon effect not found! Id: {int_0}", LoggerType.Warning);
			uint_1 = 2147483648u;
			break;
		}
	}
}
