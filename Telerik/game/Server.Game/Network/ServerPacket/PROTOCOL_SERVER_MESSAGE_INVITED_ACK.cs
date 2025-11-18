using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_INVITED_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly RoomModel roomModel_0;

		public PROTOCOL_SERVER_MESSAGE_INVITED_ACK(Account account_1, RoomModel roomModel_1)
		{
			this.account_0 = account_1;
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(3077);
			base.WriteU(this.account_0.Nickname, 66);
			base.WriteD(this.roomModel_0.RoomId);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteS(this.roomModel_0.Password, 4);
		}
	}
}