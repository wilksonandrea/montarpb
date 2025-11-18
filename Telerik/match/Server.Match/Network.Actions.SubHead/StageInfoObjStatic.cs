using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;
using System;

namespace Server.Match.Network.Actions.SubHead
{
	public class StageInfoObjStatic
	{
		public StageInfoObjStatic()
		{
		}

		public static StageStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			StageStaticInfo stageStaticInfo = new StageStaticInfo()
			{
				IsDestroyed = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: StageInfoObjStatic; Destroyed: {0}", stageStaticInfo.IsDestroyed), LoggerType.Warning, null);
			}
			return stageStaticInfo;
		}

		public static void WriteInfo(SyncServerPacket S, StageStaticInfo Info)
		{
			S.WriteC(Info.IsDestroyed);
		}
	}
}