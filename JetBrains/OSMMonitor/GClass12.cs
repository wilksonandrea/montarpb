// Decompiled with JetBrains decompiler
// Type: GClass12
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#nullable disable
public static class GClass12
{
  public static UdpClient udpClient_0;

  public static bool smethod_0(IPEndPoint ipendPoint_0)
  {
    try
    {
      GClass12.udpClient_0 = new UdpClient(ipendPoint_0);
      uint ioControlCode = (uint) (int.MinValue | 402653184 /*0x18000000*/ | 12);
      GClass12.udpClient_0.Client.IOControl((int) ioControlCode, new byte[1]
      {
        Convert.ToByte(false)
      }, (byte[]) null);
      new Thread(new ThreadStart(GClass12.smethod_1)).Start();
      CLogger.Print($"Communication Address {ipendPoint_0.Address}:{ipendPoint_0.Port}", LoggerType.Info, (Exception) null);
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  private static void smethod_1()
  {
    try
    {
      GClass12.udpClient_0.BeginReceive(new AsyncCallback(GClass12.smethod_2), (object) null);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_2(IAsyncResult iasyncResult_0)
  {
    try
    {
      IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 8000);
      byte[] byte_0 = GClass12.udpClient_0.EndReceive(iasyncResult_0, ref remoteEP);
      Thread.Sleep(5);
      new Thread(new ThreadStart(GClass12.smethod_1)).Start();
      if (byte_0.Length < 2)
        return;
      GClass12.smethod_3(byte_0);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private static void smethod_3(byte[] byte_0)
  {
    try
    {
      SyncClientPacket syncClientPacket_0 = new SyncClientPacket(byte_0);
      short num = syncClientPacket_0.ReadH();
      if (num == (short) 7171)
        GClass14.smethod_0(syncClientPacket_0);
      else
        CLogger.Print(Bitwise.ToHexData($"Communication - Opcode Not Found: [{num}]", syncClientPacket_0.ToArray()), LoggerType.Opcode, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public static void smethod_4(byte[] byte_0, IPEndPoint ipendPoint_0)
  {
    GClass12.udpClient_0.Send(byte_0, byte_0.Length, ipendPoint_0);
  }
}
