using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server;

public class EquipmentSync
{
	public static void SendUDPPlayerSync(RoomModel Room, SlotModel Slot, CouponEffects Effects, int Type)
	{
		try
		{
			if (Room == null || Slot == null)
			{
				return;
			}
			PlayerEquipment equipment = Slot.Equipment;
			if (equipment == null)
			{
				CLogger.Print($"Slot Equipment Was Not Found! (UID: {Slot.PlayerId})", LoggerType.Warning);
				return;
			}
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
				int num = 0;
				num = ((!Room.IsDinoMode()) ? ((Room.ValidateTeam(Slot.Team, Slot.CostumeTeam) == TeamEnum.FR_TEAM) ? equipment.CharaRedId : equipment.CharaBlueId) : (((Room.Rounds != 1 || Slot.Team != TeamEnum.CT_TEAM) && (Room.Rounds != 2 || Slot.Team != 0)) ? ((Room.TRex != Slot.Id) ? equipment.DinoItem : (-1)) : ((Room.Rounds == 2) ? equipment.CharaRedId : equipment.CharaBlueId)));
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
				syncServerPacket.WriteC(Effects.HasFlag(CouponEffects.C4SpeedKit) ? ((byte)1) : ((byte)0));
				syncServerPacket.WriteD(equipment.WeaponPrimary);
				syncServerPacket.WriteD(equipment.WeaponSecondary);
				syncServerPacket.WriteD(equipment.WeaponMelee);
				syncServerPacket.WriteD(equipment.WeaponExplosive);
				syncServerPacket.WriteD(equipment.WeaponSpecial);
				syncServerPacket.WriteD(equipment.AccessoryId);
			}
			GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
