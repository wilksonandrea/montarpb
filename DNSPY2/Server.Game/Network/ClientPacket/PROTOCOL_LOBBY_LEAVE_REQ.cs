using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B9 RID: 441
	public class PROTOCOL_LOBBY_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00024040 File Offset: 0x00022240
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (player.Room == null && player.Match == null)
					{
						if (channel == null || player.Session == null || !channel.RemovePlayer(player))
						{
							this.uint_0 = 2147483648U;
						}
						this.Client.SendPacket(new PROTOCOL_LOBBY_LEAVE_ACK(this.uint_0));
						if (this.uint_0 == 0U)
						{
							player.ResetPages();
							player.Status.UpdateChannel(byte.MaxValue);
							AllUtils.SyncPlayerToFriends(player, false);
							AllUtils.SyncPlayerToClanMembers(player);
						}
						else
						{
							this.Client.Close(1000, true, false);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_LOBBY_LEAVE_REQ()
		{
		}

		// Token: 0x0400032D RID: 813
		private uint uint_0;
	}
}
