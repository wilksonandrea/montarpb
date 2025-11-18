// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly List<GoodsItem> list_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 1812);
    this.WriteD(((PROTOCOL_AUTH_FRIEND_INVITED_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK([In] int obj0)
  {
    ((PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK) this).int_0 = obj0;
  }

  public virtual void Write()
  {
    this.WriteH((short) 1813);
    this.WriteC((byte) ((PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK) this).int_0);
  }
}
