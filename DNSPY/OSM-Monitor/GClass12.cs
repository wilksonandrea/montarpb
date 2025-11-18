using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;

// Token: 0x02000025 RID: 37
public static class GClass12
{
	// Token: 0x060000AE RID: 174 RVA: 0x000053E8 File Offset: 0x000035E8
	public static bool smethod_0(IPEndPoint ipendPoint_0)
	{
		bool flag;
		try
		{
			GClass12.udpClient_0 = new UdpClient(ipendPoint_0);
			uint num = 2147483648U;
			uint num2 = 402653184U;
			uint num3 = num | num2 | 12U;
			GClass12.udpClient_0.Client.IOControl((int)num3, new byte[] { Convert.ToByte(false) }, null);
			new Thread(new ThreadStart(GClass12.smethod_1)).Start();
			CLogger.Print(string.Format("Communication Address {0}:{1}", ipendPoint_0.Address, ipendPoint_0.Port), LoggerType.Info, null);
			flag = true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00005498 File Offset: 0x00003698
	private static void smethod_1()
	{
		try
		{
			GClass12.udpClient_0.BeginReceive(new AsyncCallback(GClass12.smethod_2), null);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000054E0 File Offset: 0x000036E0
	private static void smethod_2(IAsyncResult iasyncResult_0)
	{
		try
		{
			IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Any, 8000);
			byte[] array = GClass12.udpClient_0.EndReceive(iasyncResult_0, ref ipendPoint);
			Thread.Sleep(5);
			new Thread(new ThreadStart(GClass12.smethod_1)).Start();
			if (array.Length >= 2)
			{
				GClass12.smethod_3(array);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00005558 File Offset: 0x00003758
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
				CLogger.Print(Bitwise.ToHexData(string.Format("Communication - Opcode Not Found: [{0}]", num), syncClientPacket.ToArray()), LoggerType.Opcode, null);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000246A File Offset: 0x0000066A
	public static void smethod_4(byte[] byte_0, IPEndPoint ipendPoint_0)
	{
		GClass12.udpClient_0.Send(byte_0, byte_0.Length, ipendPoint_0);
	}

	// Token: 0x040000A7 RID: 167
	public static UdpClient udpClient_0;
}
