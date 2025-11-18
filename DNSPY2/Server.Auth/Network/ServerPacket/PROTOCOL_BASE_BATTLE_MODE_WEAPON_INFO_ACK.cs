using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000009 RID: 9
	public class PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK : AuthServerPacket
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK()
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000024A4 File Offset: 0x000006A4
		public override void Write()
		{
			base.WriteH(2484);
			base.WriteC(0);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(33602800);
		}
	}
}
