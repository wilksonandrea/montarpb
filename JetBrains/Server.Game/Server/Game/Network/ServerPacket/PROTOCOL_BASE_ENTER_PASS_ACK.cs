// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_ENTER_PASS_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ENTER_PASS_ACK : GameServerPacket
{
  private readonly uint uint_0;

  protected new virtual void Dispose(bool Client)
  {
    try
    {
      if (this.Disposed)
        return;
      this.MStream.Dispose();
      this.BWriter.Dispose();
      if (Client)
        this.Handle.Dispose();
      this.Disposed = true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public abstract void Write();
}
