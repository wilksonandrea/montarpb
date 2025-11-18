using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A3 RID: 419
	public class PROTOCOL_CS_MEMBER_LIST_REQ : GameClientPacket
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00005554 File Offset: 0x00003754
		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00021CEC File Offset: 0x0001FEEC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int num = ((player.ClanId == 0) ? player.FindClanId : player.ClanId);
					if (ClanManager.GetClan(num).Id == 0)
					{
						this.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(uint.MaxValue, byte.MaxValue, byte.MaxValue, new byte[0]));
					}
					else
					{
						List<Account> clanPlayers = ClanManager.GetClanPlayers(num, -1L, false);
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							byte b = 0;
							for (int i = (int)(this.byte_0 * 14); i < clanPlayers.Count; i++)
							{
								this.method_0(clanPlayers[i], syncServerPacket);
								if ((b += 1) == 14)
								{
									break;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(0U, this.byte_0, b, syncServerPacket.ToArray()));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_MEMBER_LIST_REQ " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00021E10 File Offset: 0x00020010
		private void method_0(Account account_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteQ(account_0.PlayerId);
			syncServerPacket_0.WriteU(account_0.Nickname, 66);
			syncServerPacket_0.WriteC((byte)account_0.Rank);
			syncServerPacket_0.WriteC((byte)account_0.ClanAccess);
			syncServerPacket_0.WriteQ(ComDiv.GetClanStatus(account_0.Status, account_0.IsOnline));
			syncServerPacket_0.WriteD(account_0.ClanDate);
			syncServerPacket_0.WriteC((byte)account_0.NickColor);
			syncServerPacket_0.WriteD(account_0.Statistic.Clan.MatchWins);
			syncServerPacket_0.WriteD(account_0.Statistic.Clan.MatchLoses);
			syncServerPacket_0.WriteD(account_0.Equipment.NameCardId);
			syncServerPacket_0.WriteC(0);
			syncServerPacket_0.WriteD(10);
			syncServerPacket_0.WriteD(20);
			syncServerPacket_0.WriteD(30);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_MEMBER_LIST_REQ()
		{
		}

		// Token: 0x04000309 RID: 777
		private byte byte_0;
	}
}
