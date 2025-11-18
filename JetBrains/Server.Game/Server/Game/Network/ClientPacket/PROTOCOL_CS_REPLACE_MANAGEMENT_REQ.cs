// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_REPLACE_MANAGEMENT_REQ
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

public class PROTOCOL_CS_REPLACE_MANAGEMENT_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private int int_2;
  private int int_3;
  private JoinClanType joinClanType_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || ((PROTOCOL_CS_PAGE_CHATTING_REQ) this).chattingType_0 != ChattingType.ClanMemberPage)
        return;
      using (PROTOCOL_CS_PAGE_CHATTING_ACK Packet = (PROTOCOL_CS_PAGE_CHATTING_ACK) new PROTOCOL_CS_RECORD_RESET_RESULT_ACK(player, ((PROTOCOL_CS_PAGE_CHATTING_REQ) this).string_0))
        ClanManager.SendPacket((GameServerPacket) Packet, player.ClanId, -1L, true, true);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_CS_PAGE_CHATTING_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_REPLACE_INTRO_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
