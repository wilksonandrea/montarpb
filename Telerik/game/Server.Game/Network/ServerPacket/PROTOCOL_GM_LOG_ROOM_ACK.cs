using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_GM_LOG_ROOM_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly uint uint_0;

		public PROTOCOL_GM_LOG_ROOM_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(2687);
			base.WriteD(this.uint_0);
			base.WriteQ(this.account_0.PlayerId);
		}
	}
}