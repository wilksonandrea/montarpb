// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_RANK_UP_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_RANK_UP_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;

  public virtual void Write()
  {
    this.WriteH((short) 2308);
    this.WriteH((short) 0);
  }

  public PROTOCOL_BASE_RANK_UP_ACK(Account playerStatistic_1, [In] ItemsModel obj1)
  {
    ((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).itemsModel_0 = obj1;
    if (playerStatistic_1 == null)
      return;
    ((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).playerInventory_0 = playerStatistic_1.Inventory;
    ((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).list_0 = new List<ItemsModel>();
    if (obj1 == null)
      return;
    ((PROTOCOL_BASE_NEW_REWARD_POPUP_ACK) this).list_0.Add(obj1);
  }
}
