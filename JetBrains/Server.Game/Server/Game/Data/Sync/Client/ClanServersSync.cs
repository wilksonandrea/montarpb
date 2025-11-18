// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.ClanServersSync
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class ClanServersSync
{
  public static void Load(SyncClientPacket Player)
  {
    long num1 = Player.ReadQ();
    int num2 = (int) Player.ReadC();
    string str = Player.ReadS((int) Player.ReadC());
    byte[] numArray = Player.ReadB((int) Player.ReadUH());
    Account account = ClanManager.GetAccount(num1, true);
    if (account == null)
      return;
    if (num2 == 0)
      account.SendPacket(numArray, str);
    else
      account.SendCompletePacket(numArray, str);
  }
}
