// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_FRIEND_INVITED_REQ
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
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      FriendModel friend1 = player.Friend.GetFriend(((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).int_0);
      if (friend1 != null && friend1.State > 0)
      {
        Account account = ClanManager.GetAccount(friend1.get_PlayerId(), 287);
        if (account != null)
        {
          if (friend1.Info == null)
            friend1.SetModel(account.PlayerId, account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
          else
            friend1.Info.SetInfo(account.Rank, account.NickColor, account.Nickname, account.IsOnline, account.Status);
          friend1.State = 0;
          DaoManagerSQL.UpdatePlayerFriendState(player.PlayerId, friend1);
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_ACK(FriendChangeState.Accept, (FriendModel) null, 0, ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).int_0));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Update, friend1, ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).int_0));
          int num = -1;
          FriendModel friend2 = account.Friend.GetFriend(player.PlayerId, ref num);
          if (friend2 != null && friend2.State > 0)
          {
            if (friend2.Info == null)
              friend2.SetModel(player.PlayerId, player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
            else
              friend2.Info.SetInfo(player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
            friend2.State = 0;
            DaoManagerSQL.UpdatePlayerFriendState(account.PlayerId, friend2);
            UpdateChannel.Load(account, friend2, 1);
            account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INSERT_ACK(FriendChangeState.Update, friend2, num), false);
          }
        }
        else
          ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
        ((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      if (((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).uint_0 <= 0U)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INFO_ACK(((PROTOCOL_AUTH_FRIEND_ACCEPT_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_FRIEND_ACCEPT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_AUTH_FRIEND_DELETE_REQ) this).int_0 = (int) this.ReadC();
}
