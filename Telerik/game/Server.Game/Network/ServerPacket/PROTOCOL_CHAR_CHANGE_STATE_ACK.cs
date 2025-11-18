using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CHAR_CHANGE_STATE_ACK : GameServerPacket
	{
		private readonly CharacterModel characterModel_0;

		public PROTOCOL_CHAR_CHANGE_STATE_ACK(CharacterModel characterModel_1)
		{
			this.characterModel_0 = characterModel_1;
		}

		public override void Write()
		{
			base.WriteH(6153);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteC(20);
			base.WriteC((byte)this.characterModel_0.Slot);
		}
	}
}