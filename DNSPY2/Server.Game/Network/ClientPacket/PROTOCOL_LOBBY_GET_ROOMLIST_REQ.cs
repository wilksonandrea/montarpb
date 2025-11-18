using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B8 RID: 440
	public class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : GameClientPacket
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00023CBC File Offset: 0x00021EBC
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
						int num = (int)Math.Ceiling((double)rooms.Count / 10.0);
						int num2 = (int)Math.Ceiling((double)waitPlayers.Count / 8.0);
						if (player.LastRoomPage >= num)
						{
							player.LastRoomPage = 0;
						}
						if (player.LastPlayerPage >= num2)
						{
							player.LastPlayerPage = 0;
						}
						int num3 = 0;
						int num4 = 0;
						byte[] array = this.method_0(player.LastRoomPage, ref num3, rooms);
						byte[] array2 = this.method_2(player.LastPlayerPage, ref num4, waitPlayers);
						GameClient client = this.Client;
						int count = rooms.Count;
						int count2 = waitPlayers.Count;
						Account account = player;
						int num5 = account.LastRoomPage;
						account.LastRoomPage = num5 + 1;
						int num6 = num5;
						Account account2 = player;
						num5 = account2.LastPlayerPage;
						account2.LastPlayerPage = num5 + 1;
						client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(count, count2, num6, num5, num3, num4, array, array2));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_GET_ROOMLIST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00023DF8 File Offset: 0x00021FF8
		private byte[] method_0(int int_0, ref int int_1, List<RoomModel> list_0)
		{
			int num = ((int_0 == 0) ? 10 : 11);
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * num; i < list_0.Count; i++)
				{
					this.method_1(list_0[i], syncServerPacket);
					int num2 = int_1 + 1;
					int_1 = num2;
					if (num2 == 10)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00023E6C File Offset: 0x0002206C
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x00023F2C File Offset: 0x0002212C
		private byte[] method_2(int int_0, ref int int_1, List<Account> list_0)
		{
			int num = ((int_0 == 0) ? 8 : 9);
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * num; i < list_0.Count; i++)
				{
					this.method_3(list_0[i], syncServerPacket);
					int num2 = int_1 + 1;
					int_1 = num2;
					if (num2 == 8)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00023F9C File Offset: 0x0002219C
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_LOBBY_GET_ROOMLIST_REQ()
		{
		}
	}
}
