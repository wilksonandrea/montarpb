// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_GET_CHANNELLIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(0, ((PROTOCOL_BASE_GAMEGUARD_REQ) this).byte_0));
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
