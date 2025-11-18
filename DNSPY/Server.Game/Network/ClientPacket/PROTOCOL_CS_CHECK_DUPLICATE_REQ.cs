using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200018D RID: 397
	public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : GameClientPacket
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000053DA File Offset: 0x000035DA
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001FF74 File Offset: 0x0001E174
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CS_CHECK_DUPLICATE_ACK((!ClanManager.IsClanNameExist(this.string_0)) ? 0U : 2147483648U));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CHECK_DUPLICATE_REQ()
		{
		}

		// Token: 0x040002E6 RID: 742
		private string string_0;
	}
}
