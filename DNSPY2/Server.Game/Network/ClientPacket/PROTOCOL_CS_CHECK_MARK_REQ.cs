using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200018F RID: 399
	public class PROTOCOL_CS_CHECK_MARK_REQ : GameClientPacket
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x000053FE File Offset: 0x000035FE
		public override void Read()
		{
			this.uint_0 = base.ReadUD();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00020064 File Offset: 0x0001E264
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player == null || ClanManager.GetClan(player.ClanId).Logo == this.uint_0 || ClanManager.IsClanLogoExist(this.uint_0))
				{
					this.uint_1 = 2147483648U;
				}
				this.Client.SendPacket(new PROTOCOL_CS_CHECK_MARK_ACK(this.uint_1));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CHECK_MARK_REQ()
		{
		}

		// Token: 0x040002E9 RID: 745
		private uint uint_0;

		// Token: 0x040002EA RID: 746
		private uint uint_1;
	}
}
