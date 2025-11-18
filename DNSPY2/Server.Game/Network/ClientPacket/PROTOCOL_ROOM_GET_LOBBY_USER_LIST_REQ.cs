using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CE RID: 462
	public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ : GameClientPacket
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x000057B3 File Offset: 0x000039B3
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = base.ReadD();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00025E04 File Offset: 0x00024004
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && this.int_0 > 0 && this.int_0 <= 8)
					{
						ChannelModel channel = player.GetChannel();
						if (channel != null)
						{
							using (PROTOCOL_SERVER_MESSAGE_INVITED_ACK protocol_SERVER_MESSAGE_INVITED_ACK = new PROTOCOL_SERVER_MESSAGE_INVITED_ACK(player, room))
							{
								byte[] completeBytes = protocol_SERVER_MESSAGE_INVITED_ACK.GetCompleteBytes("PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ");
								for (int i = 0; i < this.int_0; i++)
								{
									Account account = AccountManager.GetAccount(channel.GetPlayer(this.int_1).PlayerId, true);
									if (account != null)
									{
										account.SendCompletePacket(completeBytes, protocol_SERVER_MESSAGE_INVITED_ACK.GetType().Name);
									}
								}
								goto IL_B2;
							}
						}
						this.uint_0 = 2147483648U;
					}
					IL_B2:
					this.Client.SendPacket(new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ()
		{
		}

		// Token: 0x04000378 RID: 888
		private int int_0;

		// Token: 0x04000379 RID: 889
		private uint uint_0;

		// Token: 0x0400037A RID: 890
		private int int_1;
	}
}
