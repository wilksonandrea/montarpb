using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000EC RID: 236
	public class PROTOCOL_ROOM_CHATTING_ACK : GameServerPacket
	{
		// Token: 0x06000241 RID: 577 RVA: 0x000045CA File Offset: 0x000027CA
		public PROTOCOL_ROOM_CHATTING_ACK(int int_2, int int_3, bool bool_1, string string_1)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
			this.bool_0 = bool_1;
			this.string_0 = string_1;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000120DC File Offset: 0x000102DC
		public override void Write()
		{
			base.WriteH(3606);
			base.WriteH((short)this.int_0);
			base.WriteD(this.int_1);
			base.WriteC((this.bool_0 > false) ? 1 : 0);
			base.WriteD(this.string_0.Length + 1);
			base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
		}

		// Token: 0x040001B1 RID: 433
		private readonly string string_0;

		// Token: 0x040001B2 RID: 434
		private readonly int int_0;

		// Token: 0x040001B3 RID: 435
		private readonly int int_1;

		// Token: 0x040001B4 RID: 436
		private readonly bool bool_0;
	}
}
