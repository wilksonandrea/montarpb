using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : AuthServerPacket
{
	private readonly FriendModel friendModel_0;

	private readonly int int_0;

	private readonly FriendState friendState_0;

	private readonly FriendChangeState friendChangeState_0;

	public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, FriendState friendState_1, int int_1)
	{
		friendState_0 = friendState_1;
		friendModel_0 = friendModel_1;
		friendChangeState_0 = friendChangeState_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1815);
		WriteC((byte)friendChangeState_0);
		WriteC((byte)int_0);
		if (friendChangeState_0 != FriendChangeState.Insert && friendChangeState_0 != FriendChangeState.Update)
		{
			WriteB(new byte[24]);
			return;
		}
		PlayerInfo ınfo = friendModel_0.Info;
		if (ınfo == null)
		{
			WriteB(new byte[24]);
			return;
		}
		WriteC((byte)(ınfo.Nickname.Length + 1));
		WriteN(ınfo.Nickname, ınfo.Nickname.Length + 2, "UTF-16LE");
		WriteQ(friendModel_0.PlayerId);
		WriteD(ComDiv.GetFriendStatus(friendModel_0, friendState_0));
		WriteD(uint.MaxValue);
		WriteC((byte)ınfo.Rank);
		WriteB(new byte[6]);
	}
}
