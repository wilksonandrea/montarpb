using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017F RID: 383
	public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ : GameClientPacket
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000052E9 File Offset: 0x000034E9
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001F210 File Offset: 0x0001D410
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					MatchModel match = player.Match;
					if (match != null && player.MatchSlot == match.Leader)
					{
						match.Training = this.int_0;
						using (PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK protocol_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK = new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(0U, this.int_0))
						{
							match.SendPacketToPlayers(protocol_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK);
							goto IL_6B;
						}
					}
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(2147483648U, 0));
					IL_6B:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_REQ()
		{
		}

		// Token: 0x040002D0 RID: 720
		private int int_0;
	}
}
