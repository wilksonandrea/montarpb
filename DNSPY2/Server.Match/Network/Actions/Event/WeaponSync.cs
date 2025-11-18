using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000025 RID: 37
	public class WeaponSync
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000086A8 File Offset: 0x000068A8
		public static WeaponSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			WeaponSyncInfo weaponSyncInfo = new WeaponSyncInfo
			{
				WeaponId = C.ReadD(),
				Accessory = C.ReadC(),
				Extensions = C.ReadC()
			};
			if (!OnlyBytes)
			{
				weaponSyncInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(weaponSyncInfo.WeaponId, 2);
			}
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot {0}; Weapon Id: {1}; Extensions: {2}; Unknowns: {3}", new object[] { Action.Slot, weaponSyncInfo.WeaponId, weaponSyncInfo.Extensions, weaponSyncInfo.Accessory }), LoggerType.Warning, null);
			}
			return weaponSyncInfo;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000226F File Offset: 0x0000046F
		public static void WriteInfo(SyncServerPacket S, WeaponSyncInfo Info)
		{
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000020A2 File Offset: 0x000002A2
		public WeaponSync()
		{
		}
	}
}
