using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200019E RID: 414
	public class PROTOCOL_CS_DETAIL_INFO_REQ : GameClientPacket
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x000054FE File Offset: 0x000036FE
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = (int)base.ReadC();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0002163C File Offset: 0x0001F83C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					player.FindClanId = this.int_0;
					ClanModel clan = ClanManager.GetClan(player.FindClanId);
					if (clan.Id > 0)
					{
						this.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(this.int_1, clan));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_DETAIL_INFO_REQ()
		{
		}

		// Token: 0x04000301 RID: 769
		private int int_0;

		// Token: 0x04000302 RID: 770
		private int int_1;
	}
}
