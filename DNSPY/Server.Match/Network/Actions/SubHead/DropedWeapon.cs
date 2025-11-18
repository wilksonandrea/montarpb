using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000A RID: 10
	public class DropedWeapon
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00006998 File Offset: 0x00004B98
		public static DropedWeaponInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			DropedWeaponInfo dropedWeaponInfo = new DropedWeaponInfo
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
				CLogger.Print(string.Format("Sub Head: DroppedWeapon; Weapon Flag: {0}; X: {1}; Y: {2}; Z: {3}", new object[]
				{
					dropedWeaponInfo.WeaponFlag,
					dropedWeaponInfo.WeaponPos.X,
					dropedWeaponInfo.WeaponPos.Y,
					dropedWeaponInfo.WeaponPos.Z
				}), LoggerType.Warning, null);
			}
			return dropedWeaponInfo;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00006A64 File Offset: 0x00004C64
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

		// Token: 0x06000031 RID: 49 RVA: 0x000020A2 File Offset: 0x000002A2
		public DropedWeapon()
		{
		}
	}
}
