using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000104 RID: 260
	public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK()
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000495C File Offset: 0x00002B5C
		public override void Write()
		{
			base.WriteH(3683);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
		}
	}
}
