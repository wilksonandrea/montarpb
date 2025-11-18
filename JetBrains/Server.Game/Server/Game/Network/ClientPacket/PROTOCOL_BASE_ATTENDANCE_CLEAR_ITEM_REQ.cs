// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ : GameClientPacket
{
  private EventErrorEnum eventErrorEnum_0;
  private int int_0;
  private int int_1;
  private int int_2;

  private bool method_0(Account account_0, TicketModel int_1, [In] int obj2)
  {
    if (!DaoManagerSQL.IsTicketUsedByPlayer(account_0.PlayerId, int_1.Token))
      return DaoManagerSQL.CreatePlayerRedeemHistory(account_0.PlayerId, int_1.Token, obj2);
    return ComDiv.UpdateDB("base_redeem_history", "owner_id", (object) account_0.PlayerId, "used_token", (object) int_1.Token, new string[1]
    {
      "used_count"
    }, new object[1]{ (object) obj2 });
  }

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ) this).string_0 = this.ReadU(66);
  }

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(!DaoManagerSQL.IsPlayerNameExist(((PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ) this).string_0) ? 0U : 2147483923U));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
