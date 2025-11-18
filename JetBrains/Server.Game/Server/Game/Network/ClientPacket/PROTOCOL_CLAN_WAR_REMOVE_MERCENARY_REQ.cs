// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ
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

public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Match == null)
        return;
      ChannelModel channel = AllUtils.GetChannel(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_1, ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_1 - ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_1 / 10 * 10);
      if (channel != null)
      {
        MatchModel match = channel.GetMatch(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_0);
        if (match != null)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(0U, match.Clan));
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(2147483648U /*0x80000000*/));
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(2147483648U /*0x80000000*/));
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
