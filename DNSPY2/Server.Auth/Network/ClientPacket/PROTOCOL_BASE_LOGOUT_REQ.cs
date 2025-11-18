using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000045 RID: 69
	public class PROTOCOL_BASE_LOGOUT_REQ : AuthClientPacket
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008554 File Offset: 0x00006754
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_LOGOUT_ACK());
				this.Client.Close(5000, true);
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_LOGOUT_REQ: " + ex.Message, LoggerType.Error, ex);
				this.Client.Close(0, true);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_LOGOUT_REQ()
		{
		}
	}
}
