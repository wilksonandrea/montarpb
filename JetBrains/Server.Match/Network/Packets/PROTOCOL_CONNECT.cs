// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Packets.PROTOCOL_CONNECT
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Packets;

public class PROTOCOL_CONNECT
{
  public void SendPacket(byte[] iasyncResult_0, [In] IPEndPoint obj1)
  {
    try
    {
      ((MatchManager) this).MainSocket.BeginSendTo(iasyncResult_0, 0, iasyncResult_0.Length, SocketFlags.None, (EndPoint) obj1, new AsyncCallback(this.method_4), (object) ((MatchManager) this).MainSocket);
    }
    catch (Exception ex)
    {
      CLogger.Print($"Failed to send package to {obj1}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  private void method_4([In] IAsyncResult obj0)
  {
    try
    {
      if (!(obj0.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(obj0);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error during EndSendCallback: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
