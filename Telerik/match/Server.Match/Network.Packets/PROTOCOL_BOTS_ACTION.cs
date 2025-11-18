using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using System;
using System.IO;

namespace Server.Match.Network.Packets
{
	public class PROTOCOL_BOTS_ACTION
	{
		public PROTOCOL_BOTS_ACTION()
		{
		}

		public static byte[] GET_CODE(byte[] Data)
		{
			bool flag;
			byte[] array;
			SyncClientPacket syncClientPacket = new SyncClientPacket(Data);
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteT(syncClientPacket.ReadT());
				for (int i = 0; i < 18; i++)
				{
					ActionModel actionModel = new ActionModel();
					try
					{
						bool flag1 = false;
						actionModel.Length = syncClientPacket.ReadUH(out flag);
						if (!flag)
						{
							actionModel.Slot = syncClientPacket.ReadUH();
							actionModel.SubHead = (UdpSubHead)syncClientPacket.ReadC();
							if (actionModel.SubHead != (UdpSubHead.Grenade | UdpSubHead.DroppedWeapon | UdpSubHead.ObjectStatic | UdpSubHead.ObjectMove | UdpSubHead.ObjectDynamic | UdpSubHead.ObjectAnim | UdpSubHead.NonPlayerCharacter | UdpSubHead.StageInfoChara | UdpSubHead.StageInfoObjectStatic | UdpSubHead.StageInfoObjectMove | UdpSubHead.StageInfoObjectDyamic | UdpSubHead.StageInfoObjectAnim | UdpSubHead.StageInfoObjectControl | UdpSubHead.StageInfoMission | UdpSubHead.ArtificialIntelligence | UdpSubHead.DomiSkillObject | UdpSubHead.DomiEvent | UdpSubHead.Sentrygun))
							{
								syncServerPacket.WriteH(actionModel.Length);
								syncServerPacket.WriteH(actionModel.Slot);
								syncServerPacket.WriteC((byte)actionModel.SubHead);
								switch (actionModel.SubHead)
								{
									case UdpSubHead.User:
									case UdpSubHead.StageInfoChara:
									{
										actionModel.Flag = (UdpGameEvent)syncClientPacket.ReadUD();
										actionModel.Data = syncClientPacket.ReadB(actionModel.Length - 9);
										syncServerPacket.WriteD((uint)actionModel.Flag);
										syncServerPacket.WriteB(actionModel.Data);
										if (actionModel.Data.Length != 0 || (int)actionModel.Flag == 0)
										{
											break;
										}
										flag1 = true;
										break;
									}
									case UdpSubHead.Grenade:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(30));
										break;
									}
									case UdpSubHead.DroppedWeapon:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(31));
										break;
									}
									case UdpSubHead.ObjectStatic:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(10));
										break;
									}
									case UdpSubHead.ObjectMove:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(16));
										break;
									}
									case UdpSubHead.ObjectDynamic:
									case UdpSubHead.NonPlayerCharacter:
									case UdpSubHead.StageInfoObjectMove:
									case UdpSubHead.StageInfoObjectDyamic:
									{
										if (!ConfigLoader.IsTestMode)
										{
											break;
										}
										CLogger.Print(Bitwise.ToHexData(string.Format("BOT SUB HEAD: '{0}' or '{1}'", actionModel.SubHead, (int)actionModel.SubHead), Data), LoggerType.Opcode, null);
										break;
									}
									case UdpSubHead.ObjectAnim:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(8));
										break;
									}
									case UdpSubHead.StageInfoObjectStatic:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(1));
										break;
									}
									case UdpSubHead.StageInfoObjectAnim:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(9));
										break;
									}
									case UdpSubHead.StageInfoObjectControl:
									{
										syncServerPacket.WriteB(syncClientPacket.ReadB(9));
										break;
									}
									default:
									{
										goto case UdpSubHead.StageInfoObjectDyamic;
									}
								}
								if (flag1)
								{
									break;
								}
							}
							else
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (ConfigLoader.IsTestMode)
						{
							CLogger.Print(string.Format("BOTS ACTION DATA - Buffer (Length: {0}): | {1}", (int)Data.Length, exception.Message), LoggerType.Error, exception);
						}
						syncServerPacket.SetMStream(new MemoryStream());
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}