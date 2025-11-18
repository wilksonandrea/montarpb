using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BA RID: 442
	public class PROTOCOL_LOBBY_NEW_MYINFO_REQ : GameClientPacket
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0002411C File Offset: 0x0002231C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					this.Client.SendPacket(new PROTOCOL_LOBBY_NEW_MYINFO_ACK(player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_NEW_MYINFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_LOBBY_NEW_MYINFO_REQ()
		{
		}
	}
}
