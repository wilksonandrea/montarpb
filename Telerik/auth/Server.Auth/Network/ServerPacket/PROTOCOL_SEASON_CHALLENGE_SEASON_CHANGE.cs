using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : AuthServerPacket
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