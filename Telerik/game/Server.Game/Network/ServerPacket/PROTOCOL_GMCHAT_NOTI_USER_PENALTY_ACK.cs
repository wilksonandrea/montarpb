using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly uint uint_0;

		public PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(6658);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteC((byte)(this.account_0.Nickname.Length + 1));
				base.WriteN(this.account_0.Nickname, this.account_0.Nickname.Length + 2, "UTF-16LE");
				base.WriteQ(this.account_0.PlayerId);
			}
		}
	}
}