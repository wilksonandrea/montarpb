using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000044 RID: 68
	public class PROTOCOL_BASE_LOGIN_WAIT_REQ : AuthClientPacket
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008514 File Offset: 0x00006714
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_LOGIN_WAIT_ACK(0));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_LOGIN_WAIT_REQ()
		{
		}
	}
}
