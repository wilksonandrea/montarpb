using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_START_COUNTDOWN_ACK : GameServerPacket
	{
		private readonly CountDownEnum countDownEnum_0;

		public PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum countDownEnum_1)
		{
			this.countDownEnum_0 = countDownEnum_1;
		}

		public override void Write()
		{
			base.WriteH(5126);
			base.WriteC((byte)this.countDownEnum_0);
		}
	}
}