using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000081 RID: 129
	public class PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK : GameServerPacket
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003616 File Offset: 0x00001816
		public override void Write()
		{
			base.WriteH(5144);
			base.WriteH(0);
		}
	}
}
