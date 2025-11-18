using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000012 RID: 18
	public class Animation
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00006FB8 File Offset: 0x000051B8
		public static AnimationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			AnimationInfo animationInfo = new AnimationInfo
			{
				Animation = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; POV: {1}", Action.Slot, animationInfo.Animation), LoggerType.Warning, null);
			}
			return animationInfo;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000021C1 File Offset: 0x000003C1
		public static void WriteInfo(SyncServerPacket S, AnimationInfo Info)
		{
			S.WriteH(Info.Animation);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000020A2 File Offset: 0x000002A2
		public Animation()
		{
		}
	}
}
