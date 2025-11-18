using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class DropWeapon
	{
		public DropWeapon()
		{
		}

		public static DropWeaponInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			DropWeaponInfo dropWeaponInfo = new DropWeaponInfo()
			{
				AmmoPrin = C.ReadUH(),
				AmmoDual = C.ReadUH(),
				AmmoTotal = C.ReadUH(),
				Unk1 = C.ReadUH(),
				Unk2 = C.ReadUH(),
				Unk3 = C.ReadUH(),
				Flags = C.ReadC(),
				WeaponId = C.ReadD(),
				Accessory = C.ReadC(),
				Extensions = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot : {0}; Weapon Id: {1}; Ext: {2}; Acc: {3}; Ammo Prin: {4}; Ammo Dual: {5}; Ammo Total: {6}", new object[] { Action.Slot, dropWeaponInfo.WeaponId, dropWeaponInfo.Extensions, dropWeaponInfo.Accessory, dropWeaponInfo.AmmoPrin, dropWeaponInfo.AmmoDual, dropWeaponInfo.AmmoTotal }), LoggerType.Warning, null);
			}
			return dropWeaponInfo;
		}

		public static void WriteInfo(SyncServerPacket S, DropWeaponInfo Info)
		{
			if (!ConfigLoader.UseMaxAmmoInDrop)
			{
				S.WriteH(Info.AmmoPrin);
				S.WriteH(Info.AmmoDual);
				S.WriteH(Info.AmmoTotal);
			}
			else
			{
				S.WriteH(65535);
				S.WriteH(Info.AmmoDual);
				S.WriteH(10000);
			}
			S.WriteH(Info.Unk1);
			S.WriteH(Info.Unk2);
			S.WriteH(Info.Unk3);
			S.WriteC((byte)(Info.Flags + Info.Counter));
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
		}
	}
}