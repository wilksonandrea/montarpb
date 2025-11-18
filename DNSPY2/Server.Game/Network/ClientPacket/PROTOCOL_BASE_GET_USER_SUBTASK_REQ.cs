using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000154 RID: 340
	public class PROTOCOL_BASE_GET_USER_SUBTASK_REQ : GameClientPacket
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00005073 File Offset: 0x00003273
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001A2B8 File Offset: 0x000184B8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerSession player2 = player.GetChannel().GetPlayer(this.int_0);
					if (player2 != null)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_SUBTASK_ACK(player2));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_USER_SUBTASK_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_USER_SUBTASK_REQ()
		{
		}

		// Token: 0x04000274 RID: 628
		private int int_0;
	}
}
