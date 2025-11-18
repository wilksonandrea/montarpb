using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000033 RID: 51
	public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00002C94 File Offset: 0x00000E94
		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, int int_1)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = (FriendState)friendModel_1.State;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002CBD File Offset: 0x00000EBD
		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, int int_1, int int_2)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = (FriendState)int_1;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_2;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002CBD File Offset: 0x00000EBD
		public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, FriendState friendState_1, int int_1)
		{
			this.friendChangeState_0 = friendChangeState_1;
			this.friendState_0 = friendState_1;
			this.friendModel_0 = friendModel_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000A574 File Offset: 0x00008774
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
			PlayerInfo info = this.friendModel_0.Info;
			if (info == null)
			{
				base.WriteB(new byte[24]);
				return;
			}
			base.WriteC((byte)(info.Nickname.Length + 1));
			base.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
			base.WriteQ(this.friendModel_0.PlayerId);
			base.WriteD(ComDiv.GetFriendStatus(this.friendModel_0, this.friendState_0));
			base.WriteD(uint.MaxValue);
			base.WriteC((byte)info.Rank);
			base.WriteB(new byte[6]);
		}

		// Token: 0x0400005F RID: 95
		private readonly FriendModel friendModel_0;

		// Token: 0x04000060 RID: 96
		private readonly int int_0;

		// Token: 0x04000061 RID: 97
		private readonly FriendState friendState_0;

		// Token: 0x04000062 RID: 98
		private readonly FriendChangeState friendChangeState_0;
	}
}
