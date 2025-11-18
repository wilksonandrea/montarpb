// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FIND_USER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FIND_USER_REQ : GameClientPacket
{
  private string string_0;
  private uint uint_0;

  public virtual void Write()
  {
    ((BaseServerPacket) this).WriteD(3853);
    ((BaseServerPacket) this).WriteD(((PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK) this).int_0);
  }

  public virtual void Read() => ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).string_0 = this.ReadU(66);

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Nickname.Length != 0 && !(player.Nickname == ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).string_0))
      {
        if (player.Friend.Friends.Count >= 50)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487800U));
        }
        else
        {
          Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).string_0, 1, 287);
          if (account != null)
          {
            if (player.Friend.GetFriendIdx(account.PlayerId) == -1)
            {
              if (account.Friend.Friends.Count >= 50)
              {
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487800U));
              }
              else
              {
                int num = AllUtils.AddFriend(account, player, 2);
                if (AllUtils.AddFriend(player, account, num != 1 ? 1 : 0) != -1 && num != -1)
                {
                  FriendModel friend1 = account.Friend.GetFriend(player.PlayerId, ref ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).int_1);
                  if (friend1 != null)
                  {
                    MessageModel bool_1 = ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
                    if (bool_1 != null)
                      account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), true);
                    account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(num == 0 ? FriendChangeState.Insert : FriendChangeState.Update, friend1, ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).int_1), false);
                  }
                  FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, ref ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).int_0);
                  if (friend2 == null)
                    return;
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Insert, friend2, ((PROTOCOL_AUTH_FRIEND_INSERT_REQ) this).int_0));
                }
                else
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487801U));
              }
            }
            else
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487809U));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487810U));
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(2147487799U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
