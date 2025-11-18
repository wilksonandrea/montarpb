using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000182 RID: 386
	public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ : GameClientPacket
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001F65C File Offset: 0x0001D85C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					if (match == null || !match.RemovePlayer(player))
					{
						this.uint_0 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(this.uint_0));
					if (this.uint_0 == 0U)
					{
						player.Status.UpdateClanMatch(byte.MaxValue);
						AllUtils.SyncPlayerToClanMembers(player);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ()
		{
		}

		// Token: 0x040002D8 RID: 728
		private uint uint_0;
	}
}
