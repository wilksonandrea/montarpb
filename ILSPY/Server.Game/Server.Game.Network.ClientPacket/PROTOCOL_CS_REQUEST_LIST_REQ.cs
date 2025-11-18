using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REQUEST_LIST_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (player.ClanId == 0)
			{
				Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(-1));
				return;
			}
			List<ClanInvite> clanRequestList = DaoManagerSQL.GetClanRequestList(player.ClanId);
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			int num = 0;
			int num2 = ((int_0 != 0) ? 14 : 13);
			for (int i = int_0 * num2; i < clanRequestList.Count; i++)
			{
				method_0(clanRequestList[i], syncServerPacket);
				if (++num == 13)
				{
					break;
				}
			}
			Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(0, num, int_0, syncServerPacket.ToArray()));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_REQUEST_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
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
}
