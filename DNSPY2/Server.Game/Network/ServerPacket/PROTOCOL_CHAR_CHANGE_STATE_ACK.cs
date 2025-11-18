using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000085 RID: 133
	public class PROTOCOL_CHAR_CHANGE_STATE_ACK : GameServerPacket
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000036C0 File Offset: 0x000018C0
		public PROTOCOL_CHAR_CHANGE_STATE_ACK(CharacterModel characterModel_1)
		{
			this.characterModel_0 = characterModel_1;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000036CF File Offset: 0x000018CF
		public override void Write()
		{
			base.WriteH(6153);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteC(20);
			base.WriteC((byte)this.characterModel_0.Slot);
		}

		// Token: 0x04000101 RID: 257
		private readonly CharacterModel characterModel_0;
	}
}
