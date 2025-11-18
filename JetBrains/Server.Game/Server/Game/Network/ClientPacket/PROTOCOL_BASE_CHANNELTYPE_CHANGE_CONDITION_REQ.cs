// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : GameClientPacket
{
  private bool method_0([In] Account obj0, List<VisitItemModel> account_0)
  {
    try
    {
      int num = 0;
      foreach (VisitItemModel visitItemModel in account_0)
      {
        GoodsItem good = ShopManager.GetGood(visitItemModel.GoodId);
        if (good != null)
        {
          if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && obj0.Character.GetCharacter(good.Item.Id) == null)
            AllUtils.CreateCharacter(obj0, good.Item);
          else
            obj0.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, obj0, good.Item));
          ++num;
        }
      }
      return num > 0;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ()
  {
    ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).eventErrorEnum_0 = EventErrorEnum.VISIT_EVENT_SUCCESS;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_ATTENDANCE_REQ) this).int_0 = this.ReadD();
    ((PROTOCOL_BASE_ATTENDANCE_REQ) this).int_1 = (int) this.ReadC();
  }
}
