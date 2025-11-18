// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_CS_CHECK_DUPLICATE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Run()
  {
    try
    {
      if (this.Client.Player == null || !DaoManagerSQL.DeleteClanInviteDB(this.Client.PlayerId))
        ((PROTOCOL_CS_CANCEL_REQUEST_REQ) this).uint_0 = 2147487835U;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_CHATTING_ACK(((PROTOCOL_CS_CANCEL_REQUEST_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_CS_CHATTING_REQ) this).chattingType_0 = (ChattingType) this.ReadH();
    ((PROTOCOL_CS_CHATTING_REQ) this).string_0 = this.ReadU((int) this.ReadH() * 2);
  }
}
