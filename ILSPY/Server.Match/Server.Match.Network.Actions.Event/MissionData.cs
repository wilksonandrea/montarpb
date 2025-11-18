using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class MissionData
{
	public static MissionDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog, bool OnlyBytes = false)
	{
		MissionDataInfo missionDataInfo = new MissionDataInfo
		{
			PlantTime = C.ReadT(),
			Bomb = C.ReadC()
		};
		if (!OnlyBytes)
		{
			missionDataInfo.BombEnum = (BombFlag)(missionDataInfo.Bomb & 0xF);
			missionDataInfo.BombId = missionDataInfo.Bomb >> 4;
		}
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot}; Bomb: {missionDataInfo.BombEnum}; Id: {missionDataInfo.BombId}; PlantTime: {missionDataInfo.PlantTime}; Time: {Time}", LoggerType.Warning);
		}
		return missionDataInfo;
	}

	public static void WriteInfo(SyncServerPacket S, MissionDataInfo Info)
	{
		S.WriteT(Info.PlantTime);
		S.WriteC((byte)Info.Bomb);
	}
}
