// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_SHOP_REPAIR_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_REPAIR_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly List<ItemsModel> list_0;
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 1096);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteD(1);
    this.WriteC((byte) 1);
    this.WriteD(63266001);
    this.WriteC((byte) 1);
    this.WriteD(1512052359);
    this.WriteC((byte) 1);
  }
}
