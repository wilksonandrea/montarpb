using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class SoundPosRotation
	{
		public SoundPosRotation()
		{
		}

		public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			SoundPosRotationInfo soundPosRotationInfo = new SoundPosRotationInfo()
			{
				Time = C.ReadT()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Time: {1}", Action.Slot, soundPosRotationInfo.Time), LoggerType.Warning, null);
			}
			return soundPosRotationInfo;
		}

		public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, float Time, bool GenLog)
		{
			SoundPosRotationInfo soundPosRotationInfo = new SoundPosRotationInfo()
			{
				Time = Time
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Time: {1}", Action.Slot, soundPosRotationInfo.Time), LoggerType.Warning, null);
			}
			return soundPosRotationInfo;
		}

		public static void WriteInfo(SyncServerPacket S, SoundPosRotationInfo Info)
		{
			S.WriteT(Info.Time);
		}
	}
}