// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_REQUEST_CONTEXT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REQUEST_CONTEXT_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id > 0 && clan.OwnerId == player.PlayerId)
      {
        if (clan.Authority != ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_3)
          clan.Authority = ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_3;
        if (clan.RankLimit != ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_0)
          clan.RankLimit = ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_0;
        if (clan.MinAgeLimit != ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_1)
          clan.MinAgeLimit = ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_1;
        if (clan.MaxAgeLimit != ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_2)
          clan.MaxAgeLimit = ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_2;
        if (clan.JoinType != ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).joinClanType_0)
          clan.JoinType = ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).joinClanType_0;
        DaoManagerSQL.UpdateClanInfo(clan.Id, clan.Authority, clan.RankLimit, clan.MinAgeLimit, clan.MaxAgeLimit, (int) clan.JoinType);
      }
      else
        ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_REPLACE_NOTICE_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
