using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly Account account_0;

		public PROTOCOL_AUTH_GET_POINT_CASH_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(1058);
			base.WriteD(this.uint_0);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.account_0.Tags);
			base.WriteD(0);
		}
	}
}