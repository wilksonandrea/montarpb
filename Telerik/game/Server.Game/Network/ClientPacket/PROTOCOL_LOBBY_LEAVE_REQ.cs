using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_LEAVE_REQ : GameClientPacket
	{
		private uint uint_0;

		public PROTOCOL_LOBBY_LEAVE_REQ()
		{
		}

		public override void Read()
		{
		}

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
							this.uint_0 = -2147483648;
						}
						this.Client.SendPacket(new PROTOCOL_LOBBY_LEAVE_ACK(this.uint_0));
						if (this.uint_0 != 0)
						{
							this.Client.Close(1000, true, false);
						}
						else
						{
							player.ResetPages();
							player.Status.UpdateChannel(255);
							AllUtils.SyncPlayerToFriends(player, false);
							AllUtils.SyncPlayerToClanMembers(player);
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_LEAVE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}