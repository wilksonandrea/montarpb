using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x02000008 RID: 8
	public class ObjectMove
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00006914 File Offset: 0x00004B14
		public static ObjectMoveInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			ObjectMoveInfo objectMoveInfo = new ObjectMoveInfo
			{
				Unk = C.ReadB(16)
			};
			if (GenLog)
			{
				CLogger.Print(Bitwise.ToHexData("UDP_SUB_HEAD: OBJECT_MOVE", objectMoveInfo.Unk), LoggerType.Opcode, null);
			}
			return objectMoveInfo;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void WriteInfo(SyncServerPacket S, ObjectMoveInfo Info)
		{
			S.WriteB(Info.Unk);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000020A2 File Offset: 0x000002A2
		public ObjectMove()
		{
		}
	}
}
