// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.ReloadConfig
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class ReloadConfig
{
  public static void Load(SyncClientPacket C)
  {
    long num1 = C.ReadQ();
    int num2 = (int) C.ReadC();
    long num3 = C.ReadQ();
    FriendModel friendModel = (FriendModel) null;
    if (num2 <= 1)
    {
      int num4 = (int) C.ReadC();
      bool flag = C.ReadC() == (byte) 1;
      friendModel = new FriendModel(num3)
      {
        State = num4,
        Removed = flag
      };
    }
    if (friendModel == null && num2 <= 1)
      return;
    Account account = ClanManager.GetAccount(num1, true);
    if (account == null)
      return;
    if (num2 <= 1)
    {
      friendModel.Info.Nickname = account.Nickname;
      friendModel.Info.Rank = account.Rank;
      friendModel.Info.IsOnline = account.IsOnline;
      friendModel.Info.Status = account.Status;
    }
    if (num2 == 0)
      account.Friend.AddFriend(friendModel);
    else if (num2 == 1)
    {
      account.Friend.GetFriend(num3);
    }
    else
    {
      if (num2 != 2)
        return;
      account.Friend.RemoveFriend(num3);
    }
  }
}
