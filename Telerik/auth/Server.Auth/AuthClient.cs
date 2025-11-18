using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Server;
using Server.Auth.Network;
using Server.Auth.Network.ClientPacket;
using Server.Auth.Network.ServerPacket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Server.Auth
{
	public class AuthClient : IDisposable
	{
		public int ServerId;

		public Socket Client;

		public Account Player;

		public DateTime SessionDate;

		public int SessionId;

		public ushort SessionSeed;

		private ushort ushort_0;

		public int SessionShift;

		public int FirstPacketId;

		private bool bool_0;

		private bool bool_1;

		private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, true);

		private readonly ushort[] ushort_1 = new ushort[] { 1030, 1099, 2499 };

		public AuthClient(int int_0, Socket socket_0)
		{
			this.ServerId = int_0;
			this.Client = socket_0;
		}

		public void CheckOut(byte[] BufferTotal, int FirstLength)
		{
			int length = (int)BufferTotal.Length;
			try
			{
				if (length > FirstLength + 3)
				{
					byte[] numArray = new byte[length - FirstLength - 3];
					Array.Copy(BufferTotal, FirstLength + 3, numArray, 0, (int)numArray.Length);
					if (numArray.Length != 0)
					{
						int uInt16 = BitConverter.ToUInt16(numArray, 0) & 32767;
						if (uInt16 <= 0 || uInt16 > (int)numArray.Length - 2)
						{
							CLogger.Print(string.Format("Invalid PacketLength in CheckOut. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), uInt16, (int)numArray.Length), LoggerType.Hack, null);
							this.Close(0, true);
						}
						else
						{
							byte[] numArray1 = new byte[uInt16];
							Array.Copy(numArray, 2, numArray1, 0, (int)numArray1.Length);
							byte[] numArray2 = Bitwise.Decrypt(numArray1, this.SessionShift);
							ushort uInt161 = BitConverter.ToUInt16(numArray2, 0);
							if (this.CheckSeed(BitConverter.ToUInt16(numArray2, 2), false))
							{
								this.method_7(uInt161, numArray2, "REQ");
								this.CheckOut(numArray, uInt16);
							}
							else
							{
								this.Close(0, true);
							}
						}
					}
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		public bool CheckSeed(ushort PacketSeed, bool IsTheFirstPacket)
		{
			if (PacketSeed == this.method_6())
			{
				return true;
			}
			CLogger.Print(string.Format("Connection blocked. IP: {0}; Date: {1}; SessionId: {2}; PacketSeed: {3} / NextSessionSeed: {4}; PrimarySeed: {5}", new object[] { this.GetIPAddress(), DateTimeUtil.Now(), this.SessionId, PacketSeed, this.ushort_0, this.SessionSeed }), LoggerType.Hack, null);
			if (IsTheFirstPacket)
			{
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_3());
			}
			return false;
		}

		public void Close(int TimeMS, bool DestroyConnection)
		{
			if (this.bool_1)
			{
				return;
			}
			try
			{
				this.bool_1 = true;
				AuthXender.Client.RemoveSession(this);
				Account player = this.Player;
				if (DestroyConnection)
				{
					if (player != null)
					{
						player.SetOnlineStatus(false);
						if (player.Status.ServerId == 0)
						{
							SendRefresh.RefreshAccount(player, false);
						}
						player.Status.ResetData(player.PlayerId);
						player.SimpleClear();
						player.UpdateCacheInfo();
						this.Player = null;
					}
					if (this.Client != null)
					{
						this.Client.Close(TimeMS);
					}
					Thread.Sleep(TimeMS);
					this.Dispose();
				}
				else if (player != null)
				{
					player.SimpleClear();
					player.UpdateCacheInfo();
					this.Player = null;
				}
				UpdateServer.RefreshSChannel(this.ServerId);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("AuthClient.Close: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public void Dispose()
		{
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.bool_0)
				{
					this.Player = null;
					if (this.Client != null)
					{
						this.Client.Dispose();
						this.Client = null;
					}
					if (disposing)
					{
						this.safeHandle_0.Dispose();
					}
					this.bool_0 = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public IPAddress GetAddress()
		{
			IPAddress address;
			try
			{
				if (this.Client == null || this.Client.RemoteEndPoint == null)
				{
					address = null;
				}
				else
				{
					address = (this.Client.RemoteEndPoint as IPEndPoint).Address;
				}
			}
			catch
			{
				address = null;
			}
			return address;
		}

		public string GetIPAddress()
		{
			string str;
			try
			{
				str = (this.Client == null || this.Client.RemoteEndPoint == null ? "" : (this.Client.RemoteEndPoint as IPEndPoint).Address.ToString());
			}
			catch
			{
				str = "";
			}
			return str;
		}

		public void HeartBeatCounter()
		{
			TimerState timerState = new TimerState();
			timerState.StartTimer(TimeSpan.FromMinutes(20), (object object_0) => {
				if (!this.bool_1)
				{
					CLogger.Print(string.Concat("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: ", this.GetIPAddress()), LoggerType.Hack, null);
					this.Close(0, true);
				}
				lock (object_0)
				{
					timerState.StopJob();
				}
			});
		}

		private void method_0()
		{
			Thread.Sleep(10000);
			if (this.Client != null && this.FirstPacketId == 0)
			{
				CLogger.Print(string.Concat("Connection destroyed due to no responses. IPAddress: ", this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true);
			}
		}

		private void method_1()
		{
			this.SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
		}

		private void method_2(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket asyncState = iasyncResult_0.AsyncState as Socket;
				if (asyncState != null && asyncState.Connected)
				{
					asyncState.EndSend(iasyncResult_0);
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		private void method_3()
		{
			try
			{
				StateObject stateObject = new StateObject()
				{
					WorkSocket = this.Client,
					RemoteEP = new IPEndPoint(IPAddress.Any, 0)
				};
				this.Client.BeginReceive(stateObject.TcpBuffer, 0, 8912, SocketFlags.None, new AsyncCallback(this.method_4), stateObject);
			}
			catch
			{
				this.Close(0, true);
			}
		}

		private void method_4(IAsyncResult iasyncResult_0)
		{
			StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
			try
			{
				int ınt32 = asyncState.WorkSocket.EndReceive(iasyncResult_0);
				if (ınt32 > 0)
				{
					if (ınt32 <= 8912)
					{
						byte[] numArray = new byte[ınt32];
						Array.Copy(asyncState.TcpBuffer, 0, numArray, 0, ınt32);
						int uInt16 = BitConverter.ToUInt16(numArray, 0) & 32767;
						if (uInt16 <= 0 || uInt16 > ınt32 - 2)
						{
							CLogger.Print(string.Format("Invalid PacketLength. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), uInt16, ınt32), LoggerType.Hack, null);
							this.Close(0, true);
							return;
						}
						else
						{
							byte[] numArray1 = new byte[uInt16];
							Array.Copy(numArray, 2, numArray1, 0, (int)numArray1.Length);
							byte[] numArray2 = Bitwise.Decrypt(numArray1, this.SessionShift);
							ushort uInt161 = BitConverter.ToUInt16(numArray2, 0);
							ushort uInt162 = BitConverter.ToUInt16(numArray2, 2);
							this.method_5(uInt161);
							if (!this.CheckSeed(uInt162, true))
							{
								this.Close(0, true);
								return;
							}
							else if (!this.bool_1)
							{
								this.method_7(uInt161, numArray2, "REQ");
								this.CheckOut(numArray, uInt16);
								ThreadPool.QueueUserWorkItem((object object_0) => {
									try
									{
										this.method_3();
									}
									catch (Exception exception1)
									{
										Exception exception = exception1;
										CLogger.Print(string.Concat("Error processing packet in thread pool for IP: ", this.GetIPAddress(), ": ", exception.Message), LoggerType.Error, exception);
										this.Close(0, true);
									}
								});
							}
							else
							{
								return;
							}
						}
					}
					else
					{
						CLogger.Print(string.Concat("Received data exceeds buffer size. IP: ", this.GetIPAddress()), LoggerType.Error, null);
						this.Close(0, true);
						return;
					}
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		private void method_5(ushort ushort_2)
		{
			if (this.FirstPacketId != 0)
			{
				return;
			}
			this.FirstPacketId = ushort_2;
			if (ushort_2 != 1281 && ushort_2 != 2309)
			{
				CLogger.Print(string.Format("Connection destroyed due to unknown first packet. Opcode: {0}; IPAddress: {1}", ushort_2, this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true);
			}
		}

		private ushort method_6()
		{
			this.ushort_0 = (ushort)(this.ushort_0 * 214013 + 2531011 >> 16 & 32767);
			return this.ushort_0;
		}

		private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
		{
			try
			{
				AuthClientPacket pROTOCOLBASEGETSYSTEMINFOREQ = null;
				if (ushort_2 <= 2332)
				{
					if (ushort_2 > 2311)
					{
						switch (ushort_2)
						{
							case 2314:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_SYSTEM_INFO_REQ();
								break;
							}
							case 2315:
							case 2317:
							case 2319:
							case 2321:
							{
								goto Label0;
							}
							case 2316:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_USER_INFO_REQ();
								break;
							}
							case 2318:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_INVEN_INFO_REQ();
								break;
							}
							case 2320:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_OPTION_REQ();
								break;
							}
							case 2322:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_OPTION_SAVE_REQ();
								break;
							}
							default:
							{
								if (ushort_2 == 2328)
								{
									pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_USER_LEAVE_REQ();
									break;
								}
								else if (ushort_2 == 2332)
								{
									pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
									break;
								}
								else
								{
									goto Label0;
								}
							}
						}
					}
					else if (ushort_2 == 1057)
					{
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
					}
					else if (ushort_2 == 1281)
					{
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_LOGIN_REQ();
					}
					else
					{
						switch (ushort_2)
						{
							case 2307:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_LOGOUT_REQ();
								break;
							}
							case 2309:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
								break;
							}
							case 2311:
							{
								pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GAMEGUARD_REQ();
								break;
							}
							default:
							{
								goto Label0;
							}
						}
					}
				}
				else if (ushort_2 <= 2459)
				{
					if (ushort_2 == 2399)
					{
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
					}
					else if (ushort_2 == 2414)
					{
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_DAILY_RECORD_REQ();
					}
					else
					{
						if (ushort_2 != 2459)
						{
							goto Label0;
						}
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_GET_MAP_INFO_REQ();
					}
				}
				else if (ushort_2 <= 2516)
				{
					if (ushort_2 == 2489)
					{
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
					}
					else
					{
						if (ushort_2 != 2516)
						{
							goto Label0;
						}
						pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ();
					}
				}
				else if (ushort_2 == 7681)
				{
					pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_MATCH_SERVER_IDX_REQ();
				}
				else
				{
					if (ushort_2 != 7699)
					{
						goto Label0;
					}
					pROTOCOLBASEGETSYSTEMINFOREQ = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
				}
			Label3:
				if (pROTOCOLBASEGETSYSTEMINFOREQ != null)
				{
					using (pROTOCOLBASEGETSYSTEMINFOREQ)
					{
						if (ConfigLoader.DebugMode)
						{
							CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", pROTOCOLBASEGETSYSTEMINFOREQ.GetType().Name, this.Client.RemoteEndPoint, ushort_2), LoggerType.Debug, null);
						}
						pROTOCOLBASEGETSYSTEMINFOREQ.Makeme(this, byte_0);
						ThreadPool.QueueUserWorkItem((object object_0) => {
							try
							{
								pROTOCOLBASEGETSYSTEMINFOREQ.Run();
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								CLogger.Print(exception.Message, LoggerType.Error, exception);
								this.Close(50, true);
							}
						});
						pROTOCOLBASEGETSYSTEMINFOREQ.Dispose();
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				CLogger.Print(exception2.Message, LoggerType.Error, exception2);
			}
			return;
		Label0:
			CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}] | {1}", ushort_2, string_0), byte_0), LoggerType.Opcode, null);
			goto Label3;
		}

		public void SendCompletePacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				byte[] bytes = BitConverter.GetBytes((ushort)((int)OriginalData.Length + 2));
				byte[] numArray = new byte[(int)OriginalData.Length + (int)bytes.Length];
				Array.Copy(bytes, 0, numArray, 0, (int)bytes.Length);
				Array.Copy(OriginalData, 0, numArray, 2, (int)OriginalData.Length);
				byte[] numArray1 = new byte[(int)numArray.Length + 5];
				Array.Copy(numArray, 0, numArray1, 0, (int)numArray.Length);
				if (ConfigLoader.DebugMode)
				{
					ushort uInt16 = BitConverter.ToUInt16(numArray1, 2);
					Bitwise.ToByteString(numArray1);
					CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", PacketName, this.Client.RemoteEndPoint, uInt16), LoggerType.Debug, null);
				}
				Bitwise.ProcessPacket(numArray1, 2, 5, this.ushort_1);
				if (this.Client != null && numArray1.Length != 0)
				{
					this.Client.BeginSend(numArray1, 0, (int)numArray1.Length, SocketFlags.None, new AsyncCallback(this.method_2), this.Client);
				}
				numArray1 = null;
			}
			catch
			{
				this.Close(0, true);
			}
		}

		public void SendPacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				if ((int)OriginalData.Length >= 2)
				{
					this.SendCompletePacket(OriginalData, PacketName);
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		public void SendPacket(AuthServerPacket Packet)
		{
			try
			{
				using (Packet)
				{
					byte[] bytes = Packet.GetBytes("AuthClient.SendPacket");
					this.SendPacket(bytes, Packet.GetType().Name);
					Packet.Dispose();
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		public void StartSession()
		{
			try
			{
				this.ushort_0 = this.SessionSeed;
				this.SessionShift = (this.SessionId + Bitwise.CRYPTO[0]) % 7 + 1;
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_1());
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_3());
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_0());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				this.Close(0, true);
			}
		}
	}
}