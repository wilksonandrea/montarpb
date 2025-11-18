// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.PlayerSync
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public static class PlayerSync
{
  public static void Load(SyncClientPacket C)
  {
    int num1 = (int) C.ReadC();
    int num2 = (int) C.ReadC();
    long system1 = C.ReadQ();
    long system2 = C.ReadQ();
    Account account1 = ClanManager.GetAccount(system1, true);
    if (account1 == null)
      return;
    Account account2 = ClanManager.GetAccount(system2, true);
    if (account2 == null)
      return;
    FriendState friendState = num2 == 1 ? FriendState.Online : FriendState.Offline;
    if (num1 == 0)
    {
      int num3 = -1;
      FriendModel friend = account2.Friend.GetFriend(account1.PlayerId, ref num3);
      if (num3 == -1 || friend == null)
        return;
      account2.SendPacket((AuthServerPacket) new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(FriendChangeState.Update, friend, friendState, num3));
    }
    else
      account2.SendPacket((AuthServerPacket) new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(account1, friendState));
  }
}
