using System;
using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK : GameServerPacket
{
	private readonly List<Account> list_0;

	private readonly List<int> list_1;

	public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(ChannelModel channelModel_0)
	{
		list_0 = channelModel_0.GetWaitPlayers();
		list_1 = method_0(list_0.Count, (list_0.Count >= 8) ? 8 : list_0.Count);
	}

	public override void Write()
	{
		WriteH(3676);
		WriteD(list_1.Count);
		foreach (int item in list_1)
		{
			Account account = list_0[item];
			WriteD(account.GetSessionId());
			WriteD(account.GetRank());
			WriteC((byte)(account.Nickname.Length + 1));
			WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
			WriteC((byte)account.NickColor);
		}
	}

	private List<int> method_0(int int_0, int int_1)
	{
		if (int_0 != 0 && int_1 != 0)
		{
			Random random = new Random();
			List<int> list = new List<int>();
			for (int i = 0; i < int_0; i++)
			{
				list.Add(i);
			}
			for (int j = 0; j < list.Count; j++)
			{
				int index = random.Next(list.Count);
				int value = list[j];
				list[j] = list[index];
				list[index] = value;
			}
			return list.GetRange(0, int_1);
		}
		return new List<int>();
	}
}
