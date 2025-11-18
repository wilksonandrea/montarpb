using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BE RID: 446
	public class PROTOCOL_MATCH_SERVER_IDX_REQ : GameClientPacket
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x000056FC File Offset: 0x000038FC
		public override void Read()
		{
			this.short_0 = base.ReadH();
			base.ReadC();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002471C File Offset: 0x0002291C
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_MATCH_SERVER_IDX_ACK(this.short_0));
				this.Client.Close(0, false, false);
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MATCH_SERVER_IDX_REQ: " + ex.Message, LoggerType.Warning, null);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_MATCH_SERVER_IDX_REQ()
		{
		}

		// Token: 0x04000333 RID: 819
		private short short_0;
	}
}
