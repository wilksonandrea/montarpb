using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000C RID: 12
	public class ObjectAnim
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public static ObjectAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			ObjectAnimInfo objectAnimInfo = new ObjectAnimInfo
			{
				Life = C.ReadUH(),
				Anim1 = C.ReadC(),
				Anim2 = C.ReadC(),
				SyncDate = C.ReadT()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("Sub Head: ObjectAnim; Life: {0}; Animation[1]: {1}; Animation[2]: {2}; Sync: {3}", new object[] { objectAnimInfo.Life, objectAnimInfo.Anim1, objectAnimInfo.Anim2, objectAnimInfo.SyncDate }), LoggerType.Warning, null);
			}
			return objectAnimInfo;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000020DC File Offset: 0x000002DC
		public static void WriteInfo(SyncServerPacket S, ObjectAnimInfo Info)
		{
			S.WriteH(Info.Life);
			S.WriteC(Info.Anim1);
			S.WriteC(Info.Anim2);
			S.WriteT(Info.SyncDate);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000020A2 File Offset: 0x000002A2
		public ObjectAnim()
		{
		}
	}
}
