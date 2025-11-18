using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000184 RID: 388
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ : GameClientPacket
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001F808 File Offset: 0x0001DA08
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
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(channel.Matches.Count));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ()
		{
		}
	}
}
