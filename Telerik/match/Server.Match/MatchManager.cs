using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Server.Match
{
	public class MatchManager
	{
		private readonly string string_0;

		private readonly int int_0;

		public Socket MainSocket;

		public bool ServerIsClosed;

		public MatchManager(string string_1, int int_1)
		{
			this.string_0 = string_1;
			this.int_0 = int_1;
		}

		public void BeginReceive(MatchClient Client, byte[] Buffer)
		{
			try
			{
				if (Client != null)
				{
					Client.BeginReceive(Buffer, DateTimeUtil.Now());
				}
				else
				{
					CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private void method_0()
		{
			try
			{
				StateObject stateObject = new StateObject()
				{
					WorkSocket = this.MainSocket,
					RemoteEP = new IPEndPoint(IPAddress.Any, 0)
				};
				this.MainSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, new AsyncCallback(this.method_1), stateObject);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private void method_1(IAsyncResult iasyncResult_0)
		{
			if (!iasyncResult_0.IsCompleted)
			{
				CLogger.Print("IAsyncResult is not completed.", LoggerType.Warning, null);
			}
			StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
			Socket workSocket = asyncState.WorkSocket;
			IPEndPoint remoteEP = asyncState.RemoteEP as IPEndPoint;
			try
			{
				try
				{
					EndPoint pEndPoint = new IPEndPoint(IPAddress.Any, 0);
					int ınt32 = workSocket.EndReceiveFrom(iasyncResult_0, ref pEndPoint);
					if (ınt32 > 0)
					{
						byte[] numArray = new byte[ınt32];
						Buffer.BlockCopy(asyncState.UdpBuffer, 0, numArray, 0, ınt32);
						if ((int)numArray.Length < 22 || (int)numArray.Length > 8096)
						{
							CLogger.Print(string.Format("Invalid Buffer Length: {0}; IP: {1}", (int)numArray.Length, remoteEP), LoggerType.Hack, null);
							return;
						}
						else
						{
							this.BeginReceive(new MatchClient(workSocket, pEndPoint as IPEndPoint), numArray);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(string.Concat("Error during EndReceiveCallback: ", exception.Message), LoggerType.Error, exception);
					this.method_2(remoteEP);
					this.method_3(string.Format("{0}", remoteEP.Address));
				}
			}
			finally
			{
				this.method_0();
			}
		}

		private bool method_2(IPEndPoint ipendPoint_0)
		{
			Socket socket;
			bool flag;
			try
			{
				if (ipendPoint_0 == null)
				{
					flag = false;
				}
				else if (!MatchXender.UdpClients.ContainsKey(ipendPoint_0) || !MatchXender.UdpClients.TryGetValue(ipendPoint_0, out socket))
				{
					return false;
				}
				else
				{
					flag = MatchXender.UdpClients.TryRemove(ipendPoint_0, out socket);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return false;
			}
			return flag;
		}

		private bool method_3(string string_1)
		{
			int ınt32;
			bool flag;
			try
			{
				if (string.IsNullOrEmpty(string_1) || string_1.Equals("0.0.0.0"))
				{
					flag = false;
				}
				else if (!MatchXender.SpamConnections.ContainsKey(string_1) || !MatchXender.SpamConnections.TryGetValue(string_1, out ınt32))
				{
					return false;
				}
				else
				{
					flag = MatchXender.SpamConnections.TryRemove(string_1, out ınt32);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return false;
			}
			return flag;
		}

		private void method_4(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket asyncState = iasyncResult_0.AsyncState as Socket;
				if (asyncState != null && asyncState.Connected)
				{
					asyncState.EndSend(iasyncResult_0);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error during EndSendCallback: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			try
			{
				this.MainSocket.BeginSendTo(Data, 0, (int)Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_4), this.MainSocket);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Format("Failed to send package to {0}: {1}", Address, exception.Message), LoggerType.Error, exception);
			}
		}

		public bool Start()
		{
			bool flag;
			try
			{
				this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				this.MainSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
				IPEndPoint pEndPoint = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
				this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
				this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
				this.MainSocket.DontFragment = false;
				this.MainSocket.Ttl = 128;
				this.MainSocket.Bind(pEndPoint);
				CLogger.Print(string.Format("Match Serv Address {0}:{1}", pEndPoint.Address, pEndPoint.Port), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem((object object_0) => {
					try
					{
						this.method_0();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(string.Concat("Error processing UDP packet from ", this.string_0, ": ", exception.Message), LoggerType.Error, exception);
					}
				});
				flag = true;
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				CLogger.Print(exception2.Message, LoggerType.Error, exception2);
				flag = false;
			}
			return flag;
		}
	}
}