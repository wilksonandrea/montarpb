using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000041 RID: 65
	public class PROTOCOL_BASE_GET_SYSTEM_INFO_REQ : AuthClientPacket
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public override void Read()
		{
			base.ReadC();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007958 File Offset: 0x00005B58
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
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_SYSTEM_INFO_REQ()
		{
		}
	}
}
