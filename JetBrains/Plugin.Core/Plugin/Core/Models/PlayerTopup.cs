// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerTopup
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerTopup
{
  public void AddItem(ItemsModel value)
  {
    lock (((PlayerInventory) this).Items)
      ((PlayerInventory) this).Items.Add(value);
  }

  public void LoadBasicItems()
  {
    lock (((PlayerInventory) this).Items)
      ((PlayerInventory) this).Items.AddRange((IEnumerable<ItemsModel>) TemplatePackXML.Basics);
  }

  public void LoadGeneralBeret()
  {
    lock (((PlayerInventory) this).Items)
      ((PlayerInventory) this).Items.Add((ItemsModel) new PlayerBonus(2700008, "Beret S. General", ItemEquipType.Permanent, 1U));
  }

  public void LoadHatForGM()
  {
    lock (((PlayerInventory) this).Items)
      ((PlayerInventory) this).Items.Add((ItemsModel) new PlayerBonus(700160, "MOD Hat", ItemEquipType.Permanent, 1U));
  }

  public byte[] EquipmentData(int Id)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      ItemsModel itemsModel = ((PlayerInventory) this).GetItem(Id);
      if (itemsModel != null)
      {
        syncServerPacket.WriteD(itemsModel.Id);
        syncServerPacket.WriteD((uint) itemsModel.ObjectId);
      }
      else
      {
        syncServerPacket.WriteD(Id);
        syncServerPacket.WriteD(0);
      }
      return syncServerPacket.ToArray();
    }
  }

  public long ObjectId { get; set; }

  public long PlayerId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerTopup) this).long_1;
    [CompilerGenerated, SpecialName] set => ((PlayerTopup) this).long_1 = value;
  }

  public int GoodsId
  {
    [CompilerGenerated, SpecialName] get => ((PlayerTopup) this).int_0;
    [CompilerGenerated, SpecialName] set => ((PlayerTopup) this).int_0 = value;
  }
}
