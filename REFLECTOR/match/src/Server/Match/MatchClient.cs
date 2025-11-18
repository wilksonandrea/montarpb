namespace Server.Match
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.SharpDX;
    using Plugin.Core.Utility;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Managers;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using Server.Match.Data.Models.Event.Event;
    using Server.Match.Data.Sync.Server;
    using Server.Match.Data.Utils;
    using Server.Match.Network.Actions.Event;
    using Server.Match.Network.Actions.SubHead;
    using Server.Match.Network.Packets;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public class MatchClient
    {
        private readonly Socket socket_0;
        private readonly IPEndPoint ipendPoint_0;

        public MatchClient(Socket socket_1, IPEndPoint ipendPoint_1)
        {
            this.socket_0 = socket_1;
            this.ipendPoint_0 = ipendPoint_1;
        }

        public void BeginReceive(byte[] Buffer, DateTime Date)
        {
            PacketModel model1 = new PacketModel();
            model1.Data = Buffer;
            model1.ReceiveDate = Date;
            PacketModel model = model1;
            SyncClientPacket packet = new SyncClientPacket(model.Data);
            model.Opcode = packet.ReadC();
            model.Slot = packet.ReadC();
            model.Time = packet.ReadT();
            model.Round = packet.ReadC();
            model.Length = packet.ReadUH();
            model.Respawn = packet.ReadC();
            model.RoundNumber = packet.ReadC();
            model.AccountId = packet.ReadC();
            model.Unk1 = packet.ReadC();
            model.Unk2 = packet.ReadD();
            if (model.Length > model.Data.Length)
            {
                CLogger.Print($"Packet with invalid size canceled. [ Length: {model.Length} DataLength: {model.Data.Length} ]", LoggerType.Warning, null);
            }
            else
            {
                AllUtils.GetDecryptedData(model);
                if (ConfigLoader.IsTestMode && (model.Unk1 > 0))
                {
                    CLogger.Print(Bitwise.ToHexData($"[N] Test Mode, Packet Unk: {model.Unk1}", model.Data), LoggerType.Opcode, null);
                    CLogger.Print(Bitwise.ToHexData($"[D] Test Mode, Packet Unk: {model.Unk1}", model.WithoutEndData), LoggerType.Opcode, null);
                }
                if (ConfigLoader.EnableLog && ((model.Opcode != 0x83) && ((model.Opcode != 0x84) && (model.Opcode != 3))))
                {
                    int opcode = model.Opcode;
                }
                this.ReadPacket(model);
            }
        }

        private List<ObjectHitInfo> method_0(ActionModel actionModel_0, RoomModel roomModel_0, float float_0, out byte[] byte_0)
        {
            byte_0 = new byte[0];
            if (roomModel_0 == null)
            {
                return null;
            }
            if (actionModel_0.Data.Length == 0)
            {
                return new List<ObjectHitInfo>();
            }
            byte[] data = actionModel_0.Data;
            List<ObjectHitInfo> objs = new List<ObjectHitInfo>();
            SyncClientPacket c = new SyncClientPacket(data);
            using (SyncServerPacket packet2 = new SyncServerPacket())
            {
                uint num = 0;
                PlayerModel player = roomModel_0.GetPlayer(actionModel_0.Slot, false);
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
                {
                    num += 0x100;
                    ActionStateInfo info = ActionState.ReadInfo(actionModel_0, c, false);
                    if (!roomModel_0.BotMode)
                    {
                        Equipment equip = player.Equip;
                        if ((player != null) && (equip != null))
                        {
                            int wpnPrimary = 0;
                            byte num3 = 0;
                            byte num4 = 0;
                            if (info.Flag.HasFlag(WeaponSyncType.Primary))
                            {
                                wpnPrimary = equip.WpnPrimary;
                                int num5 = ComDiv.GetIdStatics(equip.Accessory, 3);
                                num3 = (ComDiv.GetIdStatics(equip.Accessory, 1) == 30) ? ((byte) num5) : num3;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Secondary))
                            {
                                wpnPrimary = equip.WpnSecondary;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Melee))
                            {
                                wpnPrimary = equip.WpnMelee;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Explosive))
                            {
                                wpnPrimary = equip.WpnExplosive;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Special))
                            {
                                wpnPrimary = equip.WpnSpecial;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Mission) && (roomModel_0.RoomType == RoomCondition.Bomb))
                            {
                                wpnPrimary = 0x4c6e69;
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Dual))
                            {
                                num4 = 0x10;
                                if (info.Action.HasFlag(ActionFlag.Unk2048))
                                {
                                    wpnPrimary = equip.WpnPrimary;
                                }
                                if (info.Action.HasFlag(ActionFlag.Unk4096))
                                {
                                    wpnPrimary = equip.WpnPrimary;
                                }
                            }
                            if (info.Flag.HasFlag(WeaponSyncType.Ext))
                            {
                                num4 = 0x10;
                                if (info.Action.HasFlag(ActionFlag.Unk2048))
                                {
                                    wpnPrimary = equip.WpnSecondary;
                                }
                                if (info.Action.HasFlag(ActionFlag.Unk4096))
                                {
                                    wpnPrimary = equip.WpnSecondary;
                                }
                            }
                            ObjectHitInfo item = new ObjectHitInfo(6);
                            item.ObjId = player.Slot;
                            item.WeaponId = wpnPrimary;
                            item.Accessory = num3;
                            item.Extensions = num4;
                            objs.Add(item);
                        }
                    }
                    ActionState.WriteInfo(packet2, info);
                    info = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
                {
                    num += 2;
                    Animation.WriteInfo(packet2, Animation.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
                {
                    num += 0x8000000;
                    PosRotationInfo info4 = PosRotation.ReadInfo(actionModel_0, c, false);
                    if (player != null)
                    {
                        player.Position = new Half3(info4.RotationX, info4.RotationY, info4.RotationZ);
                    }
                    actionModel_0.Flag |= UdpGameEvent.SoundPosRotation;
                    PosRotation.WriteInfo(packet2, info4);
                    info4 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
                {
                    num += 0x800000;
                    SoundPosRotation.WriteInfo(packet2, SoundPosRotation.ReadInfo(actionModel_0, c, float_0, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
                {
                    num += 4;
                    List<UseObjectInfo> list2 = UseObject.ReadInfo(actionModel_0, c, false);
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= list2.Count)
                        {
                            UseObject.WriteInfo(packet2, list2);
                            list2 = null;
                            break;
                        }
                        UseObjectInfo info6 = list2[num6];
                        if (roomModel_0.BotMode || (info6.ObjectId == 0xffff))
                        {
                            AllUtils.RemoveHit(list2, num6--);
                        }
                        else
                        {
                            ObjectInfo info7 = roomModel_0.GetObject(info6.ObjectId);
                            if (info7 != null)
                            {
                                bool flag = false;
                                if (info6.SpaceFlags.HasFlag(CharaMoves.HeliInMove) && info7.UseDate.ToString("yyMMddHHmm").Equals("0101010000"))
                                {
                                    flag = true;
                                }
                                info6.SpaceFlags.HasFlag(CharaMoves.HeliUnknown);
                                info6.SpaceFlags.HasFlag(CharaMoves.HeliLeave);
                                if (info6.SpaceFlags.HasFlag(CharaMoves.HeliStopped))
                                {
                                    AnimModel animation = info7.Animation;
                                    if ((animation != null) && (animation.Id == 0))
                                    {
                                        info7.Model.GetAnim(animation.NextAnim, 0f, 0f, info7);
                                    }
                                }
                                if (!flag)
                                {
                                    ObjectHitInfo info30 = new ObjectHitInfo(3);
                                    info30.ObjSyncId = 1;
                                    info30.ObjId = info7.Id;
                                    info30.ObjLife = info7.Life;
                                    info30.AnimId1 = 0xff;
                                    info30.AnimId2 = (info7.Animation != null) ? info7.Animation.Id : 0xff;
                                    ObjectHitInfo item = info30;
                                    item.SpecialUse = AllUtils.GetDuration(info7.UseDate);
                                    objs.Add(item);
                                }
                            }
                        }
                        num6++;
                    }
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
                {
                    num += (uint) 0x10;
                    ActionObjectInfo info9 = ActionForObjectSync.ReadInfo(actionModel_0, c, false);
                    if (player != null)
                    {
                        roomModel_0.SyncInfo(objs, 1);
                    }
                    ActionForObjectSync.WriteInfo(packet2, info9);
                    info9 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
                {
                    num += (uint) 0x20;
                    RadioChat.WriteInfo(packet2, RadioChat.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
                {
                    num += 0x4000000;
                    WeaponSyncInfo info11 = WeaponSync.ReadInfo(actionModel_0, c, false, false);
                    if (player != null)
                    {
                        player.WeaponId = info11.WeaponId;
                        player.Accessory = info11.Accessory;
                        player.Extensions = info11.Extensions;
                        player.WeaponClass = info11.WeaponClass;
                        roomModel_0.SyncInfo(objs, 2);
                    }
                    WeaponSync.WriteInfo(packet2, info11);
                    info11 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
                {
                    num += 0x80;
                    WeaponRecoil.WriteInfo(packet2, WeaponRecoil.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
                {
                    num += 8;
                    HpSync.WriteInfo(packet2, HpSync.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
                {
                    num += 0x100000;
                    List<SuicideInfo> list = Suicide.ReadInfo(actionModel_0, c, false, false);
                    List<DeathServerData> deaths = new List<DeathServerData>();
                    if (player == null)
                    {
                        list = new List<SuicideInfo>();
                    }
                    else
                    {
                        int objectId = -1;
                        int weaponId = 0;
                        int num9 = 0;
                        while (true)
                        {
                            if (num9 >= list.Count)
                            {
                                if (deaths.Count > 0)
                                {
                                    SendMatchInfo.SendDeathSync(roomModel_0, player, objectId, weaponId, deaths);
                                }
                                deaths = null;
                                break;
                            }
                            SuicideInfo info14 = list[num9];
                            if (player.Dead || (player.Life <= 0))
                            {
                                AllUtils.RemoveHit(list, num9--);
                            }
                            else
                            {
                                int hitDamageBot = AllUtils.GetHitDamageBot(info14.HitInfo);
                                int killerId = AllUtils.GetKillerId(info14.HitInfo);
                                int objectType = AllUtils.GetObjectType(info14.HitInfo);
                                CharaHitPart hitPart = ((CharaHitPart) (info14.HitInfo >> 4)) & ((CharaHitPart) 0x3f);
                                CharaDeath charaDeath = AllUtils.GetCharaDeath(info14.HitInfo);
                                if ((objectType == 1) || (objectType == 0))
                                {
                                    objectId = killerId;
                                }
                                weaponId = info14.WeaponId;
                                DamageManager.SimpleDeath(roomModel_0, deaths, objs, player, player, hitDamageBot, weaponId, hitPart, charaDeath);
                                if (hitDamageBot > 0)
                                {
                                    if (ConfigLoader.UseHitMarker)
                                    {
                                        SendMatchInfo.SendHitMarkerSync(roomModel_0, player, charaDeath, HitType.Normal, hitDamageBot);
                                    }
                                    ObjectHitInfo item = new ObjectHitInfo(2);
                                    item.ObjId = player.Slot;
                                    item.ObjLife = player.Life;
                                    item.HitPart = hitPart;
                                    item.KillerSlot = objectId;
                                    item.Position = info14.PlayerPos;
                                    item.DeathType = charaDeath;
                                    item.WeaponId = weaponId;
                                    objs.Add(item);
                                }
                            }
                            num9++;
                        }
                    }
                    Suicide.WriteInfo(packet2, list);
                    deaths = null;
                    list = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.MissionData))
                {
                    num += 0x800;
                    MissionDataInfo info16 = MissionData.ReadInfo(actionModel_0, c, float_0, false, false);
                    if ((roomModel_0.Map != null) && ((player != null) && (!player.Dead && ((info16.PlantTime > 0f) && !info16.BombEnum.HasFlag(BombFlag.Stop)))))
                    {
                        BombPosition bomb = roomModel_0.Map.GetBomb(info16.BombId);
                        if (bomb != null)
                        {
                            bool flag2;
                            Vector3 vector = (flag2 = info16.BombEnum.HasFlag(BombFlag.Defuse)) ? ((Vector3) roomModel_0.BombPosition) : (info16.BombEnum.HasFlag(BombFlag.Start) ? ((Vector3) bomb.Position) : ((Vector3) new Half3(0, 0, 0)));
                            double num13 = Vector3.Distance((Vector3) player.Position, vector);
                            TeamEnum swappedTeam = AllUtils.GetSwappedTeam(player, roomModel_0);
                            if ((bomb.EveryWhere || (num13 <= 2.0)) && (((swappedTeam == TeamEnum.CT_TEAM) & flag2) || ((swappedTeam == TeamEnum.FR_TEAM) && !flag2)))
                            {
                                if (player.C4Time != info16.PlantTime)
                                {
                                    player.C4First = DateTimeUtil.Now();
                                    player.C4Time = info16.PlantTime;
                                }
                                double duration = ComDiv.GetDuration(player.C4First);
                                float num15 = flag2 ? player.DefuseDuration : player.PlantDuration;
                                if (((float_0 >= (info16.PlantTime + num15)) || (duration >= num15)) && ((!roomModel_0.HasC4 && info16.BombEnum.HasFlag(BombFlag.Start)) || (roomModel_0.HasC4 & flag2)))
                                {
                                    roomModel_0.HasC4 = !roomModel_0.HasC4;
                                    info16.Bomb |= 2;
                                    SendMatchInfo.SendBombSync(roomModel_0, player, (int) info16.BombEnum.HasFlag(BombFlag.Defuse), info16.BombId);
                                }
                            }
                        }
                    }
                    MissionData.WriteInfo(packet2, info16);
                    info16 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
                {
                    num += 0x400000;
                    DropWeaponInfo info17 = DropWeapon.ReadInfo(actionModel_0, c, false);
                    if (!roomModel_0.BotMode)
                    {
                        roomModel_0.DropCounter = (byte) (roomModel_0.DropCounter + 1);
                        if (roomModel_0.DropCounter > ConfigLoader.MaxDropWpnCount)
                        {
                            roomModel_0.DropCounter = 0;
                        }
                        info17.Counter = roomModel_0.DropCounter;
                        Equipment equip = player.Equip;
                        if ((player != null) && (equip != null))
                        {
                            int num1 = ComDiv.GetIdStatics(info17.WeaponId, 1);
                            if (num1 == 1)
                            {
                                equip.WpnPrimary = 0;
                            }
                            if (num1 == 2)
                            {
                                equip.WpnSecondary = 0;
                            }
                        }
                    }
                    DropWeapon.WriteInfo(packet2, info17);
                    info17 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
                {
                    num += 0x1000000;
                    WeaponClient client = GetWeaponForClient.ReadInfo(actionModel_0, c, false);
                    if (!roomModel_0.BotMode)
                    {
                        Equipment equip = player.Equip;
                        if ((player != null) && (equip != null))
                        {
                            int num26 = ComDiv.GetIdStatics(client.WeaponId, 1);
                            if (num26 == 1)
                            {
                                equip.WpnPrimary = client.WeaponId;
                            }
                            if (num26 == 2)
                            {
                                equip.WpnSecondary = client.WeaponId;
                            }
                        }
                    }
                    GetWeaponForClient.WriteInfo(packet2, client);
                    client = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
                {
                    num += 0x2000000;
                    FireData.WriteInfo(packet2, FireData.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
                {
                    num += 0x400;
                    CharaFireNHitData.WriteInfo(packet2, CharaFireNHitData.ReadInfo(actionModel_0, c, false, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.HitData))
                {
                    num += 0x20000;
                    List<HitDataInfo> list = HitData.ReadInfo(actionModel_0, c, false, false);
                    List<DeathServerData> deaths = new List<DeathServerData>();
                    if (player == null)
                    {
                        list = new List<HitDataInfo>();
                    }
                    else
                    {
                        int weaponId = 0;
                        int num17 = 0;
                        while (true)
                        {
                            if (num17 >= list.Count)
                            {
                                if (deaths.Count > 0)
                                {
                                    SendMatchInfo.SendDeathSync(roomModel_0, player, 0xff, weaponId, deaths);
                                }
                                break;
                            }
                            HitDataInfo hit = list[num17];
                            if ((hit.HitEnum != HitType.HelmetProtection) && (hit.HitEnum != HitType.HeadshotProtection))
                            {
                                int num18;
                                if (!AllUtils.ValidateHitData(AllUtils.GetHitDamageNormal(hit.HitIndex), hit, out num18))
                                {
                                    AllUtils.RemoveHit(list, num17--);
                                }
                                else
                                {
                                    int objectId = hit.ObjectId;
                                    CharaHitPart hitPart = AllUtils.GetHitPart(hit.HitIndex);
                                    CharaDeath dEFAULT = CharaDeath.DEFAULT;
                                    weaponId = hit.WeaponId;
                                    ObjectType hitType = AllUtils.GetHitType(hit.HitIndex);
                                    if (hitType == ObjectType.Object)
                                    {
                                        ObjectInfo objI = roomModel_0.GetObject(objectId);
                                        ObjectModel model = objI?.Model;
                                        if ((model == null) || !model.Destroyable)
                                        {
                                            if (ConfigLoader.SendFailMsg && (model == null))
                                            {
                                                CLogger.Print($"Fire Obj: {objectId} Map: {roomModel_0.MapId} Invalid Object.", LoggerType.Warning, null);
                                                player.LogPlayerPos(hit.EndBullet);
                                            }
                                        }
                                        else if (objI.Life > 0)
                                        {
                                            objI.Life -= num18;
                                            if (objI.Life <= 0)
                                            {
                                                objI.Life = 0;
                                                DamageManager.BoomDeath(roomModel_0, player, num18, weaponId, deaths, objs, hit.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
                                            }
                                            objI.DestroyState = model.CheckDestroyState(objI.Life);
                                            DamageManager.SabotageDestroy(roomModel_0, player, model, objI, num18);
                                            float duration = AllUtils.GetDuration(objI.UseDate);
                                            if ((objI.Animation != null) && ((objI.Animation.Duration > 0f) && (duration >= objI.Animation.Duration)))
                                            {
                                                objI.Model.GetAnim(objI.Animation.NextAnim, duration, objI.Animation.Duration, objI);
                                            }
                                            ObjectHitInfo info32 = new ObjectHitInfo(model.UpdateId);
                                            info32.ObjId = objI.Id;
                                            info32.ObjLife = objI.Life;
                                            info32.KillerSlot = actionModel_0.Slot;
                                            info32.ObjSyncId = (int) model.NeedSync;
                                            info32.SpecialUse = duration;
                                            info32.AnimId1 = model.Animation;
                                            info32.AnimId2 = (objI.Animation != null) ? objI.Animation.Id : 0xff;
                                            ObjectHitInfo item = info32;
                                            item.DestroyState = objI.DestroyState;
                                            objs.Add(item);
                                        }
                                    }
                                    else if (hitType != ObjectType.User)
                                    {
                                        if (hitType != ObjectType.UserObject)
                                        {
                                            CLogger.Print($"HitType: ({hitType}/{(int) hitType}) Slot: {actionModel_0.Slot}", LoggerType.Warning, null);
                                            CLogger.Print($"BoomPlayers: {hit.BoomInfo} {hit.BoomPlayers.Count}", LoggerType.Warning, null);
                                        }
                                    }
                                    else
                                    {
                                        PlayerModel model4;
                                        if (!roomModel_0.GetPlayer(objectId, out model4) || (!player.RespawnIsValid() || (player.Dead || (model4.Dead || model4.Immortal))))
                                        {
                                            AllUtils.RemoveHit(list, num17--);
                                        }
                                        else
                                        {
                                            if (hitPart == CharaHitPart.HEAD)
                                            {
                                                dEFAULT = CharaDeath.HEADSHOT;
                                            }
                                            if ((roomModel_0.RoomType == RoomCondition.DeathMatch) && ((roomModel_0.Rule == MapRules.HeadHunter) && (dEFAULT != CharaDeath.HEADSHOT)))
                                            {
                                                num18 = 1;
                                            }
                                            else if ((roomModel_0.RoomType == RoomCondition.Boss) && (dEFAULT == CharaDeath.HEADSHOT))
                                            {
                                                if (((roomModel_0.LastRound == 1) && (model4.Team == TeamEnum.FR_TEAM)) || ((roomModel_0.LastRound == 2) && (model4.Team == TeamEnum.CT_TEAM)))
                                                {
                                                    num18 /= 10;
                                                }
                                            }
                                            else if ((roomModel_0.RoomType == RoomCondition.DeathMatch) && (roomModel_0.Rule == MapRules.Chaos))
                                            {
                                                num18 = 200;
                                            }
                                            DamageManager.SimpleDeath(roomModel_0, deaths, objs, player, model4, num18, weaponId, hitPart, dEFAULT);
                                            if (num18 > 0)
                                            {
                                                if (ConfigLoader.UseHitMarker)
                                                {
                                                    SendMatchInfo.SendHitMarkerSync(roomModel_0, player, dEFAULT, hit.HitEnum, num18);
                                                }
                                                ObjectHitInfo item = new ObjectHitInfo(2);
                                                item.ObjId = model4.Slot;
                                                item.ObjLife = model4.Life;
                                                item.HitPart = hitPart;
                                                item.KillerSlot = player.Slot;
                                                item.Position = model4.Position - player.Position;
                                                item.DeathType = dEFAULT;
                                                item.WeaponId = weaponId;
                                                objs.Add(item);
                                            }
                                        }
                                    }
                                }
                            }
                            num17++;
                        }
                    }
                    HitData.WriteInfo(packet2, list);
                    deaths = null;
                    list = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.GrenadeHit))
                {
                    num += 0x10000000;
                    List<GrenadeHitInfo> list = GrenadeHit.ReadInfo(actionModel_0, c, false, false);
                    List<DeathServerData> deaths = new List<DeathServerData>();
                    if (player == null)
                    {
                        list = new List<GrenadeHitInfo>();
                    }
                    else
                    {
                        int num21 = -1;
                        int weaponId = 0;
                        int num23 = 0;
                        while (true)
                        {
                            int num24;
                            if (num23 >= list.Count)
                            {
                                if (deaths.Count > 0)
                                {
                                    SendMatchInfo.SendDeathSync(roomModel_0, player, 0xff, weaponId, deaths);
                                }
                                break;
                            }
                            GrenadeHitInfo hit = list[num23];
                            if (!AllUtils.ValidateGrenadeHit(AllUtils.GetHitDamageNormal(hit.HitInfo), hit, out num24))
                            {
                                AllUtils.RemoveHit(list, num23--);
                            }
                            else
                            {
                                int objectId = hit.ObjectId;
                                CharaHitPart hitPart = AllUtils.GetHitPart(hit.HitInfo);
                                weaponId = hit.WeaponId;
                                ObjectType hitType = AllUtils.GetHitType(hit.HitInfo);
                                if (hitType == ObjectType.Object)
                                {
                                    ObjectInfo objI = roomModel_0.GetObject(objectId);
                                    ObjectModel model = objI?.Model;
                                    if ((model == null) || (!model.Destroyable || (objI.Life <= 0)))
                                    {
                                        if (ConfigLoader.SendFailMsg && (model == null))
                                        {
                                            CLogger.Print($"Boom Obj: {objectId} Map: {roomModel_0.MapId} Invalid Object.", LoggerType.Warning, null);
                                            player.LogPlayerPos(hit.HitPos);
                                        }
                                    }
                                    else
                                    {
                                        objI.Life -= num24;
                                        if (objI.Life <= 0)
                                        {
                                            objI.Life = 0;
                                            DamageManager.BoomDeath(roomModel_0, player, num24, weaponId, deaths, objs, hit.BoomPlayers, CharaHitPart.ALL, CharaDeath.OBJECT_EXPLOSION);
                                        }
                                        objI.DestroyState = model.CheckDestroyState(objI.Life);
                                        DamageManager.SabotageDestroy(roomModel_0, player, model, objI, num24);
                                        if (num24 > 0)
                                        {
                                            ObjectHitInfo info34 = new ObjectHitInfo(model.UpdateId);
                                            info34.ObjId = objI.Id;
                                            info34.ObjLife = objI.Life;
                                            info34.KillerSlot = actionModel_0.Slot;
                                            info34.ObjSyncId = (int) model.NeedSync;
                                            info34.AnimId1 = model.Animation;
                                            info34.AnimId2 = (objI.Animation != null) ? objI.Animation.Id : 0xff;
                                            ObjectHitInfo item = info34;
                                            item.DestroyState = objI.DestroyState;
                                            item.SpecialUse = AllUtils.GetDuration(objI.UseDate);
                                            objs.Add(item);
                                        }
                                    }
                                }
                                else if (hitType != ObjectType.User)
                                {
                                    if (hitType != ObjectType.UserObject)
                                    {
                                        CLogger.Print($"Grenade Boom, HitType: ({hitType}/{(int) hitType})", LoggerType.Warning, null);
                                    }
                                }
                                else
                                {
                                    PlayerModel model6;
                                    num21++;
                                    if ((num24 <= 0) || (!roomModel_0.GetPlayer(objectId, out model6) || (!player.RespawnIsValid() || (model6.Dead || model6.Immortal))))
                                    {
                                        AllUtils.RemoveHit(list, num23--);
                                    }
                                    else
                                    {
                                        TeamEnum enum3 = ((num21 % 2) == 0) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM;
                                        if (hit.DeathType == CharaDeath.MEDICAL_KIT)
                                        {
                                            model6.Life += num24;
                                            model6.CheckLifeValue();
                                        }
                                        else if ((hit.DeathType != CharaDeath.BOOM) || ((ClassType.Dino == hit.WeaponClass) || ((enum3 != TeamEnum.FR_TEAM) && (enum3 != TeamEnum.CT_TEAM))))
                                        {
                                            DamageManager.SimpleDeath(roomModel_0, deaths, objs, player, model6, num24, weaponId, hitPart, hit.DeathType);
                                        }
                                        else
                                        {
                                            num24 = (int) Math.Ceiling((double) (((double) num24) / 2.7));
                                            DamageManager.SimpleDeath(roomModel_0, deaths, objs, player, model6, num24, weaponId, hitPart, hit.DeathType);
                                        }
                                        if (num24 > 0)
                                        {
                                            if (ConfigLoader.UseHitMarker)
                                            {
                                                SendMatchInfo.SendHitMarkerSync(roomModel_0, player, hit.DeathType, hit.HitEnum, num24);
                                            }
                                            ObjectHitInfo item = new ObjectHitInfo(2);
                                            item.ObjId = model6.Slot;
                                            item.ObjLife = model6.Life;
                                            item.HitPart = hitPart;
                                            item.KillerSlot = player.Slot;
                                            item.Position = model6.Position - player.Position;
                                            item.DeathType = hit.DeathType;
                                            item.WeaponId = weaponId;
                                            objs.Add(item);
                                        }
                                    }
                                }
                            }
                            num23++;
                        }
                    }
                    GrenadeHit.WriteInfo(packet2, list);
                    deaths = null;
                    list = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
                {
                    num += 0x200;
                    GetWeaponForHost.WriteInfo(packet2, GetWeaponForHost.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
                {
                    num += 0x40000000;
                    FireDataOnObject.WriteInfo(packet2, FireDataOnObject.ReadInfo(actionModel_0, c, false));
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireNHitDataOnObject))
                {
                    num += 0x2000;
                    FireNHitDataObjectInfo info28 = FireNHitDataOnObject.ReadInfo(actionModel_0, c, false);
                    if ((player != null) && !player.Dead)
                    {
                        SendMatchInfo.SendPortalPass(roomModel_0, player, info28.Portal);
                    }
                    FireNHitDataOnObject.WriteInfo(packet2, info28);
                    info28 = null;
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.SeizeDataForClient))
                {
                    num += 0x8000;
                    SeizeDataForClientInfo info29 = SeizeDataForClient.ReadInfo(actionModel_0, c, true);
                    if (player == null)
                    {
                    }
                    SeizeDataForClient.WriteInfo(packet2, info29);
                    info29 = null;
                }
                byte_0 = packet2.ToArray();
                if ((num != actionModel_0.Flag) && ConfigLoader.IsTestMode)
                {
                    CLogger.Print(Bitwise.ToHexData($"PVP - Missing Flag Events: '{(uint) actionModel_0.Flag}' | '{((uint) actionModel_0.Flag) - num}'", data), LoggerType.Opcode, null);
                }
                return objs;
            }
        }

        private List<ObjectHitInfo> method_1(ActionModel actionModel_0, RoomModel roomModel_0, out byte[] byte_0)
        {
            byte_0 = new byte[0];
            if (roomModel_0 == null)
            {
                return null;
            }
            if (actionModel_0.Data.Length == 0)
            {
                return new List<ObjectHitInfo>();
            }
            byte[] data = actionModel_0.Data;
            List<ObjectHitInfo> objs = new List<ObjectHitInfo>();
            SyncClientPacket packet = new SyncClientPacket(data);
            using (SyncServerPacket packet2 = new SyncServerPacket())
            {
                uint num = 0;
                PlayerModel player = roomModel_0.GetPlayer(actionModel_0.Slot, false);
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionState))
                {
                    num += 0x100;
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.Animation))
                {
                    num += 2;
                    packet2.WriteH(packet.ReadUH());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.PosRotation))
                {
                    num += 0x8000000;
                    ushort num4 = packet.ReadUH();
                    ushort num5 = packet.ReadUH();
                    ushort num6 = packet.ReadUH();
                    ushort num7 = packet.ReadUH();
                    ushort num8 = packet.ReadUH();
                    ushort num9 = packet.ReadUH();
                    if (player != null)
                    {
                        player.Position = new Half3(num8, num9, num7);
                    }
                    packet2.WriteH(num4);
                    packet2.WriteH(num5);
                    packet2.WriteH(num6);
                    packet2.WriteH(num7);
                    packet2.WriteH(num8);
                    packet2.WriteH(num9);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.SoundPosRotation))
                {
                    num += 0x800000;
                    packet2.WriteT(packet.ReadT());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.UseObject))
                {
                    num += 4;
                    byte num11 = packet.ReadC();
                    packet2.WriteC(num11);
                    for (int i = 0; i < num11; i++)
                    {
                        ushort num13 = packet.ReadUH();
                        byte num14 = packet.ReadC();
                        CharaMoves moves = (CharaMoves) packet.ReadC();
                        packet2.WriteH(num13);
                        packet2.WriteC(num14);
                        packet2.WriteC((byte) moves);
                    }
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.ActionForObjectSync))
                {
                    num += (uint) 0x10;
                    byte num15 = packet.ReadC();
                    byte num16 = packet.ReadC();
                    if (player != null)
                    {
                        roomModel_0.SyncInfo(objs, 1);
                    }
                    packet2.WriteC(num15);
                    packet2.WriteC(num16);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.RadioChat))
                {
                    num += (uint) 0x20;
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponSync))
                {
                    num += 0x4000000;
                    int num19 = packet.ReadD();
                    byte num20 = packet.ReadC();
                    byte num21 = packet.ReadC();
                    if (player != null)
                    {
                        player.WeaponId = num19;
                        player.Accessory = num20;
                        player.Extensions = num21;
                        player.WeaponClass = (ClassType) ComDiv.GetIdStatics(num19, 2);
                        roomModel_0.SyncInfo(objs, 2);
                    }
                    packet2.WriteD(num19);
                    packet2.WriteC(num20);
                    packet2.WriteC(num21);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.WeaponRecoil))
                {
                    num += 0x80;
                    int num28 = packet.ReadD();
                    byte num29 = packet.ReadC();
                    byte num30 = packet.ReadC();
                    CLogger.Print($"PVE (WeaponRecoil); Slot: {player.Slot}; Weapon Id: {num28}; Extensions: {num30}; Unknowns: {num29}", LoggerType.Warning, null);
                    packet2.WriteT(packet.ReadT());
                    packet2.WriteT(packet.ReadT());
                    packet2.WriteT(packet.ReadT());
                    packet2.WriteT(packet.ReadT());
                    packet2.WriteT(packet.ReadT());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteD(num28);
                    packet2.WriteC(num29);
                    packet2.WriteC(num30);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.HpSync))
                {
                    num += 8;
                    packet2.WriteH(packet.ReadUH());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.Suicide))
                {
                    num += 0x100000;
                    byte num32 = packet.ReadC();
                    packet2.WriteC(num32);
                    for (int i = 0; i < num32; i++)
                    {
                        Half3 half = packet.ReadUHV();
                        int num34 = packet.ReadD();
                        byte num35 = packet.ReadC();
                        byte num36 = packet.ReadC();
                        uint num37 = packet.ReadUD();
                        packet2.WriteHV(half);
                        packet2.WriteD(num34);
                        packet2.WriteC(num35);
                        packet2.WriteC(num36);
                        packet2.WriteD(num37);
                    }
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.DropWeapon))
                {
                    num += 0x400000;
                    ushort num38 = packet.ReadUH();
                    ushort num39 = packet.ReadUH();
                    ushort num40 = packet.ReadUH();
                    ushort num41 = packet.ReadUH();
                    ushort num42 = packet.ReadUH();
                    ushort num43 = packet.ReadUH();
                    byte num44 = packet.ReadC();
                    int num45 = packet.ReadD();
                    byte num46 = packet.ReadC();
                    byte num47 = packet.ReadC();
                    if (ConfigLoader.UseMaxAmmoInDrop)
                    {
                        packet2.WriteH((ushort) 0xffff);
                        packet2.WriteH(num39);
                        packet2.WriteH((short) 0x2710);
                    }
                    else
                    {
                        packet2.WriteH(num38);
                        packet2.WriteH(num39);
                        packet2.WriteH(num40);
                    }
                    packet2.WriteH(num41);
                    packet2.WriteH(num42);
                    packet2.WriteH(num43);
                    packet2.WriteC(num44);
                    packet2.WriteD(num45);
                    packet2.WriteC(num46);
                    packet2.WriteC(num47);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForClient))
                {
                    num += 0x1000000;
                    ushort num48 = packet.ReadUH();
                    ushort num49 = packet.ReadUH();
                    ushort num50 = packet.ReadUH();
                    ushort num51 = packet.ReadUH();
                    ushort num52 = packet.ReadUH();
                    ushort num53 = packet.ReadUH();
                    byte num54 = packet.ReadC();
                    int num55 = packet.ReadD();
                    byte num56 = packet.ReadC();
                    byte num57 = packet.ReadC();
                    if (ConfigLoader.UseMaxAmmoInDrop)
                    {
                        packet2.WriteH((ushort) 0xffff);
                        packet2.WriteH(num49);
                        packet2.WriteH((short) 0x2710);
                    }
                    else
                    {
                        packet2.WriteH(num48);
                        packet2.WriteH(num49);
                        packet2.WriteH(num50);
                    }
                    packet2.WriteH(num51);
                    packet2.WriteH(num52);
                    packet2.WriteH(num53);
                    packet2.WriteC(num54);
                    packet2.WriteD(num55);
                    packet2.WriteC(num56);
                    packet2.WriteC(num57);
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireData))
                {
                    num += 0x2000000;
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteH(packet.ReadH());
                    packet2.WriteD(packet.ReadD());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteH(packet.ReadUH());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.CharaFireNHitData))
                {
                    num += 0x400;
                    byte num67 = packet.ReadC();
                    packet2.WriteC(num67);
                    for (int i = 0; i < num67; i++)
                    {
                        int num69 = packet.ReadD();
                        byte num70 = packet.ReadC();
                        byte num71 = packet.ReadC();
                        ushort num72 = packet.ReadUH();
                        uint num73 = packet.ReadUD();
                        ushort num74 = packet.ReadUH();
                        ushort num75 = packet.ReadUH();
                        ushort num76 = packet.ReadUH();
                        packet2.WriteD(num69);
                        packet2.WriteC(num70);
                        packet2.WriteC(num71);
                        packet2.WriteH(num72);
                        packet2.WriteD(num73);
                        packet2.WriteH(num74);
                        packet2.WriteH(num75);
                        packet2.WriteH(num76);
                    }
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.GetWeaponForHost))
                {
                    num += 0x200;
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteH(packet.ReadUH());
                    packet2.WriteD(packet.ReadD());
                    packet2.WriteC(packet.ReadC());
                }
                if (actionModel_0.Flag.HasFlag(UdpGameEvent.FireDataOnObject))
                {
                    num += 0x40000000;
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                    packet2.WriteC(packet.ReadC());
                }
                byte_0 = packet2.ToArray();
                if (num != actionModel_0.Flag)
                {
                    CLogger.Print(Bitwise.ToHexData($"PVE - Missing Flag Events: '{(uint) actionModel_0.Flag}' | '{((uint) actionModel_0.Flag) - num}'", data), LoggerType.Opcode, null);
                }
                return objs;
            }
        }

        private void method_2(IAsyncResult iasyncResult_0)
        {
            try
            {
                Socket asyncState = iasyncResult_0.AsyncState as Socket;
                if ((asyncState != null) && asyncState.Connected)
                {
                    asyncState.EndSend(iasyncResult_0);
                }
            }
            catch (SocketException exception)
            {
                CLogger.Print($"Socket Error on Send: {exception.SocketErrorCode}", LoggerType.Warning, null);
            }
            catch (ObjectDisposedException)
            {
                CLogger.Print("Socket was closed while sending.", LoggerType.Warning, null);
            }
            catch (Exception exception2)
            {
                CLogger.Print("Error during EndSendCallback: " + exception2.Message, LoggerType.Error, exception2);
            }
        }

        public void ReadPacket(PacketModel Packet)
        {
            byte[] withEndData = Packet.WithEndData;
            byte[] withoutEndData = Packet.WithoutEndData;
            SyncClientPacket packet = new SyncClientPacket(withEndData);
            int length = withoutEndData.Length;
            try
            {
                int slotId = 0;
                RoomModel room = null;
                int opcode = Packet.Opcode;
                if (opcode > 0x41)
                {
                    if (opcode > 0x61)
                    {
                        if (opcode == 0x83)
                        {
                            packet.Advance(length);
                            slotId = packet.ReadC();
                            room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                            if (room != null)
                            {
                                PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
                                if ((player != null) && (player.PlayerIdByServer == Packet.AccountId))
                                {
                                    room.BotMode = true;
                                    byte[] data = AllUtils.BaseWriteCode(0x84, PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData), slotId, AllUtils.GetDuration(room.GetPlayer(slotId, false).StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
                                    foreach (PlayerModel model7 in room.Players)
                                    {
                                        if ((model7.Client != null) && (player.AccountIdIsValid() && (model7.Slot != Packet.Slot)))
                                        {
                                            this.SendPacket(data, model7.Client);
                                        }
                                    }
                                }
                            }
                            return;
                        }
                        else if (opcode == 0x84)
                        {
                            packet.Advance(length);
                            slotId = packet.ReadC();
                            room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                            if (room != null)
                            {
                                PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
                                if ((player != null) && (player.PlayerIdByServer == Packet.AccountId))
                                {
                                    room.BotMode = true;
                                    byte[] data = AllUtils.BaseWriteCode(0x84, PROTOCOL_BOTS_ACTION.GET_CODE(withoutEndData), Packet.Slot, AllUtils.GetDuration(player.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
                                    foreach (PlayerModel model4 in room.Players)
                                    {
                                        if ((model4.Client != null) && (player.AccountIdIsValid() && (model4.Slot != Packet.Slot)))
                                        {
                                            this.SendPacket(data, model4.Client);
                                        }
                                    }
                                }
                            }
                            return;
                        }
                    }
                    else if (opcode == 0x43)
                    {
                        string udp = $"{packet.ReadH()}.{packet.ReadH()}";
                        slotId = packet.ReadC();
                        room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                        if (room != null)
                        {
                            if (room.RemovePlayer(this.ipendPoint_0, Packet, udp) && ConfigLoader.IsTestMode)
                            {
                                CLogger.Print($"Player Disconnected. [{this.ipendPoint_0.Address}:{this.ipendPoint_0.Port}]", LoggerType.Warning, null);
                            }
                            if (room.GetPlayersCount() == 0)
                            {
                                RoomsManager.RemoveRoom(room.UniqueRoomId, room.RoomSeed);
                            }
                        }
                        return;
                    }
                    else if (opcode == 0x61)
                    {
                        slotId = packet.ReadC();
                        room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                        byte[] data = Packet.Data;
                        if (room != null)
                        {
                            PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
                            if (player != null)
                            {
                                player.LastPing = Packet.ReceiveDate;
                                this.SendPacket(data, this.ipendPoint_0);
                                if (ConfigLoader.SendPingSync)
                                {
                                    int num6;
                                    player.Latency = AllUtils.PingTime($"{this.ipendPoint_0.Address}", data, this.socket_0.Ttl, 120, false, out num6);
                                    player.Ping = num6;
                                    SendMatchInfo.SendPingSync(room, player);
                                }
                            }
                        }
                        return;
                    }
                }
                else
                {
                    DateTime time;
                    if (opcode == 3)
                    {
                        packet.Advance(length);
                        slotId = packet.ReadC();
                        room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                        if (room != null)
                        {
                            PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
                            if ((player != null) && (player.PlayerIdByServer == Packet.AccountId))
                            {
                                player.RespawnByUser = Packet.Respawn;
                                time = new DateTime();
                                if (!(room.StartTime == time))
                                {
                                    byte[] data = AllUtils.BaseWriteCode(4, room.BotMode ? this.WriteBotActionData(withoutEndData, room) : this.WritePlayerActionData(withoutEndData, room, AllUtils.GetDuration(player.StartTime), Packet), room.BotMode ? Packet.Slot : 0xff, AllUtils.GetDuration(room.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
                                    bool flag2 = !room.BotMode && (slotId != 0xff);
                                    foreach (PlayerModel model12 in room.Players)
                                    {
                                        bool flag3 = model12.Slot != Packet.Slot;
                                        if ((model12.Client != null) && (player.AccountIdIsValid() && ((((slotId == 0xff) & flag3) || (room.BotMode & flag3)) | flag2)))
                                        {
                                            this.SendPacket(data, model12.Client);
                                        }
                                    }
                                }
                            }
                        }
                        return;
                    }
                    else if (opcode == 4)
                    {
                        packet.Advance(length);
                        slotId = packet.ReadC();
                        room = RoomsManager.GetRoom(packet.ReadUD(), packet.ReadUD());
                        if (room != null)
                        {
                            PlayerModel player = room.GetPlayer(Packet.Slot, this.ipendPoint_0);
                            if ((player != null) && (player.PlayerIdByServer == Packet.AccountId))
                            {
                                player.RespawnByUser = Packet.Respawn;
                                room.BotMode = true;
                                time = new DateTime();
                                if (!(room.StartTime == time))
                                {
                                    byte[] data = AllUtils.BaseWriteCode(4, this.WriteBotActionData(withoutEndData, room), Packet.Slot, AllUtils.GetDuration(player.StartTime), Packet.Round, Packet.Respawn, Packet.RoundNumber, Packet.AccountId);
                                    foreach (PlayerModel model10 in room.Players)
                                    {
                                        bool flag = model10.Slot != Packet.Slot;
                                        if ((model10.Client != null) && (player.AccountIdIsValid() && ((slotId == 0xff) & flag)))
                                        {
                                            this.SendPacket(data, model10.Client);
                                        }
                                    }
                                }
                            }
                        }
                        return;
                    }
                    else if (opcode == 0x41)
                    {
                        string udp = $"{packet.ReadH()}.{packet.ReadH()}";
                        slotId = packet.ReadC();
                        room = RoomsManager.CreateOrGetRoom(packet.ReadUD(), packet.ReadUD());
                        if (room != null)
                        {
                            PlayerModel model2 = room.AddPlayer(this.ipendPoint_0, Packet, udp);
                            if (model2 != null)
                            {
                                if (!model2.Integrity)
                                {
                                    model2.ResetBattleInfos();
                                }
                                byte[] data = PROTOCOL_CONNECT.GET_CODE();
                                this.SendPacket(data, model2.Client);
                                if (ConfigLoader.IsTestMode)
                                {
                                    CLogger.Print($"Player Connected. [{model2.Client.Address}:{model2.Client.Port}]", LoggerType.Warning, null);
                                }
                            }
                        }
                        return;
                    }
                }
                CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{Packet.Opcode}]", withEndData), LoggerType.Opcode, null);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public void SendPacket(byte[] Data, IPEndPoint Address)
        {
            try
            {
                this.socket_0.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_2), this.socket_0);
            }
            catch (Exception exception)
            {
                CLogger.Print($"Failed to send package to {Address}: {exception.Message}", LoggerType.Error, exception);
            }
        }

        public byte[] WriteBotActionData(byte[] Data, RoomModel Room)
        {
            SyncClientPacket packet = new SyncClientPacket(Data);
            List<ObjectHitInfo> objs = new List<ObjectHitInfo>();
            using (SyncServerPacket packet2 = new SyncServerPacket())
            {
                int num = 0;
                goto TR_0023;
            TR_0006:
                if (objs.Count > 0)
                {
                    packet2.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(objs));
                }
                objs = null;
                return packet2.ToArray();
            TR_0023:
                while (true)
                {
                    if (num < 0x12)
                    {
                        ActionModel model = new ActionModel();
                        try
                        {
                            bool flag2;
                            bool flag = false;
                            model.Length = packet.ReadUH(out flag2);
                            if (!flag2)
                            {
                                model.Slot = packet.ReadUH();
                                model.SubHead = (UdpSubHead) packet.ReadC();
                                if (model.SubHead != ((UdpSubHead) 0xff))
                                {
                                    packet2.WriteH(model.Length);
                                    packet2.WriteH(model.Slot);
                                    packet2.WriteC((byte) model.SubHead);
                                    switch (model.SubHead)
                                    {
                                        case UdpSubHead.User:
                                        case UdpSubHead.StageInfoChara:
                                            byte[] buffer8;
                                            model.Flag = (UdpGameEvent) packet.ReadUD();
                                            model.Data = packet.ReadB(model.Length - 9);
                                            objs.AddRange(this.method_1(model, Room, out buffer8));
                                            packet2.GoBack(5);
                                            packet2.WriteH((ushort) (buffer8.Length + 9));
                                            packet2.WriteH(model.Slot);
                                            packet2.WriteC((byte) model.SubHead);
                                            packet2.WriteD((uint) model.Flag);
                                            packet2.WriteB(buffer8);
                                            if ((model.Data.Length == 0) && ((model.Length - 9) != 0))
                                            {
                                                flag = true;
                                            }
                                            break;

                                        case UdpSubHead.Grenade:
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteH(packet.ReadUH());
                                            packet2.WriteD(packet.ReadD());
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteC(packet.ReadC());
                                            packet2.WriteH(packet.ReadUH());
                                            packet2.WriteH(packet.ReadUH());
                                            packet2.WriteH(packet.ReadUH());
                                            packet2.WriteD(packet.ReadD());
                                            packet2.WriteD(packet.ReadD());
                                            packet2.WriteD(packet.ReadD());
                                            break;

                                        case UdpSubHead.DroppedWeapon:
                                            packet2.WriteB(packet.ReadB(0x1f));
                                            break;

                                        case UdpSubHead.ObjectStatic:
                                            packet2.WriteB(packet.ReadB(10));
                                            break;

                                        case UdpSubHead.ObjectMove:
                                            packet2.WriteB(packet.ReadB(0x10));
                                            break;

                                        case UdpSubHead.ObjectAnim:
                                            packet2.WriteB(packet.ReadB(8));
                                            break;

                                        case UdpSubHead.StageInfoObjectStatic:
                                            packet2.WriteB(packet.ReadB(1));
                                            break;

                                        case UdpSubHead.StageInfoObjectAnim:
                                            packet2.WriteB(packet.ReadB(9));
                                            break;

                                        case UdpSubHead.StageInfoObjectControl:
                                            packet2.WriteB(packet.ReadB(9));
                                            break;

                                        default:
                                            if (ConfigLoader.IsTestMode)
                                            {
                                                CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{model.SubHead}' or '{(int) model.SubHead}'", Data), LoggerType.Opcode, null);
                                            }
                                            break;
                                    }
                                    if (!flag)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            if (ConfigLoader.IsTestMode)
                            {
                                CLogger.Print($"PVE Action Data - Buffer (Length: {Data.Length}): | {exception.Message}", LoggerType.Error, exception);
                            }
                            objs = new List<ObjectHitInfo>();
                        }
                        goto TR_0006;
                    }
                    else
                    {
                        goto TR_0006;
                    }
                    break;
                }
                num++;
                goto TR_0023;
            }
        }

        public byte[] WritePlayerActionData(byte[] Data, RoomModel Room, float Time, PacketModel Packet)
        {
            SyncClientPacket c = new SyncClientPacket(Data);
            List<ObjectHitInfo> objs = new List<ObjectHitInfo>();
            using (SyncServerPacket packet2 = new SyncServerPacket())
            {
                int num = 0;
                goto TR_0023;
            TR_0006:
                if (objs.Count > 0)
                {
                    packet2.WriteB(PROTOCOL_EVENTS_ACTION.GET_CODE(objs));
                }
                objs = null;
                return packet2.ToArray();
            TR_0023:
                while (true)
                {
                    if (num < 0x12)
                    {
                        ActionModel action = new ActionModel();
                        try
                        {
                            bool flag2;
                            bool flag = false;
                            action.Length = c.ReadUH(out flag2);
                            if (!flag2)
                            {
                                action.Slot = c.ReadUH();
                                action.SubHead = (UdpSubHead) c.ReadC();
                                if (action.SubHead != ((UdpSubHead) 0xff))
                                {
                                    packet2.WriteH(action.Length);
                                    packet2.WriteH(action.Slot);
                                    packet2.WriteC((byte) action.SubHead);
                                    switch (action.SubHead)
                                    {
                                        case UdpSubHead.User:
                                        case UdpSubHead.StageInfoChara:
                                            byte[] buffer;
                                            action.Flag = (UdpGameEvent) c.ReadUD();
                                            action.Data = c.ReadB(action.Length - 9);
                                            AllUtils.CheckDataFlags(action, Packet);
                                            objs.AddRange(this.method_0(action, Room, Time, out buffer));
                                            packet2.GoBack(5);
                                            packet2.WriteH((ushort) (buffer.Length + 9));
                                            packet2.WriteH(action.Slot);
                                            packet2.WriteC((byte) action.SubHead);
                                            packet2.WriteD((uint) action.Flag);
                                            packet2.WriteB(buffer);
                                            if ((action.Data.Length == 0) && ((action.Length - 9) != 0))
                                            {
                                                flag = true;
                                            }
                                            break;

                                        case UdpSubHead.Grenade:
                                            GrenadeSync.WriteInfo(packet2, GrenadeSync.ReadInfo(c, false, false));
                                            break;

                                        case UdpSubHead.DroppedWeapon:
                                            DropedWeapon.WriteInfo(packet2, DropedWeapon.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.ObjectStatic:
                                            ObjectStatic.WriteInfo(packet2, ObjectStatic.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.ObjectMove:
                                            ObjectMove.WriteInfo(packet2, ObjectMove.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.ObjectAnim:
                                            ObjectAnim.WriteInfo(packet2, ObjectAnim.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.StageInfoObjectStatic:
                                            StageInfoObjStatic.WriteInfo(packet2, StageInfoObjStatic.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.StageInfoObjectAnim:
                                            StageInfoObjAnim.WriteInfo(packet2, StageInfoObjAnim.ReadInfo(c, false));
                                            break;

                                        case UdpSubHead.StageInfoObjectControl:
                                            StageInfoObjControl.WriteInfo(packet2, StageInfoObjControl.ReadInfo(c, false));
                                            break;

                                        default:
                                            if (ConfigLoader.IsTestMode)
                                            {
                                                CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{action.SubHead}' or '{(int) action.SubHead}'", Data), LoggerType.Opcode, null);
                                            }
                                            break;
                                    }
                                    if (!flag)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            if (ConfigLoader.IsTestMode)
                            {
                                CLogger.Print($"PVP Action Data - Buffer (Length: {Data.Length}): | {exception.Message}", LoggerType.Error, exception);
                            }
                            objs = new List<ObjectHitInfo>();
                        }
                        goto TR_0006;
                    }
                    else
                    {
                        goto TR_0006;
                    }
                    break;
                }
                num++;
                goto TR_0023;
            }
        }
    }
}

