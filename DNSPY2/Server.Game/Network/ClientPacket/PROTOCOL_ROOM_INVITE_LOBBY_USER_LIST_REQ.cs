using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C9 RID: 457
	public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : GameClientPacket
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00025ABC File Offset: 0x00023CBC
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
						this.Client.SendPacket(new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(channel));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ()
		{
		}
	}
}
