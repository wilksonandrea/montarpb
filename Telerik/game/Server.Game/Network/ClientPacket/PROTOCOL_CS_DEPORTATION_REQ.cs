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
	public class PROTOCOL_CS_DEPORTATION_REQ : GameClientPacket
	{
		private List<long> list_0 = new List<long>();

		private uint uint_0;

		public PROTOCOL_CS_DEPORTATION_REQ()
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
				ClanNote = NoteMessageClan.Deportation
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
					if (clan.Id != 0)
					{
						if (player.ClanAccess >= 1 && player.ClanAccess <= 2 || clan.OwnerId == this.Client.PlayerId)
						{
							List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, true);
							int ınt32 = 0;
							while (true)
							{
								if (ınt32 < this.list_0.Count)
								{
									Account account = AccountManager.GetAccount(this.list_0[ınt32], 31);
									if (account == null || account.ClanId != clan.Id || account.Match != null)
									{
										break;
									}
									if (!ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }))
									{
										break;
									}
									if (!ComDiv.UpdateDB("player_stat_clans", "owner_id", account.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }))
									{
										break;
									}
									using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK pROTOCOLCSMEMBERINFODELETEACK = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(account.PlayerId))
									{
										ClanManager.SendPacket(pROTOCOLCSMEMBERINFODELETEACK, clanPlayers, account.PlayerId);
									}
									account.ClanId = 0;
									account.ClanAccess = 0;
									SendClanInfo.Load(account, null, 0);
									if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
									{
										MessageModel messageModel = this.method_0(clan, account.PlayerId, this.Client.PlayerId);
										if (messageModel != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
									if (account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_CS_DEPORTATION_RESULT_ACK(), false);
									}
									this.uint_0++;
									clanPlayers.Remove(account);
									ınt32++;
								}
								else
								{
									this.Client.SendPacket(new PROTOCOL_CS_DEPORTATION_ACK(this.uint_0));
									return;
								}
							}
							this.uint_0 = -2147479463;
							this.Client.SendPacket(new PROTOCOL_CS_DEPORTATION_ACK(this.uint_0));
							return;
						}
					}
					this.uint_0 = -2147479463;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_DEPORTATION_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}