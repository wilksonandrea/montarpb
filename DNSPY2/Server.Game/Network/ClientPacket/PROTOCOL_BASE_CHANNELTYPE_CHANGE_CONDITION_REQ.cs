using System;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000138 RID: 312
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : GameClientPacket
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00004EF6 File Offset: 0x000030F6
		public override void Run()
		{
			this.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ()
		{
		}
	}
}
