using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B7 RID: 439
	public class PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ : GameClientPacket
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x000056C2 File Offset: 0x000038C2
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00023C3C File Offset: 0x00021E3C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null)
					{
						RoomModel room = channel.GetRoom(this.int_0);
						if (room != null && room.GetLeader() != null)
						{
							this.Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(room));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ()
		{
		}

		// Token: 0x0400032C RID: 812
		private int int_0;
	}
}
