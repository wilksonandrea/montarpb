using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_GMCHAT_END_CHAT_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly uint uint_0;

		public PROTOCOL_GMCHAT_END_CHAT_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(6662);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}
	}
}