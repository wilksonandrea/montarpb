using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000037 RID: 55
	public class PROTOCOL_MATCH_CLAN_SEASON_REQ : AuthClientPacket
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000071E8 File Offset: 0x000053E8
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_CLAN_SEASON_ACK(0));
				this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_MATCH_CLAN_SEASON_REQ()
		{
		}
	}
}
