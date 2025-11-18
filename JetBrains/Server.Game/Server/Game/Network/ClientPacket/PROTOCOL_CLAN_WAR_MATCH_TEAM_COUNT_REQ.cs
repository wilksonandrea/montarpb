// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ
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

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      MatchModel match = player.Match;
      if (match == null || !((RoomModel) match).RemovePlayer(player))
        ((PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(((PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ) this).uint_0));
      if (((PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ) this).uint_0 != 0U)
        return;
      player.Status.UpdateClanMatch(byte.MaxValue);
      AllUtils.SyncPlayerToClanMembers(player);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_0 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_MATCH_PROPOSE_REQ) this).int_1 = (int) this.ReadH();
  }
}
