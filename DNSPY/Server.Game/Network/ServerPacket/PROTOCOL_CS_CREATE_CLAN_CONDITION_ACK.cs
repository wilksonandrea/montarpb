using System;
using Plugin.Core;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AF RID: 175
	public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : GameServerPacket
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public override void Write()
		{
			base.WriteH(915);
			base.WriteC((byte)ConfigLoader.MinCreateRank);
			base.WriteD(ConfigLoader.MinCreateGold);
		}
	}
}
