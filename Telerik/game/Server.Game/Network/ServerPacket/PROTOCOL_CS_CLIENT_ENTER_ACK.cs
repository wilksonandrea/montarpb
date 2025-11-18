using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLIENT_ENTER_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_CS_CLIENT_ENTER_ACK(int int_2, int int_3)
		{
			this.int_1 = int_2;
			this.int_0 = int_3;
		}

		public override void Write()
		{
			base.WriteH(770);
			base.WriteD(0);
			base.WriteD(this.int_1);
			base.WriteD(this.int_0);
			if (this.int_1 == 0 || this.int_0 == 0)
			{
				base.WriteD(ClanManager.Clans.Count);
				base.WriteC(15);
				base.WriteH((ushort)Math.Ceiling((double)ClanManager.Clans.Count / 15));
				base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
			}
		}
	}
}