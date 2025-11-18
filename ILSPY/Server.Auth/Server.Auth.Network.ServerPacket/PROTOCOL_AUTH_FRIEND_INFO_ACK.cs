using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_ACK : AuthServerPacket
{
	private readonly List<FriendModel> list_0;

	public PROTOCOL_AUTH_FRIEND_INFO_ACK(List<FriendModel> list_1)
	{
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1810);
		WriteC((byte)list_0.Count);
		foreach (FriendModel item in list_0)
		{
			PlayerInfo ınfo = item.Info;
			if (ınfo == null)
			{
				WriteB(new byte[24]);
				continue;
			}
			WriteC((byte)(ınfo.Nickname.Length + 1));
			WriteN(ınfo.Nickname, ınfo.Nickname.Length + 2, "UTF-16LE");
			WriteQ(ınfo.PlayerId);
			WriteD(ComDiv.GetFriendStatus(item));
			WriteD(uint.MaxValue);
			WriteC((byte)ınfo.Rank);
			WriteB(new byte[6]);
		}
	}
}
