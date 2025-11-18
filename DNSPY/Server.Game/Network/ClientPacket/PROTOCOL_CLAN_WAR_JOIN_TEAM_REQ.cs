using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000181 RID: 385
	public class PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ : GameClientPacket
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000531D File Offset: 0x0000351D
		public override void Read()
		{
			this.int_0 = (int)base.ReadH();
			this.int_1 = (int)base.ReadH();
			this.int_2 = (int)base.ReadC();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (this.int_2 < 2 && player != null && player.Match == null && player.Room == null)
				{
					int num = this.int_1 - this.int_1 / 10 * 10;
					ChannelModel channel = ChannelsXML.GetChannel(this.int_1, (this.int_2 == 0) ? num : player.ChannelId);
					if (channel != null)
					{
						if (player.ClanId == 0)
						{
							this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147487835U, null));
						}
						else
						{
							MatchModel matchModel = ((this.int_2 == 1) ? channel.GetMatch(this.int_0, player.ClanId) : channel.GetMatch(this.int_0));
							if (matchModel != null)
							{
								this.method_0(player, matchModel);
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648U, null));
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648U, null));
					}
				}
				else
				{
					this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(2147483648U, null));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		private void method_0(Account account_0, MatchModel matchModel_0)
		{
			if (!matchModel_0.AddPlayer(account_0))
			{
				this.uint_0 = 2147483648U;
			}
			this.Client.SendPacket(new PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(this.uint_0, matchModel_0));
			if (this.uint_0 == 0U)
			{
				using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_CLAN_WAR_REGIST_MERCENARY_ACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(matchModel_0))
				{
					matchModel_0.SendPacketToPlayers(protocol_CLAN_WAR_REGIST_MERCENARY_ACK);
				}
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CLAN_WAR_JOIN_TEAM_REQ()
		{
		}

		// Token: 0x040002D4 RID: 724
		private int int_0;

		// Token: 0x040002D5 RID: 725
		private int int_1;

		// Token: 0x040002D6 RID: 726
		private int int_2;

		// Token: 0x040002D7 RID: 727
		private uint uint_0;
	}
}
