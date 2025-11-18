using System;

namespace Server.Match.Data.Enums
{
	[Flags]
	public enum ActionFlag : ushort
	{
		Unk1 = 1,
		Unk2 = 2,
		Unk4 = 4,
		Unk8 = 8,
		Unk16 = 16,
		Unk32 = 32,
		Unk64 = 64,
		Unk128 = 128,
		Unk512 = 512,
		Unk1024 = 1024,
		Unk2048 = 2048,
		Unk4096 = 4096,
		Unk8192 = 8192,
		Unk16384 = 16384,
		Unk32768 = 32768
	}
}