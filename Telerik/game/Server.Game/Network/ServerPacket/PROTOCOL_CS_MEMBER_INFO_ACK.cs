using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_MEMBER_INFO_ACK : GameServerPacket
	{
		private readonly List<Account> list_0;

		public PROTOCOL_CS_MEMBER_INFO_ACK(List<Account> list_1)
		{
			this.list_0 = list_1;
		}

		public override void Write()
		{
			base.WriteH(845);
			base.WriteC((byte)this.list_0.Count);
			foreach (Account list0 in this.list_0)
			{
				base.WriteC((byte)(list0.Nickname.Length + 1));
				base.WriteN(list0.Nickname, list0.Nickname.Length + 2, "UTF-16LE");
				base.WriteQ(list0.PlayerId);
				base.WriteQ(ComDiv.GetClanStatus(list0.Status, list0.IsOnline));
				base.WriteC((byte)list0.Rank);
				base.WriteC((byte)list0.NickColor);
				base.WriteD(list0.Statistic.Clan.MatchWins);
				base.WriteD(list0.Statistic.Clan.MatchLoses);
				base.WriteD(list0.Equipment.NameCardId);
				base.WriteC((byte)list0.Bonus.NickBorderColor);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
			}
		}
	}
}