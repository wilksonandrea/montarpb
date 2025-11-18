using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000128 RID: 296
	public class PROTOCOL_AUTH_GET_POINT_CASH_REQ : GameClientPacket
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000151D4 File Offset: 0x000133D4
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
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_GET_POINT_CASH_REQ()
		{
		}
	}
}
