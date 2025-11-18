// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Match != null && player.MatchSlot == player.Match.Leader && player.Match.State == MatchState.Ready)
      {
        MatchModel match = AllUtils.GetChannel(((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_1, ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_1 - ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_1 / 10 * 10).GetMatch(((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_0);
        if (match != null)
        {
          Account leader = ((RoomModel) match).GetLeader();
          if (leader != null && leader.Connection != null && leader.IsOnline)
            leader.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(player.Match, player));
          else
            ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
        }
        else
          ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
        ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
