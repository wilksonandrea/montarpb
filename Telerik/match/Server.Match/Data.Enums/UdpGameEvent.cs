using System;

namespace Server.Match.Data.Enums
{
	[Flags]
	public enum UdpGameEvent : uint
	{
		Animation = 2,
		UseObject = 4,
		HpSync = 8,
		ActionForObjectSync = 16,
		RadioChat = 32,
		BattleRoyalItem = 64,
		WeaponRecoil = 128,
		ActionState = 256,
		GetWeaponForHost = 512,
		CharaFireNHitData = 1024,
		DeathDataForClient = 1024,
		MissionData = 2048,
		RetriveDataForClient = 4096,
		FireNHitDataOnObject = 8192,
		DirectPickUp = 16384,
		SeizeDataForClient = 32768,
		HitData = 131072,
		Suicide = 1048576,
		DropWeapon = 4194304,
		SoundPosRotation = 8388608,
		GetWeaponForClient = 16777216,
		FireData = 33554432,
		WeaponSync = 67108864,
		PosRotation = 134217728,
		GrenadeHit = 268435456,
		FireDataOnObject = 1073741824
	}
}