// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ
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

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(((PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ) this).int_0);
      if (clan.Id == 0)
        ((PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ) this).uint_0 = 2147483648U /*0x80000000*/;
      else if (clan.RankLimit > player.Rank)
        ((PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ) this).uint_0 = 2147487867U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(((PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_CS_CHECK_MARK_REQ) this).uint_0 = this.ReadUD();
}
