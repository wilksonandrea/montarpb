using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BD RID: 445
	public class PROTOCOL_MATCH_CLAN_SEASON_REQ : GameClientPacket
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000246C0 File Offset: 0x000228C0
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_CLAN_SEASON_ACK(false));
				this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_MATCH_CLAN_SEASON_REQ()
		{
		}
	}
}
