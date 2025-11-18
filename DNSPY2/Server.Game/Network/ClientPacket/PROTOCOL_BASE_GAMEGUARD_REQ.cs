using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013E RID: 318
	public class PROTOCOL_BASE_GAMEGUARD_REQ : GameClientPacket
	{
		// Token: 0x06000318 RID: 792 RVA: 0x00004F6C File Offset: 0x0000316C
		public override void Read()
		{
			base.ReadB(48);
			this.byte_0 = base.ReadB(3);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00018DD0 File Offset: 0x00016FD0
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_GAMEGUARD_ACK(0, this.byte_0));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GAMEGUARD_REQ()
		{
		}

		// Token: 0x04000240 RID: 576
		private byte[] byte_0;
	}
}
