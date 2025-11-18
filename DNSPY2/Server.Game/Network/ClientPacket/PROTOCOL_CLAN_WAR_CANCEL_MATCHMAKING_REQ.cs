using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017B RID: 379
	public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ : GameClientPacket
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001E914 File Offset: 0x0001CB14
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ()
		{
		}
	}
}
