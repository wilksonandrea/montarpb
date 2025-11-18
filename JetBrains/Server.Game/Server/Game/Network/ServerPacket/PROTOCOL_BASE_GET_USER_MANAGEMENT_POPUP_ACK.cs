// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3097);
    this.WriteD(((PROTOCOL_SHOP_PLUS_TAG_ACK) this).int_0);
    this.WriteD(((PROTOCOL_SHOP_PLUS_TAG_ACK) this).int_1);
  }

  public PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK(
    uint int_3,
    List<ItemsModel> int_4,
    Account int_5)
  {
    ((PROTOCOL_SHOP_REPAIR_ACK) this).uint_0 = int_3;
    ((PROTOCOL_SHOP_REPAIR_ACK) this).account_0 = int_5;
    ((PROTOCOL_SHOP_REPAIR_ACK) this).list_0 = int_4;
  }
}
