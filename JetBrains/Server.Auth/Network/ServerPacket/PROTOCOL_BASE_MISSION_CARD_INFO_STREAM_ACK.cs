// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ServerPacket.PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;

#nullable disable
namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK : AuthServerPacket
{
  private readonly byte[] byte_0;
  private readonly byte byte_1;
  private readonly short short_0;

  protected virtual void Dispose(bool Name)
  {
    try
    {
      if (((BaseClientPacket) this).Disposed)
        return;
      ((BaseClientPacket) this).MStream.Dispose();
      ((BaseClientPacket) this).BReader.Dispose();
      if (Name)
        ((BaseClientPacket) this).Handle.Dispose();
      ((BaseClientPacket) this).Disposed = true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public abstract void Read();
}
