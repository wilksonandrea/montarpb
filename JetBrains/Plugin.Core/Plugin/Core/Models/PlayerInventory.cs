// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerInventory
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerInventory
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Status(AccountStatus value) => ((PlayerInfo) this).accountStatus_0 = value;

  public PlayerInventory(long value)
  {
    ((PlayerInfo) this).PlayerId = value;
    this.set_Status(new AccountStatus());
  }

  public PlayerInventory(
    long value,
    [In] int obj1,
    [In] int obj2,
    [In] string obj3,
    [In] bool obj4,
    [In] AccountStatus obj5)
  {
    ((PlayerInfo) this).PlayerId = value;
    this.SetInfo(obj1, obj2, obj3, obj4, obj5);
  }

  public void SetOnlineStatus([In] bool obj0)
  {
    if (((PlayerInfo) this).IsOnline == obj0 || !ComDiv.UpdateDB("accounts", "online", (object) obj0, "player_id", (object) ((PlayerInfo) this).PlayerId))
      return;
    ((PlayerInfo) this).IsOnline = obj0;
  }

  public void SetInfo([In] int obj0, [In] int obj1, string int_3, bool string_1, AccountStatus bool_1)
  {
    ((PlayerInfo) this).Rank = obj0;
    ((PlayerInfo) this).NickColor = obj1;
    ((PlayerInfo) this).Nickname = int_3;
    ((PlayerInfo) this).IsOnline = string_1;
    this.set_Status(bool_1);
  }

  public List<ItemsModel> Items { get; set; }

  public PlayerInventory() => this.Items = new List<ItemsModel>();

  public ItemsModel GetItem([In] int obj0)
  {
    lock (this.Items)
    {
      foreach (ItemsModel itemsModel in this.Items)
      {
        if (itemsModel.Id == obj0)
          return itemsModel;
      }
    }
    return (ItemsModel) null;
  }

  public ItemsModel GetItem([In] long obj0)
  {
    lock (this.Items)
    {
      foreach (ItemsModel itemsModel in this.Items)
      {
        if (itemsModel.ObjectId == obj0)
          return itemsModel;
      }
    }
    return (ItemsModel) null;
  }

  public List<ItemsModel> GetItemsByType([In] ItemCategory obj0)
  {
    List<ItemsModel> itemsByType = new List<ItemsModel>();
    lock (this.Items)
    {
      foreach (ItemsModel itemsModel in this.Items)
      {
        if (itemsModel.Category == obj0 || itemsModel.Id > 1600000 && itemsModel.Id < 1700000 && obj0 == ItemCategory.NewItem)
          itemsByType.Add(itemsModel);
      }
    }
    return itemsByType;
  }

  public bool RemoveItem([In] ItemsModel obj0)
  {
    lock (this.Items)
      return this.Items.Remove(obj0);
  }
}
