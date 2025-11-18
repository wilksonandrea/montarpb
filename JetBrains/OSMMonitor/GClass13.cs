// Decompiled with JetBrains decompiler
// Type: GClass13
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using System;
using System.Net;

#nullable disable
public class GClass13
{
  public static void smethod_0(string string_0)
  {
    try
    {
      foreach (SChannelModel server in SChannelXML.Servers)
      {
        if (server == null)
          break;
        IPEndPoint connection = SynchronizeXML.GetServer((int) server.Port).Connection;
        using (SyncServerPacket syncServerPacket = new SyncServerPacket())
        {
          syncServerPacket.WriteH((short) 7171);
          syncServerPacket.WriteC((byte) string_0.Length);
          syncServerPacket.WriteS(string_0, string_0.Length);
          GClass12.smethod_4(syncServerPacket.ToArray(), connection);
        }
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }
}
