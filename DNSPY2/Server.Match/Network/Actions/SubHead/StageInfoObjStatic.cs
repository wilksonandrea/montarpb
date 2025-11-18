using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000E RID: 14
	public class StageInfoObjStatic
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static StageStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			StageStaticInfo stageStaticInfo = new StageStaticInfo
			{
				IsDestroyed = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: StageInfoObjStatic; Destroyed: {0}", stageStaticInfo.IsDestroyed), LoggerType.Warning, null);
			}
			return stageStaticInfo;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002134 File Offset: 0x00000334
		public static void WriteInfo(SyncServerPacket S, StageStaticInfo Info)
		{
			S.WriteC(Info.IsDestroyed);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000020A2 File Offset: 0x000002A2
		public StageInfoObjStatic()
		{
		}
	}
}
