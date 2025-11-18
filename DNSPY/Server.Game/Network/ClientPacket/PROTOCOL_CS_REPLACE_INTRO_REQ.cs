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
	// Token: 0x020001A6 RID: 422
	public class PROTOCOL_CS_REPLACE_INTRO_REQ : GameClientPacket
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x000055A6 File Offset: 0x000037A6
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00022154 File Offset: 0x00020354
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && clan.Info != this.string_0 && (clan.OwnerId == this.Client.PlayerId || (player.ClanAccess >= 1 && player.ClanAccess <= 2)))
					{
						if (ComDiv.UpdateDB("system_clan", "info", this.string_0, "id", clan.Id))
						{
							clan.Info = this.string_0;
						}
						else
						{
							this.eventErrorEnum_0 = (EventErrorEnum)2147487860U;
						}
					}
					else
					{
						this.eventErrorEnum_0 = (EventErrorEnum)2147487835U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_REPLACE_INTRO_ACK(this.eventErrorEnum_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REPLACE_INTRO_REQ()
		{
		}

		// Token: 0x0400030E RID: 782
		private string string_0;

		// Token: 0x0400030F RID: 783
		private EventErrorEnum eventErrorEnum_0;
	}
}
