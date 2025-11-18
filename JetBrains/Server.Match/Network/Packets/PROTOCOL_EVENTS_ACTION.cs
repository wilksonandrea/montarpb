// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Packets.PROTOCOL_EVENTS_ACTION
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using System;
using System.IO;

#nullable disable
namespace Server.Match.Network.Packets;

public class PROTOCOL_EVENTS_ACTION
{
  public static byte[] GET_CODE(byte[] string_1)
  {
    SyncClientPacket syncClientPacket = new SyncClientPacket(string_1);
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteT(syncClientPacket.ReadT());
      for (int index = 0; index < 18; ++index)
      {
        ActionModel actionModel = (ActionModel) new AnimModel();
        try
        {
          bool flag1 = false;
          bool flag2;
          actionModel.Length = syncClientPacket.ReadUH(ref flag2);
          if (!flag2)
          {
            actionModel.Slot = syncClientPacket.ReadUH();
            ((AnimModel) actionModel).set_SubHead((UdpSubHead) syncClientPacket.ReadC());
            if (((AnimModel) actionModel).get_SubHead() != (UdpSubHead) 255 /*0xFF*/)
            {
              syncServerPacket.WriteH(actionModel.Length);
              syncServerPacket.WriteH(actionModel.Slot);
              syncServerPacket.WriteC((byte) ((AnimModel) actionModel).get_SubHead());
              switch (((AnimModel) actionModel).get_SubHead())
              {
                case UdpSubHead.User:
                case UdpSubHead.StageInfoChara:
                  actionModel.Flag = (UdpGameEvent) syncClientPacket.ReadUD();
                  ((AnimModel) actionModel).set_Data(syncClientPacket.ReadB((int) actionModel.Length - 9));
                  syncServerPacket.WriteD((uint) actionModel.Flag);
                  syncServerPacket.WriteB(((AnimModel) actionModel).get_Data());
                  if (((AnimModel) actionModel).get_Data().Length == 0 && actionModel.Flag != (UdpGameEvent) 0)
                  {
                    flag1 = true;
                    break;
                  }
                  break;
                case UdpSubHead.Grenade:
                  byte[] MStream1 = syncClientPacket.ReadB(30);
                  syncServerPacket.WriteB(MStream1);
                  break;
                case UdpSubHead.DroppedWeapon:
                  byte[] MStream2 = syncClientPacket.ReadB(31 /*0x1F*/);
                  syncServerPacket.WriteB(MStream2);
                  break;
                case UdpSubHead.ObjectStatic:
                  byte[] MStream3 = syncClientPacket.ReadB(10);
                  syncServerPacket.WriteB(MStream3);
                  break;
                case UdpSubHead.ObjectMove:
                  byte[] MStream4 = syncClientPacket.ReadB(16 /*0x10*/);
                  syncServerPacket.WriteB(MStream4);
                  break;
                case UdpSubHead.ObjectAnim:
                  byte[] MStream5 = syncClientPacket.ReadB(8);
                  syncServerPacket.WriteB(MStream5);
                  break;
                case UdpSubHead.StageInfoObjectStatic:
                  byte[] MStream6 = syncClientPacket.ReadB(1);
                  syncServerPacket.WriteB(MStream6);
                  break;
                case UdpSubHead.StageInfoObjectAnim:
                  byte[] MStream7 = syncClientPacket.ReadB(9);
                  syncServerPacket.WriteB(MStream7);
                  break;
                case UdpSubHead.StageInfoObjectControl:
                  byte[] MStream8 = syncClientPacket.ReadB(9);
                  syncServerPacket.WriteB(MStream8);
                  break;
                default:
                  if (ConfigLoader.IsTestMode)
                  {
                    CLogger.Print(Bitwise.ToHexData($"BOT SUB HEAD: '{((AnimModel) actionModel).get_SubHead()}' or '{(int) ((AnimModel) actionModel).get_SubHead()}'", string_1), LoggerType.Opcode, (Exception) null);
                    break;
                  }
                  break;
              }
              if (flag1)
                break;
            }
            else
              break;
          }
          else
            break;
        }
        catch (Exception ex)
        {
          if (ConfigLoader.IsTestMode)
            CLogger.Print($"BOTS ACTION DATA - Buffer (Length: {string_1.Length}): | {ex.Message}", LoggerType.Error, ex);
          syncServerPacket.SetMStream(new MemoryStream());
          break;
        }
      }
      return syncServerPacket.ToArray();
    }
  }
}
