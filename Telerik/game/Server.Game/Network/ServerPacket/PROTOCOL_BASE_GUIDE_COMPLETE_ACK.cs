using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GUIDE_COMPLETE_ACK : GameServerPacket
	{
		public PROTOCOL_BASE_GUIDE_COMPLETE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2341);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
		}
	}
}