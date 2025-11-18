using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000032 RID: 50
	public class PROTOCOL_AUTH_FRIEND_INFO_ACK : GameServerPacket
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00002C85 File Offset: 0x00000E85
		public PROTOCOL_AUTH_FRIEND_INFO_ACK(List<FriendModel> list_1)
		{
			this.list_0 = list_1;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000A480 File Offset: 0x00008680
		public override void Write()
		{
			base.WriteH(1810);
			base.WriteC((byte)this.list_0.Count);
			foreach (FriendModel friendModel in this.list_0)
			{
				PlayerInfo info = friendModel.Info;
				if (info == null)
				{
					base.WriteB(new byte[24]);
				}
				else
				{
					base.WriteC((byte)(info.Nickname.Length + 1));
					base.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
					base.WriteQ(friendModel.PlayerId);
					base.WriteD(ComDiv.GetFriendStatus(friendModel));
					base.WriteD(uint.MaxValue);
					base.WriteC((byte)info.Rank);
					base.WriteB(new byte[6]);
				}
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly List<FriendModel> list_0;
	}
}
