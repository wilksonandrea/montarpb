using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000196 RID: 406
	public class PROTOCOL_CS_CLOSE_CLAN_REQ : GameClientPacket
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00020628 File Offset: 0x0001E828
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId && ComDiv.DeleteDB("system_clan", "id", clan.Id) && ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }) && ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }) && ClanManager.RemoveClan(clan))
					{
						player.ClanId = 0;
						player.ClanAccess = 0;
						SendClanInfo.Load(clan, 1);
					}
					else
					{
						this.uint_0 = 2147487850U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLOSE_CLAN_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CLOSE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLOSE_CLAN_REQ()
		{
		}

		// Token: 0x040002F3 RID: 755
		private uint uint_0;
	}
}
