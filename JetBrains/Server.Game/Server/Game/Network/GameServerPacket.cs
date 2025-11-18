// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.GameServerPacket
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network;

public abstract class GameServerPacket : BaseServerPacket, IDisposable
{
  protected internal void Makeme(GameClient value, [In] byte[] obj1)
  {
    ((GameClientPacket) this).Client = value;
    ((BaseClientPacket) this).MStream = new MemoryStream(obj1, 4, obj1.Length - 4);
    ((BaseClientPacket) this).BReader = new BinaryReader((Stream) ((BaseClientPacket) this).MStream);
    this.Read();
  }

  public void Dispose()
  {
    try
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  protected virtual void Dispose(bool ServerId)
  {
    try
    {
      if (((BaseClientPacket) this).Disposed)
        return;
      ((BaseClientPacket) this).MStream.Dispose();
      ((BaseClientPacket) this).BReader.Dispose();
      if (ServerId)
        ((BaseClientPacket) this).Handle.Dispose();
      ((BaseClientPacket) this).Disposed = true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public abstract void Read();

  public abstract void Run();

  public GameServerPacket()
  {
    this.MStream = new MemoryStream();
    this.BWriter = new BinaryWriter((Stream) this.MStream);
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
    this.SECURITY_KEY = Bitwise.CRYPTO[0];
    this.HASH_CODE = Bitwise.CRYPTO[1];
    this.SEED_LENGTH = Bitwise.CRYPTO[2];
    this.NATIONS = ConfigLoader.National;
  }
}
