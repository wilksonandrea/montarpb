using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead;

public class ObjectMove
{
	public static ObjectMoveInfo ReadInfo(SyncClientPacket C, bool GenLog)
	{
		ObjectMoveInfo objectMoveInfo = new ObjectMoveInfo
		{
			Unk = C.ReadB(16)
		};
		if (GenLog)
		{
			CLogger.Print(Bitwise.ToHexData("UDP_SUB_HEAD: OBJECT_MOVE", objectMoveInfo.Unk), LoggerType.Opcode);
		}
		return objectMoveInfo;
	}

	public static void WriteInfo(SyncServerPacket S, ObjectMoveInfo Info)
	{
		S.WriteB(Info.Unk);
	}
}
