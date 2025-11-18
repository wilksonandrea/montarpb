// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Client.ReloadConfig
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

#nullable disable
namespace Server.Auth.Data.Sync.Client;

public class ReloadConfig
{
  public static void Load(SyncClientPacket C)
  {
    long system = C.ReadQ();
    int num1 = (int) C.ReadC();
    long num2 = C.ReadQ();
    FriendModel friendModel = (FriendModel) null;
    if (num1 <= 1)
    {
      int num3 = (int) C.ReadC();
      bool flag = C.ReadC() == (byte) 1;
      friendModel = new FriendModel(num2)
      {
        State = num3,
        Removed = flag
      };
    }
    if (friendModel == null && num1 <= 1)
      return;
    Account account = ClanManager.GetAccount(system, true);
    if (account == null)
      return;
    if (num1 <= 1)
    {
      friendModel.Info.Nickname = account.Nickname;
      friendModel.Info.Rank = account.Rank;
      friendModel.Info.IsOnline = account.IsOnline;
      friendModel.Info.Status = account.Status;
    }
    if (num1 == 0)
      account.Friend.AddFriend(friendModel);
    else if (num1 == 1)
    {
      account.Friend.GetFriend(num2);
    }
    else
    {
      if (num1 != 2)
        return;
      account.Friend.RemoveFriend(num2);
    }
  }
}
