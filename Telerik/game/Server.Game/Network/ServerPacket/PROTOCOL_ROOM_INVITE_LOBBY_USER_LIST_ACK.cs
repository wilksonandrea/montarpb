using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK : GameServerPacket
	{
		private readonly List<Account> list_0;

		private readonly List<int> list_1;

		public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(ChannelModel channelModel_0)
		{
			this.list_0 = channelModel_0.GetWaitPlayers();
			this.list_1 = this.method_0(this.list_0.Count, (this.list_0.Count >= 8 ? 8 : this.list_0.Count));
		}

		private List<int> method_0(int int_0, int int_1)
		{
			if (int_0 == 0 || int_1 == 0)
			{
				return new List<int>();
			}
			Random random = new Random();
			List<int> ınt32s = new List<int>();
			for (int i = 0; i < int_0; i++)
			{
				ınt32s.Add(i);
			}
			for (int j = 0; j < ınt32s.Count; j++)
			{
				int ınt32 = random.Next(ınt32s.Count);
				int ıtem = ınt32s[j];
				ınt32s[j] = ınt32s[ınt32];
				ınt32s[ınt32] = ıtem;
			}
			return ınt32s.GetRange(0, int_1);
		}

		public override void Write()
		{
			base.WriteH(3676);
			base.WriteD(this.list_1.Count);
			foreach (int list1 in this.list_1)
			{
				Account ıtem = this.list_0[list1];
				base.WriteD(ıtem.GetSessionId());
				base.WriteD(ıtem.GetRank());
				base.WriteC((byte)(ıtem.Nickname.Length + 1));
				base.WriteN(ıtem.Nickname, ıtem.Nickname.Length + 2, "UTF-16LE");
				base.WriteC((byte)ıtem.NickColor);
			}
		}
	}
}