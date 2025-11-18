using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

public static class GClass12
{
	public static UdpClient udpClient_0;

	public static bool smethod_0(IPEndPoint ipendPoint_0)
	{
		try
		{
			udpClient_0 = new UdpClient(ipendPoint_0);
			uint num = 2147483648u;
			uint num2 = 402653184u;
			uint ioControlCode = num | num2 | 0xCu;
			udpClient_0.Client.IOControl((int)ioControlCode, new byte[1] { Convert.ToByte(value: false) }, null);
			new Thread(smethod_1).Start();
			CLogger.Print($"Communication Address {ipendPoint_0.Address}:{ipendPoint_0.Port}", LoggerType.Info);
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
			udpClient_0.BeginReceive(smethod_2, null);
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
			byte[] array = udpClient_0.EndReceive(iasyncResult_0, ref remoteEP);
			Thread.Sleep(5);
			new Thread(smethod_1).Start();
			if (array.Length >= 2)
			{
				smethod_3(array);
			}
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
			SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
			short num = syncClientPacket.ReadH();
			if (num == 7171)
			{
				GClass14.smethod_0(syncClientPacket);
			}
			else
			{
				CLogger.Print(Bitwise.ToHexData($"Communication - Opcode Not Found: [{num}]", syncClientPacket.ToArray()), LoggerType.Opcode);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void smethod_4(byte[] byte_0, IPEndPoint ipendPoint_0)
	{
		udpClient_0.Send(byte_0, byte_0.Length, ipendPoint_0);
	}
}
