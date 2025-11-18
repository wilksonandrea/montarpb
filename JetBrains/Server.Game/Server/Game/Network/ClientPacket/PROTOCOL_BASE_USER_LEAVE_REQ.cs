// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_USER_LEAVE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_LEAVE_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_USER_TITLE_CHANGE_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_BASE_USER_ENTER_REQ) this).string_0 = this.ReadS((int) this.ReadC());
    ((PROTOCOL_BASE_USER_ENTER_REQ) this).long_0 = this.ReadQ();
  }
}
