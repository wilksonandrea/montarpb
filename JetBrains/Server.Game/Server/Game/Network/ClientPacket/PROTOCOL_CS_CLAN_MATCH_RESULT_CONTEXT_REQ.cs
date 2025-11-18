// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || (int) ClanManager.GetClan(player.ClanId).Logo == (int) ((PROTOCOL_CS_CHECK_MARK_REQ) this).uint_0 || CommandManager.IsClanLogoExist(((PROTOCOL_CS_CHECK_MARK_REQ) this).uint_0))
        ((PROTOCOL_CS_CHECK_MARK_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(((PROTOCOL_CS_CHECK_MARK_REQ) this).uint_1));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read() => ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ) this).int_0 = this.ReadD();
}
