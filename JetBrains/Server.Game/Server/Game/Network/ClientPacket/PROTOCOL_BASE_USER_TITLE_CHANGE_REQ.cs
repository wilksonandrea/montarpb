// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_USER_TITLE_CHANGE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_CHANGE_REQ : GameClientPacket
{
  private int int_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.FindPlayer = ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).string_0;
      Account account = ClanManager.GetAccount(player.FindPlayer, 1, 31 /*0x1F*/);
      if (account != null && player.Nickname.Length > 0 && player.Nickname != ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).string_0)
      {
        PlayerReport report = player.Report;
        if (report != null && report.TicketCount != 0)
        {
          if (player.Rank < 7)
            ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).uint_0 = 2147487977U;
          else if (DaoManagerSQL.CreatePlayerReportHistory(account.PlayerId, player.PlayerId, account.Nickname, player.Nickname, ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).reportType_0, ((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).string_1) && ComDiv.UpdateDB("player_reports", "ticket_count", (object) --report.TicketCount, "owner_id", (object) player.PlayerId))
            ComDiv.UpdateDB("player_reports", "reported_count", (object) ++account.Report.ReportedCount, "owner_id", (object) account.PlayerId);
        }
        else
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(2147487976U));
          return;
        }
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(((PROTOCOL_COMMUNITY_USER_REPORT_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
