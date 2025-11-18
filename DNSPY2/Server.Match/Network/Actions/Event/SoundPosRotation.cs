using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000021 RID: 33
	public class SoundPosRotation
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00008204 File Offset: 0x00006404
		public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			SoundPosRotationInfo soundPosRotationInfo = new SoundPosRotationInfo
			{
				Time = C.ReadT()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Time: {1}", Action.Slot, soundPosRotationInfo.Time), LoggerType.Warning, null);
			}
			return soundPosRotationInfo;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00008250 File Offset: 0x00006450
		public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog)
		{
			SoundPosRotationInfo soundPosRotationInfo = new SoundPosRotationInfo
			{
				Time = Time
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Time: {1}", Action.Slot, soundPosRotationInfo.Time), LoggerType.Warning, null);
			}
			return soundPosRotationInfo;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002261 File Offset: 0x00000461
		public static void WriteInfo(SyncServerPacket S, SoundPosRotationInfo Info)
		{
			S.WriteT(Info.Time);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000020A2 File Offset: 0x000002A2
		public SoundPosRotation()
		{
		}
	}
}
