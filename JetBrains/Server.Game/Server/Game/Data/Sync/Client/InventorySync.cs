// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.InventorySync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class InventorySync
{
  public static void Load(SyncClientPacket C)
  {
    int num1 = (int) C.ReadC();
    int num2 = (int) C.ReadC();
    long acc = C.ReadQ();
    long num3 = C.ReadQ();
    Account account1 = ClanManager.GetAccount(acc, 31 /*0x1F*/);
    if (account1 == null)
      return;
    Account account2 = ClanManager.GetAccount(num3, true);
    if (account2 == null)
      return;
    FriendState friendState = num2 == 1 ? FriendState.Online : FriendState.Offline;
    if (num1 == 0)
    {
      int num4 = -1;
      FriendModel friend = account2.Friend.GetFriend(account1.PlayerId, ref num4);
      if (num4 == -1 || friend == null || friend.State != 0)
        return;
      account2.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_INVITED_ACK(FriendChangeState.Update, friend, friendState, num4));
    }
    else
      account2.SendPacket((GameServerPacket) new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(account1, friendState));
  }
}
