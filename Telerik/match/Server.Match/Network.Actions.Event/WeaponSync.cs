using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class WeaponSync
	{
		public WeaponSync()
		{
		}

		public static WeaponSyncInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			WeaponSyncInfo weaponSyncInfo = new WeaponSyncInfo()
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

		public static void WriteInfo(SyncServerPacket S, WeaponSyncInfo Info)
		{
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
		}
	}
}