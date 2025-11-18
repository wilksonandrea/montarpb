using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013F RID: 319
	public class PROTOCOL_BASE_GAME_SERVER_STATE_REQ : GameClientPacket
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00018E18 File Offset: 0x00017018
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_GAME_SERVER_STATE_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GAME_SERVER_STATE_REQ()
		{
		}
	}
}
