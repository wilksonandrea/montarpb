using System;

namespace Plugin.Core.Enums;

[Flags]
public enum RoomWeaponsFlag
{
	None = 0,
	Grenade = 1,
	Melee = 2,
	Secondary = 4,
	Accessory = 8,
	Assault = 0x10,
	SMG = 0x20,
	Sniper = 0x40,
	Shotgun = 0x80,
	Machinegun = 0x100,
	Barefist = 0x200,
	RPG7 = 0x400,
	Shield = 0x800
}
