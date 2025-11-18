using System;
using Plugin.Core;
using Plugin.Core.Enums;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D7 RID: 471
	public class PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ : GameClientPacket
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00026BF8 File Offset: 0x00024DF8
		public override void Run()
		{
			try
			{
				CLogger.Print(base.GetType().Name + " CALLLED!", LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MATCH_CLAN_SEASON_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ()
		{
		}
	}
}
