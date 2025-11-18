// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.EventInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public class EventInfo
{
  public static void Load(SyncClientPacket ServerId)
  {
    Account account = ClanManager.GetAccount(ServerId.ReadQ(), true);
    if (account == null)
      return;
    account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(1));
    account.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK(2147487744U /*0x80001000*/));
    account.Close(1000, false);
  }

  public static void Load([In] SyncClientPacket obj0)
  {
    int num1 = (int) obj0.ReadC();
    int num2 = obj0.ReadD();
    ClanModel clan = ClanManager.GetClan(num2);
    if (num1 == 0)
    {
      if (clan != null)
        return;
      long num3 = obj0.ReadQ();
      uint num4 = obj0.ReadUD();
      string str1 = obj0.ReadS((int) obj0.ReadC());
      string str2 = obj0.ReadS((int) obj0.ReadC());
      CommandManager.AddClan(new ClanModel()
      {
        Id = num2,
        Name = str1,
        OwnerId = num3,
        Logo = 0U,
        Info = str2,
        CreationDate = num4
      });
    }
    else
    {
      if (clan == null)
        return;
      CommandManager.RemoveClan(clan);
    }
  }
}
