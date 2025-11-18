using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class Animation
	{
		public Animation()
		{
		}

		public static AnimationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			AnimationInfo animationInfo = new AnimationInfo()
			{
				Animation = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; POV: {1}", Action.Slot, animationInfo.Animation), LoggerType.Warning, null);
			}
			return animationInfo;
		}

		public static void WriteInfo(SyncServerPacket S, AnimationInfo Info)
		{
			S.WriteH(Info.Animation);
		}
	}
}