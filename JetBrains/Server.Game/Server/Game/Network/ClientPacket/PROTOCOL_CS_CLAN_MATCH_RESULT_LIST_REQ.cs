// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ : GameClientPacket
{
  private List<MatchModel> list_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      player.FindClanId = ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ) this).int_0;
      ClanModel clan = ClanManager.GetClan(player.FindClanId);
      if (clan.Id <= 0)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(0, clan));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
