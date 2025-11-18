// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_COMMUNITY_USER_REPORT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_REQ : GameClientPacket
{
  private uint uint_0;
  private ReportType reportType_0;
  private string string_0;
  private string string_1;

  public virtual void Run()
  {
    try
    {
      if (this.Client == null)
        return;
      if (this.Client.Player != null)
      {
        ((PROTOCOL_BASE_USER_ENTER_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
      {
        Account accountDb = AccountManager.GetAccountDB((object) ((PROTOCOL_BASE_USER_ENTER_REQ) this).long_0, 2, 31 /*0x1F*/);
        if (accountDb != null && accountDb.Username == ((PROTOCOL_BASE_USER_ENTER_REQ) this).string_0 && accountDb.Status.ServerId != byte.MaxValue)
        {
          this.Client.PlayerId = accountDb.PlayerId;
          accountDb.Connection = this.Client;
          accountDb.ServerId = this.Client.ServerId;
          accountDb.GetAccountInfos(7935);
          AllUtils.ValidateAuthLevel(accountDb);
          AllUtils.LoadPlayerInventory(accountDb);
          AllUtils.LoadPlayerMissions(accountDb);
          AllUtils.EnableQuestMission(accountDb);
          AllUtils.ValidatePlayerInventoryStatus(accountDb);
          accountDb.SetPublicIP(this.Client.GetAddress());
          accountDb.Session = new PlayerSession()
          {
            SessionId = this.Client.SessionId,
            PlayerId = this.Client.PlayerId
          };
          accountDb.Status.UpdateServer((byte) this.Client.ServerId);
          accountDb.UpdateCacheInfo();
          this.Client.Player = accountDb;
          ComDiv.UpdateDB("accounts", "ip4_address", (object) accountDb.PublicIP.ToString(), "player_id", (object) accountDb.PlayerId);
        }
        else
          ((PROTOCOL_BASE_USER_ENTER_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLEBOX_GET_LIST_ACK(((PROTOCOL_BASE_USER_ENTER_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
