using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000159 RID: 345
	public class PROTOCOL_BASE_TICKET_UPDATE_REQ : GameClientPacket
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001A7E4 File Offset: 0x000189E4
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_TICKET_UPDATE_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_TICKET_UPDATE_REQ()
		{
		}
	}
}
