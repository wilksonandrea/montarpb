using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001E7 RID: 487
	public class EquipmentSync
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0002EB24 File Offset: 0x0002CD24
		public static void SendUDPPlayerSync(RoomModel Room, SlotModel Slot, CouponEffects Effects, int Type)
		{
			try
			{
				if (Room != null && Slot != null)
				{
					PlayerEquipment equipment = Slot.Equipment;
					if (equipment == null)
					{
						CLogger.Print(string.Format("Slot Equipment Was Not Found! (UID: {0})", Slot.PlayerId), LoggerType.Warning, null);
					}
					else
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
								int num;
								if (Room.IsDinoMode(""))
								{
									if ((Room.Rounds == 1 && Slot.Team == TeamEnum.CT_TEAM) || (Room.Rounds == 2 && Slot.Team == TeamEnum.FR_TEAM))
									{
										num = ((Room.Rounds == 2) ? equipment.CharaRedId : equipment.CharaBlueId);
									}
									else if (Room.TRex == Slot.Id)
									{
										num = -1;
									}
									else
									{
										num = equipment.DinoItem;
									}
								}
								else
								{
									num = ((Room.ValidateTeam(Slot.Team, Slot.CostumeTeam) == TeamEnum.FR_TEAM) ? equipment.CharaRedId : equipment.CharaBlueId);
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
								syncServerPacket.WriteD(num);
								syncServerPacket.WriteC((byte)num2);
								syncServerPacket.WriteC((Effects.HasFlag(CouponEffects.C4SpeedKit) > false) ? 1 : 0);
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
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000025DF File Offset: 0x000007DF
		public EquipmentSync()
		{
		}
	}
}
