using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_REQUEST_LIST_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CS_REQUEST_LIST_REQ()
		{
		}

		private void method_0(ClanInvite clanInvite_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteQ(clanInvite_0.PlayerId);
			Account account = AccountManager.GetAccount(clanInvite_0.PlayerId, 31);
			if (account != null)
			{
				syncServerPacket_0.WriteU(account.Nickname, 66);
				syncServerPacket_0.WriteC((byte)account.Rank);
				syncServerPacket_0.WriteC((byte)account.NickColor);
				syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
				syncServerPacket_0.WriteD(account.Statistic.Basic.KillsCount);
				syncServerPacket_0.WriteD(account.Statistic.Basic.DeathsCount);
				syncServerPacket_0.WriteD(account.Statistic.Basic.Matches);
				syncServerPacket_0.WriteD(account.Statistic.Basic.MatchWins);
				syncServerPacket_0.WriteD(account.Statistic.Basic.MatchLoses);
				syncServerPacket_0.WriteN(clanInvite_0.Text, clanInvite_0.Text.Length + 2, "UTF-16LE");
			}
			syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			int ınt32;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId != 0)
					{
						List<ClanInvite> clanRequestList = DaoManagerSQL.GetClanRequestList(player.ClanId);
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							int ınt321 = 0;
							ınt32 = (this.int_0 != 0 ? 14 : 13);
							for (int i = this.int_0 * ınt32; i < clanRequestList.Count; i++)
							{
								this.method_0(clanRequestList[i], syncServerPacket);
								int ınt322 = ınt321 + 1;
								ınt321 = ınt322;
								if (ınt322 == 13)
								{
									break;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(0, ınt321, this.int_0, syncServerPacket.ToArray()));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(-1));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_REQUEST_LIST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}