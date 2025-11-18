// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ
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

public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ : GameClientPacket
{
  private int int_0;
  private uint uint_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      int length = ((PROTOCOL_CS_CHATTING_REQ) this).string_0.Length;
      int Exception = -1;
      bool flag1 = true;
      bool flag2 = true;
      if (length > 60 || ((PROTOCOL_CS_CHATTING_REQ) this).chattingType_0 != ChattingType.Clan)
        return;
      using (PROTOCOL_CS_CHATTING_ACK Packet = (PROTOCOL_CS_CHATTING_ACK) new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK(((PROTOCOL_CS_CHATTING_REQ) this).string_0, player))
        ClanManager.SendPacket((GameServerPacket) Packet, player.ClanId, (long) Exception, flag2, flag1);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_CHECK_DUPLICATE_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
  }
}
