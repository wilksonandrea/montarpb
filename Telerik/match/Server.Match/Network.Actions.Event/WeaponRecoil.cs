using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class WeaponRecoil
	{
		public WeaponRecoil()
		{
		}

		public static WeaponRecoilInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			WeaponRecoilInfo weaponRecoilInfo = new WeaponRecoilInfo()
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
	}
}