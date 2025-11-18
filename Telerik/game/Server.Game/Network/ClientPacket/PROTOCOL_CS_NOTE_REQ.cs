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
	public class PROTOCOL_CS_NOTE_REQ : GameClientPacket
	{
		private int int_0;

		private string string_0;

		public PROTOCOL_CS_NOTE_REQ()
		{
		}

		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.ClanInfo,
				Text = this.string_0,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			this.string_0 = base.ReadU(base.ReadC() * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (this.string_0.Length <= 120 && player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					int ınt32 = 0;
					if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
					{
						List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, this.Client.PlayerId, true);
						for (int i = 0; i < clanPlayers.Count; i++)
						{
							Account ıtem = clanPlayers[i];
							if ((this.int_0 == 0 || ıtem.ClanAccess == 2 && this.int_0 == 1 || ıtem.ClanAccess == 3 && this.int_0 == 2) && DaoManagerSQL.GetMessagesCount(ıtem.PlayerId) < 100)
							{
								ınt32++;
								MessageModel messageModel = this.method_0(clan, ıtem.PlayerId, this.Client.PlayerId);
								if (messageModel != null && ıtem.IsOnline)
								{
									ıtem.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_NOTE_ACK(ınt32));
					if (ınt32 > 0)
					{
						this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(0));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_NOTE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}