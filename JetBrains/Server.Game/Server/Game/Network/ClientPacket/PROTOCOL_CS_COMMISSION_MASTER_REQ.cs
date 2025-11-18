// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_COMMISSION_MASTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_COMMISSION_MASTER_REQ : GameClientPacket
{
  private long long_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.Room?.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_COMMISSION_MASTER_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CLIENT_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId && ComDiv.DeleteDB("system_clan", "id", (object) clan.Id))
      {
        if (ComDiv.UpdateDB("accounts", "player_id", (object) player.PlayerId, new string[2]
        {
          "clan_id",
          "clan_access"
        }, new object[2]{ (object) 0, (object) 0 }))
        {
          if (ComDiv.UpdateDB("player_stat_clans", "owner_id", (object) player.PlayerId, new string[2]
          {
            "clan_matches",
            "clan_match_wins"
          }, new object[2]{ (object) 0, (object) 0 }) && CommandManager.RemoveClan(clan))
          {
            player.ClanId = 0;
            player.ClanAccess = 0;
            SendItemInfo.Load(clan, 1);
            goto label_6;
          }
        }
      }
      ((PROTOCOL_CS_CLOSE_CLAN_REQ) this).uint_0 = 2147487850U;
label_6:
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK(((PROTOCOL_CS_CLOSE_CLAN_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CLOSE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
