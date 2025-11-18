using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_DENIAL_REQUEST_REQ : GameClientPacket
	{
		private List<long> list_0 = new List<long>();

		private int int_0;

		public PROTOCOL_CS_DENIAL_REQUEST_REQ()
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
				ClanNote = NoteMessageClan.InviteDenial
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
					if (clan.Id > 0 && (player.ClanAccess >= 1 && player.ClanAccess <= 2 || clan.OwnerId == player.PlayerId))
					{
						for (int i = 0; i < this.list_0.Count; i++)
						{
							long ıtem = this.list_0[i];
							if (DaoManagerSQL.DeleteClanInviteDB(clan.Id, ıtem))
							{
								if (DaoManagerSQL.GetMessagesCount(ıtem) < 100)
								{
									MessageModel messageModel = this.method_0(clan, ıtem, player.PlayerId);
									if (messageModel != null)
									{
										Account account = AccountManager.GetAccount(ıtem, 31);
										if (account != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
								}
								this.int_0++;
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_DENIAL_REQUEST_ACK(this.int_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_DENIAL_REQUEST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}