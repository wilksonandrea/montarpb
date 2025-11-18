// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ : GameClientPacket
{
  private long long_0;
  private uint uint_0;
  private uint uint_1;
  private byte[] byte_0;
  private string string_0;

  private int method_0([In] int obj0, [In] int obj1)
  {
    lock (((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).object_0)
      return ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).random_0.Next(obj0, obj1);
  }

  private void method_1(Account string_2, string long_0)
  {
    int itemId = ComDiv.CreateItemId(16 /*0x10*/, 0, ComDiv.GetIdStatics(((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).int_0, 3));
    int idStatics = ComDiv.GetIdStatics(((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).int_0, 2);
    if (AllUtils.CheckDuplicateCouponEffects(string_2, itemId))
    {
      ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    }
    else
    {
      ItemsModel list_1 = string_2.Inventory.GetItem(itemId);
      if (list_1 == null)
      {
        int num = string_2.Bonus.AddBonuses(itemId) ? 1 : 0;
        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemId);
        if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && !string_2.Effects.HasFlag((Enum) couponEffect.EffectFlag))
        {
          string_2.Effects |= couponEffect.EffectFlag;
          DaoManagerSQL.UpdateCouponEffect(string_2.PlayerId, string_2.Effects);
        }
        if (num != 0)
          DaoManagerSQL.UpdatePlayerBonus(string_2.PlayerId, string_2.Bonus.Bonuses, string_2.Bonus.FreePass);
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, string_2, new ItemsModel(itemId, long_0 + " [Active]", ItemEquipType.Temporary, Convert.ToUInt32(DateTimeUtil.Now().AddDays((double) idStatics).ToString("yyMMddHHmm")))));
      }
      else
      {
        DateTime exact = DateTime.ParseExact(list_1.Count.ToString(), "yyMMddHHmm", (IFormatProvider) CultureInfo.InvariantCulture);
        list_1.Count = Convert.ToUInt32(exact.AddDays((double) idStatics).ToString("yyMMddHHmm"));
        ComDiv.UpdateDB("player_items", "count", (object) (long) list_1.Count, "object_id", (object) list_1.ObjectId, "owner_id", (object) string_2.PlayerId);
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(1, string_2, list_1));
      }
    }
  }

  private void method_2([In] Account obj0, [In] int obj1)
  {
    int account_1 = ComDiv.GetIdStatics(obj1, 3) * 100 + ComDiv.GetIdStatics(obj1, 2) * 100000;
    if (DaoManagerSQL.UpdateAccountGold(obj0.PlayerId, obj0.Gold + account_1))
    {
      obj0.Gold += account_1;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK(account_1, obj0.Gold, 0));
    }
    else
      ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
  }

  private void method_3([In] Account obj0, int int_2)
  {
    PlayerBattlepass battlepass = obj0.Battlepass;
    if (battlepass != null)
    {
      int num = ComDiv.GetIdStatics(int_2, 3) * 10 + ComDiv.GetIdStatics(int_2, 2) * 100000;
      battlepass.TotalPoints += num;
      if (ComDiv.UpdateDB("player_battlepass", "total_points", (object) battlepass.TotalPoints, "owner_id", (object) obj0.PlayerId))
      {
        obj0.UpdateSeasonpass = true;
        AllUtils.UpdateSeasonPass(obj0);
      }
      else
        ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
    }
    else
      ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
  }
}
