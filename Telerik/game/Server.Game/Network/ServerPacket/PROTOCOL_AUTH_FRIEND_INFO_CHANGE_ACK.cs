using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : GameServerPacket
	{
		private readonly FriendModel friendModel_0;

		private readonly int int_0;

		private readonly FriendState friendState_0;

		private readonly FriendChangeState friendChangeState_0;

		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, int int_1)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = (FriendState)friendModel_1.State;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_1;
		}

		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, int int_1, int int_2)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = (FriendState)int_1;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_2;
		}

		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, FriendState friendState_1, int int_1)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = friendState_1;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(1815);
			base.WriteC((byte)this.friendChangeState_0);
			base.WriteC((byte)this.int_0);
			if (this.friendChangeState_0 != FriendChangeState.Insert)
			{
				if (this.friendChangeState_0 != FriendChangeState.Update)
				{
					base.WriteB(new byte[24]);
					return;
				}
			}
			PlayerInfo ınfo = this.friendModel_0.Info;
			if (ınfo == null)
			{
				base.WriteB(new byte[24]);
				return;
			}
			base.WriteC((byte)(ınfo.Nickname.Length + 1));
			base.WriteN(ınfo.Nickname, ınfo.Nickname.Length + 2, "UTF-16LE");
			base.WriteQ(this.friendModel_0.PlayerId);
			base.WriteD(ComDiv.GetFriendStatus(this.friendModel_0, this.friendState_0));
			base.WriteD((uint)-1);
			base.WriteC((byte)ınfo.Rank);
			base.WriteB(new byte[6]);
		}
	}
}