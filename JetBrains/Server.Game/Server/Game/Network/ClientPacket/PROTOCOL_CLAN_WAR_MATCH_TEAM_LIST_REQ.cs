// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Match == null)
        return;
      ChannelModel channel = player.GetChannel();
      if (channel == null || channel.Type != ChannelType.Clan)
        return;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(channel.Matches.Count));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_0 = (int) this.ReadH();
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_REQ) this).int_1 = (int) this.ReadH();
  }
}
