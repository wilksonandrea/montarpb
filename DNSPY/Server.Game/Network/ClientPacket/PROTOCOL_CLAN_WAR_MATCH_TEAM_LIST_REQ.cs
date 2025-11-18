using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000186 RID: 390
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_REQ : GameClientPacket
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001F948 File Offset: 0x0001DB48
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null && channel.Type == ChannelType.Clan)
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(channel.Matches, player.Match.MatchId));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_REQ()
		{
		}
	}
}
