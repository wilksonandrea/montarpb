namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game;
    using Server.Game.Data.Models;
    using System;

    public class EquipmentSync
    {
        public static void SendUDPPlayerSync(RoomModel Room, SlotModel Slot, CouponEffects Effects, int Type)
        {
            try
            {
                if ((Room != null) && (Slot != null))
                {
                    PlayerEquipment equipment = Slot.Equipment;
                    if (equipment == null)
                    {
                        CLogger.Print($"Slot Equipment Was Not Found! (UID: {Slot.PlayerId})", LoggerType.Warning, null);
                    }
                    else
                    {
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 1);
                            packet.WriteD(Room.UniqueRoomId);
                            packet.WriteD(Room.Seed);
                            packet.WriteQ(Room.StartTick);
                            packet.WriteC((byte) Type);
                            packet.WriteC((byte) Room.Rounds);
                            packet.WriteC((byte) Slot.Id);
                            packet.WriteC((byte) Slot.SpawnsCount);
                            packet.WriteC(BitConverter.GetBytes(Slot.PlayerId)[0]);
                            if ((Type == 0) || (Type == 2))
                            {
                                int num = 0;
                                if (!Room.IsDinoMode(""))
                                {
                                    num = (Room.ValidateTeam(Slot.Team, Slot.CostumeTeam) == TeamEnum.FR_TEAM) ? equipment.CharaRedId : equipment.CharaBlueId;
                                }
                                else if (((Room.Rounds == 1) && (Slot.Team == TeamEnum.CT_TEAM)) || ((Room.Rounds == 2) && (Slot.Team == TeamEnum.FR_TEAM)))
                                {
                                    num = (Room.Rounds == 2) ? equipment.CharaRedId : equipment.CharaBlueId;
                                }
                                else
                                {
                                    num = (Room.TRex != Slot.Id) ? equipment.DinoItem : -1;
                                }
                                int num2 = 0;
                                if (Effects.HasFlag(CouponEffects.Ketupat))
                                {
                                    num2 += 10;
                                }
                                if (Effects.HasFlag(CouponEffects.HP5))
                                {
                                    num2 += 5;
                                }
                                if (Effects.HasFlag(CouponEffects.HP10))
                                {
                                    num2 += 10;
                                }
                                packet.WriteD(num);
                                packet.WriteC((byte) num2);
                                packet.WriteC((byte) Effects.HasFlag(CouponEffects.C4SpeedKit));
                                packet.WriteD(equipment.WeaponPrimary);
                                packet.WriteD(equipment.WeaponSecondary);
                                packet.WriteD(equipment.WeaponMelee);
                                packet.WriteD(equipment.WeaponExplosive);
                                packet.WriteD(equipment.WeaponSpecial);
                                packet.WriteD(equipment.AccessoryId);
                            }
                            GameXender.Sync.SendPacket(packet.ToArray(), Room.UdpServer.Connection);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

