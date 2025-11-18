using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000024 RID: 36
	public class WeaponRecoil
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00008580 File Offset: 0x00006780
		public static WeaponRecoilInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			WeaponRecoilInfo weaponRecoilInfo = new WeaponRecoilInfo
			{
				RecoilHorzAngle = C.ReadT(),
				RecoilHorzMax = C.ReadT(),
				RecoilVertAngle = C.ReadT(),
				RecoilVertMax = C.ReadT(),
				Deviation = C.ReadT(),
				RecoilHorzCount = C.ReadC(),
				WeaponId = C.ReadD(),
				Accessory = C.ReadC(),
				Extensions = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; WeaponId: {1}", Action.Slot, weaponRecoilInfo.WeaponId), LoggerType.Warning, null);
			}
			return weaponRecoilInfo;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000862C File Offset: 0x0000682C
		public static void WriteInfo(SyncServerPacket S, WeaponRecoilInfo Info)
		{
			S.WriteT(Info.RecoilHorzAngle);
			S.WriteT(Info.RecoilHorzMax);
			S.WriteT(Info.RecoilVertAngle);
			S.WriteT(Info.RecoilVertMax);
			S.WriteT(Info.Deviation);
			S.WriteC(Info.RecoilHorzCount);
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000020A2 File Offset: 0x000002A2
		public WeaponRecoil()
		{
		}
	}
}
