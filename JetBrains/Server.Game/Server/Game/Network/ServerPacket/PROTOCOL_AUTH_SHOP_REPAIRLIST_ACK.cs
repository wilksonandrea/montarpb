// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly ShopData shopData_0;

  public virtual void Write()
  {
    this.WriteH((short) 1068);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).int_1);
    this.WriteD(((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).string_0.Length);
    this.WriteN(((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).string_0, ((PROTOCOL_AUTH_SHOP_JACKPOT_ACK) this).string_0.Length, "UTF-16LE");
  }

  public PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK([In] ShopData obj0, [In] int obj1)
  {
    ((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).shopData_0 = obj0;
    ((PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK) this).int_0 = obj1;
  }
}
