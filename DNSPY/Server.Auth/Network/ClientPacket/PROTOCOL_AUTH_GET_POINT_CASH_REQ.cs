using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000038 RID: 56
	public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : AuthClientPacket
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007244 File Offset: 0x00005444
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_GET_POINT_CASH_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_AUTH_GET_POINT_CASH_REQ()
		{
		}
	}
}
