using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Utility
{
	public static class ComDiv
	{
		public static byte[] AddressBytes(string Host)
		{
			return IPAddress.Parse(Host).GetAddressBytes();
		}

		public static string AspectRatio(int X, int Y)
		{
			return string.Format("{0}:{1}", X / ComDiv.smethod_3(X, Y), Y / ComDiv.smethod_3(X, Y));
		}

		public static int CheckEquipedItems(PlayerEquipment Equip, List<ItemsModel> Inventory, bool BattleRules)
		{
			bool flag;
			int ınt32 = 0;
			ValueTuple<bool, bool, bool, bool, bool> valueTuple = new ValueTuple<bool, bool, bool, bool, bool>(false, false, false, false, false);
			ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> ıtem2 = new ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>(false, false, false, false, false, false, false, new ValueTuple<bool, bool, bool, bool, bool>(false, false, false, false, false));
			ValueTuple<bool, bool, bool> ıtem3 = new ValueTuple<bool, bool, bool>(false, false, false);
			if (Equip.BeretItem == 0)
			{
				ıtem2.Rest.Item4 = 1;
			}
			if (Equip.AccessoryId == 0)
			{
				ıtem3.Item1 = 1;
			}
			if (Equip.SprayId == 0)
			{
				ıtem3.Item2 = 1;
			}
			if (Equip.NameCardId == 0)
			{
				ıtem3.Item3 = 1;
			}
			if (Equip.WeaponPrimary == 103004)
			{
				valueTuple.Item1 = 1;
			}
			if (BattleRules)
			{
				if (valueTuple.Item1 == null && (Equip.WeaponPrimary == 105025 || Equip.WeaponPrimary == 106007))
				{
					valueTuple.Item1 = 1;
				}
				if (valueTuple.Item3 == null && Equip.WeaponMelee == 323001)
				{
					valueTuple.Item3 = 1;
				}
			}
			ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>> valueTuple1 = ComDiv.smethod_0(Equip, Inventory, valueTuple, ıtem2, ıtem3);
			valueTuple = valueTuple1.Item1;
			ıtem2 = valueTuple1.Item2;
			ıtem3 = valueTuple1.Item3;
			bool flag1 = (valueTuple.Item1 == null || valueTuple.Item2 == null || valueTuple.Item3 == null || valueTuple.Item4 == null ? true : valueTuple.Item5 == 0);
			flag = (ıtem2.Item1 == null || ıtem2.Item2 == null || ıtem2.Item3 == null || ıtem2.Item4 == null || ıtem2.Item5 == null || ıtem2.Item6 == null || ıtem2.Item7 == null || ıtem2.Rest.Item1 == null || ıtem2.Rest.Item2 == null || ıtem2.Rest.Item3 == null || ıtem2.Rest.Item4 == null ? true : ıtem2.Rest.Item5 == 0);
			bool flag2 = (ıtem3.Item1 == null || ıtem3.Item2 == null ? true : ıtem3.Item3 == 0);
			if (flag1)
			{
				ınt32 += 2;
			}
			if (flag)
			{
				ınt32++;
			}
			if (flag2)
			{
				ınt32 += 3;
			}
			ComDiv.smethod_1(Equip, ref valueTuple, ref ıtem2, ref ıtem3);
			return ınt32;
		}

		public static int CountDB(string CommandArgument)
		{
			int ınt32 = 0;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand commandArgument = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					commandArgument.CommandText = CommandArgument;
					ınt32 = Convert.ToInt32(commandArgument.ExecuteScalar());
					commandArgument.Dispose();
					npgsqlConnection.Dispose();
					npgsqlConnection.Close();
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[QuerySQL.CountDB] ", exception.Message), LoggerType.Error, exception);
			}
			return ınt32;
		}

		public static int CreateItemId(int ItemClass, int ClassType, int Number)
		{
			return ItemClass * 100000 + ClassType * 1000 + Number;
		}

		public static bool DeleteDB(string TABEL, string Req1, object ValueReq1)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
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

		public static bool DeleteDB(string TABEL, string Req1, object ValueReq1, string Req2, object ValueReq2)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						if (Req1 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
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

		public static bool DeleteDB(string TABEL, string Req1, object[] ValueReq1, string Req2, object ValueReq2)
		{
			bool flag;
			if (ValueReq1.Length == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						string str = "";
						List<string> strs = new List<string>();
						for (int i = 0; i < (int)ValueReq1.Length; i++)
						{
							object valueReq1 = ValueReq1[i];
							string str1 = string.Concat("@Value", i.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, valueReq1);
							strs.Add(str1);
						}
						str = string.Join(",", strs.ToArray());
						npgsqlCommand.get_Parameters().AddWithValue("@Req2", ValueReq2);
						npgsqlCommand.CommandText = string.Concat(new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, " in (", str, ") AND ", Req2, "=@Req2" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Dispose();
						npgsqlConnection.Close();
					}
				}
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

		public static uint GenStockId(int ItemId)
		{
			return BitConverter.ToUInt32(ComDiv.smethod_2(ItemId), 0);
		}

		public static ulong GetClanStatus(AccountStatus status, bool isOnline)
		{
			FriendState friendState;
			int ınt32;
			int ınt321;
			int ınt322;
			int ınt323;
			ComDiv.GetPlayerLocation(status, isOnline, out friendState, out ınt32, out ınt321, out ınt322, out ınt323);
			return ComDiv.GetPlayerStatus(ınt323, ınt32, ınt321, ınt322, (int)friendState);
		}

		public static ulong GetClanStatus(FriendState state)
		{
			return ComDiv.GetPlayerStatus(0, 0, 0, 0, (int)state);
		}

		public static double GetDuration(DateTime Date)
		{
			return (double)(DateTimeUtil.Now() - Date).TotalSeconds;
		}

		public static uint GetFriendStatus(FriendModel f)
		{
			PlayerInfo ınfo = f.Info;
			if (ınfo == null)
			{
				return (uint)0;
			}
			FriendState state = FriendState.None;
			int ınt32 = 0;
			int ınt321 = 0;
			int ınt322 = 0;
			if (f.Removed)
			{
				state = FriendState.Offline;
			}
			else if (f.State <= 0)
			{
				ComDiv.GetPlayerLocation(ınfo.Status, ınfo.IsOnline, out state, out ınt322, out ınt321, out ınt32);
			}
			else
			{
				state = (FriendState)f.State;
			}
			return ComDiv.GetPlayerStatus(ınt322, ınt321, ınt32, (int)state);
		}

		public static uint GetFriendStatus(FriendModel f, FriendState stateN)
		{
			PlayerInfo ınfo = f.Info;
			if (ınfo == null)
			{
				return (uint)0;
			}
			FriendState state = stateN;
			int ınt32 = 0;
			int ınt321 = 0;
			int ınt322 = 0;
			if (f.Removed)
			{
				state = FriendState.Offline;
			}
			else if (f.State > 0)
			{
				state = (FriendState)f.State;
			}
			else if (stateN == FriendState.None)
			{
				ComDiv.GetPlayerLocation(ınfo.Status, ınfo.IsOnline, out state, out ınt322, out ınt321, out ınt32);
			}
			return ComDiv.GetPlayerStatus(ınt322, ınt321, ınt32, (int)state);
		}

		public static int GetIdStatics(int Id, int Type)
		{
			switch (Type)
			{
				case 1:
				{
					return Id / 100000;
				}
				case 2:
				{
					return Id % 100000 / 1000;
				}
				case 3:
				{
					return Id % 1000;
				}
				case 4:
				{
					return Id % 10000000 / 100000;
				}
				case 5:
				{
					return Id / 1000;
				}
				default:
				{
					return 0;
				}
			}
		}

		public static ItemCategory GetItemCategory(int ItemId)
		{
			int ıdStatics = ComDiv.GetIdStatics(ItemId, 1);
			int ınt32 = ComDiv.GetIdStatics(ItemId, 4);
			if (ıdStatics >= 1 && ıdStatics <= 5)
			{
				return ItemCategory.Weapon;
			}
			if (ıdStatics >= 6 && ıdStatics <= 14 || ıdStatics == 27 || ınt32 >= 7 && ınt32 <= 14)
			{
				return ItemCategory.Character;
			}
			if (ıdStatics >= 16 && ıdStatics <= 20 || ıdStatics == 22 || ıdStatics == 26 || ıdStatics >= 28 && ıdStatics <= 29 || ıdStatics >= 36 && ıdStatics <= 40)
			{
				return ItemCategory.Coupon;
			}
			if (ıdStatics == 15 || ıdStatics >= 30 && ıdStatics <= 35)
			{
				return ItemCategory.NewItem;
			}
			CLogger.Print(string.Format("Invalid Category [{0}]: {1}", ıdStatics, ItemId), LoggerType.Warning, null);
			return ItemCategory.None;
		}

		public static ushort GetMissionCardFlags(int missionId, int cardIdx, byte[] arrayList)
		{
			if (missionId == 0)
			{
				return (ushort)0;
			}
			int flag = 0;
			foreach (MissionCardModel card in MissionCardRAW.GetCards(missionId, cardIdx))
			{
				if (arrayList[card.ArrayIdx] < card.MissionLimit)
				{
					continue;
				}
				flag |= card.Flag;
			}
			return (ushort)flag;
		}

		public static byte[] GetMissionCardFlags(int missionId, byte[] arrayList)
		{
			byte[] array;
			if (missionId == 0)
			{
				return new byte[20];
			}
			List<MissionCardModel> cards = MissionCardRAW.GetCards(missionId);
			if (cards.Count == 0)
			{
				return new byte[20];
			}
			using (SyncServerPacket syncServerPacket = new SyncServerPacket(20L))
			{
				int flag = 0;
				for (int i = 0; i < 10; i++)
				{
					foreach (MissionCardModel card in MissionCardRAW.GetCards(cards, i))
					{
						if (arrayList[card.ArrayIdx] < card.MissionLimit)
						{
							continue;
						}
						flag |= card.Flag;
					}
					syncServerPacket.WriteH((ushort)flag);
					flag = 0;
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId)
		{
			roomId = 0;
			channelId = 0;
			serverId = 0;
			if (!isOnline)
			{
				state = FriendState.Offline;
			}
			else
			{
				if (status.RoomId != 255)
				{
					roomId = status.RoomId;
					channelId = status.ChannelId;
					state = FriendState.Room;
				}
				else if (status.RoomId == 255 && status.ChannelId != 255)
				{
					channelId = status.ChannelId;
					state = FriendState.Lobby;
				}
				else if (status.RoomId != 255 || status.ChannelId != 255)
				{
					state = FriendState.Offline;
				}
				else
				{
					state = FriendState.Online;
				}
				if (status.ServerId != 255)
				{
					serverId = status.ServerId;
					return;
				}
			}
		}

		public static void GetPlayerLocation(AccountStatus status, bool isOnline, out FriendState state, out int roomId, out int channelId, out int serverId, out int clanFId)
		{
			roomId = 0;
			channelId = 0;
			serverId = 0;
			clanFId = 0;
			if (!isOnline)
			{
				state = FriendState.Offline;
			}
			else
			{
				if (status.RoomId != 255)
				{
					roomId = status.RoomId;
					channelId = status.ChannelId;
					state = FriendState.Room;
				}
				else if ((status.ClanMatchId != 255 || status.RoomId == 255) && status.ChannelId != 255)
				{
					channelId = status.ChannelId;
					state = FriendState.Lobby;
				}
				else if (status.RoomId != 255 || status.ChannelId != 255)
				{
					state = FriendState.Offline;
				}
				else
				{
					state = FriendState.Online;
				}
				if (status.ServerId != 255)
				{
					serverId = status.ServerId;
				}
				if (status.ClanMatchId != 255)
				{
					clanFId = status.ClanMatchId + 1;
					return;
				}
			}
		}

		public static uint GetPlayerStatus(AccountStatus status, bool isOnline)
		{
			FriendState friendState;
			int ınt32;
			int ınt321;
			int ınt322;
			ComDiv.GetPlayerLocation(status, isOnline, out friendState, out ınt32, out ınt321, out ınt322);
			return ComDiv.GetPlayerStatus(ınt32, ınt321, ınt322, (int)friendState);
		}

		public static uint GetPlayerStatus(int roomId, int channelId, int serverId, int stateId)
		{
			int ınt32 = (serverId & 255) << 20;
			int ınt321 = (channelId & 255) << 12;
			int ınt322 = roomId & 4095;
			return (uint)((stateId & 255) << 28 | ınt32 | ınt321 | ınt322);
		}

		public static ulong GetPlayerStatus(int clanFId, int roomId, int channelId, int serverId, int stateId)
		{
			long ınt64 = (long)((stateId & 255) << 28);
			long ınt641 = (long)((serverId & 255) << 20);
			long ınt642 = (long)((channelId & 255) << 12);
			long ınt643 = (long)(roomId & 4095);
			return (ulong)(((long)clanFId & 4294967295L) << 32 | ınt64 | ınt641 | ınt642 | ınt643);
		}

		public static T NextOf<T>(IList<T> List, T Item)
		{
			int ınt32 = List.IndexOf(Item);
			return List[(ınt32 == List.Count - 1 ? 0 : ınt32 + 1)];
		}

		public static T ParseEnum<T>(string Value)
		{
			return (T)Enum.Parse(typeof(T), Value, true);
		}

		public static int Percentage(int Total, int Percent)
		{
			return Total * Percent / 100;
		}

		public static float Percentage(float Total, int Percent)
		{
			return Total * (float)Percent / 100f;
		}

		private static ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>> smethod_0(PlayerEquipment playerEquipment_0, List<ItemsModel> list_0, ValueTuple<bool, bool, bool, bool, bool> valueTuple_0, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> valueTuple_1, ValueTuple<bool, bool, bool> valueTuple_2)
		{
			lock (list_0)
			{
				HashSet<int> ınt32s = new HashSet<int>(
					from itemsModel_0 in list_0
					where itemsModel_0.Count != 0
					select itemsModel_0.Id);
				if (ınt32s.Contains(playerEquipment_0.WeaponPrimary))
				{
					valueTuple_0.Item1 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.WeaponSecondary))
				{
					valueTuple_0.Item2 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.WeaponMelee))
				{
					valueTuple_0.Item3 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.WeaponExplosive))
				{
					valueTuple_0.Item4 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.WeaponSpecial))
				{
					valueTuple_0.Item5 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.CharaRedId))
				{
					valueTuple_1.Item1 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.CharaBlueId))
				{
					valueTuple_1.Item2 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartHead))
				{
					valueTuple_1.Item3 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartFace))
				{
					valueTuple_1.Item4 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartJacket))
				{
					valueTuple_1.Item5 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartPocket))
				{
					valueTuple_1.Item6 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartGlove))
				{
					valueTuple_1.Item7 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartBelt))
				{
					valueTuple_1.Rest.Item1 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartHolster))
				{
					valueTuple_1.Rest.Item2 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.PartSkin))
				{
					valueTuple_1.Rest.Item3 = 1;
				}
				if (playerEquipment_0.BeretItem != 0 && ınt32s.Contains(playerEquipment_0.BeretItem))
				{
					valueTuple_1.Rest.Item4 = 1;
				}
				if (ınt32s.Contains(playerEquipment_0.DinoItem))
				{
					valueTuple_1.Rest.Item5 = 1;
				}
				if (playerEquipment_0.AccessoryId != 0 && ınt32s.Contains(playerEquipment_0.AccessoryId))
				{
					valueTuple_2.Item1 = 1;
				}
				if (playerEquipment_0.SprayId != 0 && ınt32s.Contains(playerEquipment_0.SprayId))
				{
					valueTuple_2.Item2 = 1;
				}
				if (playerEquipment_0.NameCardId != 0 && ınt32s.Contains(playerEquipment_0.NameCardId))
				{
					valueTuple_2.Item3 = 1;
				}
			}
			return new ValueTuple<ValueTuple<bool, bool, bool, bool, bool>, ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>>, ValueTuple<bool, bool, bool>>(valueTuple_0, valueTuple_1, valueTuple_2);
		}

		private static void smethod_1(PlayerEquipment playerEquipment_0, ref ValueTuple<bool, bool, bool, bool, bool> valueTuple_0, ref ValueTuple<bool, bool, bool, bool, bool, bool, bool, ValueTuple<bool, bool, bool, bool, bool>> valueTuple_1, ref ValueTuple<bool, bool, bool> valueTuple_2)
		{
			if (valueTuple_0.Item1 == null)
			{
				playerEquipment_0.WeaponPrimary = 103004;
			}
			if (valueTuple_0.Item2 == null)
			{
				playerEquipment_0.WeaponSecondary = 202003;
			}
			if (valueTuple_0.Item3 == null)
			{
				playerEquipment_0.WeaponMelee = 301001;
			}
			if (valueTuple_0.Item4 == null)
			{
				playerEquipment_0.WeaponExplosive = 407001;
			}
			if (valueTuple_0.Item5 == null)
			{
				playerEquipment_0.WeaponSpecial = 508001;
			}
			if (valueTuple_1.Item1 == null)
			{
				playerEquipment_0.CharaRedId = 601001;
			}
			if (valueTuple_1.Item2 == null)
			{
				playerEquipment_0.CharaBlueId = 602002;
			}
			if (valueTuple_1.Item3 == null)
			{
				playerEquipment_0.PartHead = 1000700000;
			}
			if (valueTuple_1.Item4 == null)
			{
				playerEquipment_0.PartFace = 1000800000;
			}
			if (valueTuple_1.Item5 == null)
			{
				playerEquipment_0.PartJacket = 1000900000;
			}
			if (valueTuple_1.Item6 == null)
			{
				playerEquipment_0.PartPocket = 1001000000;
			}
			if (valueTuple_1.Item7 == null)
			{
				playerEquipment_0.PartGlove = 1001100000;
			}
			if (valueTuple_1.Rest.Item1 == null)
			{
				playerEquipment_0.PartBelt = 1001200000;
			}
			if (valueTuple_1.Rest.Item2 == null)
			{
				playerEquipment_0.PartHolster = 1001300000;
			}
			if (valueTuple_1.Rest.Item3 == null)
			{
				playerEquipment_0.PartSkin = 1001400000;
			}
			if (valueTuple_1.Rest.Item4 == null)
			{
				playerEquipment_0.BeretItem = 0;
			}
			if (valueTuple_1.Rest.Item5 == null)
			{
				playerEquipment_0.DinoItem = 1500511;
			}
			if (valueTuple_2.Item1 == null)
			{
				playerEquipment_0.AccessoryId = 0;
			}
			if (valueTuple_2.Item2 == null)
			{
				playerEquipment_0.SprayId = 0;
			}
			if (valueTuple_2.Item3 == null)
			{
				playerEquipment_0.NameCardId = 0;
			}
			if (playerEquipment_0.PartHead == 1000700000 && playerEquipment_0.PartFace != 1000800000)
			{
				playerEquipment_0.PartHead = 0;
			}
		}

		private static byte[] smethod_2(int int_0)
		{
			byte[] bytes = BitConverter.GetBytes(int_0);
			bytes[3] = 64;
			return bytes;
		}

		private static int smethod_3(int int_0, int int_1)
		{
			while (int_1 != 0)
			{
				int int0 = int_0 % int_1;
				int_0 = int_1;
				int_1 = int0;
			}
			return int_0;
		}

		public static string[] SplitObjects(string Input, string Delimiter)
		{
			return Input.Split(new string[] { Delimiter }, StringSplitOptions.None);
		}

		public static char[] SubArray(this char[] Input, int StartIndex, int Length)
		{
			List<char> chrs = new List<char>();
			for (int i = StartIndex; i < Length; i++)
			{
				chrs.Add(Input[i]);
			}
			return chrs.ToArray();
		}

		public static string ToTitleCase(string Text)
		{
			string titleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text.Split(new char[] { ' ' })[0].ToLower());
			Text = Text.Replace(Text.Split(new char[] { ' ' })[0], titleCase);
			return Text;
		}

		public static void TryCreateItem(ItemsModel Model, PlayerInventory Inventory, long OwnerId)
		{
			DateTime dateTime;
			try
			{
				ItemsModel ıtem = Inventory.GetItem(Model.Id);
				if (ıtem != null)
				{
					Model.ObjectId = ıtem.ObjectId;
					if (ıtem.Equip == ItemEquipType.Durable)
					{
						if (!ShopManager.IsRepairableItem(Model.Id))
						{
							ItemsModel model = Model;
							model.Count = model.Count + ıtem.Count;
							ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
						}
						else
						{
							Model.Count = 100;
							ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
						}
					}
					else if (ıtem.Equip == ItemEquipType.Temporary)
					{
						uint count = ıtem.Count;
						DateTime dateTime1 = DateTime.ParseExact(count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
						if (Model.Category == ItemCategory.Coupon)
						{
							count = Model.Count;
							TimeSpan timeSpan = DateTime.ParseExact(count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture) - DateTimeUtil.Now();
							Model.Equip = ItemEquipType.Temporary;
							dateTime = dateTime1.AddDays(timeSpan.TotalDays);
							Model.Count = Convert.ToUInt32(dateTime.ToString("yyMMddHHmm"));
						}
						else
						{
							Model.Equip = ItemEquipType.Temporary;
							dateTime = dateTime1.AddSeconds((double)((float)Model.Count));
							Model.Count = Convert.ToUInt32(dateTime.ToString("yyMMddHHmm"));
						}
						ComDiv.UpdateDB("player_items", "count", (long)((ulong)Model.Count), "owner_id", OwnerId, "id", Model.Id);
					}
					ıtem.Equip = Model.Equip;
					ıtem.Count = Model.Count;
				}
				else if (DaoManagerSQL.CreatePlayerInventoryItem(Model, OwnerId))
				{
					Inventory.AddItem(Model);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void UpdateChars(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("chara_red_side", Equip.CharaRedId);
			Query.AddQuery("chara_blue_side", Equip.CharaBlueId);
			Query.AddQuery("part_head", Equip.PartHead);
			Query.AddQuery("part_face", Equip.PartFace);
			Query.AddQuery("part_jacket", Equip.PartJacket);
			Query.AddQuery("part_pocket", Equip.PartPocket);
			Query.AddQuery("part_glove", Equip.PartGlove);
			Query.AddQuery("part_belt", Equip.PartBelt);
			Query.AddQuery("part_holster", Equip.PartHolster);
			Query.AddQuery("part_skin", Equip.PartSkin);
			Query.AddQuery("beret_item_part", Equip.BeretItem);
			Query.AddQuery("dino_item_chara", Equip.DinoItem);
		}

		public static bool UpdateDB(string TABEL, string[] COLUMNS, params object[] VALUES)
		{
			bool flag;
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && (int)COLUMNS.Length != (int)VALUES.Length)
			{
				CLogger.Print(string.Concat("[Update Database] Wrong values: ", string.Join(",", COLUMNS), "/", string.Join(",", VALUES)), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length == 0 || VALUES.Length == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						string str = "";
						List<string> strs = new List<string>();
						for (int i = 0; i < (int)VALUES.Length; i++)
						{
							object vALUES = VALUES[i];
							string cOLUMNS = COLUMNS[i];
							string str1 = string.Concat("@Value", i.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, vALUES);
							strs.Add(string.Concat(cOLUMNS, "=", str1));
						}
						str = string.Join(",", strs.ToArray());
						npgsqlCommand.CommandText = string.Concat("UPDATE ", TABEL, " SET ", str);
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB1] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string[] COLUMNS, params object[] VALUES)
		{
			bool flag;
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && (int)COLUMNS.Length != (int)VALUES.Length)
			{
				CLogger.Print(string.Concat("[Update Database] Wrong values: ", string.Join(",", COLUMNS), "/", string.Join(",", VALUES)), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length == 0 || VALUES.Length == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						string str = "";
						List<string> strs = new List<string>();
						for (int i = 0; i < (int)VALUES.Length; i++)
						{
							object vALUES = VALUES[i];
							string cOLUMNS = COLUMNS[i];
							string str1 = string.Concat("@Value", i.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, vALUES);
							strs.Add(string.Concat(cOLUMNS, "=", str1));
						}
						str = string.Join(",", strs.ToArray());
						npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, "=@Req1" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB2] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.get_Parameters().AddWithValue("@Value", VALUE);
						npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" });
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB3] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string Req2, object valueReq2, string[] COLUMNS, params object[] VALUES)
		{
			bool flag;
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && (int)COLUMNS.Length != (int)VALUES.Length)
			{
				CLogger.Print(string.Concat("[Update Database] Wrong values: ", string.Join(",", COLUMNS), "/", string.Join(",", VALUES)), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length == 0 || VALUES.Length == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						string str = "";
						List<string> strs = new List<string>();
						for (int i = 0; i < (int)VALUES.Length; i++)
						{
							object vALUES = VALUES[i];
							string cOLUMNS = COLUMNS[i];
							string str1 = string.Concat("@Value", i.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, vALUES);
							strs.Add(string.Concat(cOLUMNS, "=", str1));
						}
						str = string.Join(",", strs.ToArray());
						if (Req1 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req2", valueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, "=@Req1" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB4] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateDB(string TABEL, string Req1, int[] ValueReq1, string Req2, object ValueReq2, string[] COLUMNS, params object[] VALUES)
		{
			bool flag;
			if (COLUMNS.Length != 0 && VALUES.Length != 0 && (int)COLUMNS.Length != (int)VALUES.Length)
			{
				CLogger.Print(string.Concat("[updateDB5] Wrong values: ", string.Join(",", COLUMNS), "/", string.Join(",", VALUES)), LoggerType.Warning, null);
				return false;
			}
			if (COLUMNS.Length == 0 || VALUES.Length == 0)
			{
				return false;
			}
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						string str = "";
						List<string> strs = new List<string>();
						for (int i = 0; i < (int)VALUES.Length; i++)
						{
							object vALUES = VALUES[i];
							string cOLUMNS = COLUMNS[i];
							string str1 = string.Concat("@Value", i.ToString());
							npgsqlCommand.get_Parameters().AddWithValue(str1, vALUES);
							strs.Add(string.Concat(cOLUMNS, "=", str1));
						}
						str = string.Join(",", strs.ToArray());
						if (Req1 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, " = ANY (@Req1)" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, " = ANY (@Req1) AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB5] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1, string Req2, object ValueReq2)
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
					{
						npgsqlConnection.Open();
						npgsqlCommand.CommandType = CommandType.Text;
						npgsqlCommand.get_Parameters().AddWithValue("@Value", VALUE);
						if (Req1 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req1", ValueReq1);
						}
						if (Req2 != null)
						{
							npgsqlCommand.get_Parameters().AddWithValue("@Req2", ValueReq2);
						}
						if (Req1 != null && Req2 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" });
						}
						else if (Req2 != null && Req1 == null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req2, "=@Req2" });
						}
						else if (Req2 != null && Req1 != null)
						{
							npgsqlCommand.CommandText = string.Concat(new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" });
						}
						npgsqlCommand.ExecuteNonQuery();
						npgsqlCommand.Dispose();
						npgsqlConnection.Close();
					}
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("[AllUtils.UpdateDB6] ", exception.Message), LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static void UpdateItems(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("accesory_id", Equip.AccessoryId);
			Query.AddQuery("spray_id", Equip.SprayId);
			Query.AddQuery("namecard_id", Equip.NameCardId);
		}

		public static void UpdateWeapons(PlayerEquipment Equip, DBQuery Query)
		{
			Query.AddQuery("weapon_primary", Equip.WeaponPrimary);
			Query.AddQuery("weapon_secondary", Equip.WeaponSecondary);
			Query.AddQuery("weapon_melee", Equip.WeaponMelee);
			Query.AddQuery("weapon_explosive", Equip.WeaponExplosive);
			Query.AddQuery("weapon_special", Equip.WeaponSpecial);
		}

		public static bool ValidateAllPlayersAccount()
		{
			bool flag;
			try
			{
				using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
				{
					NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
					npgsqlConnection.Open();
					npgsqlCommand.CommandType = CommandType.Text;
					npgsqlCommand.CommandText = string.Format("UPDATE accounts SET online = {0} WHERE online = {1}", false, true);
					npgsqlCommand.ExecuteNonQuery();
					npgsqlCommand.Dispose();
					npgsqlConnection.Close();
				}
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

		public static uint ValidateStockId(int ItemId)
		{
			int ıdStatics = ComDiv.GetIdStatics(ItemId, 4);
			return ComDiv.GenStockId((ıdStatics < 7 || ıdStatics > 14 ? ItemId : ıdStatics));
		}

		public static uint Verificate(byte A, byte B, byte C, byte D)
		{
			byte[] a = new byte[] { A, B, C, D };
			if (!a.Any<byte>())
			{
				return (uint)0;
			}
			if (B < 60)
			{
				CLogger.Print(string.Format("Refresh Rate is below the minimum limit ({0})", B), LoggerType.Warning, null);
				return (uint)0;
			}
			if (C >= 0 && C <= 1)
			{
				return BitConverter.ToUInt32(a, 0);
			}
			CLogger.Print(string.Format("Unknown Window State ({0})", C), LoggerType.Warning, null);
			return (uint)0;
		}
	}
}