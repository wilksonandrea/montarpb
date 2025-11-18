using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_MATCH_CLAN_SEASON_ACK : GameServerPacket
	{
		private readonly bool bool_0;

		public PROTOCOL_MATCH_CLAN_SEASON_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		public override void Write()
		{
			base.WriteH(7700);
			base.WriteH(0);
			base.WriteD(2);
			base.WriteD(this.bool_0);
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