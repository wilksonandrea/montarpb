using System;

namespace Plugin.Core.Enums;

[Flags]
public enum RoomStageFlag
{
	NONE = 0,
	TEAM_SWAP = 1,
	RANDOM_MAP = 2,
	PASSWORD = 4,
	OBSERVER_MODE = 8,
	REAL_IP = 0x10,
	TEAM_BALANCE = 0x20,
	OBSERVER = 0x40,
	INTER_ENTER = 0x80
}
