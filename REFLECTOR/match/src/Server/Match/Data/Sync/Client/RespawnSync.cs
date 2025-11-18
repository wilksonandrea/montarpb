namespace Server.Match.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match;
    using Server.Match.Data.Managers;
    using Server.Match.Data.Models;
    using Server.Match.Data.Utils;
    using Server.Match.Data.XML;
    using Server.Match.Network.Packets;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RespawnSync
    {
        public static void Load(SyncClientPacket C)
        {
            Class1 class2 = new Class1();
            uint uniqueRoomId = C.ReadUD();
            uint seed = C.ReadUD();
            long startTick = C.ReadQ();
            int num4 = C.ReadC();
            int round = C.ReadC();
            int slot = C.ReadC();
            int respawn = C.ReadC();
            int num8 = C.ReadC();
            int charaId = 0;
            int percent = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            bool flag = false;
            if ((num4 != 0) && (num4 != 2))
            {
                if (C.ToArray().Length > 0x17)
                {
                    CLogger.Print($"RespawnSync (Length > 23): {C.ToArray().Length}", LoggerType.Warning, null);
                }
            }
            else
            {
                charaId = C.ReadD();
                percent = C.ReadC();
                flag = C.ReadC() == 1;
                num11 = C.ReadD();
                num12 = C.ReadD();
                num13 = C.ReadD();
                num14 = C.ReadD();
                num15 = C.ReadD();
                num16 = C.ReadD();
                if (C.ToArray().Length > 0x35)
                {
                    CLogger.Print($"RespawnSync (Length > 53): {C.ToArray().Length}", LoggerType.Warning, null);
                }
            }
            class2.roomModel_0 = RoomsManager.GetRoom(uniqueRoomId, seed);
            if (class2.roomModel_0 != null)
            {
                class2.roomModel_0.ResyncTick(startTick, seed);
                class2.playerModel_0 = class2.roomModel_0.GetPlayer(slot, true);
                if ((class2.playerModel_0 != null) && (class2.playerModel_0.PlayerIdByUser != num8))
                {
                    CLogger.Print($"Invalid User Ids: [By User: {class2.playerModel_0.PlayerIdByUser} / Server: {num8}]", LoggerType.Warning, null);
                }
                if ((class2.playerModel_0 != null) && (class2.playerModel_0.PlayerIdByUser == num8))
                {
                    class2.playerModel_0.PlayerIdByServer = num8;
                    class2.playerModel_0.RespawnByServer = respawn;
                    class2.playerModel_0.Integrity = false;
                    if (round > class2.roomModel_0.ServerRound)
                    {
                        class2.roomModel_0.ServerRound = round;
                    }
                    if ((num4 == 0) || (num4 == 2))
                    {
                        AssistServerData assist = AssistManager.GetAssist(class2.playerModel_0.Slot, class2.roomModel_0.RoomId);
                        if ((assist != null) && AssistManager.RemoveAssist(assist))
                        {
                            Predicate<AssistServerData> match = class2.predicate_0;
                            if (class2.predicate_0 == null)
                            {
                                Predicate<AssistServerData> local1 = class2.predicate_0;
                                match = class2.predicate_0 = new Predicate<AssistServerData>(class2.method_0);
                            }
                            using (List<AssistServerData>.Enumerator enumerator = AssistManager.Assists.FindAll(match).GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    AssistManager.RemoveAssist(enumerator.Current);
                                }
                            }
                        }
                        Equipment equipment1 = new Equipment();
                        equipment1.WpnPrimary = num11;
                        equipment1.WpnSecondary = num12;
                        equipment1.WpnMelee = num13;
                        equipment1.WpnExplosive = num14;
                        equipment1.WpnSpecial = num15;
                        equipment1.Accessory = num16;
                        Equipment equipment = equipment1;
                        class2.playerModel_0.Dead = false;
                        class2.playerModel_0.PlantDuration = ConfigLoader.PlantDuration;
                        class2.playerModel_0.DefuseDuration = ConfigLoader.DefuseDuration;
                        class2.playerModel_0.Equip = equipment;
                        if (flag)
                        {
                            class2.playerModel_0.PlantDuration -= ComDiv.Percentage(ConfigLoader.PlantDuration, 50);
                            class2.playerModel_0.DefuseDuration -= ComDiv.Percentage(ConfigLoader.DefuseDuration, 0x19);
                        }
                        if (!class2.roomModel_0.BotMode)
                        {
                            if (class2.roomModel_0.SourceToMap == -1)
                            {
                                class2.roomModel_0.RoundResetRoomF1(round);
                            }
                            else
                            {
                                class2.roomModel_0.RoundResetRoomS1(round);
                            }
                        }
                        if (charaId == -1)
                        {
                            class2.playerModel_0.Immortal = true;
                        }
                        else
                        {
                            class2.playerModel_0.Immortal = false;
                            int charaHP = CharaStructureXML.GetCharaHP(charaId);
                            charaHP += ComDiv.Percentage(charaHP, percent);
                            class2.playerModel_0.MaxLife = charaHP;
                            class2.playerModel_0.ResetLife();
                        }
                    }
                    if (!class2.roomModel_0.BotMode && ((num4 != 2) && class2.roomModel_0.ObjectsIsValid()))
                    {
                        List<ObjectHitInfo> objs = new List<ObjectHitInfo>();
                        ObjectInfo[] objects = class2.roomModel_0.Objects;
                        int index = 0;
                        while (true)
                        {
                            if (index >= objects.Length)
                            {
                                PlayerModel[] players = class2.roomModel_0.Players;
                                index = 0;
                                while (true)
                                {
                                    if (index >= players.Length)
                                    {
                                        if (objs.Count > 0)
                                        {
                                            byte[] data = AllUtils.BaseWriteCode(4, PROTOCOL_EVENTS_ACTION.GET_CODE(objs), 0xff, AllUtils.GetDuration(class2.roomModel_0.StartTime), round, respawn, 0, num8);
                                            MatchXender.Client.SendPacket(data, class2.playerModel_0.Client);
                                        }
                                        objs.Clear();
                                        objs = null;
                                        break;
                                    }
                                    PlayerModel model2 = players[index];
                                    if ((model2.Slot != slot) && (model2.AccountIdIsValid() && !model2.Immortal))
                                    {
                                        DateTime time = new DateTime();
                                        if ((model2.StartTime != time) && ((model2.MaxLife != model2.Life) || model2.Dead))
                                        {
                                            ObjectHitInfo item = new ObjectHitInfo(4);
                                            item.ObjId = model2.Slot;
                                            item.ObjLife = model2.Life;
                                            objs.Add(item);
                                        }
                                    }
                                    index++;
                                }
                                break;
                            }
                            ObjectInfo info = objects[index];
                            ObjectModel model = info.Model;
                            if ((model != null) && (((num4 != 2) && (model.Destroyable && (info.Life != model.Life))) || model.NeedSync))
                            {
                                ObjectHitInfo info1 = new ObjectHitInfo(3);
                                info1.ObjSyncId = (int) model.NeedSync;
                                info1.AnimId1 = model.Animation;
                                info1.AnimId2 = (info.Animation != null) ? info.Animation.Id : 0xff;
                                ObjectHitInfo item = info1;
                                item.DestroyState = info.DestroyState;
                                item.ObjId = model.Id;
                                item.ObjLife = info.Life;
                                item.SpecialUse = AllUtils.GetDuration(info.UseDate);
                                objs.Add(item);
                            }
                            index++;
                        }
                    }
                }
            }
        }

        [CompilerGenerated]
        private sealed class Class1
        {
            public PlayerModel playerModel_0;
            public RoomModel roomModel_0;
            public Predicate<AssistServerData> predicate_0;

            internal bool method_0(AssistServerData assistServerData_0) => 
                (assistServerData_0.Victim == this.playerModel_0.Slot) && (assistServerData_0.RoomId == this.roomModel_0.RoomId);
        }
    }
}

