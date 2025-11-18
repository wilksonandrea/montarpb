using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A2 RID: 418
	public class PROTOCOL_CS_MEMBER_CONTEXT_REQ : GameClientPacket
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00021C6C File Offset: 0x0001FE6C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					int num = ((player.ClanId == 0) ? player.FindClanId : player.ClanId);
					int num2;
					int num3;
					if (num == 0)
					{
						num2 = -1;
						num3 = 0;
					}
					else
					{
						num2 = 0;
						num3 = DaoManagerSQL.GetClanPlayers(num);
					}
					this.Client.SendPacket(new PROTOCOL_CS_MEMBER_CONTEXT_ACK(num2, num3));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_MEMBER_CONTEXT_REQ()
		{
		}
	}
}
