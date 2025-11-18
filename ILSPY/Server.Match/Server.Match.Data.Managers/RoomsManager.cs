using System.Collections.Generic;
using Plugin.Core.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;

namespace Server.Match.Data.Managers;

public static class RoomsManager
{
	private static readonly List<RoomModel> list_0 = new List<RoomModel>();

	public static RoomModel CreateOrGetRoom(uint UniqueRoomId, uint Seed)
	{
		lock (list_0)
		{
			foreach (RoomModel item in list_0)
			{
				if (item.UniqueRoomId == UniqueRoomId && item.RoomSeed == Seed)
				{
					return item;
				}
			}
			int roomInfo = AllUtils.GetRoomInfo(UniqueRoomId, 2);
			int roomInfo2 = AllUtils.GetRoomInfo(UniqueRoomId, 1);
			int roomInfo3 = AllUtils.GetRoomInfo(UniqueRoomId, 0);
			RoomModel roomModel = new RoomModel(roomInfo)
			{
				UniqueRoomId = UniqueRoomId,
				RoomSeed = Seed,
				RoomId = roomInfo3,
				ChannelId = roomInfo2,
				MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2),
				RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0),
				Rule = (MapRules)AllUtils.GetSeedInfo(Seed, 1)
			};
			list_0.Add(roomModel);
			return roomModel;
		}
	}

	public static RoomModel GetRoom(uint UniqueRoomId, uint Seed)
	{
		lock (list_0)
		{
			foreach (RoomModel item in list_0)
			{
				if (item != null && item.UniqueRoomId == UniqueRoomId && item.RoomSeed == Seed)
				{
					return item;
				}
			}
			return null;
		}
	}

	public static void RemoveRoom(uint UniqueRoomId, uint Seed)
	{
		lock (list_0)
		{
			int num = 0;
			while (true)
			{
				if (num < list_0.Count)
				{
					RoomModel roomModel = list_0[num];
					if (roomModel.UniqueRoomId == UniqueRoomId && roomModel.RoomSeed == Seed)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			list_0.RemoveAt(num);
		}
	}
}
