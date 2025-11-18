using System;
using System.IO;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Network.Packets
{
	// Token: 0x02000005 RID: 5
	public class PROTOCOL_BOTS_ACTION
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000626C File Offset: 0x0000446C
		public static byte[] GET_CODE(byte[] Data)
		{
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			byte[] array9;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteT(syncClientPacket.ReadT());
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag = false;
						bool flag2;
						actionModel.Length = syncClientPacket.ReadUH(out flag2);
						if (flag2)
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
						case UdpSubHead.User:
						case UdpSubHead.StageInfoChara:
							actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
							actionModel.Data = syncClientPacket.ReadB((int)(actionModel.Length - 9));
							syncServerPacket.WriteD((uint)actionModel.Flag);
							syncServerPacket.WriteB(actionModel.Data);
							if (actionModel.Data.Length == 0 && actionModel.Flag != (UdpGameEvent)0U)
							{
								flag = true;
								goto IL_20B;
							}
							goto IL_20B;
						case UdpSubHead.Grenade:
						{
							byte[] array = syncClientPacket.ReadB(30);
							syncServerPacket.WriteB(array);
							goto IL_20B;
						}
						case UdpSubHead.DroppedWeapon:
						{
							byte[] array2 = syncClientPacket.ReadB(31);
							syncServerPacket.WriteB(array2);
							goto IL_20B;
						}
						case UdpSubHead.ObjectStatic:
						{
							byte[] array3 = syncClientPacket.ReadB(10);
							syncServerPacket.WriteB(array3);
							goto IL_20B;
						}
						case UdpSubHead.ObjectMove:
						{
							byte[] array4 = syncClientPacket.ReadB(16);
							syncServerPacket.WriteB(array4);
							goto IL_20B;
						}
						case UdpSubHead.ObjectAnim:
						{
							byte[] array5 = syncClientPacket.ReadB(8);
							syncServerPacket.WriteB(array5);
							goto IL_20B;
						}
						case UdpSubHead.StageInfoObjectStatic:
						{
							byte[] array6 = syncClientPacket.ReadB(1);
							syncServerPacket.WriteB(array6);
							goto IL_20B;
						}
						case UdpSubHead.StageInfoObjectAnim:
						{
							byte[] array7 = syncClientPacket.ReadB(9);
							syncServerPacket.WriteB(array7);
							goto IL_20B;
						}
						case UdpSubHead.StageInfoObjectControl:
						{
							byte[] array8 = syncClientPacket.ReadB(9);
							syncServerPacket.WriteB(array8);
							goto IL_20B;
						}
						}
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(Bitwise.ToHexData(string.Format("BOT SUB HEAD: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
						}
						IL_20B:
						if (flag)
						{
							break;
						}
					}
					catch (Exception ex)
					{
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("BOTS ACTION DATA - Buffer (Length: {0}): | {1}", Data.Length, ex.Message), LoggerType.Error, ex);
						}
						syncServerPacket.SetMStream(new MemoryStream());
						break;
					}
				}
				array9 = syncServerPacket.ToArray();
			}
			return array9;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000020A2 File Offset: 0x000002A2
		public PROTOCOL_BOTS_ACTION()
		{
		}
	}
}
