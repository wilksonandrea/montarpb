// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ : GameClientPacket
{
  private CharacterModel method_0([In] Account obj0, [In] List<GoodsItem> obj1)
  {
    foreach (GoodsItem goodsItem in obj1)
    {
      if (goodsItem != null && goodsItem.Item.Id != 0)
        return new CharacterModel()
        {
          Id = goodsItem.Item.Id,
          Slot = obj0.Character.GenSlotId(goodsItem.Item.Id),
          Name = ((PROTOCOL_CHAR_CREATE_CHARA_REQ) this).string_0,
          CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
          PlayTime = 0
        };
    }
    return (CharacterModel) null;
  }

  public PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_REQ()
  {
    ((PROTOCOL_CHAR_CREATE_CHARA_REQ) this).list_0 = new List<CartGoods>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read() => ((PROTOCOL_CHAR_DELETE_CHARA_REQ) this).int_0 = (int) this.ReadC();
}
