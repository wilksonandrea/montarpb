using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_ACCEPT_REQUEST_REQ : GameClientPacket
	{
		private List<long> list_0 = new List<long>();

		private int int_0;

		public PROTOCOL_CS_ACCEPT_REQUEST_REQ()
		{
		}

		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.InviteAccept
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		public override void Read()
		{
			int ınt32 = base.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				long ınt64 = base.ReadQ();
				this.list_0.Add(ınt64);
			}
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id <= 0 || (player.ClanAccess < 1 || player.ClanAccess > 2) && player.PlayerId != clan.OwnerId)
					{
						this.int_0 = -1;
					}
					else
					{
						List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, true);
						if (clanPlayers.Count < clan.MaxPlayers)
						{
							for (int i = 0; i < this.list_0.Count; i++)
							{
								Account account = AccountManager.GetAccount(this.list_0[i], 31);
								if (account != null && clanPlayers.Count < clan.MaxPlayers && account.ClanId == 0 && DaoManagerSQL.GetRequestClanId(account.PlayerId) > 0)
								{
									using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK pROTOCOLCSMEMBERINFOCHANGEACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account))
									{
										ClanManager.SendPacket(pROTOCOLCSMEMBERINFOCHANGEACK, clanPlayers);
									}
									account.ClanId = player.ClanId;
									account.ClanDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
									account.ClanAccess = 3;
									SendClanInfo.Load(account, null, 3);
									ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, new string[] { "clan_access", "clan_id", "clan_date" }, new object[] { account.ClanAccess, account.ClanId, (long)((ulong)account.ClanDate) });
									DaoManagerSQL.DeleteClanInviteDB(player.ClanId, account.PlayerId);
									if (account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers), false);
										RoomModel room = account.Room;
										if (room != null)
										{
											room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(account, clan));
										}
										account.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, clanPlayers.Count + 1), false);
									}
									if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
									{
										MessageModel messageModel = this.method_0(clan, account.PlayerId, this.Client.PlayerId);
										if (messageModel != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
									this.int_0++;
									clanPlayers.Add(account);
								}
							}
							clanPlayers = null;
						}
						else
						{
							this.int_0 = -1;
							return;
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_ACK((uint)this.int_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_ACCEPT_REQUEST_RESULT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}