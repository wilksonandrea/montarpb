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
	public class PROTOCOL_CS_COMMISSION_STAFF_REQ : GameClientPacket
	{
		private List<long> list_0 = new List<long>();

		private uint uint_0;

		public PROTOCOL_CS_COMMISSION_STAFF_REQ()
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
				ClanNote = NoteMessageClan.Staff
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
						if (player.ClanAccess == 1 || clan.OwnerId == this.Client.PlayerId)
						{
							for (int i = 0; i < this.list_0.Count; i++)
							{
								Account account = AccountManager.GetAccount(this.list_0[i], 31);
								if (account != null && account.ClanId == clan.Id && account.ClanAccess == 3 && ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", account.PlayerId))
								{
									account.ClanAccess = 2;
									SendClanInfo.Load(account, null, 3);
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
										account.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK(), false);
									}
									this.uint_0++;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_ACK(this.uint_0));
							return;
						}
					}
					this.uint_0 = -2147479463;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_COMMISSION_STAFF_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}