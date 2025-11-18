using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000185 RID: 389
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ : GameClientPacket
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000535D File Offset: 0x0000355D
		public override void Read()
		{
			this.int_0 = (int)base.ReadH();
			this.int_1 = (int)base.ReadH();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001F880 File Offset: 0x0001DA80
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					int num = this.int_1 - this.int_1 / 10 * 10;
					ChannelModel channel = ChannelsXML.GetChannel(this.int_1, num);
					if (channel != null)
					{
						MatchModel match = channel.GetMatch(this.int_0);
						if (match != null)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(0U, match.Clan));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(2147483648U));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(2147483648U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ()
		{
		}

		// Token: 0x040002DC RID: 732
		private int int_0;

		// Token: 0x040002DD RID: 733
		private int int_1;
	}
}
