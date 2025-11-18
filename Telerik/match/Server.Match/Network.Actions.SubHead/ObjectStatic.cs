using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.SubHead;
using System;

namespace Server.Match.Network.Actions.SubHead
{
	public class ObjectStatic
	{
		public ObjectStatic()
		{
		}

		public static ObjectStaticInfo ReadInfo(SyncClientPacket C, bool GenLog)
		{
			ObjectStaticInfo objectStaticInfo = new ObjectStaticInfo()
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

		public static void WriteInfo(SyncServerPacket S, ObjectStaticInfo Info)
		{
			S.WriteH(Info.Type);
			S.WriteH(Info.Life);
			S.WriteC(Info.DestroyedBySlot);
		}
	}
}