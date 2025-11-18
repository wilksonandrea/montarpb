// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_NEW_REWARD_POPUP_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_NEW_REWARD_POPUP_ACK : GameServerPacket
{
  private readonly ItemsModel itemsModel_0;
  private readonly PlayerInventory playerInventory_0;
  private readonly List<ItemsModel> list_0;

  public virtual void Write()
  {
    this.WriteH((short) 2341);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
  }

  public PROTOCOL_BASE_NEW_REWARD_POPUP_ACK([In] int obj0, Account list_1)
  {
    ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).int_0 = obj0;
    ((PROTOCOL_BASE_INV_ITEM_DATA_ACK) this).account_0 = list_1;
  }
}
