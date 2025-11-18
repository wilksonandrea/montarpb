// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_LOBBY_CHATTING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_CHATTING_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly string string_1;
  private readonly int int_0;
  private readonly int int_1;
  private readonly bool bool_0;

  public PROTOCOL_LOBBY_CHATTING_ACK(int uint_1, Account account_1, [In] List<ItemsModel> obj2)
  {
    ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).int_0 = uint_1;
    if (account_1 != null)
    {
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).playerInventory_0 = account_1.Inventory;
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0 = new List<ItemsModel>();
    }
    foreach (ItemsModel itemsModel in obj2)
    {
      ItemsModel Type = new ItemsModel(itemsModel);
      if (Type != null)
      {
        if (uint_1 == 0)
          ComDiv.TryCreateItem(Type, account_1.Inventory, account_1.PlayerId);
        UpdateServer.LoadItem(account_1, Type);
        ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0.Add(Type);
      }
    }
  }

  public PROTOCOL_LOBBY_CHATTING_ACK([In] int obj0, Account account_0, ItemsModel list_1)
  {
    ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).int_0 = obj0;
    if (account_0 != null)
    {
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).playerInventory_0 = account_0.Inventory;
      ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0 = new List<ItemsModel>();
    }
    ItemsModel Type = new ItemsModel(list_1);
    if (Type == null)
      return;
    if (obj0 == 0)
      ComDiv.TryCreateItem(Type, account_0.Inventory, account_0.PlayerId);
    UpdateServer.LoadItem(account_0, Type);
    ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0.Add(Type);
  }

  public virtual void Write()
  {
    this.WriteH((short) 3334);
    this.WriteH((short) 0);
    this.WriteH((ushort) ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).playerInventory_0.Items.Count);
    this.WriteH((ushort) ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0.Count);
    foreach (ItemsModel itemsModel in ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).list_0)
    {
      this.WriteD((uint) itemsModel.ObjectId);
      this.WriteD(itemsModel.Id);
      this.WriteC((byte) itemsModel.Equip);
      this.WriteD(itemsModel.Count);
    }
    this.WriteC((byte) ((PROTOCOL_INVENTORY_GET_INFO_ACK) this).int_0);
  }
}
