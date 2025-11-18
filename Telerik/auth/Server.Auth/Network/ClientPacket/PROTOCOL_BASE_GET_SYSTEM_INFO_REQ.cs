using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_SYSTEM_INFO_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_GET_SYSTEM_INFO_REQ()
		{
		}

		public override void Read()
		{
			base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ServerConfig config = AuthXender.Client.Config;
					if (config != null)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_NOTICE_ACK(config));
						if (config.OfficialBannerEnabled)
						{
							this.Client.SendPacket(new PROTOCOL_BASE_URL_LIST_ACK(config));
						}
						this.Client.SendPacket(new PROTOCOL_BASE_BOOSTEVENT_INFO_ACK());
						this.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK());
						this.Client.SendPacket(new PROTOCOL_BASE_GET_SYSTEM_INFO_ACK(config));
						this.Client.SendPacket(new PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK());
					}
					if (!player.MyConfigsLoaded)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_OPTION_ACK(0, player.Config));
					}
					List<MessageModel> giftMessages = DaoManagerSQL.GetGiftMessages(player.PlayerId);
					if (giftMessages.Count > 0)
					{
						DaoManagerSQL.RecycleMessages(player.PlayerId, giftMessages);
						if (giftMessages.Count > 0)
						{
							this.Client.SendPacket(new PROTOCOL_BASE_USER_GIFTLIST_ACK(0, giftMessages));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}