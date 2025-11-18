// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.FriendInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class FriendInfo
{
  public static void Load([In] SyncClientPacket obj0)
  {
    long num1 = obj0.ReadQ();
    int num2 = (int) obj0.ReadC();
    Account account = ClanManager.GetAccount(num1, true);
    if (account == null || num2 != 3)
      return;
    int num3 = obj0.ReadD();
    int num4 = (int) obj0.ReadC();
    account.ClanId = num3;
    account.ClanAccess = num4;
  }
}
