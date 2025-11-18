using System;
using System.IO;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Network.Packets;

public class PROTOCOL_BOTS_ACTION
{
	public static byte[] GET_CODE(byte[] Data)
	{
		SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteT(syncClientPacket.ReadT());
		for (int i = 0; i < 18; i++)
		{
			ActionModel actionModel = new ActionModel();
			try
			{
				bool flag = false;
				actionModel.Length = syncClientPacket.ReadUH(out var Exception);
				if (Exception)
				{
					break;
				}
				actionModel.Slot = syncClientPacket.ReadUH();
				actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
				if (actionModel.SubHead == (UdpSubHead)255)
				{
					break;
				}
				syncServerPacket.WriteH(actionModel.Length);
				syncServerPacket.WriteH(actionModel.Slot);
				syncServerPacket.WriteC((byte)actionModel.SubHead);
				switch (actionModel.SubHead)
				{
				case UdpSubHead.Grenade:
				{
					byte[] value6 = syncClientPacket.ReadB(30);
					syncServerPacket.WriteB(value6);
					break;
				}
				case UdpSubHead.DroppedWeapon:
				{
					byte[] value5 = syncClientPacket.ReadB(31);
					syncServerPacket.WriteB(value5);
					break;
				}
				case UdpSubHead.ObjectStatic:
				{
					byte[] value3 = syncClientPacket.ReadB(10);
					syncServerPacket.WriteB(value3);
					break;
				}
				case UdpSubHead.ObjectMove:
				{
					byte[] value8 = syncClientPacket.ReadB(16);
					syncServerPacket.WriteB(value8);
					break;
				}
				case UdpSubHead.ObjectAnim:
				{
					byte[] value7 = syncClientPacket.ReadB(8);
					syncServerPacket.WriteB(value7);
					break;
				}
				case UdpSubHead.User:
				case UdpSubHead.StageInfoChara:
					actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
					actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
					syncServerPacket.WriteD((uint)actionModel.Flag);
					syncServerPacket.WriteB(actionModel.Data);
					if (actionModel.Data.Length == 0 && actionModel.Flag != 0)
					{
						flag = true;
					}
					break;
				case UdpSubHead.StageInfoObjectStatic:
				{
					byte[] value4 = syncClientPacket.ReadB(1);
					syncServerPacket.WriteB(value4);
					break;
				}
				default:
					if (ConfigLoader.IsTestMode)
					{
						CLogger.Print(Bitwise.ToHexData($"BOT SUB HEAD: '{actionModel.SubHead}' or '{(int)actionModel.SubHead}'", Data), LoggerType.Opcode);
					}
					break;
				case UdpSubHead.StageInfoObjectAnim:
				{
					byte[] value2 = syncClientPacket.ReadB(9);
					syncServerPacket.WriteB(value2);
					break;
				}
				case UdpSubHead.StageInfoObjectControl:
				{
					byte[] value = syncClientPacket.ReadB(9);
					syncServerPacket.WriteB(value);
					break;
				}
				}
				if (flag)
				{
					break;
				}
				continue;
			}
			catch (Exception ex)
			{
				if (ConfigLoader.IsTestMode)
				{
					CLogger.Print($"BOTS ACTION DATA - Buffer (Length: {Data.Length}): | {ex.Message}", LoggerType.Error, ex);
				}
				syncServerPacket.SetMStream(new MemoryStream());
			}
			break;
		}
		return syncServerPacket.ToArray();
	}
}
