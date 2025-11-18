using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : GameClientPacket
	{
		public PROTOCOL_LOBBY_GET_ROOMLIST_REQ()
		{
		}

		private byte[] method_0(int int_0, ref int int_1, List<RoomModel> list_0)
		{
			byte[] array;
			int ınt32 = (int_0 == 0 ? 10 : 11);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * ınt32; i < list_0.Count; i++)
				{
					this.method_1(list_0[i], syncServerPacket);
					int int1 = int_1 + 1;
					int_1 = int1;
					if (int1 == 10)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private void method_1(RoomModel roomModel_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(roomModel_0.RoomId);
			syncServerPacket_0.WriteU(roomModel_0.Name, 46);
			syncServerPacket_0.WriteC((byte)roomModel_0.MapId);
			syncServerPacket_0.WriteC((byte)roomModel_0.Rule);
			syncServerPacket_0.WriteC((byte)roomModel_0.Stage);
			syncServerPacket_0.WriteC((byte)roomModel_0.RoomType);
			syncServerPacket_0.WriteC((byte)roomModel_0.State);
			syncServerPacket_0.WriteC((byte)roomModel_0.GetCountPlayers());
			syncServerPacket_0.WriteC((byte)roomModel_0.GetSlotCount());
			syncServerPacket_0.WriteC((byte)roomModel_0.Ping);
			syncServerPacket_0.WriteH((ushort)roomModel_0.WeaponsFlag);
			syncServerPacket_0.WriteD(roomModel_0.GetFlag());
			syncServerPacket_0.WriteH(0);
			syncServerPacket_0.WriteB(roomModel_0.LeaderAddr);
			syncServerPacket_0.WriteC(0);
		}

		private byte[] method_2(int int_0, ref int int_1, List<Account> list_0)
		{
			byte[] array;
			int ınt32 = (int_0 == 0 ? 8 : 9);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * ınt32; i < list_0.Count; i++)
				{
					this.method_3(list_0[i], syncServerPacket);
					int int1 = int_1 + 1;
					int_1 = int1;
					if (int1 == 8)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private void method_3(Account account_0, SyncServerPacket syncServerPacket_0)
		{
			ClanModel clan = ClanManager.GetClan(account_0.ClanId);
			syncServerPacket_0.WriteD(account_0.GetSessionId());
			syncServerPacket_0.WriteD(clan.Logo);
			syncServerPacket_0.WriteC((byte)clan.Effect);
			syncServerPacket_0.WriteU(clan.Name, 34);
			syncServerPacket_0.WriteH((short)account_0.GetRank());
			syncServerPacket_0.WriteU(account_0.Nickname, 66);
			syncServerPacket_0.WriteC((byte)account_0.NickColor);
			syncServerPacket_0.WriteC((byte)this.NATIONS);
			syncServerPacket_0.WriteD(account_0.Equipment.NameCardId);
			syncServerPacket_0.WriteC((byte)account_0.Bonus.NickBorderColor);
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ChannelModel channel = player.GetChannel();
					if (channel != null)
					{
						channel.RemoveEmptyRooms();
						List<RoomModel> rooms = channel.Rooms;
						List<Account> waitPlayers = channel.GetWaitPlayers();
						int ınt32 = (int)Math.Ceiling((double)rooms.Count / 10);
						int ınt321 = (int)Math.Ceiling((double)waitPlayers.Count / 8);
						if (player.LastRoomPage >= ınt32)
						{
							player.LastRoomPage = 0;
						}
						if (player.LastPlayerPage >= ınt321)
						{
							player.LastPlayerPage = 0;
						}
						int ınt322 = 0;
						int ınt323 = 0;
						byte[] numArray = this.method_0(player.LastRoomPage, ref ınt322, rooms);
						byte[] numArray1 = this.method_2(player.LastPlayerPage, ref ınt323, waitPlayers);
						GameClient client = this.Client;
						int count = rooms.Count;
						int count1 = waitPlayers.Count;
						Account account = player;
						int lastRoomPage = account.LastRoomPage;
						account.LastRoomPage = lastRoomPage + 1;
						int ınt324 = lastRoomPage;
						Account account1 = player;
						lastRoomPage = account1.LastPlayerPage;
						account1.LastPlayerPage = lastRoomPage + 1;
						client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(count, count1, ınt324, lastRoomPage, ınt322, ınt323, numArray, numArray1));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_GET_ROOMLIST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}