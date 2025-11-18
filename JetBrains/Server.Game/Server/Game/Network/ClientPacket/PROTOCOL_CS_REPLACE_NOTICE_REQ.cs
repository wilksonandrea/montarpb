// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_REPLACE_NOTICE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REPLACE_NOTICE_REQ : GameClientPacket
{
  private string string_0;
  private EventErrorEnum eventErrorEnum_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ClanModel clan = ClanManager.GetClan(player.ClanId);
      if (clan.Id > 0 && clan.Info != ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).string_0 && (clan.OwnerId == this.Client.PlayerId || player.ClanAccess >= 1 && player.ClanAccess <= 2))
      {
        if (ComDiv.UpdateDB("system_clan", "info", (object) ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).string_0, "id", (object) clan.Id))
          clan.Info = ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).string_0;
        else
          ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).eventErrorEnum_0 = EventErrorEnum.CLAN_REPLACE_INTRO_ERROR;
      }
      else
        ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).eventErrorEnum_0 = EventErrorEnum.CLAN_FAILED_CHANGE_OPTION;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK(((PROTOCOL_CS_REPLACE_INTRO_REQ) this).eventErrorEnum_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_3 = (int) this.ReadC();
    ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_2 = (int) this.ReadC();
    ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).int_1 = (int) this.ReadC();
    ((PROTOCOL_CS_REPLACE_MANAGEMENT_REQ) this).joinClanType_0 = (JoinClanType) this.ReadC();
  }
}
