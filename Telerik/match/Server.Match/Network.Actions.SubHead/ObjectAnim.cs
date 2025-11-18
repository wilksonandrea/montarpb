using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;
using System;

namespace Server.Match.Network.Actions.SubHead
{
	public class ObjectAnim
	{
		public ObjectAnim()
		{
		}

		public static ObjectAnimInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			ObjectAnimInfo objectAnimInfo = new ObjectAnimInfo()
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

		public static void WriteInfo(SyncServerPacket S, ObjectAnimInfo Info)
		{
			S.WriteH(Info.Life);
			S.WriteC(Info.Anim1);
			S.WriteC(Info.Anim2);
			S.WriteT(Info.SyncDate);
		}
	}
}