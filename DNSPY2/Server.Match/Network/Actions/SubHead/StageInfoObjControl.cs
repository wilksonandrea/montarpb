using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x02000009 RID: 9
	public class StageInfoObjControl
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00006950 File Offset: 0x00004B50
		public static StageControlInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			StageControlInfo stageControlInfo = new StageControlInfo
			{
				Unk = C.ReadB(9)
			};
			if (GenLog)
			{
				CLogger.Print("Sub Head: StageInfoObjControl; " + Bitwise.ToHexData("Controled Object Hex Data", stageControlInfo.Unk), LoggerType.Opcode, null);
			}
			return stageControlInfo;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000020CE File Offset: 0x000002CE
		public static void WriteInfo(SyncServerPacket S, StageControlInfo Info)
		{
			S.WriteB(Info.Unk);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000020A2 File Offset: 0x000002A2
		public StageInfoObjControl()
		{
		}
	}
}
