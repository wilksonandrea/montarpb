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
		bool flag;
		try
		{
			GClass12.udpClient_0 = new UdpClient(ipendPoint_0);
			uint uInt32 = -2147483648 | 402653184 | 12;
			GClass12.udpClient_0.Client.IOControl((int)uInt32, new byte[] { Convert.ToByte(false) }, null);
			(new Thread(new ThreadStart(GClass12.smethod_1))).Start();
			CLogger.Print(string.Format("Communication Address {0}:{1}", ipendPoint_0.Address, ipendPoint_0.Port), LoggerType.Info, null);
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	private static void smethod_1()
	{
		try
		{
			GClass12.udpClient_0.BeginReceive(new AsyncCallback(GClass12.smethod_2), null);
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
	}

	private static void smethod_2(IAsyncResult iasyncResult_0)
	{
		try
		{
			IPEndPoint pEndPoint = new IPEndPoint(IPAddress.Any, 8000);
			byte[] numArray = GClass12.udpClient_0.EndReceive(iasyncResult_0, ref pEndPoint);
			Thread.Sleep(5);
			(new Thread(new ThreadStart(GClass12.smethod_1))).Start();
			if ((int)numArray.Length >= 2)
			{
				GClass12.smethod_3(numArray);
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
	}

	private static void smethod_3(byte[] byte_0)
	{
		try
		{
			SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
			short ınt16 = syncClientPacket.ReadH();
			if (ınt16 != 7171)
			{
				CLogger.Print(Bitwise.ToHexData(string.Format("Communication - Opcode Not Found: [{0}]", ınt16), syncClientPacket.ToArray()), LoggerType.Opcode, null);
			}
			else
			{
				GClass14.smethod_0(syncClientPacket);
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
	}

	public static void smethod_4(byte[] byte_0, IPEndPoint ipendPoint_0)
	{
		GClass12.udpClient_0.Send(byte_0, (int)byte_0.Length, ipendPoint_0);
	}
}