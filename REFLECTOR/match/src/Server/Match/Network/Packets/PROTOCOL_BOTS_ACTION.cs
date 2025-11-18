namespace Server.Match.Network.Packets
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Match.Data.Enums;
    using Server.Match.Data.Models;
    using System;
    using System.IO;

    public class PROTOCOL_BOTS_ACTION
    {
        public static byte[] GET_CODE(byte[] Data)
        {
            SyncClientPacket packet = new SyncClientPacket(Data);
            using (SyncServerPacket packet2 = new SyncServerPacket())
            {
                packet2.WriteT(packet.ReadT());
                int num = 0;
                goto TR_0021;
            TR_0004:
                return packet2.ToArray();
            TR_0021:
                while (true)
                {
                    if (num < 0x12)
                    {
                        ActionModel model = new ActionModel();
                        try
                        {
                            bool flag2;
                            bool flag = false;
                            model.Length = packet.ReadUH(out flag2);
                            if (!flag2)
                            {
                                model.Slot = packet.ReadUH();
                                model.SubHead = (UdpSubHead) packet.ReadC();
                                if (model.SubHead != ((UdpSubHead) 0xff))
                                {
                                    packet2.WriteH(model.Length);
                                    packet2.WriteH(model.Slot);
                                    packet2.WriteC((byte) model.SubHead);
                                    switch (model.SubHead)
                                    {
                                        case UdpSubHead.User:
                                        case UdpSubHead.StageInfoChara:
                                            model.Flag = (UdpGameEvent) packet.ReadUD();
                                            model.Data = packet.ReadB(model.Length - 9);
                                            packet2.WriteD((uint) model.Flag);
                                            packet2.WriteB(model.Data);
                                            if ((model.Data.Length == 0) && (model.Flag != ((UdpGameEvent) 0)))
                                            {
                                                flag = true;
                                            }
                                            break;

                                        case UdpSubHead.Grenade:
                                            packet2.WriteB(packet.ReadB(30));
                                            break;

                                        case UdpSubHead.DroppedWeapon:
                                            packet2.WriteB(packet.ReadB(0x1f));
                                            break;

                                        case UdpSubHead.ObjectStatic:
                                            packet2.WriteB(packet.ReadB(10));
                                            break;

                                        case UdpSubHead.ObjectMove:
                                            packet2.WriteB(packet.ReadB(0x10));
                                            break;

                                        case UdpSubHead.ObjectAnim:
                                            packet2.WriteB(packet.ReadB(8));
                                            break;

                                        case UdpSubHead.StageInfoObjectStatic:
                                            packet2.WriteB(packet.ReadB(1));
                                            break;

                                        case UdpSubHead.StageInfoObjectAnim:
                                            packet2.WriteB(packet.ReadB(9));
                                            break;

                                        case UdpSubHead.StageInfoObjectControl:
                                            packet2.WriteB(packet.ReadB(9));
                                            break;

                                        default:
                                            if (ConfigLoader.IsTestMode)
                                            {
                                                CLogger.Print(Bitwise.ToHexData($"BOT SUB HEAD: '{model.SubHead}' or '{(int) model.SubHead}'", Data), LoggerType.Opcode, null);
                                            }
                                            break;
                                    }
                                    if (!flag)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            if (ConfigLoader.IsTestMode)
                            {
                                CLogger.Print($"BOTS ACTION DATA - Buffer (Length: {Data.Length}): | {exception.Message}", LoggerType.Error, exception);
                            }
                            packet2.SetMStream(new MemoryStream());
                        }
                        goto TR_0004;
                    }
                    else
                    {
                        goto TR_0004;
                    }
                    break;
                }
                num++;
                goto TR_0021;
            }
        }
    }
}

