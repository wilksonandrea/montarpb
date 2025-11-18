// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Update.KillFragInfo
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Update;

public class KillFragInfo
{
  private void method_3([In] IAsyncResult obj0)
  {
    try
    {
      if (!(obj0.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(obj0);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("GameSync socket disposed during SendCallback.", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public void Close()
  {
    if (((GameSync) this).bool_0)
      return;
    ((GameSync) this).bool_0 = true;
    try
    {
      if (((GameSync) this).ClientSocket == null)
        return;
      ((GameSync) this).ClientSocket.Close();
      ((GameSync) this).ClientSocket.Dispose();
      ((GameSync) this).ClientSocket = (Socket) null;
    }
    catch (Exception ex)
    {
      CLogger.Print("Error closing GameSync: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
