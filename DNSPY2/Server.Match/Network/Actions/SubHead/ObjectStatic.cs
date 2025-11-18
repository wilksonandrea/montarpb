using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000D RID: 13
	public class ObjectStatic
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00006D68 File Offset: 0x00004F68
		public static ObjectStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			ObjectStaticInfo objectStaticInfo = new ObjectStaticInfo
			{
				Type = C.ReadUH(),
				Life = C.ReadUH(),
				DestroyedBySlot = C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: ObjectStatic; Type: {0}; Life: {1}; Destroyed: {2}", objectStaticInfo.Type, objectStaticInfo.Life, objectStaticInfo.DestroyedBySlot), LoggerType.Warning, null);
			}
			return objectStaticInfo;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000210E File Offset: 0x0000030E
		public static void WriteInfo(SyncServerPacket S, ObjectStaticInfo Info)
		{
			S.WriteH(Info.Type);
			S.WriteH(Info.Life);
			S.WriteC(Info.DestroyedBySlot);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000020A2 File Offset: 0x000002A2
		public ObjectStatic()
		{
		}
	}
}
