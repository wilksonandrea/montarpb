using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class GClass12
{
    public static UdpClient udpClient_0;

    public static bool smethod_0(IPEndPoint ipendPoint_0)
    {
        try
        {
            udpClient_0 = new UdpClient(ipendPoint_0);
            uint num3 = (0x80000000 | 0x18000000) | ((uint) 12);
            byte[] optionInValue = new byte[] { Convert.ToByte(false) };
            udpClient_0.Client.IOControl((int) num3, optionInValue, null);
            new Thread(new ThreadStart(GClass12.smethod_1)).Start();
            CLogger.Print($"Communication Address {ipendPoint_0.Address}:{ipendPoint_0.Port}", LoggerType.Info, null);
            return true;
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            return false;
        }
    }

    private static void smethod_1()
    {
        try
        {
            udpClient_0.BeginReceive(new AsyncCallback(GClass12.smethod_2), null);
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }

    private static void smethod_2(IAsyncResult iasyncResult_0)
    {
        try
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0x1f40);
            byte[] buffer = udpClient_0.EndReceive(iasyncResult_0, ref remoteEP);
            Thread.Sleep(5);
            new Thread(new ThreadStart(GClass12.smethod_1)).Start();
            if (buffer.Length >= 2)
            {
                smethod_3(buffer);
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }

    private static void smethod_3(byte[] byte_0)
    {
        try
        {
            SyncClientPacket packet = new SyncClientPacket(byte_0);
            short num = packet.ReadH();
            if (num == 0x1c03)
            {
                GClass14.smethod_0(packet);
            }
            else
            {
                CLogger.Print(Bitwise.ToHexData($"Communication - Opcode Not Found: [{num}]", packet.ToArray()), LoggerType.Opcode, null);
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }

    public static void smethod_4(byte[] byte_0, IPEndPoint ipendPoint_0)
    {
        udpClient_0.Send(byte_0, byte_0.Length, ipendPoint_0);
    }
}

