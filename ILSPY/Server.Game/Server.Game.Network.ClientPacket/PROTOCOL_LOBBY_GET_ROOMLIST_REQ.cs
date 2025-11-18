using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : GameClientPacket
{
	public override void Read()
	{
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
			ChannelModel channel = player.GetChannel();
			if (channel != null)
			{
				channel.RemoveEmptyRooms();
				List<RoomModel> rooms = channel.Rooms;
				List<Account> waitPlayers = channel.GetWaitPlayers();
				int num = (int)Math.Ceiling((double)rooms.Count / 10.0);
				int num2 = (int)Math.Ceiling((double)waitPlayers.Count / 8.0);
				if (player.LastRoomPage >= num)
				{
					player.LastRoomPage = 0;
				}
				if (player.LastPlayerPage >= num2)
				{
					player.LastPlayerPage = 0;
				}
				int int_ = 0;
				int int_2 = 0;
				byte[] byte_ = method_0(player.LastRoomPage, ref int_, rooms);
				byte[] byte_2 = method_2(player.LastPlayerPage, ref int_2, waitPlayers);
				Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(rooms.Count, waitPlayers.Count, player.LastRoomPage++, player.LastPlayerPage++, int_, int_2, byte_, byte_2));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_GET_ROOMLIST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private byte[] method_0(int int_0, ref int int_1, List<RoomModel> list_0)
	{
		int num = ((int_0 == 0) ? 10 : 11);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_0 * num; i < list_0.Count; i++)
		{
			method_1(list_0[i], syncServerPacket);
			if (++int_1 == 10)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private void method_1(RoomModel roomModel_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(roomModel_0.RoomId);
		syncServerPacket_0.WriteU(roomModel_0.Name, 46);
		syncServerPacket_0.WriteC((byte)roomModel_0.MapId);
		syncServerPacket_0.WriteC((byte)roomModel_0.Rule);
		syncServerPacket_0.WriteC((byte)roomModel_0.Stage);
		syncServerPacket_0.WriteC((byte)roomModel_0.RoomType);
		syncServerPacket_0.WriteC((byte)roomModel_0.State);
		syncServerPacket_0.WriteC((byte)roomModel_0.GetCountPlayers());
		syncServerPacket_0.WriteC((byte)roomModel_0.GetSlotCount());
		syncServerPacket_0.WriteC((byte)roomModel_0.Ping);
		syncServerPacket_0.WriteH((ushort)roomModel_0.WeaponsFlag);
		syncServerPacket_0.WriteD(roomModel_0.GetFlag());
		syncServerPacket_0.WriteH(0);
		syncServerPacket_0.WriteB(roomModel_0.LeaderAddr);
		syncServerPacket_0.WriteC(0);
	}

	private byte[] method_2(int int_0, ref int int_1, List<Account> list_0)
	{
		int num = ((int_0 == 0) ? 8 : 9);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_0 * num; i < list_0.Count; i++)
		{
			method_3(list_0[i], syncServerPacket);
			if (++int_1 == 8)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private void method_3(Account account_0, SyncServerPacket syncServerPacket_0)
	{
		ClanModel clan = ClanManager.GetClan(account_0.ClanId);
		syncServerPacket_0.WriteD(account_0.GetSessionId());
		syncServerPacket_0.WriteD(clan.Logo);
		syncServerPacket_0.WriteC((byte)clan.Effect);
		syncServerPacket_0.WriteU(clan.Name, 34);
		syncServerPacket_0.WriteH((short)account_0.GetRank());
		syncServerPacket_0.WriteU(account_0.Nickname, 66);
		syncServerPacket_0.WriteC((byte)account_0.NickColor);
		syncServerPacket_0.WriteC((byte)NATIONS);
		syncServerPacket_0.WriteD(account_0.Equipment.NameCardId);
		syncServerPacket_0.WriteC((byte)account_0.Bonus.NickBorderColor);
	}
}
