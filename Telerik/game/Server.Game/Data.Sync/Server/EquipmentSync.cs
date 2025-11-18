using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using System;

namespace Server.Game.Data.Sync.Server
{
	public class EquipmentSync
	{
		public EquipmentSync()
		{
		}

		public static void SendUDPPlayerSync(RoomModel Room, SlotModel Slot, CouponEffects Effects, int Type)
		{
			try
			{
				if (Room != null && Slot != null)
				{
					PlayerEquipment equipment = Slot.Equipment;
					if (equipment != null)
					{
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(1);
							syncServerPacket.WriteD(Room.UniqueRoomId);
							syncServerPacket.WriteD(Room.Seed);
							syncServerPacket.WriteQ(Room.StartTick);
							syncServerPacket.WriteC((byte)Type);
							syncServerPacket.WriteC((byte)Room.Rounds);
							syncServerPacket.WriteC((byte)Slot.Id);
							syncServerPacket.WriteC((byte)Slot.SpawnsCount);
							syncServerPacket.WriteC(BitConverter.GetBytes(Slot.PlayerId)[0]);
							if (Type == 0 || Type == 2)
							{
								int ınt32 = 0;
								if (!Room.IsDinoMode(""))
								{
									ınt32 = (Room.ValidateTeam(Slot.Team, Slot.CostumeTeam) == TeamEnum.FR_TEAM ? equipment.CharaRedId : equipment.CharaBlueId);
								}
								else if ((Room.Rounds != 1 || Slot.Team != TeamEnum.CT_TEAM) && (Room.Rounds != 2 || Slot.Team != TeamEnum.FR_TEAM))
								{
									ınt32 = (Room.TRex != Slot.Id ? equipment.DinoItem : -1);
								}
								else
								{
									ınt32 = (Room.Rounds == 2 ? equipment.CharaRedId : equipment.CharaBlueId);
								}
								int ınt321 = 0;
								if (Effects.HasFlag(CouponEffects.Ketupat))
								{
									ınt321 += 10;
								}
								if (Effects.HasFlag(CouponEffects.HP5))
								{
									ınt321 += 5;
								}
								if (Effects.HasFlag(CouponEffects.HP10))
								{
									ınt321 += 10;
								}
								syncServerPacket.WriteD(ınt32);
								syncServerPacket.WriteC((byte)ınt321);
								syncServerPacket.WriteC((byte)Effects.HasFlag(CouponEffects.C4SpeedKit));
								syncServerPacket.WriteD(equipment.WeaponPrimary);
								syncServerPacket.WriteD(equipment.WeaponSecondary);
								syncServerPacket.WriteD(equipment.WeaponMelee);
								syncServerPacket.WriteD(equipment.WeaponExplosive);
								syncServerPacket.WriteD(equipment.WeaponSpecial);
								syncServerPacket.WriteD(equipment.AccessoryId);
							}
							GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
						}
					}
					else
					{
						CLogger.Print(string.Format("Slot Equipment Was Not Found! (UID: {0})", Slot.PlayerId), LoggerType.Warning, null);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}