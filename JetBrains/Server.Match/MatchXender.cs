// Decompiled with JetBrains decompiler
// Type: Server.Match.MatchXender
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.SubHead;
using Server.Match.Data.Sync;
using Server.Match.Network.Actions.Event;
using Server.Match.Network.Actions.SubHead;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match;

public class MatchXender
{
  public byte[] WritePlayerActionData(
    byte[] Packet,
    RoomModel roomModel_0,
    float float_0,
    [In] PacketModel obj3)
  {
    SyncClientPacket syncClientPacket = new SyncClientPacket(Packet);
    List<ObjectHitInfo> Data = new List<ObjectHitInfo>();
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      for (int index = 0; index < 18; ++index)
      {
        ActionModel socket_1 = (ActionModel) new AnimModel();
        try
        {
          bool flag1 = false;
          bool flag2;
          socket_1.Length = syncClientPacket.ReadUH(ref flag2);
          if (!flag2)
          {
            socket_1.Slot = syncClientPacket.ReadUH();
            ((AnimModel) socket_1).set_SubHead((UdpSubHead) syncClientPacket.ReadC());
            if (((AnimModel) socket_1).get_SubHead() != (UdpSubHead) 255 /*0xFF*/)
            {
              syncServerPacket.WriteH(socket_1.Length);
              syncServerPacket.WriteH(socket_1.Slot);
              syncServerPacket.WriteC((byte) ((AnimModel) socket_1).get_SubHead());
              switch (((AnimModel) socket_1).get_SubHead())
              {
                case UdpSubHead.User:
                case UdpSubHead.StageInfoChara:
                  socket_1.Flag = (UdpGameEvent) syncClientPacket.ReadUD();
                  ((AnimModel) socket_1).set_Data(syncClientPacket.ReadB((int) socket_1.Length - 9));
                  MatchSync.CheckDataFlags(socket_1, obj3);
                  byte[] MStream;
                  Data.AddRange((IEnumerable<ObjectHitInfo>) ((MatchClient) this).method_0(socket_1, roomModel_0, float_0, ref MStream));
                  syncServerPacket.GoBack(5);
                  syncServerPacket.WriteH((ushort) (MStream.Length + 9));
                  syncServerPacket.WriteH(socket_1.Slot);
                  syncServerPacket.WriteC((byte) ((AnimModel) socket_1).get_SubHead());
                  syncServerPacket.WriteD((uint) socket_1.Flag);
                  syncServerPacket.WriteB(MStream);
                  if (((AnimModel) socket_1).get_Data().Length == 0 && (int) socket_1.Length - 9 != 0)
                  {
                    flag1 = true;
                    break;
                  }
                  break;
                case UdpSubHead.Grenade:
                  GrenadeInfo Info = ObjectAnim.ReadInfo(syncClientPacket, false, false);
                  ObjectStatic.WriteInfo(syncServerPacket, Info);
                  break;
                case UdpSubHead.DroppedWeapon:
                  DropedWeaponInfo GenLog1 = GrenadeSync.ReadInfo(syncClientPacket, false);
                  ObjectAnim.WriteInfo(syncServerPacket, GenLog1);
                  break;
                case UdpSubHead.ObjectStatic:
                  ObjectStaticInfo GenLog2 = StageInfoObjStatic.ReadInfo(syncClientPacket, false);
                  StageInfoObjAnim.WriteInfo(syncServerPacket, GenLog2);
                  break;
                case UdpSubHead.ObjectMove:
                  ObjectMoveInfo objectMoveInfo = StageInfoObjControl.ReadInfo(syncClientPacket, false);
                  DropedWeapon.WriteInfo(syncServerPacket, objectMoveInfo);
                  break;
                case UdpSubHead.ObjectAnim:
                  ObjectAnimInfo objectAnimInfo = ObjectStatic.ReadInfo(syncClientPacket, false);
                  StageInfoObjStatic.WriteInfo(syncServerPacket, objectAnimInfo);
                  break;
                case UdpSubHead.StageInfoObjectStatic:
                  StageStaticInfo GenLog3 = StageInfoObjAnim.ReadInfo(syncClientPacket, false);
                  ActionForObjectSync.WriteInfo(syncServerPacket, GenLog3);
                  break;
                case UdpSubHead.StageInfoObjectAnim:
                  StageAnimInfo GenLog4 = ActionForObjectSync.ReadInfo(syncClientPacket, false);
                  ActionState.WriteInfo(syncServerPacket, GenLog4);
                  break;
                case UdpSubHead.StageInfoObjectControl:
                  StageControlInfo GenLog5 = DropedWeapon.ReadInfo(syncClientPacket, false);
                  GrenadeSync.WriteInfo(syncServerPacket, GenLog5);
                  break;
                default:
                  if (ConfigLoader.IsTestMode)
                  {
                    CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{((AnimModel) socket_1).get_SubHead()}' or '{(int) ((AnimModel) socket_1).get_SubHead()}'", Packet), LoggerType.Opcode, (Exception) null);
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
            CLogger.Print($"PVP Action Data - Buffer (Length: {Packet.Length}): | {ex.Message}", LoggerType.Error, ex);
          Data = new List<ObjectHitInfo>();
          break;
        }
      }
      if (Data.Count > 0)
        syncServerPacket.WriteB(StageInfoObjControl.GET_CODE(Data));
      return syncServerPacket.ToArray();
    }
  }

  private List<ObjectHitInfo> method_1([In] ActionModel obj0, [In] RoomModel obj1, [In] ref byte[] obj2)
  {
    obj2 = new byte[0];
    if (obj1 == null)
      return (List<ObjectHitInfo>) null;
    if (((AnimModel) obj0).get_Data().Length == 0)
      return new List<ObjectHitInfo>();
    byte[] Length = ((AnimModel) obj0).get_Data();
    List<ObjectHitInfo> objectHitInfoList = new List<ObjectHitInfo>();
    SyncClientPacket syncClientPacket = new SyncClientPacket(Length);
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      uint num1 = 0;
      PlayerModel player = ((ObjectMoveInfo) obj1).GetPlayer((int) obj0.Slot, false);
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.ActionState))
      {
        num1 += 256U /*0x0100*/;
        ActionFlag actionFlag = (ActionFlag) syncClientPacket.ReadUH();
        byte Disposing1 = syncClientPacket.ReadC();
        WeaponSyncType Disposing2 = (WeaponSyncType) syncClientPacket.ReadC();
        syncServerPacket.WriteH((ushort) actionFlag);
        syncServerPacket.WriteC(Disposing1);
        syncServerPacket.WriteC((byte) Disposing2);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.Animation))
      {
        num1 += 2U;
        ushort num2 = syncClientPacket.ReadUH();
        syncServerPacket.WriteH(num2);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.PosRotation))
      {
        num1 += 134217728U /*0x08000000*/;
        ushort num3 = syncClientPacket.ReadUH();
        ushort num4 = syncClientPacket.ReadUH();
        ushort num5 = syncClientPacket.ReadUH();
        ushort num6 = syncClientPacket.ReadUH();
        ushort other = syncClientPacket.ReadUH();
        ushort num7 = syncClientPacket.ReadUH();
        if (player != null)
          player.Position = new Half3(other, num7, num6);
        syncServerPacket.WriteH(num3);
        syncServerPacket.WriteH(num4);
        syncServerPacket.WriteH(num5);
        syncServerPacket.WriteH(num6);
        syncServerPacket.WriteH(other);
        syncServerPacket.WriteH(num7);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.SoundPosRotation))
      {
        num1 += 8388608U /*0x800000*/;
        float num8 = syncClientPacket.ReadT();
        syncServerPacket.WriteT(num8);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.UseObject))
      {
        num1 += 4U;
        byte Disposing3 = syncClientPacket.ReadC();
        syncServerPacket.WriteC(Disposing3);
        for (int index = 0; index < (int) Disposing3; ++index)
        {
          ushort num9 = syncClientPacket.ReadUH();
          byte Disposing4 = syncClientPacket.ReadC();
          CharaMoves Disposing5 = (CharaMoves) syncClientPacket.ReadC();
          syncServerPacket.WriteH(num9);
          syncServerPacket.WriteC(Disposing4);
          syncServerPacket.WriteC((byte) Disposing5);
        }
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.ActionForObjectSync))
      {
        num1 += 16U /*0x10*/;
        byte Disposing6 = syncClientPacket.ReadC();
        byte Disposing7 = syncClientPacket.ReadC();
        if (player != null)
          obj1.SyncInfo(objectHitInfoList, 1);
        syncServerPacket.WriteC(Disposing6);
        syncServerPacket.WriteC(Disposing7);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.RadioChat))
      {
        num1 += 32U /*0x20*/;
        byte Disposing8 = syncClientPacket.ReadC();
        byte Disposing9 = syncClientPacket.ReadC();
        syncServerPacket.WriteC(Disposing8);
        syncServerPacket.WriteC(Disposing9);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.WeaponSync))
      {
        num1 += 67108864U /*0x04000000*/;
        int Model = syncClientPacket.ReadD();
        byte Disposing10 = syncClientPacket.ReadC();
        byte Disposing11 = syncClientPacket.ReadC();
        if (player != null)
        {
          player.WeaponId = Model;
          player.Accessory = Disposing10;
          player.Extensions = Disposing11;
          player.WeaponClass = (ClassType) ComDiv.GetIdStatics(Model, 2);
          obj1.SyncInfo(objectHitInfoList, 2);
        }
        syncServerPacket.WriteD(Model);
        syncServerPacket.WriteC(Disposing10);
        syncServerPacket.WriteC(Disposing11);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.WeaponRecoil))
      {
        num1 += 128U /*0x80*/;
        float num10 = syncClientPacket.ReadT();
        float num11 = syncClientPacket.ReadT();
        float num12 = syncClientPacket.ReadT();
        float num13 = syncClientPacket.ReadT();
        float num14 = syncClientPacket.ReadT();
        byte Disposing12 = syncClientPacket.ReadC();
        int num15 = syncClientPacket.ReadD();
        byte Disposing13 = syncClientPacket.ReadC();
        byte Disposing14 = syncClientPacket.ReadC();
        CLogger.Print($"PVE (WeaponRecoil); Slot: {player.Slot}; Weapon Id: {num15}; Extensions: {Disposing14}; Unknowns: {Disposing13}", LoggerType.Warning, (Exception) null);
        syncServerPacket.WriteT(num10);
        syncServerPacket.WriteT(num11);
        syncServerPacket.WriteT(num12);
        syncServerPacket.WriteT(num13);
        syncServerPacket.WriteT(num14);
        syncServerPacket.WriteC(Disposing12);
        syncServerPacket.WriteD(num15);
        syncServerPacket.WriteC(Disposing13);
        syncServerPacket.WriteC(Disposing14);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.HpSync))
      {
        num1 += 8U;
        ushort num16 = syncClientPacket.ReadUH();
        syncServerPacket.WriteH(num16);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.Suicide))
      {
        num1 += 1048576U /*0x100000*/;
        byte Disposing15 = syncClientPacket.ReadC();
        syncServerPacket.WriteC(Disposing15);
        for (int index = 0; index < (int) Disposing15; ++index)
        {
          Half3 half3 = syncClientPacket.ReadUHV();
          int num17 = syncClientPacket.ReadD();
          byte Disposing16 = syncClientPacket.ReadC();
          byte Disposing17 = syncClientPacket.ReadC();
          uint num18 = syncClientPacket.ReadUD();
          syncServerPacket.WriteHV(half3);
          syncServerPacket.WriteD(num17);
          syncServerPacket.WriteC(Disposing16);
          syncServerPacket.WriteC(Disposing17);
          syncServerPacket.WriteD(num18);
        }
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.DropWeapon))
      {
        num1 += 4194304U /*0x400000*/;
        ushort num19 = syncClientPacket.ReadUH();
        ushort num20 = syncClientPacket.ReadUH();
        ushort num21 = syncClientPacket.ReadUH();
        ushort num22 = syncClientPacket.ReadUH();
        ushort num23 = syncClientPacket.ReadUH();
        ushort num24 = syncClientPacket.ReadUH();
        byte Disposing18 = syncClientPacket.ReadC();
        int num25 = syncClientPacket.ReadD();
        byte Disposing19 = syncClientPacket.ReadC();
        byte Disposing20 = syncClientPacket.ReadC();
        if (ConfigLoader.UseMaxAmmoInDrop)
        {
          syncServerPacket.WriteH(ushort.MaxValue);
          syncServerPacket.WriteH(num20);
          syncServerPacket.WriteH((short) 10000);
        }
        else
        {
          syncServerPacket.WriteH(num19);
          syncServerPacket.WriteH(num20);
          syncServerPacket.WriteH(num21);
        }
        syncServerPacket.WriteH(num22);
        syncServerPacket.WriteH(num23);
        syncServerPacket.WriteH(num24);
        syncServerPacket.WriteC(Disposing18);
        syncServerPacket.WriteD(num25);
        syncServerPacket.WriteC(Disposing19);
        syncServerPacket.WriteC(Disposing20);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.GetWeaponForClient))
      {
        num1 += 16777216U /*0x01000000*/;
        ushort num26 = syncClientPacket.ReadUH();
        ushort num27 = syncClientPacket.ReadUH();
        ushort num28 = syncClientPacket.ReadUH();
        ushort num29 = syncClientPacket.ReadUH();
        ushort num30 = syncClientPacket.ReadUH();
        ushort num31 = syncClientPacket.ReadUH();
        byte Disposing21 = syncClientPacket.ReadC();
        int num32 = syncClientPacket.ReadD();
        byte Disposing22 = syncClientPacket.ReadC();
        byte Disposing23 = syncClientPacket.ReadC();
        if (ConfigLoader.UseMaxAmmoInDrop)
        {
          syncServerPacket.WriteH(ushort.MaxValue);
          syncServerPacket.WriteH(num27);
          syncServerPacket.WriteH((short) 10000);
        }
        else
        {
          syncServerPacket.WriteH(num26);
          syncServerPacket.WriteH(num27);
          syncServerPacket.WriteH(num28);
        }
        syncServerPacket.WriteH(num29);
        syncServerPacket.WriteH(num30);
        syncServerPacket.WriteH(num31);
        syncServerPacket.WriteC(Disposing21);
        syncServerPacket.WriteD(num32);
        syncServerPacket.WriteC(Disposing22);
        syncServerPacket.WriteC(Disposing23);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.FireData))
      {
        num1 += 33554432U /*0x02000000*/;
        byte Disposing24 = syncClientPacket.ReadC();
        byte Disposing25 = syncClientPacket.ReadC();
        short num33 = syncClientPacket.ReadH();
        int num34 = syncClientPacket.ReadD();
        byte Disposing26 = syncClientPacket.ReadC();
        byte Disposing27 = syncClientPacket.ReadC();
        ushort num35 = syncClientPacket.ReadUH();
        ushort num36 = syncClientPacket.ReadUH();
        ushort num37 = syncClientPacket.ReadUH();
        syncServerPacket.WriteC(Disposing24);
        syncServerPacket.WriteC(Disposing25);
        syncServerPacket.WriteH(num33);
        syncServerPacket.WriteD(num34);
        syncServerPacket.WriteC(Disposing26);
        syncServerPacket.WriteC(Disposing27);
        syncServerPacket.WriteH(num35);
        syncServerPacket.WriteH(num36);
        syncServerPacket.WriteH(num37);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.CharaFireNHitData))
      {
        num1 += 1024U /*0x0400*/;
        byte Disposing28 = syncClientPacket.ReadC();
        syncServerPacket.WriteC(Disposing28);
        for (int index = 0; index < (int) Disposing28; ++index)
        {
          int num38 = syncClientPacket.ReadD();
          byte Disposing29 = syncClientPacket.ReadC();
          byte Disposing30 = syncClientPacket.ReadC();
          ushort num39 = syncClientPacket.ReadUH();
          uint num40 = syncClientPacket.ReadUD();
          ushort num41 = syncClientPacket.ReadUH();
          ushort num42 = syncClientPacket.ReadUH();
          ushort num43 = syncClientPacket.ReadUH();
          syncServerPacket.WriteD(num38);
          syncServerPacket.WriteC(Disposing29);
          syncServerPacket.WriteC(Disposing30);
          syncServerPacket.WriteH(num39);
          syncServerPacket.WriteD(num40);
          syncServerPacket.WriteH(num41);
          syncServerPacket.WriteH(num42);
          syncServerPacket.WriteH(num43);
        }
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.GetWeaponForHost))
      {
        num1 += 512U /*0x0200*/;
        CharaDeath Disposing31 = (CharaDeath) syncClientPacket.ReadC();
        byte Disposing32 = syncClientPacket.ReadC();
        ushort num44 = syncClientPacket.ReadUH();
        ushort num45 = syncClientPacket.ReadUH();
        ushort num46 = syncClientPacket.ReadUH();
        int num47 = syncClientPacket.ReadD();
        CharaHitPart Disposing33 = (CharaHitPart) syncClientPacket.ReadC();
        syncServerPacket.WriteC((byte) Disposing31);
        syncServerPacket.WriteC(Disposing32);
        syncServerPacket.WriteH(num44);
        syncServerPacket.WriteH(num45);
        syncServerPacket.WriteH(num46);
        syncServerPacket.WriteD(num47);
        syncServerPacket.WriteC((byte) Disposing33);
      }
      if (obj0.Flag.HasFlag((System.Enum) UdpGameEvent.FireDataOnObject))
      {
        num1 += 1073741824U /*0x40000000*/;
        byte Disposing34 = syncClientPacket.ReadC();
        CharaHitPart Disposing35 = (CharaHitPart) syncClientPacket.ReadC();
        byte Disposing36 = syncClientPacket.ReadC();
        syncServerPacket.WriteC(Disposing34);
        syncServerPacket.WriteC((byte) Disposing35);
        syncServerPacket.WriteC(Disposing36);
      }
      obj2 = syncServerPacket.ToArray();
      if ((UdpGameEvent) num1 != obj0.Flag)
        CLogger.Print(Bitwise.ToHexData($"PVE - Missing Flag Events: '{(ValueType) (uint) obj0.Flag}' | '{(ValueType) (uint) (obj0.Flag - num1)}'", Length), LoggerType.Opcode, (Exception) null);
      return objectHitInfoList;
    }
  }

  public byte[] WriteBotActionData([In] byte[] obj0, [In] RoomModel obj1)
  {
    SyncClientPacket syncClientPacket = new SyncClientPacket(obj0);
    List<ObjectHitInfo> Data = new List<ObjectHitInfo>();
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
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
                  byte[] MStream1;
                  Data.AddRange((IEnumerable<ObjectHitInfo>) this.method_1(actionModel, obj1, ref MStream1));
                  syncServerPacket.GoBack(5);
                  syncServerPacket.WriteH((ushort) (MStream1.Length + 9));
                  syncServerPacket.WriteH(actionModel.Slot);
                  syncServerPacket.WriteC((byte) ((AnimModel) actionModel).get_SubHead());
                  syncServerPacket.WriteD((uint) actionModel.Flag);
                  syncServerPacket.WriteB(MStream1);
                  if (((AnimModel) actionModel).get_Data().Length == 0 && (int) actionModel.Length - 9 != 0)
                  {
                    flag1 = true;
                    break;
                  }
                  break;
                case UdpSubHead.Grenade:
                  byte Disposing1 = syncClientPacket.ReadC();
                  byte Disposing2 = syncClientPacket.ReadC();
                  byte Disposing3 = syncClientPacket.ReadC();
                  byte Disposing4 = syncClientPacket.ReadC();
                  ushort num1 = syncClientPacket.ReadUH();
                  int num2 = syncClientPacket.ReadD();
                  byte Disposing5 = syncClientPacket.ReadC();
                  byte Disposing6 = syncClientPacket.ReadC();
                  ushort num3 = syncClientPacket.ReadUH();
                  ushort num4 = syncClientPacket.ReadUH();
                  ushort num5 = syncClientPacket.ReadUH();
                  int num6 = syncClientPacket.ReadD();
                  int num7 = syncClientPacket.ReadD();
                  int num8 = syncClientPacket.ReadD();
                  syncServerPacket.WriteC(Disposing1);
                  syncServerPacket.WriteC(Disposing2);
                  syncServerPacket.WriteC(Disposing3);
                  syncServerPacket.WriteC(Disposing4);
                  syncServerPacket.WriteH(num1);
                  syncServerPacket.WriteD(num2);
                  syncServerPacket.WriteC(Disposing5);
                  syncServerPacket.WriteC(Disposing6);
                  syncServerPacket.WriteH(num3);
                  syncServerPacket.WriteH(num4);
                  syncServerPacket.WriteH(num5);
                  syncServerPacket.WriteD(num6);
                  syncServerPacket.WriteD(num7);
                  syncServerPacket.WriteD(num8);
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
                    CLogger.Print(Bitwise.ToHexData($"PVP Sub Head: '{((AnimModel) actionModel).get_SubHead()}' or '{(int) ((AnimModel) actionModel).get_SubHead()}'", obj0), LoggerType.Opcode, (Exception) null);
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
            CLogger.Print($"PVE Action Data - Buffer (Length: {obj0.Length}): | {ex.Message}", LoggerType.Error, ex);
          Data = new List<ObjectHitInfo>();
          break;
        }
      }
      if (Data.Count > 0)
        syncServerPacket.WriteB(StageInfoObjControl.GET_CODE(Data));
      return syncServerPacket.ToArray();
    }
  }

  public void SendPacket(byte[] actionModel_0, IPEndPoint roomModel_0)
  {
    try
    {
      ((MatchClient) this).socket_0.BeginSendTo(actionModel_0, 0, actionModel_0.Length, SocketFlags.None, (EndPoint) roomModel_0, new AsyncCallback(this.method_2), (object) ((MatchClient) this).socket_0);
    }
    catch (Exception ex)
    {
      CLogger.Print($"Failed to send package to {roomModel_0}: {ex.Message}", LoggerType.Error, ex);
    }
  }

  private void method_2([In] IAsyncResult obj0)
  {
    try
    {
      if (!(obj0.AsyncState is Socket asyncState) || !asyncState.Connected)
        return;
      asyncState.EndSend(obj0);
    }
    catch (SocketException ex)
    {
      CLogger.Print($"Socket Error on Send: {ex.SocketErrorCode}", LoggerType.Warning, (Exception) null);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("Socket was closed while sending.", LoggerType.Warning, (Exception) null);
    }
    catch (Exception ex)
    {
      CLogger.Print("Error during EndSendCallback: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public static MatchSync Sync { get; set; }

  public static MatchManager Client { get; [param: In] set; }

  public static ConcurrentDictionary<string, int> SpamConnections { get; [CompilerGenerated, SpecialName] set; }

  public static ConcurrentDictionary<IPEndPoint, Socket> UdpClients { [CompilerGenerated, SpecialName] get; [CompilerGenerated, SpecialName] [param: In] set; }
}
