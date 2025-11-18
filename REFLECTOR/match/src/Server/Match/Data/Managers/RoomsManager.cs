namespace Server.Match.Data.Managers
{
    using Plugin.Core.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Utils;
    using System;
    using System.Collections.Generic;

    public static class RoomsManager
    {
        private static readonly List<RoomModel> list_0 = new List<RoomModel>();

        public static RoomModel CreateOrGetRoom(uint UniqueRoomId, uint Seed)
        {
            List<RoomModel> list = list_0;
            lock (list)
            {
                using (List<RoomModel>.Enumerator enumerator = list_0.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        RoomModel current = enumerator.Current;
                        if ((current.UniqueRoomId == UniqueRoomId) && (current.RoomSeed == Seed))
                        {
                            return current;
                        }
                    }
                }
                int roomInfo = AllUtils.GetRoomInfo(UniqueRoomId, 1);
                int num2 = AllUtils.GetRoomInfo(UniqueRoomId, 0);
                RoomModel model1 = new RoomModel(AllUtils.GetRoomInfo(UniqueRoomId, 2));
                model1.UniqueRoomId = UniqueRoomId;
                model1.RoomSeed = Seed;
                model1.RoomId = num2;
                model1.ChannelId = roomInfo;
                model1.MapId = (MapIdEnum) AllUtils.GetSeedInfo(Seed, 2);
                model1.RoomType = (RoomCondition) AllUtils.GetSeedInfo(Seed, 0);
                model1.Rule = (MapRules) ((byte) AllUtils.GetSeedInfo(Seed, 1));
                RoomModel item = model1;
                list_0.Add(item);
                return item;
            }
        }

        public static RoomModel GetRoom(uint UniqueRoomId, uint Seed)
        {
            List<RoomModel> list = list_0;
            lock (list)
            {
                using (List<RoomModel>.Enumerator enumerator = list_0.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        RoomModel current = enumerator.Current;
                        if ((current != null) && ((current.UniqueRoomId == UniqueRoomId) && (current.RoomSeed == Seed)))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void RemoveRoom(uint UniqueRoomId, uint Seed)
        {
            List<RoomModel> list = list_0;
            lock (list)
            {
                for (int i = 0; i < list_0.Count; i++)
                {
                    RoomModel model = list_0[i];
                    if ((model.UniqueRoomId == UniqueRoomId) && (model.RoomSeed == Seed))
                    {
                        list_0.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}

