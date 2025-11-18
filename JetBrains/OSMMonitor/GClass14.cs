// Decompiled with JetBrains decompiler
// Type: GClass14
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using System;

#nullable disable
public class GClass14
{
  public static void smethod_0(SyncClientPacket syncClientPacket_0)
  {
    byte mission = syncClientPacket_0.ReadC();
    string str = syncClientPacket_0.ReadS((int) mission);
    if (string.IsNullOrEmpty(str) || mission >= (byte) 60)
      return;
    CLogger.Print("From Server Message: " + str, LoggerType.Info, (Exception) null);
  }
}
