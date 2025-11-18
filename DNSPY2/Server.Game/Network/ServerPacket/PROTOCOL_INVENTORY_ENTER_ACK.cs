using System;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D6 RID: 214
	public class PROTOCOL_INVENTORY_ENTER_ACK : GameServerPacket
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_INVENTORY_ENTER_ACK()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004323 File Offset: 0x00002523
		public override void Write()
		{
			base.WriteH(3330);
			base.WriteD(0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}
