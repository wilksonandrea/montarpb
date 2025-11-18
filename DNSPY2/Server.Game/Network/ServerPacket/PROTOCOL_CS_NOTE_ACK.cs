using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BD RID: 189
	public class PROTOCOL_CS_NOTE_ACK : GameServerPacket
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00003F11 File Offset: 0x00002111
		public PROTOCOL_CS_NOTE_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00003F20 File Offset: 0x00002120
		public override void Write()
		{
			base.WriteH(893);
			base.WriteD(this.int_0);
		}

		// Token: 0x0400015B RID: 347
		private readonly int int_0;
	}
}
