using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(8456);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			base.WriteC(1);
			base.WriteC(6);
			base.WriteD(2580);
			base.WriteC(5);
			base.WriteC(5);
			base.WriteC(1);
		}
	}
}