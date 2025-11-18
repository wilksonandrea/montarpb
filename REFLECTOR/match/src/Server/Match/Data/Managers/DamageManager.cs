namespace Server.Match.Data.Managers
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Sync.Server;
    using System;
    using System.Collections.Generic;

    public static class DamageManager
    {
        public static void BoomDeath(RoomModel Room, PlayerModel Killer, int Damage, int WeaponId, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, List<int> BoomPlayers, CharaHitPart HitPart, CharaDeath DeathType)
        {
            if ((BoomPlayers != null) && (BoomPlayers.Count != 0))
            {
                foreach (int num in BoomPlayers)
                {
                    PlayerModel model;
                    if (Room.GetPlayer(num, out model) && !model.Dead)
                    {
                        smethod_0(Room, Deaths, model, Killer, DeathType);
                        if (Damage > 0)
                        {
                            if (ConfigLoader.UseHitMarker)
                            {
                                SendMatchInfo.SendHitMarkerSync(Room, Killer, DeathType, HitType.Normal, Damage);
                            }
                            ObjectHitInfo info1 = new ObjectHitInfo(2);
                            info1.ObjId = model.Slot;
                            info1.ObjLife = model.Life;
                            info1.HitPart = HitPart;
                            info1.KillerSlot = Killer.Slot;
                            info1.Position = model.Position - Killer.Position;
                            info1.DeathType = DeathType;
                            info1.WeaponId = WeaponId;
                            ObjectHitInfo item = info1;
                            Objs.Add(item);
                        }
                    }
                }
            }
        }

        public static void SabotageDestroy(RoomModel Room, PlayerModel Player, ObjectModel ObjM, ObjectInfo ObjI, int Damage)
        {
            if ((ObjM.UltraSync > 0) && ((Room.RoomType == RoomCondition.Destroy) || (Room.RoomType == RoomCondition.Defense)))
            {
                if ((ObjM.UltraSync == 1) || (ObjM.UltraSync == 3))
                {
                    Room.Bar1 = ObjI.Life;
                }
                else if ((ObjM.UltraSync == 2) || (ObjM.UltraSync == 4))
                {
                    Room.Bar2 = ObjI.Life;
                }
                SendMatchInfo.SendSabotageSync(Room, Player, Damage, (ObjM.UltraSync == 4) ? 2 : 1);
            }
        }

        public static void SimpleDeath(RoomModel Room, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, PlayerModel Killer, PlayerModel Victim, int Damage, int WeaponId, CharaHitPart HitPart, CharaDeath DeathType)
        {
            Victim.Life -= Damage;
            smethod_2(Room, Victim, Killer, Victim.Life);
            if (Victim.Life <= 0)
            {
                smethod_0(Room, Deaths, Victim, Killer, DeathType);
            }
            else
            {
                smethod_1(Objs, Victim, Killer, DeathType, HitPart);
            }
        }

        private static void smethod_0(RoomModel roomModel_0, List<DeathServerData> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0)
        {
            playerModel_0.Life = 0;
            playerModel_0.Dead = true;
            playerModel_0.LastDie = DateTimeUtil.Now();
            DeathServerData data1 = new DeathServerData();
            data1.Player = playerModel_0;
            data1.DeathType = charaDeath_0;
            DeathServerData item = data1;
            AssistServerData assist = AssistManager.GetAssist(playerModel_0.Slot, roomModel_0.RoomId);
            item.AssistSlot = (assist == null) ? playerModel_1.Slot : (assist.IsAssist ? assist.Killer : playerModel_1.Slot);
            list_0.Add(item);
            AssistManager.RemoveAssist(assist);
        }

        private static void smethod_1(List<ObjectHitInfo> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0, CharaHitPart charaHitPart_0)
        {
            ObjectHitInfo info1 = new ObjectHitInfo(5);
            info1.ObjId = playerModel_0.Slot;
            info1.KillerSlot = playerModel_1.Slot;
            info1.DeathType = charaDeath_0;
            info1.ObjLife = playerModel_0.Life;
            info1.HitPart = charaHitPart_0;
            ObjectHitInfo item = info1;
            list_0.Add(item);
        }

        private static void smethod_2(RoomModel roomModel_0, PlayerModel playerModel_0, PlayerModel playerModel_1, int int_0)
        {
            AssistServerData data1 = new AssistServerData();
            data1.RoomId = roomModel_0.RoomId;
            data1.Killer = playerModel_1.Slot;
            data1.Victim = playerModel_0.Slot;
            data1.IsKiller = int_0 <= 0;
            data1.VictimDead = int_0 <= 0;
            AssistServerData assist = data1;
            assist.IsAssist = !assist.IsKiller;
            if (assist.Killer != assist.Victim)
            {
                AssistManager.AddAssist(assist);
            }
        }
    }
}

