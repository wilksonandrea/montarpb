using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Server.Match.Data.Models.SubHead;
using System;

namespace Server.Match.Network.Actions.SubHead
{
	public class DropedWeapon
	{
		public DropedWeapon()
		{
		}

		public static DropedWeaponInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			DropedWeaponInfo dropedWeaponInfo = new DropedWeaponInfo()
			{
				WeaponPos = C.ReadUHV(),
				Unk1 = C.ReadUH(),
				Unk2 = C.ReadUH(),
				Unk3 = C.ReadUH(),
				Unk4 = C.ReadUH(),
				WeaponFlag = C.ReadC(),
				Unks = C.ReadB(16)
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: DroppedWeapon; Weapon Flag: {0}; X: {1}; Y: {2}; Z: {3}", new object[] { dropedWeaponInfo.WeaponFlag, dropedWeaponInfo.WeaponPos.X, dropedWeaponInfo.WeaponPos.Y, dropedWeaponInfo.WeaponPos.Z }), LoggerType.Warning, null);
			}
			return dropedWeaponInfo;
		}

		public static void WriteInfo(SyncServerPacket S, DropedWeaponInfo Info)
		{
			S.WriteHV(Info.WeaponPos);
			S.WriteH(Info.Unk1);
			S.WriteH(Info.Unk2);
			S.WriteH(Info.Unk3);
			S.WriteH(Info.Unk4);
			S.WriteC(Info.WeaponFlag);
			S.WriteB(Info.Unks);
		}
	}
}