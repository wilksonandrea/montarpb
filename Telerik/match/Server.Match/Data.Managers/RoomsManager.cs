using Plugin.Core.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;
using System;
using System.Collections.Generic;

namespace Server.Match.Data.Managers
{
	public static class RoomsManager
	{
		private readonly static List<RoomModel> list_0;

		static RoomsManager()
		{
			RoomsManager.list_0 = new List<RoomModel>();
		}

		public static RoomModel CreateOrGetRoom(uint UniqueRoomId, uint Seed)
		{
			RoomModel roomModel;
			lock (RoomsManager.list_0)
			{
				foreach (RoomModel list0 in RoomsManager.list_0)
				{
					if (list0.UniqueRoomId != UniqueRoomId || list0.RoomSeed != Seed)
					{
						continue;
					}
					roomModel = list0;
					return roomModel;
				}
				int roomInfo = AllUtils.GetRoomInfo(UniqueRoomId, 2);
				int ınt32 = AllUtils.GetRoomInfo(UniqueRoomId, 1);
				int roomInfo1 = AllUtils.GetRoomInfo(UniqueRoomId, 0);
				RoomModel roomModel1 = new RoomModel(roomInfo)
				{
					UniqueRoomId = UniqueRoomId,
					RoomSeed = Seed,
					RoomId = roomInfo1,
					ChannelId = ınt32,
					MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2),
					RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0),
					Rule = (MapRules)((byte)AllUtils.GetSeedInfo(Seed, 1))
				};
				RoomsManager.list_0.Add(roomModel1);
				roomModel = roomModel1;
			}
			return roomModel;
		}

		public static RoomModel GetRoom(uint UniqueRoomId, uint Seed)
		{
			RoomModel roomModel;
			lock (RoomsManager.list_0)
			{
				foreach (RoomModel list0 in RoomsManager.list_0)
				{
					if (list0 == null || list0.UniqueRoomId != UniqueRoomId || list0.RoomSeed != Seed)
					{
						continue;
					}
					roomModel = list0;
					return roomModel;
				}
				roomModel = null;
			}
			return roomModel;
		}

		public static void RemoveRoom(uint UniqueRoomId, uint Seed)
		{
			lock (RoomsManager.list_0)
			{
				int ınt32 = 0;
				while (ınt32 < RoomsManager.list_0.Count)
				{
					RoomModel ıtem = RoomsManager.list_0[ınt32];
					if (ıtem.UniqueRoomId != UniqueRoomId || ıtem.RoomSeed != Seed)
					{
						ınt32++;
					}
					else
					{
						RoomsManager.list_0.RemoveAt(ınt32);
						return;
					}
				}
			}
		}
	}
}