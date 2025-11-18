using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015B RID: 347
	public class PROTOCOL_BASE_USER_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_USER_LEAVE_ACK(0));
				this.Client.Close(0, false, false);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_USER_LEAVE_REQ()
		{
		}
	}
}
