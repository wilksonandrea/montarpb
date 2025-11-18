using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000190 RID: 400
	public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ : GameClientPacket
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0000540C File Offset: 0x0000360C
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000200E8 File Offset: 0x0001E2E8
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
						this.Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(0, clan));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ()
		{
		}

		// Token: 0x040002EB RID: 747
		private int int_0;
	}
}
