using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000130 RID: 304
	public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ : GameClientPacket
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000160C4 File Offset: 0x000142C4
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(2148110592U));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ()
		{
		}
	}
}
