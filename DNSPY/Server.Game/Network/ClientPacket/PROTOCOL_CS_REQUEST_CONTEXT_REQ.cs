using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A9 RID: 425
	public class PROTOCOL_CS_REQUEST_CONTEXT_REQ : GameClientPacket
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00022490 File Offset: 0x00020690
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_CS_REQUEST_CONTEXT_ACK(player.ClanId));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REQUEST_CONTEXT_REQ()
		{
		}
	}
}
