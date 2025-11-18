using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000048 RID: 72
	public class PROTOCOL_MATCH_SERVER_IDX_REQ : AuthClientPacket
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00002ADF File Offset: 0x00000CDF
		public override void Read()
		{
			this.short_0 = base.ReadH();
			base.ReadC();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008C5C File Offset: 0x00006E5C
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_SERVER_IDX_ACK(this.short_0));
				this.Client.Close(0, false);
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + ex.Message, LoggerType.Warning, null);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_MATCH_SERVER_IDX_REQ()
		{
		}

		// Token: 0x04000095 RID: 149
		private short short_0;
	}
}
