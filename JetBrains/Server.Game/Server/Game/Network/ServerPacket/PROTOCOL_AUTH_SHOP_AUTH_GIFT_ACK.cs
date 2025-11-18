// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly List<ItemsModel> list_0;
  private readonly List<ItemsModel> list_1;
  private readonly List<ItemsModel> list_2;
  private readonly List<ItemsModel> list_3;

  public virtual void Write()
  {
    this.WriteH((short) 1046);
    this.WriteH((short) 0);
    if (((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).uint_0 == 1U)
    {
      this.WriteC((byte) ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).list_0.Count);
      foreach (GoodsItem goodsItem in ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).list_0)
      {
        this.WriteD(0);
        this.WriteD(goodsItem.Id);
        this.WriteC((byte) 0);
      }
      this.WriteD(((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).account_0.Cash);
      this.WriteD(((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).account_0.Gold);
      this.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
    }
    else
      this.WriteD(((PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK) this).uint_0);
  }

  public PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK([In] string obj0, string account_1, [In] bool obj2)
  {
    ((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_0 = obj0;
    ((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).string_1 = account_1;
    ((PROTOCOL_AUTH_RECV_WHISPER_ACK) this).bool_0 = obj2;
  }
}
