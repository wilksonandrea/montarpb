using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200014B RID: 331
	public class PROTOCOL_SEASON_CHALLENGE_INFO_REQ : GameClientPacket
	{
		// Token: 0x0600033F RID: 831 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00019534 File Offset: 0x00017734
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + "; " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_SEASON_CHALLENGE_INFO_REQ()
		{
		}
	}
}
