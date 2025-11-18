using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001AA RID: 426
	public class PROTOCOL_CS_REQUEST_INFO_REQ : GameClientPacket
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00005610 File Offset: 0x00003810
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000224E8 File Offset: 0x000206E8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_CS_REQUEST_INFO_ACK(this.long_0, DaoManagerSQL.GetRequestClanInviteText(player.ClanId, this.long_0)));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REQUEST_INFO_REQ()
		{
		}

		// Token: 0x04000318 RID: 792
		private long long_0;
	}
}
