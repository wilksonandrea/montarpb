using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A8 RID: 424
	public class PROTOCOL_CS_REPLACE_NOTICE_REQ : GameClientPacket
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x000055FA File Offset: 0x000037FA
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0002239C File Offset: 0x0002059C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && clan.News != this.string_0 && (clan.OwnerId == this.Client.PlayerId || (player.ClanAccess >= 1 && player.ClanAccess <= 2)))
					{
						if (ComDiv.UpdateDB("system_clan", "news", this.string_0, "id", clan.Id))
						{
							clan.News = this.string_0;
						}
						else
						{
							this.eventErrorEnum_0 = (EventErrorEnum)2147487859U;
						}
					}
					else
					{
						this.eventErrorEnum_0 = (EventErrorEnum)2147487835U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_REPLACE_NOTICE_ACK(this.eventErrorEnum_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REPLACE_NOTICE_REQ()
		{
		}

		// Token: 0x04000316 RID: 790
		private string string_0;

		// Token: 0x04000317 RID: 791
		private EventErrorEnum eventErrorEnum_0;
	}
}
