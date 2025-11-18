// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomBombC4
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public static class RoomBombC4
{
  public static void Load(SyncClientPacket C)
  {
    long num1 = C.ReadQ();
    long num2 = C.ReadQ();
    int num3 = C.ReadD();
    ItemEquipType itemEquipType = (ItemEquipType) C.ReadC();
    ItemCategory itemCategory = (ItemCategory) C.ReadC();
    uint num4 = C.ReadUD();
    byte mission = C.ReadC();
    string str = C.ReadS((int) mission);
    Account account = ClanManager.GetAccount(num1, true);
    if (account == null)
      return;
    ItemsModel itemsModel1 = account.Inventory.GetItem(num2);
    if (itemsModel1 == null)
    {
      ItemsModel itemsModel2 = new ItemsModel()
      {
        ObjectId = num2,
        Id = num3,
        Equip = itemEquipType,
        Count = num4,
        Category = itemCategory,
        Name = str
      };
      account.Inventory.AddItem(itemsModel2);
    }
    else
      itemsModel1.Count = num4;
  }

  public static void Load(SyncClientPacket C)
  {
    long num1 = C.ReadQ();
    int num2 = (int) C.ReadC();
    int num3 = (int) C.ReadC();
    int num4 = C.ReadD();
    int num5 = C.ReadD();
    int num6 = C.ReadD();
    Account account = ClanManager.GetAccount(num1, true);
    if (account == null || num2 != 0)
      return;
    account.Rank = num3;
    account.Gold = num4;
    account.Cash = num5;
    account.Tags = num6;
  }
}
