using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B1 RID: 433
	public class PROTOCOL_GM_LOG_LOBBY_REQ : GameClientPacket
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x00005676 File Offset: 0x00003876
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00022C4C File Offset: 0x00020E4C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.IsGM())
				{
					long playerId = player.GetChannel().GetPlayer(this.int_0).PlayerId;
					if (playerId > 0L)
					{
						this.Client.SendPacket(new PROTOCOL_GM_LOG_LOBBY_ACK(0U, playerId));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_GM_LOG_LOBBY_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GM_LOG_LOBBY_REQ()
		{
		}

		// Token: 0x04000325 RID: 805
		private int int_0;
	}
}
