using System;

namespace Server.Match.Data.Enums;

[Flags]
public enum UdpGameEvent : uint
{
	ActionState = 0x100u,
	Animation = 2u,
	PosRotation = 0x8000000u,
	SoundPosRotation = 0x800000u,
	UseObject = 4u,
	ActionForObjectSync = 0x10u,
	RadioChat = 0x20u,
	WeaponSync = 0x4000000u,
	WeaponRecoil = 0x80u,
	HpSync = 8u,
	Suicide = 0x100000u,
	MissionData = 0x800u,
	RetriveDataForClient = 0x1000u,
	SeizeDataForClient = 0x8000u,
	DropWeapon = 0x400000u,
	GetWeaponForClient = 0x1000000u,
	FireData = 0x2000000u,
	CharaFireNHitData = 0x400u,
	HitData = 0x20000u,
	GrenadeHit = 0x10000000u,
	GetWeaponForHost = 0x200u,
	FireDataOnObject = 0x40000000u,
	FireNHitDataOnObject = 0x2000u,
	BattleRoyalItem = 0x40u,
	DirectPickUp = 0x4000u,
	DeathDataForClient = 0x400u
}
