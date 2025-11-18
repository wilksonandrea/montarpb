using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003C RID: 60
	public class PROTOCOL_BASE_GAME_SERVER_STATE_REQ : AuthClientPacket
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000073B4 File Offset: 0x000055B4
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

		// Token: 0x060000CE RID: 206 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GAME_SERVER_STATE_REQ()
		{
		}
	}
}
