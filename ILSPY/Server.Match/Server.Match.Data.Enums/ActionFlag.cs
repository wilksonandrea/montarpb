using System;

namespace Server.Match.Data.Enums;

[Flags]
public enum ActionFlag : ushort
{
	Unk1 = 1,
	Unk2 = 2,
	Unk4 = 4,
	Unk8 = 8,
	Unk16 = 0x10,
	Unk32 = 0x20,
	Unk64 = 0x40,
	Unk128 = 0x80,
	Unk512 = 0x200,
	Unk1024 = 0x400,
	Unk2048 = 0x800,
	Unk4096 = 0x1000,
	Unk8192 = 0x2000,
	Unk16384 = 0x4000,
	Unk32768 = 0x8000
}
