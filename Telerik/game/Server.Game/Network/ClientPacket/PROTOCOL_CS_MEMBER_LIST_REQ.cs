using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_MEMBER_LIST_REQ : GameClientPacket
	{
		private byte byte_0;

		public PROTOCOL_CS_MEMBER_LIST_REQ()
		{
		}

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

		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int ınt32 = (player.ClanId == 0 ? player.FindClanId : player.ClanId);
					if (ClanManager.GetClan(ınt32).Id != 0)
					{
						List<Account> clanPlayers = ClanManager.GetClanPlayers(ınt32, -1L, false);
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							byte num = 0;
							for (int i = this.byte_0 * 14; i < clanPlayers.Count; i++)
							{
								this.method_0(clanPlayers[i], syncServerPacket);
								byte num1 = (byte)(num + 1);
								num = num1;
								if (num1 == 14)
								{
									break;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(0, this.byte_0, num, syncServerPacket.ToArray()));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(-1, 255, 255, new byte[0]));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_MEMBER_LIST_REQ ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}