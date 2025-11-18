using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;

namespace Server.Match.Data.Managers
{
	// Token: 0x02000060 RID: 96
	public static class RoomsManager
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000B788 File Offset: 0x00009988
		public static RoomModel CreateOrGetRoom(uint UniqueRoomId, uint Seed)
		{
			List<RoomModel> list = RoomsManager.list_0;
			RoomModel roomModel3;
			lock (list)
			{
				foreach (RoomModel roomModel in RoomsManager.list_0)
				{
					if (roomModel.UniqueRoomId == UniqueRoomId && roomModel.RoomSeed == Seed)
					{
						return roomModel;
					}
				}
				int roomInfo = AllUtils.GetRoomInfo(UniqueRoomId, 2);
				int roomInfo2 = AllUtils.GetRoomInfo(UniqueRoomId, 1);
				int roomInfo3 = AllUtils.GetRoomInfo(UniqueRoomId, 0);
				RoomModel roomModel2 = new RoomModel(roomInfo)
				{
					UniqueRoomId = UniqueRoomId,
					RoomSeed = Seed,
					RoomId = roomInfo3,
					ChannelId = roomInfo2,
					MapId = (MapIdEnum)AllUtils.GetSeedInfo(Seed, 2),
					RoomType = (RoomCondition)AllUtils.GetSeedInfo(Seed, 0),
					Rule = (MapRules)AllUtils.GetSeedInfo(Seed, 1)
				};
				RoomsManager.list_0.Add(roomModel2);
				roomModel3 = roomModel2;
			}
			return roomModel3;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B894 File Offset: 0x00009A94
		public static RoomModel GetRoom(uint UniqueRoomId, uint Seed)
		{
			List<RoomModel> list = RoomsManager.list_0;
			RoomModel roomModel2;
			lock (list)
			{
				foreach (RoomModel roomModel in RoomsManager.list_0)
				{
					if (roomModel != null && roomModel.UniqueRoomId == UniqueRoomId && roomModel.RoomSeed == Seed)
					{
						return roomModel;
					}
				}
				roomModel2 = null;
			}
			return roomModel2;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B92C File Offset: 0x00009B2C
		public static void RemoveRoom(uint UniqueRoomId, uint Seed)
		{
			List<RoomModel> list = RoomsManager.list_0;
			lock (list)
			{
				for (int i = 0; i < RoomsManager.list_0.Count; i++)
				{
					RoomModel roomModel = RoomsManager.list_0[i];
					if (roomModel.UniqueRoomId == UniqueRoomId && roomModel.RoomSeed == Seed)
					{
						RoomsManager.list_0.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00002B08 File Offset: 0x00000D08
		// Note: this type is marked as 'beforefieldinit'.
		static RoomsManager()
		{
		}

		// Token: 0x04000151 RID: 337
		private static readonly List<RoomModel> list_0 = new List<RoomModel>();
	}
}
