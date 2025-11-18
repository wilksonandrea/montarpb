using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_FRIEND_INVITED_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_AUTH_FRIEND_INVITED_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(66);
		}

		public override void Run()
		{
			int ınt32;
			int ınt321;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length == 0 || player.Nickname == this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479497));
					}
					else if (player.Friend.Friends.Count < 50)
					{
						Account account = AccountManager.GetAccount(this.string_0, 1, 287);
						if (account == null)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479486));
						}
						else if (player.Friend.GetFriendIdx(account.PlayerId) != -1)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479487));
						}
						else if (account.Friend.Friends.Count < 50)
						{
							int ınt322 = AllUtils.AddFriend(account, player, 2);
							if (AllUtils.AddFriend(player, account, ınt322 != 1) != -1)
							{
								if (ınt322 == -1)
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479495));
									return;
								}
								FriendModel friend = account.Friend.GetFriend(player.PlayerId, out ınt32);
								if (friend != null)
								{
									account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((ınt322 == 0 ? FriendChangeState.Insert : FriendChangeState.Update), friend, ınt32), false);
								}
								FriendModel friendModel = player.Friend.GetFriend(account.PlayerId, out ınt321);
								if (friendModel != null)
								{
									this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, friendModel, ınt321));
									goto Label1;
								}
								else
								{
									goto Label1;
								}
							}
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479495));
							return;
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479496));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(-2147479496));
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_FRIEND_INVITED_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}