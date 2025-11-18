using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_MATCH_CLAN_SEASON_ACK : AuthServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_MATCH_CLAN_SEASON_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(7700);
			base.WriteH(0);
			base.WriteD(2);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[109]);
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
			base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
			base.WriteB(new byte[103]);
		}
	}
}