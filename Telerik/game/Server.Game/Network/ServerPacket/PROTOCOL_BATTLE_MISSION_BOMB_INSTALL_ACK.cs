using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly float float_0;

		private readonly float float_1;

		private readonly float float_2;

		private readonly byte byte_0;

		private readonly ushort ushort_0;

		public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(int int_1, byte byte_1, ushort ushort_1, float float_3, float float_4, float float_5)
		{
			this.byte_0 = byte_1;
			this.int_0 = int_1;
			this.ushort_0 = ushort_1;
			this.float_0 = float_3;
			this.float_1 = float_4;
			this.float_2 = float_5;
		}

		public override void Write()
		{
			base.WriteH(5157);
			base.WriteD(this.int_0);
			base.WriteC(this.byte_0);
			base.WriteH(this.ushort_0);
			base.WriteT(this.float_0);
			base.WriteT(this.float_1);
			base.WriteT(this.float_2);
		}
	}
}