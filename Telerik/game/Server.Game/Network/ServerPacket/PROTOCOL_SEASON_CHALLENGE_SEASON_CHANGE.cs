using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : GameServerPacket
	{
		public PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE()
		{
		}

		public override void Write()
		{
			base.WriteH(8451);
			base.WriteH(0);
			base.WriteC(1);
		}
	}
}