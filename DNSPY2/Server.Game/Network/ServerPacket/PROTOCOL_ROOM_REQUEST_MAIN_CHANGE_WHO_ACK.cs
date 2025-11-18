using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000103 RID: 259
	public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK : GameServerPacket
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00004934 File Offset: 0x00002B34
		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00004934 File Offset: 0x00002B34
		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int int_0)
		{
			this.uint_0 = (uint)int_0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00004943 File Offset: 0x00002B43
		public override void Write()
		{
			base.WriteH(3616);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001DE RID: 478
		private readonly uint uint_0;
	}
}
