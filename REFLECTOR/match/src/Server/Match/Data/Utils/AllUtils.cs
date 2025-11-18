namespace Server.Match.Data.Utils
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.SharpDX;
    using Plugin.Core.Utility;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using Server.Match.Data.Models.Event;
    using Server.Match.Data.XML;
    using System;
    using System.Collections;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;

    public static class AllUtils
    {
        public static byte[] BaseWriteCode(int Opcode, byte[] Actions, int SlotId, float Time, int Round, int Respawn, int RoundNumber, int AccountId)
        {
            int shift = ((0x11 + Actions.Length) % 6) + 1;
            byte[] buffer = Bitwise.Encrypt(Actions, shift);
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) Opcode);
                packet.WriteC((byte) SlotId);
                packet.WriteT(Time);
                packet.WriteC((byte) Round);
                packet.WriteH((ushort) (0x11 + buffer.Length));
                packet.WriteC((byte) Respawn);
                packet.WriteC((byte) RoundNumber);
                packet.WriteC((byte) AccountId);
                packet.WriteC(0);
                packet.WriteD(0);
                packet.WriteB(buffer);
                return packet.ToArray();
            }
        }

        public static void CheckDataFlags(ActionModel Action, PacketModel Packet)
        {
            UdpGameEvent flag = Action.Flag;
            if ((flag.HasFlag(UdpGameEvent.WeaponSync) && (Packet.Opcode != 4)) && ((flag & (UdpGameEvent.DropWeapon | UdpGameEvent.GetWeaponForClient)) > ((UdpGameEvent) 0)))
            {
                Action.Flag -= UdpGameEvent.WeaponSync;
            }
        }

        public static CharaDeath GetCharaDeath(uint HitInfo) => 
            ((CharaDeath) HitInfo) & (CharaDeath.MEDICAL_KIT | CharaDeath.OBJECT_EXPLOSION);

        public static void GetDecryptedData(PacketModel Packet)
        {
            try
            {
                if (Packet.Data.Length < Packet.Length)
                {
                    CLogger.Print($"Invalid packet size. (Packet.Data.Length >= Packet.Length): [ {Packet.Data.Length} | {Packet.Length} ]", LoggerType.Warning, null);
                }
                else
                {
                    byte[] destinationArray = new byte[Packet.Length - 0x11];
                    Array.Copy(Packet.Data, 0x11, destinationArray, 0, destinationArray.Length);
                    byte[] sourceArray = Bitwise.Decrypt(destinationArray, (Packet.Length % 6) + 1);
                    byte[] buffer2 = new byte[sourceArray.Length - 9];
                    Array.Copy(sourceArray, buffer2, buffer2.Length);
                    Packet.WithEndData = sourceArray;
                    Packet.WithoutEndData = buffer2;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static float GetDuration(DateTime Date) => 
            (float) (DateTimeUtil.Now() - Date).TotalSeconds;

        public static int GetHitDamageBot(uint HitInfo) => 
            (int) (HitInfo >> 20);

        public static int GetHitDamageNormal(uint HitInfo) => 
            (int) (HitInfo >> 0x15);

        public static int GetHitHelmet(uint info) => 
            ((int) (info >> 0x11)) & 7;

        public static CharaHitPart GetHitPart(uint HitInfo) => 
            ((CharaHitPart) (HitInfo >> 11)) & ((CharaHitPart) 0x3f);

        public static ObjectType GetHitType(uint HitInfo) => 
            ((ObjectType) HitInfo) & ObjectType.Object;

        public static int GetHitWho(uint HitInfo) => 
            ((int) (HitInfo >> 2)) & 0x1ff;

        public static int GetKillerId(uint HitInfo) => 
            ((int) (HitInfo >> 11)) & 0x1ff;

        public static int GetObjectType(uint HitInfo) => 
            ((int) (HitInfo >> 10)) & 1;

        public static int GetRoomInfo(uint UniqueRoomId, int Type) => 
            (Type != 0) ? ((Type != 1) ? ((Type != 2) ? 0 : (((int) (UniqueRoomId >> 20)) & 0xfff)) : (((int) (UniqueRoomId >> 12)) & 0xff)) : (((int) UniqueRoomId) & 0xfff);

        public static int GetSeedInfo(uint Seed, int Type) => 
            (Type != 0) ? ((Type != 1) ? ((Type != 2) ? 0 : (((int) (Seed >> 20)) & 0xfff)) : (((int) (Seed >> 12)) & 0xff)) : (((int) Seed) & 0xfff);

        public static TeamEnum GetSwappedTeam(PlayerModel Player, RoomModel Room)
        {
            if ((Player == null) || (Room == null))
            {
                return TeamEnum.TEAM_DRAW;
            }
            TeamEnum team = Player.Team;
            if (Room.IsTeamSwap)
            {
                team = (team == TeamEnum.FR_TEAM) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM;
            }
            return team;
        }

        public static ItemClass ItemClassified(ClassType ClassWeapon)
        {
            ItemClass unknown = ItemClass.Unknown;
            if (ClassWeapon == ClassType.Assault)
            {
                unknown = ItemClass.Primary;
            }
            else if ((ClassWeapon == ClassType.SMG) || (ClassWeapon == ClassType.DualSMG))
            {
                unknown = ItemClass.Primary;
            }
            else if (ClassWeapon == ClassType.Sniper)
            {
                unknown = ItemClass.Primary;
            }
            else if ((ClassWeapon == ClassType.Shotgun) || (ClassWeapon == ClassType.DualShotgun))
            {
                unknown = ItemClass.Primary;
            }
            else if (ClassWeapon == ClassType.Machinegun)
            {
                unknown = ItemClass.Primary;
            }
            else if ((ClassWeapon == ClassType.HandGun) || ((ClassWeapon == ClassType.DualHandGun) || (ClassWeapon == ClassType.CIC)))
            {
                unknown = ItemClass.Secondary;
            }
            else if ((ClassWeapon == ClassType.Knife) || ((ClassWeapon == ClassType.DualKnife) || (ClassWeapon == ClassType.Knuckle)))
            {
                unknown = ItemClass.Melee;
            }
            else if (ClassWeapon == ClassType.ThrowingGrenade)
            {
                unknown = ItemClass.Explosive;
            }
            else if (ClassWeapon == ClassType.ThrowingSpecial)
            {
                unknown = ItemClass.Special;
            }
            else if (ClassWeapon == ClassType.Dino)
            {
                unknown = ItemClass.Unknown;
            }
            return unknown;
        }

        public static int PingTime(string Address, byte[] Buffer, int TTL, int TimeOut, bool IsFragmented, out int Ping)
        {
            int num = 0;
            try
            {
                PingOptions options1 = new PingOptions();
                options1.Ttl = TTL;
                options1.DontFragment = IsFragmented;
                PingOptions options = options1;
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(Address, TimeOut, Buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        num = Convert.ToInt32(reply.RoundtripTime);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
            Ping = smethod_0(num);
            return num;
        }

        public static void RemoveHit(IList List, int Idx)
        {
            List.RemoveAt(Idx);
        }

        private static byte smethod_0(int int_0) => 
            (int_0 > 100) ? (((int_0 < 100) || (int_0 > 200)) ? (((int_0 < 200) || (int_0 > 300)) ? (((int_0 < 300) || (int_0 > 400)) ? (((int_0 < 400) || (int_0 > 500)) ? 0 : 1) : 2) : 3) : 4) : 5;

        public static bool ValidateGrenadeHit(int RawDamage, GrenadeHitInfo Hit, out int Damage)
        {
            if (!ConfigLoader.AntiScript)
            {
                Damage = (ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage;
                return true;
            }
            ItemsStatistic statistic = ItemStatisticXML.GetItemStats(Hit.WeaponId);
            if (statistic == null)
            {
                CLogger.Print($"The Item Statistic was not found. Please add: {Hit.WeaponId} to config!", LoggerType.Warning, null);
                Damage = 0;
                return false;
            }
            float num = Vector3.Distance((Vector3) Hit.FirePos, (Vector3) Hit.HitPos);
            if (ItemClassified(Hit.WeaponClass) == ItemClass.Explosive)
            {
                if (num > statistic.Range)
                {
                    Damage = 0;
                    return false;
                }
                if (RawDamage > statistic.Damage)
                {
                    Damage = 0;
                    return false;
                }
            }
            Damage = (ItemClassified(Hit.WeaponClass) == ItemClass.Explosive) ? (RawDamage * ConfigLoader.GrenateDamageMultipler) : RawDamage;
            return true;
        }

        public static bool ValidateHitData(int RawDamage, HitDataInfo Hit, out int Damage)
        {
            if (!ConfigLoader.AntiScript)
            {
                Damage = RawDamage;
                return true;
            }
            ItemsStatistic statistic = ItemStatisticXML.GetItemStats(Hit.WeaponId);
            if (statistic == null)
            {
                CLogger.Print($"The Item Statistic was not found. Please add: {Hit.WeaponId} to config!", LoggerType.Warning, null);
                Damage = 0;
                return false;
            }
            ItemClass class2 = ItemClassified(Hit.WeaponClass);
            float num = Vector3.Distance((Vector3) Hit.StartBullet, (Vector3) Hit.EndBullet);
            if ((class2 != ItemClass.Melee) && (num > statistic.Range))
            {
                Damage = 0;
                return false;
            }
            if ((class2 == ItemClass.Melee) && (num > statistic.Range))
            {
                Damage = 0;
                return false;
            }
            if (GetHitPart(Hit.HitIndex) != CharaHitPart.HEAD)
            {
                int num2 = statistic.Damage + ((statistic.Damage * 30) / 100);
                if ((class2 != ItemClass.Melee) && (RawDamage > num2))
                {
                    Damage = 0;
                    return false;
                }
                if ((class2 == ItemClass.Melee) && (RawDamage > statistic.Damage))
                {
                    Damage = 0;
                    return false;
                }
            }
            Damage = RawDamage;
            return true;
        }
    }
}

