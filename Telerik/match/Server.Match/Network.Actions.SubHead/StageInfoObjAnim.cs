using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;
using System;

namespace Server.Match.Network.Actions.SubHead
{
	public class StageInfoObjAnim
	{
		public StageInfoObjAnim()
		{
		}

		public static StageAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			StageAnimInfo stageAnimInfo = new StageAnimInfo()
			{
				Unk = C.ReadC(),
				Life = C.ReadUH(),
				SyncDate = C.ReadT(),
				Anim1 = C.ReadC(),
				Anim2 = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: StageObjAnim; Unk: {0}; Life: {1}; Sync: {2}; Animation[1]: {3}; Animation[2]: {4}", new object[] { stageAnimInfo.Unk, stageAnimInfo.Life, stageAnimInfo.SyncDate, stageAnimInfo.Anim1, stageAnimInfo.Anim2 }), LoggerType.Warning, null);
			}
			return stageAnimInfo;
		}

		public static void WriteInfo(SyncServerPacket S, StageAnimInfo Info)
		{
			S.WriteC(Info.Unk);
			S.WriteH(Info.Life);
			S.WriteT(Info.SyncDate);
			S.WriteC(Info.Anim1);
			S.WriteC(Info.Anim2);
		}
	}
}