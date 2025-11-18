// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.AuthClientPacket
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network;

public abstract class AuthClientPacket : BaseClientPacket, IDisposable
{
  protected AuthClient Client;

  public byte[] GetBytes(string value)
  {
    try
    {
      this.Write();
      return ((BaseServerPacket) this).MStream.ToArray();
    }
    catch (Exception ex)
    {
      CLogger.Print($"GetBytes problem at: {value}; {ex.Message}", LoggerType.Error, ex);
      return new byte[0];
    }
  }

  public byte[] GetCompleteBytes(string value)
  {
    try
    {
      byte[] bytes = this.GetBytes("AuthServerPacket.GetCompleteBytes");
      return bytes.Length >= 2 ? bytes : new byte[0];
    }
    catch (Exception ex)
    {
      CLogger.Print($"GetCompleteBytes problem at: {value}; {ex.Message}", LoggerType.Error, ex);
      return new byte[0];
    }
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

  protected virtual void Dispose(bool value)
  {
    try
    {
      if (((BaseServerPacket) this).Disposed)
        return;
      ((BaseServerPacket) this).MStream.Dispose();
      ((BaseServerPacket) this).BWriter.Dispose();
      if (value)
        ((BaseServerPacket) this).Handle.Dispose();
      ((BaseServerPacket) this).Disposed = true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public abstract void Write();

  public AuthClientPacket()
  {
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
    this.SECURITY_KEY = Bitwise.CRYPTO[0];
    this.HASH_CODE = Bitwise.CRYPTO[1];
    this.SEED_LENGTH = Bitwise.CRYPTO[2];
    this.NATIONS = ConfigLoader.National;
  }
}
