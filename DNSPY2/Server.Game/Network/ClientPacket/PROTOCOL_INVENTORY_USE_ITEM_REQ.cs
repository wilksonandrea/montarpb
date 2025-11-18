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

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B5 RID: 437
	public class PROTOCOL_INVENTORY_USE_ITEM_REQ : GameClientPacket
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00005692 File Offset: 0x00003892
		public override void Read()
		{
			this.long_0 = (long)base.ReadD();
			this.byte_0 = base.ReadB((int)base.ReadC());
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00022E44 File Offset: 0x00021044
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel item = player.Inventory.GetItem(this.long_0);
					if (item != null && item.Id > 1700000)
					{
						int num = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(item.Id, 3));
						uint num2 = Convert.ToUInt32(DateTimeUtil.Now().AddDays((double)ComDiv.GetIdStatics(item.Id, 2)).ToString("yyMMddHHmm"));
						if (num != 1600047 && num != 1600051)
						{
							if (num != 1600010)
							{
								if (num != 1600052)
								{
									if (num != 1600005)
									{
										if (this.byte_0.Length != 0)
										{
											this.uint_0 = (uint)this.byte_0[0];
											goto IL_F2;
										}
										goto IL_F2;
									}
								}
								this.uint_0 = BitConverter.ToUInt32(this.byte_0, 0);
								goto IL_F2;
							}
						}
						this.string_0 = Bitwise.HexArrayToString(this.byte_0, this.byte_0.Length);
						IL_F2:
						this.method_0(num, num2, player);
					}
					else
					{
						this.uint_1 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(this.uint_1, item, player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_INVENTORY_USE_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00022FAC File Offset: 0x000211AC
		private void method_0(int int_0, uint uint_2, Account account_0)
		{
			if (int_0 == 1600051)
			{
				if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length > 16)
				{
					this.uint_1 = 2147483648U;
					return;
				}
				ClanModel clan = ClanManager.GetClan(account_0.ClanId);
				if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
				{
					if (!ClanManager.IsClanNameExist(this.string_0) && ComDiv.UpdateDB("system_clan", "name", this.string_0, "id", account_0.ClanId))
					{
						clan.Name = this.string_0;
						using (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK protocol_CS_REPLACE_NAME_RESULT_ACK = new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(this.string_0))
						{
							ClanManager.SendPacket(protocol_CS_REPLACE_NAME_RESULT_ACK, account_0.ClanId, -1L, true, true);
							return;
						}
					}
					this.uint_1 = 2147483648U;
					return;
				}
				this.uint_1 = 2147483648U;
				return;
			}
			else
			{
				if (int_0 == 1600052)
				{
					ClanModel clan2 = ClanManager.GetClan(account_0.ClanId);
					if (clan2.Id > 0 && clan2.OwnerId == this.Client.PlayerId && !ClanManager.IsClanLogoExist(this.uint_0) && DaoManagerSQL.UpdateClanLogo(account_0.ClanId, this.uint_0))
					{
						clan2.Logo = this.uint_0;
						using (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK protocol_CS_REPLACE_MARK_RESULT_ACK = new PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(this.uint_0))
						{
							ClanManager.SendPacket(protocol_CS_REPLACE_MARK_RESULT_ACK, account_0.ClanId, -1L, true, true);
							return;
						}
					}
					this.uint_1 = 2147483648U;
					return;
				}
				if (int_0 == 1600047)
				{
					if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length < ConfigLoader.MinNickSize || this.string_0.Length > ConfigLoader.MaxNickSize || account_0.Inventory.GetItem(1600010) != null)
					{
						this.uint_1 = 2147483648U;
						return;
					}
					if (DaoManagerSQL.IsPlayerNameExist(this.string_0))
					{
						this.uint_1 = 2147483923U;
						return;
					}
					if (ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
					{
						DaoManagerSQL.CreatePlayerNickHistory(account_0.PlayerId, account_0.Nickname, this.string_0, "Nickname changed (Item)");
						account_0.Nickname = this.string_0;
						this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						if (account_0.Room != null)
						{
							using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_ROOM_GET_NICKNAME_ACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
							{
								account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_NICKNAME_ACK);
							}
							account_0.Room.UpdateSlotsInfo();
						}
						if (account_0.ClanId > 0)
						{
							using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_CS_MEMBER_INFO_CHANGE_ACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account_0))
							{
								ClanManager.SendPacket(protocol_CS_MEMBER_INFO_CHANGE_ACK, account_0.ClanId, -1L, true, true);
							}
						}
						AllUtils.SyncPlayerToFriends(account_0, true);
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else if (int_0 == 1600006)
				{
					if (!ComDiv.UpdateDB("accounts", "nick_color", (int)this.uint_0, "player_id", account_0.PlayerId))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					account_0.NickColor = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Name Color [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_ROOM_GET_COLOR_NICK_ACK = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(account_0.SlotId, account_0.NickColor))
						{
							account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_COLOR_NICK_ACK);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 == 1600187)
				{
					if (!ComDiv.UpdateDB("player_bonus", "muzzle_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					account_0.Bonus.MuzzleColor = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Muzzle Color [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(account_0.SlotId, account_0.Bonus.MuzzleColor))
						{
							account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 == 1600205)
				{
					if (!ComDiv.UpdateDB("player_bonus", "nick_border_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					account_0.Bonus.NickBorderColor = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Nick Border Color [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK protocol_ROOM_GET_NICK_OUTLINE_COLOR_ACK = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(account_0.SlotId, account_0.Bonus.NickBorderColor))
						{
							account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_NICK_OUTLINE_COLOR_ACK);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 == 1600193)
				{
					ClanModel clan3 = ClanManager.GetClan(account_0.ClanId);
					if (clan3.Id <= 0 || clan3.OwnerId != this.Client.PlayerId)
					{
						this.uint_1 = 2147483648U;
						return;
					}
					if (ComDiv.UpdateDB("system_clan", "effects", (int)this.uint_0, "id", account_0.ClanId))
					{
						clan3.Effect = (int)this.uint_0;
						using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK protocol_CS_REPLACE_MARKEFFECT_RESULT_ACK = new PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK((int)this.uint_0))
						{
							ClanManager.SendPacket(protocol_CS_REPLACE_MARKEFFECT_RESULT_ACK, account_0.ClanId, -1L, true, true);
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else if (int_0 == 1600009)
				{
					if (this.uint_0 >= 51U || this.uint_0 < (uint)(account_0.Rank - 10) || this.uint_0 > (uint)(account_0.Rank + 10))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					if (!ComDiv.UpdateDB("player_bonus", "fake_rank", (int)this.uint_0, "owner_id", account_0.PlayerId))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					account_0.Bonus.FakeRank = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Rank [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_RANK_ACK protocol_ROOM_GET_RANK_ACK = new PROTOCOL_ROOM_GET_RANK_ACK(account_0.SlotId, account_0.GetRank()))
						{
							account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_RANK_ACK);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 == 1600010)
				{
					if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length < ConfigLoader.MinNickSize || this.string_0.Length > ConfigLoader.MaxNickSize)
					{
						this.uint_1 = 2147483648U;
						return;
					}
					if (!ComDiv.UpdateDB("player_bonus", "fake_nick", account_0.Nickname, "owner_id", account_0.PlayerId) || !ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
					{
						this.uint_1 = 2147483648U;
						return;
					}
					account_0.Bonus.FakeNick = account_0.Nickname;
					account_0.Nickname = this.string_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
					this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Nick [Active]", ItemEquipType.Temporary, uint_2)));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_ROOM_GET_NICKNAME_ACK2 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
						{
							account_0.Room.SendPacketToPlayers(protocol_ROOM_GET_NICKNAME_ACK2);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 == 1600014)
				{
					if (ComDiv.UpdateDB("player_bonus", "crosshair_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
					{
						account_0.Bonus.CrosshairColor = (int)this.uint_0;
						this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
						this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Crosshair Color [Active]", ItemEquipType.Temporary, uint_2)));
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else if (int_0 == 1600005)
				{
					ClanModel clan4 = ClanManager.GetClan(account_0.ClanId);
					if (clan4.Id > 0 && clan4.OwnerId == this.Client.PlayerId && ComDiv.UpdateDB("system_clan", "name_color", (int)this.uint_0, "id", clan4.Id))
					{
						clan4.NameColor = (int)this.uint_0;
						this.Client.SendPacket(new PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(clan4.NameColor));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else if (int_0 == 1600183)
				{
					if (!string.IsNullOrWhiteSpace(this.string_0) && this.string_0.Length <= 60 && !string.IsNullOrWhiteSpace(account_0.Nickname))
					{
						GameXender.Client.SendPacketToAllClients(new PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(account_0.Nickname, this.string_0));
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else if (int_0 == 1600085)
				{
					if (account_0.Room == null)
					{
						this.uint_1 = 2147483648U;
						return;
					}
					Account playerBySlot = account_0.Room.GetPlayerBySlot((int)this.uint_0);
					if (playerBySlot != null)
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_ITEM_ACK(playerBySlot));
						return;
					}
					this.uint_1 = 2147483648U;
					return;
				}
				else
				{
					CLogger.Print(string.Format("Coupon effect not found! Id: {0}", int_0), LoggerType.Warning, null);
					this.uint_1 = 2147483648U;
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000056B3 File Offset: 0x000038B3
		public PROTOCOL_INVENTORY_USE_ITEM_REQ()
		{
		}

		// Token: 0x04000327 RID: 807
		private long long_0;

		// Token: 0x04000328 RID: 808
		private uint uint_0;

		// Token: 0x04000329 RID: 809
		private uint uint_1 = 1U;

		// Token: 0x0400032A RID: 810
		private byte[] byte_0;

		// Token: 0x0400032B RID: 811
		private string string_0;
	}
}
