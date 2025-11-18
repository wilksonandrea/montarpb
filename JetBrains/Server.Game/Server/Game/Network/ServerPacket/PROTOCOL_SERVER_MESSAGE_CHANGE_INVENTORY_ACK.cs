// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK : GameServerPacket
{
  private readonly PlayerInventory playerInventory_0;
  private readonly PlayerCharacters playerCharacters_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly PlayerEquipment playerEquipment_1;
  private readonly List<ItemsModel> list_0;
  private readonly List<int> list_1;
  private readonly List<int> list_2;
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 8451);
    this.WriteH((short) 0);
    this.WriteC((byte) 1);
  }

  public PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK([In] uint obj0, [In] int[] obj1)
  {
    ((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).uint_0 = obj0;
    ((PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK) this).int_0 = obj1;
  }
}
