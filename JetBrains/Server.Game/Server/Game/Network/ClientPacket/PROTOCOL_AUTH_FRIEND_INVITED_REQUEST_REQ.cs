// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      FriendModel friend1 = player.Friend.GetFriend(((PROTOCOL_AUTH_FRIEND_DELETE_REQ) this).int_0);
      if (friend1 != null)
      {
        DaoManagerSQL.DeletePlayerFriend(friend1.get_PlayerId(), player.PlayerId);
        Account account = ClanManager.GetAccount(friend1.get_PlayerId(), 287);
        if (account != null)
        {
          int num = -1;
          FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, ref num);
          if (friend2 != null)
          {
            friend2.Removed = true;
            DaoManagerSQL.UpdatePlayerFriendBlock(account.PlayerId, friend2);
            UpdateChannel.Load(account, friend2, 2);
            account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Update, friend2, num), false);
          }
        }
        player.Friend.RemoveFriend(friend1);
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_ACK(FriendChangeState.Delete, (FriendModel) null, 0, ((PROTOCOL_AUTH_FRIEND_DELETE_REQ) this).int_0));
      }
      else
        ((PROTOCOL_AUTH_FRIEND_DELETE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(((PROTOCOL_AUTH_FRIEND_DELETE_REQ) this).uint_0));
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(player.Friend.Friends));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_FRIEND_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_AUTH_FRIEND_INVITED_REQ) this).string_0 = this.ReadU(66);

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Nickname.Length != 0 && !(player.Nickname == ((PROTOCOL_AUTH_FRIEND_INVITED_REQ) this).string_0))
      {
        if (player.Friend.Friends.Count >= 50)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487800U));
        }
        else
        {
          Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_FRIEND_INVITED_REQ) this).string_0, 1, 287);
          if (account != null)
          {
            if (player.Friend.GetFriendIdx(account.PlayerId) == -1)
            {
              if (account.Friend.Friends.Count >= 50)
              {
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487800U));
              }
              else
              {
                int num1 = AllUtils.AddFriend(account, player, 2);
                if (AllUtils.AddFriend(player, account, num1 != 1 ? 1 : 0) != -1 && num1 != -1)
                {
                  int num2;
                  FriendModel friend1 = account.Friend.GetFriend(player.PlayerId, ref num2);
                  if (friend1 != null)
                    account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(num1 == 0 ? FriendChangeState.Insert : FriendChangeState.Update, friend1, num2), false);
                  int num3;
                  FriendModel friend2 = player.Friend.GetFriend(account.PlayerId, ref num3);
                  if (friend2 == null)
                    return;
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Insert, friend2, num3));
                }
                else
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487801U));
              }
            }
            else
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487809U));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487810U));
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_GET_POINT_CASH_ACK(2147487799U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
