using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000047 RID: 71
	public class PROTOCOL_BASE_USER_LEAVE_REQ : AuthClientPacket
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008C0C File Offset: 0x00006E0C
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_USER_LEAVE_ACK(0));
				this.Client.Close(0, false);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_USER_LEAVE_REQ()
		{
		}
	}
}
