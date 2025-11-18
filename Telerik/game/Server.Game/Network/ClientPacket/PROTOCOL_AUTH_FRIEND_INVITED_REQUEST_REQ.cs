using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ()
		{
		}

		private Account method_0(Account account_0)
		{
			FriendModel friend = account_0.Friend.GetFriend(this.int_0);
			if (friend == null)
			{
				return null;
			}
			return AccountManager.GetAccount(friend.PlayerId, 287);
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = this.method_0(player);
					if (account == null)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479491));
					}
					else if (account.Status.ServerId == 255 || account.Status.ServerId == 0)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147471358));
						return;
					}
					else if (account.MatchSlot < 0)
					{
						int friendIdx = account.Friend.GetFriendIdx(player.PlayerId);
						if (friendIdx == -1)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479490));
						}
						else if (!account.IsOnline)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479489));
						}
						else
						{
							account.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(friendIdx), false);
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147471357));
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}