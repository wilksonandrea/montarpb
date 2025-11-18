using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class WeaponSync
{
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
			CLogger.Print($"PVP Slot {Action.Slot}; Weapon Id: {weaponSyncInfo.WeaponId}; Extensions: {weaponSyncInfo.Extensions}; Unknowns: {weaponSyncInfo.Accessory}", LoggerType.Warning);
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
