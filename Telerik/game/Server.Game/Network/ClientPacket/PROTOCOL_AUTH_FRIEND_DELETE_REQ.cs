using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_FRIEND_DELETE_REQ : GameClientPacket
	{
		private int int_0;

		private uint uint_0;

		public PROTOCOL_AUTH_FRIEND_DELETE_REQ()
		{
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
					FriendModel friend = player.Friend.GetFriend(this.int_0);
					if (friend == null)
					{
						this.uint_0 = -2147483648;
					}
					else
					{
						DaoManagerSQL.DeletePlayerFriend(friend.PlayerId, player.PlayerId);
						Account account = AccountManager.GetAccount(friend.PlayerId, 287);
						if (account != null)
						{
							int ınt32 = -1;
							FriendModel friendModel = account.Friend.GetFriend(player.PlayerId, out ınt32);
							if (friendModel != null)
							{
								friendModel.Removed = true;
								DaoManagerSQL.UpdatePlayerFriendBlock(account.PlayerId, friendModel);
								SendFriendInfo.Load(account, friendModel, 2);
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friendModel, ınt32), false);
							}
						}
						player.Friend.RemoveFriend(friend);
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Delete, null, 0, this.int_0));
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_DELETE_ACK(this.uint_0));
					this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_FRIEND_DELETE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}