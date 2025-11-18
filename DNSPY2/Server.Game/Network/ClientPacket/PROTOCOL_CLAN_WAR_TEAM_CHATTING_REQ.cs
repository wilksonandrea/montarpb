using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000189 RID: 393
	public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ : GameClientPacket
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00005385 File Offset: 0x00003585
		public override void Read()
		{
			this.chattingType_0 = (ChattingType)base.ReadH();
			this.string_0 = base.ReadS((int)base.ReadH());
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Match != null)
				{
					if (this.chattingType_0 == ChattingType.Match)
					{
						MatchModel match = player.Match;
						using (PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK protocol_CLAN_WAR_TEAM_CHATTING_ACK = new PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(player.Nickname, this.string_0))
						{
							match.SendPacketToPlayers(protocol_CLAN_WAR_TEAM_CHATTING_ACK);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ()
		{
		}

		// Token: 0x040002DF RID: 735
		private ChattingType chattingType_0;

		// Token: 0x040002E0 RID: 736
		private string string_0;
	}
}
