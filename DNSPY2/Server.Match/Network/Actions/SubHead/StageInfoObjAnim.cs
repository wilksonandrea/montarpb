using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000F RID: 15
	public class StageInfoObjAnim
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00006E18 File Offset: 0x00005018
		public static StageAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			StageAnimInfo stageAnimInfo = new StageAnimInfo
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

		// Token: 0x0600003F RID: 63 RVA: 0x00002142 File Offset: 0x00000342
		public static void WriteInfo(SyncServerPacket S, StageAnimInfo Info)
		{
			S.WriteC(Info.Unk);
			S.WriteH(Info.Life);
			S.WriteT(Info.SyncDate);
			S.WriteC(Info.Anim1);
			S.WriteC(Info.Anim2);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000020A2 File Offset: 0x000002A2
		public StageInfoObjAnim()
		{
		}
	}
}
