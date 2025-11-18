namespace Plugin.Core.Utility
{
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
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ComDiv
    {
        public static byte[] AddressBytes(string Host) => 
            IPAddress.Parse(Host).GetAddressBytes();

        public static string AspectRatio(int X, int Y) => 
            $"{X / smethod_3(X, Y)}:{Y / smethod_3(X, Y)}";

        public static int CheckEquipedItems(PlayerEquipment Equip, List<ItemsModel> Inventory, bool BattleRules)
        {
            int num = 0;
            (bool, bool, bool, bool, bool) tuple = (false, false, false, false, false);
            (bool, bool, bool, bool, bool, bool, bool, (bool, bool, bool, bool, bool)) tuple2 = (false, false, false, false, false, false, false, (false, false, false, false, false));
            (bool, bool, bool) tuple3 = (false, false, false);
            if (Equip.BeretItem == 0)
            {
                tuple2.Rest.Item4 = true;
            }
            if (Equip.AccessoryId == 0)
            {
                tuple3.Item1 = true;
            }
            if (Equip.SprayId == 0)
            {
                tuple3.Item2 = true;
            }
            if (Equip.NameCardId == 0)
            {
                tuple3.Item3 = true;
            }
            if (Equip.WeaponPrimary == 0x1925c)
            {
                tuple.Item1 = true;
            }
            if (BattleRules)
            {
                if (!tuple.Item1 && ((Equip.WeaponPrimary == 0x19a41) || (Equip.WeaponPrimary == 0x19e17)))
                {
                    tuple.Item1 = true;
                }
                if (!tuple.Item3 && (Equip.WeaponMelee == 0x4edb9))
                {
                    tuple.Item3 = true;
                }
            }
            ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, (bool, bool, bool, bool, bool)), (bool, bool, bool)) tuple1 = smethod_0(Equip, Inventory, tuple, tuple2, tuple3);
            tuple = tuple1.Item1;
            tuple2 = tuple1.Item2;
            tuple3 = tuple1.Item3;
            bool flag = (!tuple.Item1 || (!tuple.Item2 || (!tuple.Item3 || !tuple.Item4))) ? true : !tuple.Item5;
            bool flag2 = (!tuple3.Item1 || !tuple3.Item2) ? true : !tuple3.Item3;
            if (flag)
            {
                num += 2;
            }
            if ((!tuple2.Item1 || (!tuple2.Item2 || (!tuple2.Item3 || (!tuple2.Item4 || (!tuple2.Item5 || (!tuple2.Item6 || (!tuple2.Item7 || (!tuple2.Rest.Item1 || (!tuple2.Rest.Item2 || (!tuple2.Rest.Item3 || !tuple2.Rest.Item4)))))))))) ? true : !tuple2.Rest.Item5)
            {
                num++;
            }
            if (flag2)
            {
                num += 3;
            }
            smethod_1(Equip, ref tuple, ref tuple2, ref tuple3);
            return num;
        }

        public static int CountDB(string CommandArgument)
        {
            int num = 0;
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandText = CommandArgument;
                    num = Convert.ToInt32(command1.ExecuteScalar());
                    command1.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("[QuerySQL.CountDB] " + exception.Message, LoggerType.Error, exception);
            }
            return num;
        }

        public static int CreateItemId(int ItemClass, int ClassType, int Number) => 
            ((ItemClass * 0x186a0) + (ClassType * 0x3e8)) + Number;

        public static bool DeleteDB(string TABEL, string Req1, object ValueReq1)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Req1", ValueReq1);
                        string[] textArray1 = new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" };
                        command.CommandText = string.Concat(textArray1);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool DeleteDB(string TABEL, string Req1, object ValueReq1, string Req2, object ValueReq2)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        if (Req1 != null)
                        {
                            command.Parameters.AddWithValue("@Req1", ValueReq1);
                        }
                        if (Req2 != null)
                        {
                            command.Parameters.AddWithValue("@Req2", ValueReq2);
                        }
                        if ((Req1 != null) && (Req2 == null))
                        {
                            string[] textArray1 = new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1" };
                            command.CommandText = string.Concat(textArray1);
                        }
                        else if ((Req2 != null) && (Req1 == null))
                        {
                            string[] textArray2 = new string[] { "DELETE FROM ", TABEL, " WHERE ", Req2, "=@Req2" };
                            command.CommandText = string.Concat(textArray2);
                        }
                        else if ((Req2 != null) && (Req1 != null))
                        {
                            string[] textArray3 = new string[] { "DELETE FROM ", TABEL, " WHERE ", Req1, "=@Req1 AND ", Req2, "=@Req2" };
                            command.CommandText = string.Concat(textArray3);
                        }
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool DeleteDB(string TABEL, string Req1, object[] ValueReq1, string Req2, object ValueReq2)
        {
            if (ValueReq1.Length == 0)
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        string str = "";
                        List<string> list = new List<string>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= ValueReq1.Length)
                            {
                                str = string.Join(",", list.ToArray());
                                command.Parameters.AddWithValue("@Req2", ValueReq2);
                                string[] textArray1 = new string[9];
                                textArray1[0] = "DELETE FROM ";
                                textArray1[1] = TABEL;
                                textArray1[2] = " WHERE ";
                                textArray1[3] = Req1;
                                textArray1[4] = " in (";
                                textArray1[5] = str;
                                textArray1[6] = ") AND ";
                                textArray1[7] = Req2;
                                textArray1[8] = "=@Req2";
                                command.CommandText = string.Concat(textArray1);
                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Dispose();
                                connection.Close();
                                break;
                            }
                            object obj2 = ValueReq1[index];
                            string parameterName = "@Value" + index.ToString();
                            command.Parameters.AddWithValue(parameterName, obj2);
                            list.Add(parameterName);
                            index++;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static uint GenStockId(int ItemId) => 
            BitConverter.ToUInt32(smethod_2(ItemId), 0);

        public static ulong GetClanStatus(FriendState state) => 
            GetPlayerStatus(0, 0, 0, 0, (int) state);

        public static ulong GetClanStatus(AccountStatus status, bool isOnline)
        {
            FriendState state;
            int num;
            int num2;
            int num3;
            int num4;
            GetPlayerLocation(status, isOnline, out state, out num, out num2, out num3, out num4);
            return GetPlayerStatus(num4, num, num2, num3, (int) state);
        }

        public static double GetDuration(DateTime Date) => 
            (DateTimeUtil.Now() - Date).TotalSeconds;

        public static uint GetFriendStatus(FriendModel f)
        {
            PlayerInfo info = f.Info;
            if (info == null)
            {
                return 0;
            }
            FriendState none = FriendState.None;
            int serverId = 0;
            int channelId = 0;
            int roomId = 0;
            if (f.Removed)
            {
                none = FriendState.Offline;
            }
            else if (f.State > 0)
            {
                none = (FriendState) f.State;
            }
            else
            {
                GetPlayerLocation(info.Status, info.IsOnline, out none, out roomId, out channelId, out serverId);
            }
            return GetPlayerStatus(roomId, channelId, serverId, (int) none);
        }

        public static uint GetFriendStatus(FriendModel f, FriendState stateN)
        {
            PlayerInfo info = f.Info;
            if (info == null)
            {
                return 0;
            }
            FriendState offline = stateN;
            int serverId = 0;
            int channelId = 0;
            int roomId = 0;
            if (f.Removed)
            {
                offline = FriendState.Offline;
            }
            else if (f.State > 0)
            {
                offline = (FriendState) f.State;
            }
            else if (stateN == FriendState.None)
            {
                GetPlayerLocation(info.Status, info.IsOnline, out offline, out roomId, out channelId, out serverId);
            }
            return GetPlayerStatus(roomId, channelId, serverId, (int) offline);
        }

        public static int GetIdStatics(int Id, int Type)
        {
            switch (Type)
            {
                case 1:
                    return (Id / 0x186a0);

                case 2:
                    return ((Id % 0x186a0) / 0x3e8);

                case 3:
                    return (Id % 0x3e8);

                case 4:
                    return ((Id % 0x989680) / 0x186a0);

                case 5:
                    return (Id / 0x3e8);
            }
            return 0;
        }

        public static ItemCategory GetItemCategory(int ItemId)
        {
            int num = GetIdStatics(ItemId, 1);
            int num2 = GetIdStatics(ItemId, 4);
            if ((num >= 1) && (num <= 5))
            {
                return ItemCategory.Weapon;
            }
            if ((((num >= 6) && (num <= 14)) || (num == 0x1b)) || ((num2 >= 7) && (num2 <= 14)))
            {
                return ItemCategory.Character;
            }
            if ((((num >= 0x10) && (num <= 20)) || ((num == 0x16) || ((num == 0x1a) || ((num >= 0x1c) && (num <= 0x1d))))) || ((num >= 0x24) && (num <= 40)))
            {
                return ItemCategory.Coupon;
            }
            if ((num == 15) || ((num >= 30) && (num <= 0x23)))
            {
                return ItemCategory.NewItem;
            }
            CLogger.Print($"Invalid Category [{num}]: {ItemId}", LoggerType.Warning, null);
            return ItemCategory.None;
        }

        public static byte[] GetMissionCardFlags(int missionId, byte[] arrayList)
        {
            byte[] buffer;
            if (missionId == 0)
            {
                return new byte[20];
            }
            List<MissionCardModel> cards = MissionCardRAW.GetCards(missionId);
            if (cards.Count == 0)
            {
                return new byte[20];
            }
            using (SyncServerPacket packet = new SyncServerPacket(20L))
            {
                int num = 0;
                int cardBasicId = 0;
                while (true)
                {
                    if (cardBasicId >= 10)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    foreach (MissionCardModel model in MissionCardRAW.GetCards(cards, cardBasicId))
                    {
                        if (arrayList[model.ArrayIdx] >= model.MissionLimit)
                        {
                            num |= model.Flag;
                        }
                    }
                    packet.WriteH((ushort) num);
                    num = 0;
                    cardBasicId++;
                }
            }
            return buffer;
        }

        public static ushort GetMissionCardFlags(int missionId, int cardIdx, byte[] arrayList)
        {
            if (missionId == 0)
            {
                return 0;
            }
            int num = 0;
            foreach (MissionCardModel model in MissionCardRAW.GetCards(missionId, cardIdx))
            {
                if (arrayList[model.ArrayIdx] >= model.MissionLimit)
                {
                    num |= model.Flag;
                }
            }
            return (ushort) num;
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
                if (status.RoomId != 0xff)
                {
                    roomId = status.RoomId;
                    channelId = status.ChannelId;
                    state = FriendState.Room;
                }
                else if ((status.RoomId != 0xff) || (status.ChannelId == 0xff))
                {
                    state = ((status.RoomId != 0xff) || (status.ChannelId != 0xff)) ? FriendState.Offline : FriendState.Online;
                }
                else
                {
                    channelId = status.ChannelId;
                    state = FriendState.Lobby;
                }
                if (status.ServerId != 0xff)
                {
                    serverId = status.ServerId;
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
                if (status.RoomId != 0xff)
                {
                    roomId = status.RoomId;
                    channelId = status.ChannelId;
                    state = FriendState.Room;
                }
                else if (((status.ClanMatchId == 0xff) && (status.RoomId != 0xff)) || (status.ChannelId == 0xff))
                {
                    state = ((status.RoomId != 0xff) || (status.ChannelId != 0xff)) ? FriendState.Offline : FriendState.Online;
                }
                else
                {
                    channelId = status.ChannelId;
                    state = FriendState.Lobby;
                }
                if (status.ServerId != 0xff)
                {
                    serverId = status.ServerId;
                }
                if (status.ClanMatchId != 0xff)
                {
                    clanFId = status.ClanMatchId + 1;
                }
            }
        }

        public static uint GetPlayerStatus(AccountStatus status, bool isOnline)
        {
            FriendState state;
            int num;
            int num2;
            int num3;
            GetPlayerLocation(status, isOnline, out state, out num, out num2, out num3);
            return GetPlayerStatus(num, num2, num3, (int) state);
        }

        public static uint GetPlayerStatus(int roomId, int channelId, int serverId, int stateId)
        {
            int num2 = (channelId & 0xff) << 12;
            int num3 = roomId & 0xfff;
            return (uint) (((((stateId & 0xff) << 0x1c) | ((serverId & 0xff) << 20)) | num2) | num3);
        }

        public static ulong GetPlayerStatus(int clanFId, int roomId, int channelId, int serverId, int stateId)
        {
            long num2 = (serverId & 0xff) << 20;
            long num3 = (channelId & 0xff) << 12;
            long num4 = roomId & 0xfff;
            return (ulong) ((((((clanFId & 0xffffffffL) << 0x20) | ((stateId & 0xff) << 0x1c)) | num2) | num3) | num4);
        }

        public static T NextOf<T>(IList<T> List, T Item)
        {
            int index = List.IndexOf(Item);
            return List[(index == (List.Count - 1)) ? 0 : (index + 1)];
        }

        public static T ParseEnum<T>(string Value) => 
            (T) System.Enum.Parse(typeof(T), Value, true);

        public static int Percentage(int Total, int Percent) => 
            (Total * Percent) / 100;

        public static float Percentage(float Total, int Percent) => 
            (Total * Percent) / 100f;

        private static ((bool, bool, bool, bool, bool), (bool, bool, bool, bool, bool, bool, bool, (bool, bool, bool, bool, bool)), (bool, bool, bool)) smethod_0(PlayerEquipment playerEquipment_0, List<ItemsModel> list_0, (bool, bool, bool, bool, bool) valueTuple_0, (bool, bool, bool, bool, bool, bool, bool, (bool, bool, bool, bool, bool)) valueTuple_1, (bool, bool, bool) valueTuple_2)
        {
            List<ItemsModel> list = list_0;
            lock (list)
            {
                Func<ItemsModel, bool> predicate = Class5.<>9__1_0;
                if (Class5.<>9__1_0 == null)
                {
                    Func<ItemsModel, bool> local1 = Class5.<>9__1_0;
                    predicate = Class5.<>9__1_0 = new Func<ItemsModel, bool>(Class5.<>9.method_0);
                }
                Func<ItemsModel, int> selector = Class5.<>9__1_1;
                if (Class5.<>9__1_1 == null)
                {
                    Func<ItemsModel, int> local2 = Class5.<>9__1_1;
                    selector = Class5.<>9__1_1 = new Func<ItemsModel, int>(Class5.<>9.method_1);
                }
                HashSet<int> set = new HashSet<int>(list_0.Where<ItemsModel>(predicate).Select<ItemsModel, int>(selector));
                if (set.Contains(playerEquipment_0.WeaponPrimary))
                {
                    valueTuple_0.Item1 = true;
                }
                if (set.Contains(playerEquipment_0.WeaponSecondary))
                {
                    valueTuple_0.Item2 = true;
                }
                if (set.Contains(playerEquipment_0.WeaponMelee))
                {
                    valueTuple_0.Item3 = true;
                }
                if (set.Contains(playerEquipment_0.WeaponExplosive))
                {
                    valueTuple_0.Item4 = true;
                }
                if (set.Contains(playerEquipment_0.WeaponSpecial))
                {
                    valueTuple_0.Item5 = true;
                }
                if (set.Contains(playerEquipment_0.CharaRedId))
                {
                    valueTuple_1.Item1 = true;
                }
                if (set.Contains(playerEquipment_0.CharaBlueId))
                {
                    valueTuple_1.Item2 = true;
                }
                if (set.Contains(playerEquipment_0.PartHead))
                {
                    valueTuple_1.Item3 = true;
                }
                if (set.Contains(playerEquipment_0.PartFace))
                {
                    valueTuple_1.Item4 = true;
                }
                if (set.Contains(playerEquipment_0.PartJacket))
                {
                    valueTuple_1.Item5 = true;
                }
                if (set.Contains(playerEquipment_0.PartPocket))
                {
                    valueTuple_1.Item6 = true;
                }
                if (set.Contains(playerEquipment_0.PartGlove))
                {
                    valueTuple_1.Item7 = true;
                }
                if (set.Contains(playerEquipment_0.PartBelt))
                {
                    valueTuple_1.Rest.Item1 = true;
                }
                if (set.Contains(playerEquipment_0.PartHolster))
                {
                    valueTuple_1.Rest.Item2 = true;
                }
                if (set.Contains(playerEquipment_0.PartSkin))
                {
                    valueTuple_1.Rest.Item3 = true;
                }
                if ((playerEquipment_0.BeretItem != 0) && set.Contains(playerEquipment_0.BeretItem))
                {
                    valueTuple_1.Rest.Item4 = true;
                }
                if (set.Contains(playerEquipment_0.DinoItem))
                {
                    valueTuple_1.Rest.Item5 = true;
                }
                if ((playerEquipment_0.AccessoryId != 0) && set.Contains(playerEquipment_0.AccessoryId))
                {
                    valueTuple_2.Item1 = true;
                }
                if ((playerEquipment_0.SprayId != 0) && set.Contains(playerEquipment_0.SprayId))
                {
                    valueTuple_2.Item2 = true;
                }
                if ((playerEquipment_0.NameCardId != 0) && set.Contains(playerEquipment_0.NameCardId))
                {
                    valueTuple_2.Item3 = true;
                }
            }
            return (valueTuple_0, valueTuple_1, valueTuple_2);
        }

        private static void smethod_1(PlayerEquipment playerEquipment_0, ref (bool, bool, bool, bool, bool) valueTuple_0, ref (bool, bool, bool, bool, bool, bool, bool, (bool, bool, bool, bool, bool)) valueTuple_1, ref (bool, bool, bool) valueTuple_2)
        {
            if (!valueTuple_0.Item1)
            {
                playerEquipment_0.WeaponPrimary = 0x1925c;
            }
            if (!valueTuple_0.Item2)
            {
                playerEquipment_0.WeaponSecondary = 0x31513;
            }
            if (!valueTuple_0.Item3)
            {
                playerEquipment_0.WeaponMelee = 0x497c9;
            }
            if (!valueTuple_0.Item4)
            {
                playerEquipment_0.WeaponExplosive = 0x635d9;
            }
            if (!valueTuple_0.Item5)
            {
                playerEquipment_0.WeaponSpecial = 0x7c061;
            }
            if (!valueTuple_1.Item1)
            {
                playerEquipment_0.CharaRedId = 0x92ba9;
            }
            if (!valueTuple_1.Item2)
            {
                playerEquipment_0.CharaBlueId = 0x92f92;
            }
            if (!valueTuple_1.Item3)
            {
                playerEquipment_0.PartHead = 0x3ba57860;
            }
            if (!valueTuple_1.Item4)
            {
                playerEquipment_0.PartFace = 0x3ba6ff00;
            }
            if (!valueTuple_1.Item5)
            {
                playerEquipment_0.PartJacket = 0x3ba885a0;
            }
            if (!valueTuple_1.Item6)
            {
                playerEquipment_0.PartPocket = 0x3baa0c40;
            }
            if (!valueTuple_1.Item7)
            {
                playerEquipment_0.PartGlove = 0x3bab92e0;
            }
            if (!valueTuple_1.Rest.Item1)
            {
                playerEquipment_0.PartBelt = 0x3bad1980;
            }
            if (!valueTuple_1.Rest.Item2)
            {
                playerEquipment_0.PartHolster = 0x3baea020;
            }
            if (!valueTuple_1.Rest.Item3)
            {
                playerEquipment_0.PartSkin = 0x3bb026c0;
            }
            if (!valueTuple_1.Rest.Item4)
            {
                playerEquipment_0.BeretItem = 0;
            }
            if (!valueTuple_1.Rest.Item5)
            {
                playerEquipment_0.DinoItem = 0x16e55f;
            }
            if (!valueTuple_2.Item1)
            {
                playerEquipment_0.AccessoryId = 0;
            }
            if (!valueTuple_2.Item2)
            {
                playerEquipment_0.SprayId = 0;
            }
            if (!valueTuple_2.Item3)
            {
                playerEquipment_0.NameCardId = 0;
            }
            if ((playerEquipment_0.PartHead == 0x3ba57860) && (playerEquipment_0.PartFace != 0x3ba6ff00))
            {
                playerEquipment_0.PartHead = 0;
            }
        }

        private static byte[] smethod_2(int int_0)
        {
            byte[] bytes = BitConverter.GetBytes(int_0);
            bytes[3] = 0x40;
            return bytes;
        }

        private static int smethod_3(int int_0, int int_1)
        {
            while (int_1 != 0)
            {
                int num1 = int_0 % int_1;
                int_0 = int_1;
                int_1 = num1;
            }
            return int_0;
        }

        public static string[] SplitObjects(string Input, string Delimiter)
        {
            string[] separator = new string[] { Delimiter };
            return Input.Split(separator, StringSplitOptions.None);
        }

        public static char[] SubArray(this char[] Input, int StartIndex, int Length)
        {
            List<char> list = new List<char>();
            for (int i = StartIndex; i < Length; i++)
            {
                list.Add(Input[i]);
            }
            return list.ToArray();
        }

        public static string ToTitleCase(string Text)
        {
            char[] separator = new char[] { ' ' };
            string newValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text.Split(separator)[0].ToLower());
            char[] chArray2 = new char[] { ' ' };
            Text = Text.Replace(Text.Split(chArray2)[0], newValue);
            return Text;
        }

        public static void TryCreateItem(ItemsModel Model, PlayerInventory Inventory, long OwnerId)
        {
            try
            {
                ItemsModel model = Inventory.GetItem(Model.Id);
                if (model == null)
                {
                    if (DaoManagerSQL.CreatePlayerInventoryItem(Model, OwnerId))
                    {
                        Inventory.AddItem(Model);
                    }
                }
                else
                {
                    Model.ObjectId = model.ObjectId;
                    if (model.Equip == ItemEquipType.Durable)
                    {
                        if (ShopManager.IsRepairableItem(Model.Id))
                        {
                            Model.Count = 100;
                            UpdateDB("player_items", "count", (long) Model.Count, "owner_id", OwnerId, "id", Model.Id);
                        }
                        else
                        {
                            Model.Count += model.Count;
                            UpdateDB("player_items", "count", (long) Model.Count, "owner_id", OwnerId, "id", Model.Id);
                        }
                    }
                    else if (model.Equip == ItemEquipType.Temporary)
                    {
                        DateTime time = DateTime.ParseExact(model.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
                        if (Model.Category != ItemCategory.Coupon)
                        {
                            Model.Equip = ItemEquipType.Temporary;
                            Model.Count = Convert.ToUInt32(time.AddSeconds((double) Model.Count).ToString("yyMMddHHmm"));
                        }
                        else
                        {
                            TimeSpan span = (TimeSpan) (DateTime.ParseExact(Model.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture) - DateTimeUtil.Now());
                            Model.Equip = ItemEquipType.Temporary;
                            Model.Count = Convert.ToUInt32(time.AddDays(span.TotalDays).ToString("yyMMddHHmm"));
                        }
                        UpdateDB("player_items", "count", (long) Model.Count, "owner_id", OwnerId, "id", Model.Id);
                    }
                    model.Equip = Model.Equip;
                    model.Count = Model.Count;
                }
            }
            catch (Exception exception)
            {
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
            if ((COLUMNS.Length != 0) && ((VALUES.Length != 0) && (COLUMNS.Length != VALUES.Length)))
            {
                CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
                return false;
            }
            if ((COLUMNS.Length == 0) || (VALUES.Length == 0))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        string str = "";
                        List<string> list = new List<string>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= VALUES.Length)
                            {
                                str = string.Join(",", list.ToArray());
                                command.CommandText = "UPDATE " + TABEL + " SET " + str;
                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Close();
                                break;
                            }
                            object obj2 = VALUES[index];
                            string str2 = COLUMNS[index];
                            string parameterName = "@Value" + index.ToString();
                            command.Parameters.AddWithValue(parameterName, obj2);
                            list.Add(str2 + "=" + parameterName);
                            index++;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB1] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string[] COLUMNS, params object[] VALUES)
        {
            if ((COLUMNS.Length != 0) && ((VALUES.Length != 0) && (COLUMNS.Length != VALUES.Length)))
            {
                CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
                return false;
            }
            if ((COLUMNS.Length == 0) || (VALUES.Length == 0))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        string str = "";
                        List<string> list = new List<string>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= VALUES.Length)
                            {
                                str = string.Join(",", list.ToArray());
                                command.Parameters.AddWithValue("@Req1", ValueReq1);
                                string[] textArray1 = new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, "=@Req1" };
                                command.CommandText = string.Concat(textArray1);
                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Close();
                                break;
                            }
                            object obj2 = VALUES[index];
                            string str2 = COLUMNS[index];
                            string parameterName = "@Value" + index.ToString();
                            command.Parameters.AddWithValue(parameterName, obj2);
                            list.Add(str2 + "=" + parameterName);
                            index++;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB2] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Value", VALUE);
                        command.Parameters.AddWithValue("@Req1", ValueReq1);
                        string[] textArray1 = new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" };
                        command.CommandText = string.Concat(textArray1);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB3] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateDB(string TABEL, string Req1, object ValueReq1, string Req2, object valueReq2, string[] COLUMNS, params object[] VALUES)
        {
            if ((COLUMNS.Length != 0) && ((VALUES.Length != 0) && (COLUMNS.Length != VALUES.Length)))
            {
                CLogger.Print("[Update Database] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
                return false;
            }
            if ((COLUMNS.Length == 0) || (VALUES.Length == 0))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        string str = "";
                        List<string> list = new List<string>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= VALUES.Length)
                            {
                                str = string.Join(",", list.ToArray());
                                if (Req1 != null)
                                {
                                    command.Parameters.AddWithValue("@Req1", ValueReq1);
                                }
                                if (Req2 != null)
                                {
                                    command.Parameters.AddWithValue("@Req2", valueReq2);
                                }
                                if ((Req1 != null) && (Req2 == null))
                                {
                                    string[] textArray1 = new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, "=@Req1" };
                                    command.CommandText = string.Concat(textArray1);
                                }
                                else if ((Req2 != null) && (Req1 == null))
                                {
                                    string[] textArray2 = new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req2, "=@Req2" };
                                    command.CommandText = string.Concat(textArray2);
                                }
                                else if ((Req2 != null) && (Req1 != null))
                                {
                                    string[] textArray3 = new string[9];
                                    textArray3[0] = "UPDATE ";
                                    textArray3[1] = TABEL;
                                    textArray3[2] = " SET ";
                                    textArray3[3] = str;
                                    textArray3[4] = " WHERE ";
                                    textArray3[5] = Req1;
                                    textArray3[6] = "=@Req1 AND ";
                                    textArray3[7] = Req2;
                                    textArray3[8] = "=@Req2";
                                    command.CommandText = string.Concat(textArray3);
                                }
                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Close();
                                break;
                            }
                            object obj2 = VALUES[index];
                            string str2 = COLUMNS[index];
                            string parameterName = "@Value" + index.ToString();
                            command.Parameters.AddWithValue(parameterName, obj2);
                            list.Add(str2 + "=" + parameterName);
                            index++;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB4] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateDB(string TABEL, string Req1, int[] ValueReq1, string Req2, object ValueReq2, string[] COLUMNS, params object[] VALUES)
        {
            if ((COLUMNS.Length != 0) && ((VALUES.Length != 0) && (COLUMNS.Length != VALUES.Length)))
            {
                CLogger.Print("[updateDB5] Wrong values: " + string.Join(",", COLUMNS) + "/" + string.Join(",", VALUES), LoggerType.Warning, null);
                return false;
            }
            if ((COLUMNS.Length == 0) || (VALUES.Length == 0))
            {
                return false;
            }
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        string str = "";
                        List<string> list = new List<string>();
                        int index = 0;
                        while (true)
                        {
                            if (index >= VALUES.Length)
                            {
                                str = string.Join(",", list.ToArray());
                                if (Req1 != null)
                                {
                                    command.Parameters.AddWithValue("@Req1", ValueReq1);
                                }
                                if (Req2 != null)
                                {
                                    command.Parameters.AddWithValue("@Req2", ValueReq2);
                                }
                                if ((Req1 != null) && (Req2 == null))
                                {
                                    string[] textArray1 = new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req1, " = ANY (@Req1)" };
                                    command.CommandText = string.Concat(textArray1);
                                }
                                else if ((Req2 != null) && (Req1 == null))
                                {
                                    string[] textArray2 = new string[] { "UPDATE ", TABEL, " SET ", str, " WHERE ", Req2, "=@Req2" };
                                    command.CommandText = string.Concat(textArray2);
                                }
                                else if ((Req2 != null) && (Req1 != null))
                                {
                                    string[] textArray3 = new string[9];
                                    textArray3[0] = "UPDATE ";
                                    textArray3[1] = TABEL;
                                    textArray3[2] = " SET ";
                                    textArray3[3] = str;
                                    textArray3[4] = " WHERE ";
                                    textArray3[5] = Req1;
                                    textArray3[6] = " = ANY (@Req1) AND ";
                                    textArray3[7] = Req2;
                                    textArray3[8] = "=@Req2";
                                    command.CommandText = string.Concat(textArray3);
                                }
                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Close();
                                break;
                            }
                            object obj2 = VALUES[index];
                            string str2 = COLUMNS[index];
                            string parameterName = "@Value" + index.ToString();
                            command.Parameters.AddWithValue(parameterName, obj2);
                            list.Add(str2 + "=" + parameterName);
                            index++;
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB5] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static bool UpdateDB(string TABEL, string COLUMN, object VALUE, string Req1, object ValueReq1, string Req2, object ValueReq2)
        {
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Value", VALUE);
                        if (Req1 != null)
                        {
                            command.Parameters.AddWithValue("@Req1", ValueReq1);
                        }
                        if (Req2 != null)
                        {
                            command.Parameters.AddWithValue("@Req2", ValueReq2);
                        }
                        if ((Req1 != null) && (Req2 == null))
                        {
                            string[] textArray1 = new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req1, "=@Req1" };
                            command.CommandText = string.Concat(textArray1);
                        }
                        else if ((Req2 != null) && (Req1 == null))
                        {
                            string[] textArray2 = new string[] { "UPDATE ", TABEL, " SET ", COLUMN, "=@Value WHERE ", Req2, "=@Req2" };
                            command.CommandText = string.Concat(textArray2);
                        }
                        else if ((Req2 != null) && (Req1 != null))
                        {
                            string[] textArray3 = new string[9];
                            textArray3[0] = "UPDATE ";
                            textArray3[1] = TABEL;
                            textArray3[2] = " SET ";
                            textArray3[3] = COLUMN;
                            textArray3[4] = "=@Value WHERE ";
                            textArray3[5] = Req1;
                            textArray3[6] = "=@Req1 AND ";
                            textArray3[7] = Req2;
                            textArray3[8] = "=@Req2";
                            command.CommandText = string.Concat(textArray3);
                        }
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.UpdateDB6] " + exception.Message, LoggerType.Error, exception);
                return false;
            }
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
            try
            {
                using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                {
                    connection.Open();
                    NpgsqlCommand command1 = connection.CreateCommand();
                    command1.CommandType = CommandType.Text;
                    command1.CommandText = $"UPDATE accounts SET online = {false} WHERE online = {true}";
                    command1.ExecuteNonQuery();
                    command1.Dispose();
                    connection.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static uint ValidateStockId(int ItemId)
        {
            int num = GetIdStatics(ItemId, 4);
            return GenStockId(((num < 7) || (num > 14)) ? ItemId : num);
        }

        public static uint Verificate(byte A, byte B, byte C, byte D)
        {
            byte[] source = new byte[] { A, B, C, D };
            if (source.Any<byte>())
            {
                if (B < 60)
                {
                    CLogger.Print($"Refresh Rate is below the minimum limit ({B})", LoggerType.Warning, null);
                    return 0;
                }
                if ((C >= 0) && (C <= 1))
                {
                    return BitConverter.ToUInt32(source, 0);
                }
                CLogger.Print($"Unknown Window State ({C})", LoggerType.Warning, null);
            }
            return 0;
        }

        [Serializable, CompilerGenerated]
        private sealed class Class5
        {
            public static readonly ComDiv.Class5 <>9 = new ComDiv.Class5();
            public static Func<ItemsModel, bool> <>9__1_0;
            public static Func<ItemsModel, int> <>9__1_1;

            internal bool method_0(ItemsModel itemsModel_0) => 
                itemsModel_0.Count != 0;

            internal int method_1(ItemsModel itemsModel_0) => 
                itemsModel_0.Id;
        }
    }
}

