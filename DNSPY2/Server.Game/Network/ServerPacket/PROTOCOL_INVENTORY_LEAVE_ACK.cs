using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D8 RID: 216
	public class PROTOCOL_INVENTORY_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_INVENTORY_LEAVE_ACK()
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000434C File Offset: 0x0000254C
		public override void Write()
		{
			base.WriteH(3332);
			base.WriteH(0);
			base.WriteD(0);
		}
	}
}
