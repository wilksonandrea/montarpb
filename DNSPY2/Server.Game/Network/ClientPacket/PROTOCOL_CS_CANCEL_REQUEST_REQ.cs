using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200018B RID: 395
	public class PROTOCOL_CS_CANCEL_REQUEST_REQ : GameClientPacket
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001FE68 File Offset: 0x0001E068
		public override void Run()
		{
			try
			{
				if (this.Client.Player == null || !DaoManagerSQL.DeleteClanInviteDB(this.Client.PlayerId))
				{
					this.uint_0 = 2147487835U;
				}
				this.Client.SendPacket(new PROTOCOL_CS_CANCEL_REQUEST_ACK(this.uint_0));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CANCEL_REQUEST_REQ()
		{
		}

		// Token: 0x040002E3 RID: 739
		private uint uint_0;
	}
}
