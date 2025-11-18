// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.Update.ClanInfo
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Net.Sockets;

#nullable disable
namespace Server.Auth.Data.Sync.Update;

public class ClanInfo
{
  private void method_3(IAsyncResult ipendPoint_0)
  {
    try
    {
      if (!(ipendPoint_0.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(ipendPoint_0);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("AuthSync socket disposed during SendCallback.", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public void Close()
  {
    if (((AuthSync) this).bool_0)
      return;
    ((AuthSync) this).bool_0 = true;
    try
    {
      if (((AuthSync) this).ClientSocket == null)
        return;
      ((AuthSync) this).ClientSocket.Close();
      ((AuthSync) this).ClientSocket.Dispose();
      ((AuthSync) this).ClientSocket = (Socket) null;
    }
    catch (Exception ex)
    {
      CLogger.Print("Error closing AuthSync: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
