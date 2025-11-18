using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200014E RID: 334
	public class PROTOCOL_BASE_GUIDE_COMPLETE_REQ : GameClientPacket
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00019614 File Offset: 0x00017814
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_GUIDE_COMPLETE_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GUIDE_COMPLETE_REQ()
		{
		}
	}
}
