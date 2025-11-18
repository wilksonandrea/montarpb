using System;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A5 RID: 165
	public class PROTOCOL_CS_CLIENT_ENTER_ACK : GameServerPacket
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00003B9B File Offset: 0x00001D9B
		public PROTOCOL_CS_CLIENT_ENTER_ACK(int int_2, int int_3)
		{
			this.int_1 = int_2;
			this.int_0 = int_3;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		public override void Write()
		{
			base.WriteH(770);
			base.WriteD(0);
			base.WriteD(this.int_1);
			base.WriteD(this.int_0);
			if (this.int_1 == 0 || this.int_0 == 0)
			{
				base.WriteD(ClanManager.Clans.Count);
				base.WriteC(15);
				base.WriteH((ushort)Math.Ceiling((double)ClanManager.Clans.Count / 15.0));
				base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
			}
		}

		// Token: 0x0400013D RID: 317
		private readonly int int_0;

		// Token: 0x0400013E RID: 318
		private readonly int int_1;
	}
}
