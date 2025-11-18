using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match;
using Server.Match.Data.Sync.Client;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Server.Match.Data.Sync
{
	public class MatchSync
	{
		protected Socket ClientSocket;

		private bool bool_0;

		public MatchSync(IPEndPoint ipendPoint_0)
		{
			this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.ClientSocket.Bind(ipendPoint_0);
			this.ClientSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
		}

		public void Close()
		{
			if (this.bool_0)
			{
				return;
			}
			this.bool_0 = true;
			try
			{
				if (this.ClientSocket != null)
				{
					this.ClientSocket.Close();
					this.ClientSocket.Dispose();
					this.ClientSocket = null;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error closing MatchSync: ", exception.Message), LoggerType.Error, exception);
			}
		}

		private void method_0()
		{
			if (this.bool_0)
			{
				return;
			}
			try
			{
				StateObject stateObject = new StateObject()
				{
					WorkSocket = this.ClientSocket,
					RemoteEP = new IPEndPoint(IPAddress.Any, 8000)
				};
				this.ClientSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, new AsyncCallback(this.method_1), stateObject);
			}
			catch (ObjectDisposedException objectDisposedException)
			{
				CLogger.Print("MatchSync socket disposed during StartReceive.", LoggerType.Warning, null);
				this.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error in StartReceive: ", exception.Message), LoggerType.Error, exception);
				this.Close();
			}
		}

		private void method_1(IAsyncResult iasyncResult_0)
		{
			if (this.bool_0 || MatchXender.Client == null || MatchXender.Client.ServerIsClosed)
			{
				return;
			}
			StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
			try
			{
				try
				{
					int ınt32 = this.ClientSocket.EndReceiveFrom(iasyncResult_0, ref asyncState.RemoteEP);
					if (ınt32 > 0)
					{
						byte[] numArray = new byte[ınt32];
						Array.Copy(asyncState.UdpBuffer, 0, numArray, 0, ınt32);
						ThreadPool.QueueUserWorkItem((object object_0) => {
							try
							{
								this.method_2(numArray);
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								CLogger.Print(string.Concat("Error processing AuthSync packet in thread pool: ", exception.Message), LoggerType.Error, exception);
							}
						});
					}
				}
				catch (ObjectDisposedException objectDisposedException)
				{
					CLogger.Print("MatchSync socket disposed during ReceiveCallback.", LoggerType.Warning, null);
					this.Close();
				}
				catch (Exception exception3)
				{
					Exception exception2 = exception3;
					CLogger.Print(string.Concat("Error in ReceiveCallback: ", exception2.Message), LoggerType.Error, exception2);
				}
			}
			finally
			{
				this.method_0();
			}
		}

		private void method_2(byte[] byte_0)
		{
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
				short ınt16 = syncClientPacket.ReadH();
				switch (ınt16)
				{
					case 1:
					{
						RespawnSync.Load(syncClientPacket);
						break;
					}
					case 2:
					{
						RemovePlayerSync.Load(syncClientPacket);
						break;
					}
					case 3:
					{
						MatchRoundSync.Load(syncClientPacket);
						break;
					}
					default:
					{
						CLogger.Print(Bitwise.ToHexData(string.Format("Match - Opcode Not Found: [{0}]", ınt16), syncClientPacket.ToArray()), LoggerType.Opcode, null);
						break;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private void method_3(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket asyncState = iasyncResult_0.AsyncState as Socket;
				if (asyncState != null && asyncState.Connected)
				{
					asyncState.EndSend(iasyncResult_0);
				}
			}
			catch (ObjectDisposedException objectDisposedException)
			{
				CLogger.Print("MatchSync socket disposed during SendCallback.", LoggerType.Warning, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error in SendCallback: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			if (this.bool_0)
			{
				return;
			}
			try
			{
				this.ClientSocket.BeginSendTo(Data, 0, (int)Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_3), this.ClientSocket);
			}
			catch (ObjectDisposedException objectDisposedException)
			{
				CLogger.Print(string.Format("MatchSync socket disposed during SendPacket to {0}.", Address), LoggerType.Warning, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Format("Error sending UDP packet to {0}: {1}", Address, exception.Message), LoggerType.Error, exception);
			}
		}

		public bool Start()
		{
			bool flag;
			try
			{
				IPEndPoint localEndPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
				CLogger.Print(string.Format("Match Sync Address {0}:{1}", localEndPoint.Address, localEndPoint.Port), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_0());
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
	}
}