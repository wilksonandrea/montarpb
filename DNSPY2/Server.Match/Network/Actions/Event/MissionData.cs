using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x0200001D RID: 29
	public class MissionData
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00007F64 File Offset: 0x00006164
		public static MissionDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog, bool OnlyBytes = false)
		{
			MissionDataInfo missionDataInfo = new MissionDataInfo
			{
				PlantTime = C.ReadT(),
				Bomb = (int)C.ReadC()
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

		// Token: 0x06000069 RID: 105 RVA: 0x00002212 File Offset: 0x00000412
		public static void WriteInfo(SyncServerPacket S, MissionDataInfo Info)
		{
			S.WriteT(Info.PlantTime);
			S.WriteC((byte)Info.Bomb);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000020A2 File Offset: 0x000002A2
		public MissionData()
		{
		}
	}
}
