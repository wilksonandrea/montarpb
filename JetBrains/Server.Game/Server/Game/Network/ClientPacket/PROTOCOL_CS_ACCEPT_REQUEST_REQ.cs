// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_ACCEPT_REQUEST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_ACCEPT_REQUEST_REQ : GameClientPacket
{
  private List<long> list_0;
  private int int_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_ACCEPT_REQUEST_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ) this).chattingType_0 = (ChattingType) this.ReadH();
    ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ) this).string_0 = this.ReadS((int) this.ReadH());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Match == null || ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ) this).chattingType_0 != ChattingType.Match)
        return;
      using (PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK warTeamChattingAck = (PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK) new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(player.Nickname, ((PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ) this).string_0))
        player.Match.SendPacketToPlayers((GameServerPacket) warTeamChattingAck);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CLAN_WAR_TEAM_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
