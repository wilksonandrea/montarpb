using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_FRIEND_INFO_ACK : GameServerPacket
	{
		private readonly List<FriendModel> list_0;

		public PROTOCOL_AUTH_FRIEND_INFO_ACK(List<FriendModel> list_1)
		{
			this.list_0 = list_1;
		}

		public override void Write()
		{
			base.WriteH(1810);
			base.WriteC((byte)this.list_0.Count);
			foreach (FriendModel list0 in this.list_0)
			{
				PlayerInfo ınfo = list0.Info;
				if (ınfo != null)
				{
					base.WriteC((byte)(ınfo.Nickname.Length + 1));
					base.WriteN(ınfo.Nickname, ınfo.Nickname.Length + 2, "UTF-16LE");
					base.WriteQ(list0.PlayerId);
					base.WriteD(ComDiv.GetFriendStatus(list0));
					base.WriteD((uint)-1);
					base.WriteC((byte)ınfo.Rank);
					base.WriteB(new byte[6]);
				}
				else
				{
					base.WriteB(new byte[24]);
				}
			}
		}
	}
}