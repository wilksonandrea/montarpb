using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_CHANGE_COSTUME_ACK : GameServerPacket
	{
		private readonly SlotModel slotModel_0;

		public PROTOCOL_ROOM_CHANGE_COSTUME_ACK(SlotModel slotModel_1)
		{
			this.slotModel_0 = slotModel_1;
		}

		public override void Write()
		{
			base.WriteH(3678);
			base.WriteD(this.slotModel_0.Id);
			base.WriteC((byte)this.slotModel_0.CostumeTeam);
		}
	}
}