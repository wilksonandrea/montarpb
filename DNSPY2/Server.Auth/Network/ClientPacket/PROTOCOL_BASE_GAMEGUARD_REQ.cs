using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003B RID: 59
	public class PROTOCOL_BASE_GAMEGUARD_REQ : AuthClientPacket
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public override void Read()
		{
			base.ReadB(48);
			this.byte_0 = base.ReadB(3);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007364 File Offset: 0x00005564
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_GAMEGUARD_ACK(0, this.byte_0));
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GAMEGUARD_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GAMEGUARD_REQ()
		{
		}

		// Token: 0x04000071 RID: 113
		private byte[] byte_0;
	}
}
