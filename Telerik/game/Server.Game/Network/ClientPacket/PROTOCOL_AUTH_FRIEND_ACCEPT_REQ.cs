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
	public class PROTOCOL_AUTH_FRIEND_ACCEPT_REQ : GameClientPacket
	{
		private int int_0;

		private uint uint_0;

		public PROTOCOL_AUTH_FRIEND_ACCEPT_REQ()
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
					if (friend == null || friend.State <= 0)
					{
						this.uint_0 = -2147483648;
					}
					else
					{
						Account account = AccountManager.GetAccount(friend.PlayerId, 287);
						if (account == null)
						{
							this.uint_0 = -2147483648;
						}
						else
						{
							if (friend.Info != null)
							{
								friend.Info.SetInfo(account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
							}
							else
							{
								friend.SetModel(account.PlayerId, account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
							}
							friend.State = 0;
							DaoManagerSQL.UpdatePlayerFriendState(player.PlayerId, friend);
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Accept, null, 0, this.int_0));
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, this.int_0));
							int ınt32 = -1;
							FriendModel friendModel = account.Friend.GetFriend(player.PlayerId, out ınt32);
							if (friendModel != null && friendModel.State > 0)
							{
								if (friendModel.Info != null)
								{
									friendModel.Info.SetInfo(player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
								}
								else
								{
									friendModel.SetModel(player.PlayerId, player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
								}
								friendModel.State = 0;
								DaoManagerSQL.UpdatePlayerFriendState(account.PlayerId, friendModel);
								SendFriendInfo.Load(account, friendModel, 1);
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friendModel, ınt32), false);
							}
						}
					}
					if (this.uint_0 > 0)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(this.uint_0));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_FRIEND_ACCEPT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}