// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerBonus
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerBonus
{
  public PlayerBonus()
  {
  }

  public PlayerBonus(int value) => this.SetItemId(value);

  public PlayerBonus(int value, [In] string obj1, [In] ItemEquipType obj2, [In] uint obj3)
  {
    this.SetItemId(value);
    ((ItemsModel) this).Name = obj1;
    ((ItemsModel) this).Equip = obj2;
    ((ItemsModel) this).Count = obj3;
  }

  public PlayerBonus(ItemsModel int_1)
  {
    ((ItemsModel) this).Id = int_1.Id;
    ((ItemsModel) this).Name = int_1.Name;
    ((ItemsModel) this).Count = int_1.Count;
    ((ItemsModel) this).Equip = int_1.Equip;
    ((ItemsModel) this).Category = int_1.Category;
    ((ItemsModel) this).ObjectId = int_1.ObjectId;
  }

  public void SetItemId(int int_1)
  {
    ((ItemsModel) this).Id = int_1;
    ((ItemsModel) this).Category = ComDiv.GetItemCategory(int_1);
  }

  public long OwnerId { get; [param: In] set; }

  public int Bonuses { get; [param: In] set; }

  public int CrosshairColor { get; [param: In] set; }

  public int MuzzleColor { get; set; }

  public int FreePass { get; set; }

  public int FakeRank { get; set; }

  public int NickBorderColor { get; set; }

  public string FakeNick { get; set; }

  public PlayerBonus()
  {
    this.CrosshairColor = 4;
    this.FakeRank = 55;
    this.FakeNick = "";
  }

  public bool RemoveBonuses(int value)
  {
    int bonuses = this.Bonuses;
    int freePass = this.FreePass;
    switch (value)
    {
      case 1600001:
        ((PlayerConfig) this).method_0(1);
        break;
      case 1600002:
        ((PlayerConfig) this).method_0(2);
        break;
      case 1600003:
        ((PlayerConfig) this).method_0(4);
        break;
      case 1600004:
        ((PlayerConfig) this).method_0(16 /*0x10*/);
        break;
      case 1600011:
        ((PlayerConfig) this).method_2(128 /*0x80*/);
        break;
      case 1600037:
        ((PlayerConfig) this).method_0(8);
        break;
      case 1600038:
        ((PlayerConfig) this).method_0(64 /*0x40*/);
        break;
      case 1600119:
        ((PlayerConfig) this).method_0(32 /*0x20*/);
        break;
      case 1600201:
        ((PlayerConfig) this).method_0(512 /*0x0200*/);
        break;
      case 1600202:
        ((PlayerConfig) this).method_0(1024 /*0x0400*/);
        break;
      case 1600203:
        ((PlayerConfig) this).method_0(2048 /*0x0800*/);
        break;
      case 1600204:
        ((PlayerConfig) this).method_0(4096 /*0x1000*/);
        break;
    }
    return this.Bonuses != bonuses || this.FreePass != freePass;
  }
}
