using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000135 RID: 309
	public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ : GameClientPacket
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00004E80 File Offset: 0x00003080
		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00018034 File Offset: 0x00016234
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK((!DaoManagerSQL.IsPlayerNameExist(this.string_0)) ? 0U : 2147483923U));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ()
		{
		}

		// Token: 0x04000231 RID: 561
		private string string_0;
	}
}
