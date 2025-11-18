using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000144 RID: 324
	public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ : GameClientPacket
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00004FBC File Offset: 0x000031BC
		public override void Read()
		{
			this.string_0 = base.ReadU(33);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000190F8 File Offset: 0x000172F8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Nickname.Length != 0 && !(player.Nickname == this.string_0))
				{
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ()
		{
		}

		// Token: 0x04000246 RID: 582
		private string string_0;
	}
}
