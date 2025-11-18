using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200014F RID: 335
	public class PROTOCOL_BASE_LOGOUT_REQ : GameClientPacket
	{
		// Token: 0x0600034B RID: 843 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00019654 File Offset: 0x00017854
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_LOGOUT_ACK());
				this.Client.Close(1000, true, false);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				this.Client.Close(0, true, false);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_LOGOUT_REQ()
		{
		}
	}
}
