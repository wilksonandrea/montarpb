using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_GM_PAUSE_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_BATTLE_GM_PAUSE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(5206);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteD(1);
			}
		}
	}
}