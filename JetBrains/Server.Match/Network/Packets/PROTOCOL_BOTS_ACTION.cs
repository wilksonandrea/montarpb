// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Packets.PROTOCOL_BOTS_ACTION
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

public class PROTOCOL_BOTS_ACTION
{
  private bool method_2(IPEndPoint string_1)
  {
    try
    {
      Socket socket;
      return string_1 != null && MatchManager.get_UdpClients().ContainsKey(string_1) && MatchManager.get_UdpClients().TryGetValue(string_1, out socket) && MatchManager.get_UdpClients().TryRemove(string_1, out socket);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return false;
  }

  private bool method_3([In] string obj0)
  {
    try
    {
      int num;
      return !string.IsNullOrEmpty(obj0) && !obj0.Equals("0.0.0.0") && MatchXender.SpamConnections.ContainsKey(obj0) && MatchXender.SpamConnections.TryGetValue(obj0, out num) && MatchXender.SpamConnections.TryRemove(obj0, out num);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
    return false;
  }
}
