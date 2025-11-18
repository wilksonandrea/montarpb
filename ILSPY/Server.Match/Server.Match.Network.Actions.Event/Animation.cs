using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class Animation
{
	public static AnimationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		AnimationInfo animationInfo = new AnimationInfo
		{
			Animation = C.ReadUH()
		};
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot}; POV: {animationInfo.Animation}", LoggerType.Warning);
		}
		return animationInfo;
	}

	public static void WriteInfo(SyncServerPacket S, AnimationInfo Info)
	{
		S.WriteH(Info.Animation);
	}
}
