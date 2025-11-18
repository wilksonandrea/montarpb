using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_MEMBER_LIST_REQ : GameClientPacket
{
	private byte byte_0;

	public override void Read()
	{
		byte_0 = ReadC();
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
			int clanId = ((player.ClanId == 0) ? player.FindClanId : player.ClanId);
			if (ClanManager.GetClan(clanId).Id == 0)
			{
				Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(uint.MaxValue, byte.MaxValue, byte.MaxValue, new byte[0]));
				return;
			}
			List<Account> clanPlayers = ClanManager.GetClanPlayers(clanId, -1L, UseCache: false);
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			byte b = 0;
			for (int i = byte_0 * 14; i < clanPlayers.Count; i++)
			{
				method_0(clanPlayers[i], syncServerPacket);
				if ((b = (byte)(b + 1)) == 14)
				{
					break;
				}
			}
			Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(0u, byte_0, b, syncServerPacket.ToArray()));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_MEMBER_LIST_REQ " + ex.Message, LoggerType.Error, ex);
		}
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
}
