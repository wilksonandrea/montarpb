using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000039 RID: 57
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : AuthClientPacket
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000072A0 File Offset: 0x000054A0
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ()
		{
		}
	}
}
