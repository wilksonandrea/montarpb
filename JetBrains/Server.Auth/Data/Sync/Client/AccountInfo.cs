// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.AccountInfo
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class AccountInfo
{
  public static void RefreshAccount([In] Account obj0, bool Member)
  {
    try
    {
      ChannelCache.RefreshSChannel(0);
      AccountManager.GetFriendlyAccounts(obj0.Friend);
      foreach (FriendModel friend in obj0.Friend.Friends)
      {
        PlayerInfo info = friend.Info;
        if (info != null)
        {
          SChannelModel server = SChannelXML.GetServer((int) info.Status.ServerId);
          if (server != null)
            ChannelCache.SendRefreshPacket(0, obj0.PlayerId, friend.get_PlayerId(), Member, server);
        }
      }
      if (obj0.ClanId <= 0)
        return;
      foreach (Account clanPlayer in obj0.ClanPlayers)
      {
        if (clanPlayer != null && clanPlayer.IsOnline)
        {
          SChannelModel server = SChannelXML.GetServer((int) clanPlayer.Status.ServerId);
          if (server != null)
            ChannelCache.SendRefreshPacket(1, obj0.PlayerId, clanPlayer.PlayerId, Member, server);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
