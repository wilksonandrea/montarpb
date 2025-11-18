using System;

namespace Plugin.Core.Enums
{
	[Flags]
	public enum CouponEffects : long
	{
		Defense90 = 1,
		Ketupat = 2,
		Defense20 = 4,
		HollowPointPlus = 8,
		Defense10 = 16,
		HP5 = 32,
		JackHollowPoint = 64,
		ExtraGrenade = 128,
		C4SpeedKit = 256,
		HollowPoint = 512,
		FullMetalJack = 1024,
		Defense5 = 2048,
		Invincible = 4096,
		HP10 = 8192,
		QuickChangeReload = 16384,
		QuickChangeWeapon = 32768,
		FlashProtect = 65536,
		GetDroppedWeapon = 131072,
		Ammo40 = 262144,
		Respawn20 = 524288,
		Respawn30 = 1048576,
		Respawn50 = 2097152,
		Respawn100 = 4194304,
		Ammo10 = 8388608,
		ExtraThrowGrenade = 67108864,
		Unk1 = 134217728,
		Unk2 = 268435456,
		Unk3 = 536870912,
		Unk4 = 1073741824,
		Camoflage50 = 2147483648,
		Camoflage99 = 4294967296,
		Unk7 = 8589934592,
		Unk8 = 17179869184,
		Unk9 = 34359738368,
		Unk10 = 68719476736,
		Unk11 = 137438953472,
		Unk12 = 274877906944,
		Unk13 = 549755813888,
		Unk14 = 1099511627776,
		Unk15 = 2199023255552,
		Unk16 = 4398046511104,
		Unk17 = 8796093022208,
		Unk18 = 17592186044416,
		Unk19 = 35184372088832
	}
}