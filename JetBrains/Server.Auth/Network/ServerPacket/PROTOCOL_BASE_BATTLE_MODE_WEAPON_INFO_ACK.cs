// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;
using System.IO;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK : AuthServerPacket
{
  protected internal void Makeme(AuthClient Host, byte[] Port)
  {
    ((AuthClientPacket) this).Client = Host;
    ((BaseClientPacket) this).MStream = new MemoryStream(Port, 4, Port.Length - 4);
    ((BaseClientPacket) this).BReader = new BinaryReader((Stream) ((BaseClientPacket) this).MStream);
    ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).Read();
  }

  public void Dispose()
  {
    try
    {
      ((PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK) this).Dispose(true);
      GC.SuppressFinalize((object) this);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
