using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ : GameClientPacket
	{
		private long long_0;

		private uint uint_0;

		private uint uint_1;

		private byte[] byte_0;

		private string string_0;

		public PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ()
		{
		}

		private void method_0(int int_0, Account account_0)
		{
			if (int_0 == 1600051)
			{
				if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length > 16)
				{
					this.uint_1 = -2147483648;
					return;
				}
				ClanModel clan = ClanManager.GetClan(account_0.ClanId);
				if (clan.Id <= 0 || clan.OwnerId != this.Client.PlayerId)
				{
					this.uint_1 = -2147483648;
					return;
				}
				if (ClanManager.IsClanNameExist(this.string_0) || !ComDiv.UpdateDB("system_clan", "name", this.string_0, "id", account_0.ClanId))
				{
					this.uint_1 = -2147483648;
					return;
				}
				clan.Name = this.string_0;
				using (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK pROTOCOLCSREPLACENAMERESULTACK = new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(this.string_0))
				{
					ClanManager.SendPacket(pROTOCOLCSREPLACENAMERESULTACK, account_0.ClanId, -1L, true, true);
				}
			}
			else if (int_0 != 1600052)
			{
				if (int_0 == 1600047)
				{
					if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length < ConfigLoader.MinNickSize || this.string_0.Length > ConfigLoader.MaxNickSize || account_0.Inventory.GetItem(1600010) != null)
					{
						this.uint_1 = -2147483648;
						return;
					}
					if (DaoManagerSQL.IsPlayerNameExist(this.string_0))
					{
						this.uint_1 = -2147483373;
						return;
					}
					if (!ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
					{
						this.uint_1 = -2147483648;
						return;
					}
					DaoManagerSQL.CreatePlayerNickHistory(account_0.PlayerId, account_0.Nickname, this.string_0, "Changed (Coupon)");
					account_0.Nickname = this.string_0;
					this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
					this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_NICKNAME_ACK pROTOCOLROOMGETNICKNAMEACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
						{
							account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKNAMEACK);
						}
						account_0.Room.UpdateSlotsInfo();
					}
					if (account_0.ClanId > 0)
					{
						using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK pROTOCOLCSMEMBERINFOCHANGEACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account_0))
						{
							ClanManager.SendPacket(pROTOCOLCSMEMBERINFOCHANGEACK, account_0.ClanId, -1L, true, true);
						}
					}
					AllUtils.SyncPlayerToFriends(account_0, true);
					return;
				}
				if (int_0 == 1600006)
				{
					if (!ComDiv.UpdateDB("accounts", "nick_color", (int)this.uint_0, "player_id", account_0.PlayerId))
					{
						this.uint_1 = -2147483648;
						return;
					}
					account_0.NickColor = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
					this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_NICKNAME_ACK pROTOCOLROOMGETNICKNAMEACK1 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
						{
							account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKNAMEACK1);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
				else if (int_0 != 1600187)
				{
					if (int_0 == 1600193)
					{
						ClanModel uint0 = ClanManager.GetClan(account_0.ClanId);
						if (uint0.Id <= 0 || uint0.OwnerId != this.Client.PlayerId)
						{
							this.uint_1 = -2147483648;
							return;
						}
						if (!ComDiv.UpdateDB("system_clan", "effects", (int)this.uint_0, "id", account_0.ClanId))
						{
							this.uint_1 = -2147483648;
							return;
						}
						uint0.Effect = (int)this.uint_0;
						using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK pROTOCOLCSREPLACEMARKEFFECTRESULTACK = new PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK((int)this.uint_0))
						{
							ClanManager.SendPacket(pROTOCOLCSREPLACEMARKEFFECTRESULTACK, account_0.ClanId, -1L, true, true);
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						return;
					}
					if (int_0 == 1600205)
					{
						if (!ComDiv.UpdateDB("player_bonus", "nick_border_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
						{
							this.uint_1 = -2147483648;
							return;
						}
						account_0.Bonus.NickBorderColor = (int)this.uint_0;
						this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
						if (account_0.Room != null)
						{
							using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK pROTOCOLROOMGETNICKOUTLINECOLORACK = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(account_0.SlotId, account_0.Bonus.NickBorderColor))
							{
								account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKOUTLINECOLORACK);
							}
							account_0.Room.UpdateSlotsInfo();
							return;
						}
					}
					else if (int_0 == 1600009)
					{
						if (this.uint_0 >= 51 || this.uint_0 < account_0.Rank - 10 || this.uint_0 > account_0.Rank + 10)
						{
							this.uint_1 = -2147483648;
							return;
						}
						if (!ComDiv.UpdateDB("player_bonus", "fake_rank", (int)this.uint_0, "owner_id", account_0.PlayerId))
						{
							this.uint_1 = -2147483648;
							return;
						}
						account_0.Bonus.FakeRank = (int)this.uint_0;
						this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						if (account_0.Room != null)
						{
							using (PROTOCOL_ROOM_GET_RANK_ACK pROTOCOLROOMGETRANKACK = new PROTOCOL_ROOM_GET_RANK_ACK(account_0.SlotId, account_0.GetRank()))
							{
								account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETRANKACK);
							}
							account_0.Room.UpdateSlotsInfo();
							return;
						}
					}
					else if (int_0 != 1600010)
					{
						if (int_0 == 1600014)
						{
							if (!ComDiv.UpdateDB("player_bonus", "crosshair_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
							{
								this.uint_1 = -2147483648;
								return;
							}
							account_0.Bonus.CrosshairColor = (int)this.uint_0;
							this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
							return;
						}
						if (int_0 == 1600005)
						{
							ClanModel clanModel = ClanManager.GetClan(account_0.ClanId);
							if (clanModel.Id <= 0 || clanModel.OwnerId != this.Client.PlayerId || !ComDiv.UpdateDB("system_clan", "name_color", (int)this.uint_0, "id", clanModel.Id))
							{
								this.uint_1 = -2147483648;
								return;
							}
							clanModel.NameColor = (int)this.uint_0;
							this.Client.SendPacket(new PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(clanModel.NameColor));
							this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
							return;
						}
						if (int_0 == 1600183)
						{
							if (string.IsNullOrWhiteSpace(this.string_0) || this.string_0.Length > 60 || string.IsNullOrWhiteSpace(account_0.Nickname))
							{
								this.uint_1 = -2147483648;
								return;
							}
							GameXender.Client.SendPacketToAllClients(new PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(account_0.Nickname, this.string_0));
							return;
						}
						if (int_0 == 1600085)
						{
							if (account_0.Room == null)
							{
								this.uint_1 = -2147483648;
								return;
							}
							Account playerBySlot = account_0.Room.GetPlayerBySlot((int)this.uint_0);
							if (playerBySlot == null)
							{
								this.uint_1 = -2147483648;
								return;
							}
							this.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_ITEM_ACK(playerBySlot));
							return;
						}
						CLogger.Print(string.Format("Coupon effect not found! Id: {0}", int_0), LoggerType.Warning, null);
						this.uint_1 = -2147483648;
					}
					else
					{
						if (string.IsNullOrEmpty(this.string_0) || this.string_0.Length < ConfigLoader.MinNickSize || this.string_0.Length > ConfigLoader.MaxNickSize)
						{
							this.uint_1 = -2147483648;
							return;
						}
						if (!ComDiv.UpdateDB("player_bonus", "fake_nick", account_0.Nickname, "owner_id", account_0.PlayerId) || !ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
						{
							this.uint_1 = -2147483648;
							return;
						}
						account_0.Bonus.FakeNick = account_0.Nickname;
						account_0.Nickname = this.string_0;
						this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
						this.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
						this.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
						if (account_0.Room != null)
						{
							using (PROTOCOL_ROOM_GET_NICKNAME_ACK pROTOCOLROOMGETNICKNAMEACK2 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
							{
								account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETNICKNAMEACK2);
							}
							account_0.Room.UpdateSlotsInfo();
							return;
						}
					}
				}
				else
				{
					if (!ComDiv.UpdateDB("player_bonus", "muzzle_color", (int)this.uint_0, "owner_id", account_0.PlayerId))
					{
						this.uint_1 = -2147483648;
						return;
					}
					account_0.Bonus.MuzzleColor = (int)this.uint_0;
					this.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK((int)this.byte_0.Length, account_0));
					if (account_0.Room != null)
					{
						using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK pROTOCOLROOMGETCOLORMUZZLEFLASHACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(account_0.SlotId, account_0.Bonus.MuzzleColor))
						{
							account_0.Room.SendPacketToPlayers(pROTOCOLROOMGETCOLORMUZZLEFLASHACK);
						}
						account_0.Room.UpdateSlotsInfo();
						return;
					}
				}
			}
			else
			{
				ClanModel clan1 = ClanManager.GetClan(account_0.ClanId);
				if (clan1.Id <= 0 || clan1.OwnerId != this.Client.PlayerId || ClanManager.IsClanLogoExist(this.uint_0) || !DaoManagerSQL.UpdateClanLogo(account_0.ClanId, this.uint_0))
				{
					this.uint_1 = -2147483648;
					return;
				}
				clan1.Logo = this.uint_0;
				using (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK pROTOCOLCSREPLACEMARKRESULTACK = new PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(this.uint_0))
				{
					ClanManager.SendPacket(pROTOCOLCSREPLACEMARKRESULTACK, account_0.ClanId, -1L, true, true);
				}
			}
		}

		public override void Read()
		{
			this.long_0 = (long)base.ReadUD();
			this.byte_0 = base.ReadB((int)base.ReadC());
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel ıtem = player.Inventory.GetItem(this.long_0);
					if (ıtem == null || ıtem.Id <= 1600000)
					{
						this.uint_1 = -2147483648;
					}
					else
					{
						int ınt32 = ComDiv.CreateItemId(16, 0, ComDiv.GetIdStatics(ıtem.Id, 3));
						if (ınt32 != 1610047 && ınt32 != 1610051)
						{
							if (ınt32 == 1600010)
							{
								goto Label3;
							}
							if (ınt32 != 1610052)
							{
								if (ınt32 == 1600005)
								{
									goto Label4;
								}
								if (this.byte_0.Length != 0)
								{
									this.uint_0 = this.byte_0[0];
									goto Label0;
								}
								else
								{
									goto Label0;
								}
							}
						Label4:
							this.uint_0 = BitConverter.ToUInt32(this.byte_0, 0);
							goto Label0;
						}
					Label3:
						this.string_0 = Bitwise.HexArrayToString(this.byte_0, (int)this.byte_0.Length);
					Label0:
						this.method_0(ınt32, player);
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK(this.uint_1));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}