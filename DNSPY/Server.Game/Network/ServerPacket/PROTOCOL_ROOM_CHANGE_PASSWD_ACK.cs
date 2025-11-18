using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E8 RID: 232
	public class PROTOCOL_ROOM_CHANGE_PASSWD_ACK : GameServerPacket
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00004536 File Offset: 0x00002736
		public PROTOCOL_ROOM_CHANGE_PASSWD_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00004545 File Offset: 0x00002745
		public override void Write()
		{
			base.WriteH(3603);
			base.WriteS(this.string_0, 4);
		}

		// Token: 0x040001AC RID: 428
		private readonly string string_0;
	}
}
