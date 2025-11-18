using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_COMMISSION_MASTER_REQ : GameClientPacket
	{
		private long long_0;

		private uint uint_0;

		public PROTOCOL_CS_COMMISSION_MASTER_REQ()
		{
		}

		private MessageModel method_0(ClanModel clanModel_0, long long_1, long long_2)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_2,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Master
			};
			if (!DaoManagerSQL.CreateMessage(long_1, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanAccess == 1)
					{
						Account account = AccountManager.GetAccount(this.long_0, 31);
						int clanId = player.ClanId;
						if (account != null)
						{
							if (account.ClanId != clanId)
							{
								this.uint_0 = -2147483648;
								this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(this.uint_0));
								return;
							}
							if (account.Rank <= 10)
							{
								this.uint_0 = -2147479368;
							}
							else
							{
								ClanModel clan = ClanManager.GetClan(clanId);
								if (clan.Id <= 0 || clan.OwnerId != this.Client.PlayerId || account.ClanAccess != 2 || !ComDiv.UpdateDB("system_clan", "owner_id", this.long_0, "id", clanId) || !ComDiv.UpdateDB("accounts", "clan_access", 1, "player_id", this.long_0) || !ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", player.PlayerId))
								{
									this.uint_0 = -2147479552;
								}
								else
								{
									account.ClanAccess = 1;
									player.ClanAccess = 2;
									clan.OwnerId = this.long_0;
									if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
									{
										MessageModel messageModel = this.method_0(clan, account.PlayerId, player.PlayerId);
										if (messageModel != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
									if (account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK(), false);
									}
								}
							}
						}
						else
						{
							this.uint_0 = -2147483648;
							this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(this.uint_0));
							return;
						}
						this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(this.uint_0));
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_COMMISSION_MASTER_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}