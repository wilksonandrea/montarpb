using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class MissionData
	{
		public MissionData()
		{
		}

		public static MissionDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog, bool OnlyBytes = false)
		{
			MissionDataInfo missionDataInfo = new MissionDataInfo()
			{
				PlantTime = C.ReadT(),
				Bomb = C.ReadC()
			};
			if (!OnlyBytes)
			{
				missionDataInfo.BombEnum = (BombFlag)(missionDataInfo.Bomb & 15);
				missionDataInfo.BombId = missionDataInfo.Bomb >> 4;
			}
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Bomb: {1}; Id: {2}; PlantTime: {3}; Time: {4}", new object[] { Action.Slot, missionDataInfo.BombEnum, missionDataInfo.BombId, missionDataInfo.PlantTime, Time }), LoggerType.Warning, null);
			}
			return missionDataInfo;
		}

		public static void WriteInfo(SyncServerPacket S, MissionDataInfo Info)
		{
			S.WriteT(Info.PlantTime);
			S.WriteC((byte)Info.Bomb);
		}
	}
}