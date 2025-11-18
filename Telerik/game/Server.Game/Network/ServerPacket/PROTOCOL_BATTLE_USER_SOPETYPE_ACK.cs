using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_USER_SOPETYPE_ACK : GameServerPacket
	{
		private readonly Account account_0;

		public PROTOCOL_BATTLE_USER_SOPETYPE_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(5277);
			base.WriteD(this.account_0.SlotId);
			base.WriteC((byte)this.account_0.Sight);
		}
	}
}