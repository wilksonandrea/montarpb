// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Network.BaseClientPacket
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.RAW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Plugin.Core.Network;

public abstract class BaseClientPacket
{
  protected MemoryStream MStream;
  protected BinaryReader BReader;
  protected SafeHandle Handle;
  protected bool Disposed;
  protected int SECURITY_KEY;
  protected int HASH_CODE;
  protected int SEED_LENGTH;
  protected NationsEnum NATIONS;

  private static void smethod_2(string MissionId, string CardBasicId, [In] int obj2)
  {
    int num1 = MissionCardRAW.smethod_1(CardBasicId);
    if (num1 == 0)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("Invalid: " + CardBasicId, LoggerType.Warning, (Exception) null);
    }
    byte[] Length;
    try
    {
      Length = File.ReadAllBytes(MissionId);
    }
    catch
    {
      Length = new byte[0];
    }
    if (Length.Length == 0)
      return;
    try
    {
      SyncClientPacket syncClientPacket = new SyncClientPacket(Length);
      syncClientPacket.ReadS(4);
      int num2 = syncClientPacket.ReadD();
      syncClientPacket.ReadB(16 /*0x10*/);
      int num3 = 0;
      int num4 = 0;
      for (int index = 0; index < 40; ++index)
      {
        int num5 = num4++;
        int num6 = num3;
        if (num4 == 4)
        {
          num4 = 0;
          ++num3;
        }
        int num7 = (int) syncClientPacket.ReadUH();
        int num8 = (int) syncClientPacket.ReadC();
        int num9 = (int) syncClientPacket.ReadC();
        int num10 = (int) syncClientPacket.ReadC();
        ClassType classType = (ClassType) syncClientPacket.ReadC();
        int num11 = (int) syncClientPacket.ReadUH();
        int num12 = num5;
        MissionItemAward missionItemAward = new MissionItemAward(num6, num12);
        ((MissionCardModel) missionItemAward).MapId = num9;
        ((MissionCardModel) missionItemAward).WeaponReq = classType;
        ((MissionCardModel) missionItemAward).WeaponReqId = num11;
        ((MissionCardModel) missionItemAward).MissionType = (MissionType) num8;
        ((MissionCardModel) missionItemAward).MissionLimit = num10;
        ((MissionCardModel) missionItemAward).MissionId = num1;
        MissionCardModel missionCardModel = (MissionCardModel) missionItemAward;
        MissionCardRAW.list_1.Add(missionCardModel);
        if (num2 == 1)
          syncClientPacket.ReadB(24);
      }
      int num13 = num2 == 2 ? 5 : 1;
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int num14 = syncClientPacket.ReadD();
        int num15 = syncClientPacket.ReadD();
        int CardBasicId1 = syncClientPacket.ReadD();
        for (int index2 = 0; index2 < num13; ++index2)
        {
          syncClientPacket.ReadD();
          syncClientPacket.ReadD();
          syncClientPacket.ReadD();
          syncClientPacket.ReadD();
        }
        if (obj2 == 1)
        {
          MissionCardModel missionCardModel = new MissionCardModel();
          ((MissionCardAwards) missionCardModel).Id = num1;
          ((MissionCardAwards) missionCardModel).Card = index1;
          missionCardModel.set_Exp(num2 == 1 ? num15 * 10 : num15);
          missionCardModel.set_Gold(num14);
          MissionCardAwards missionCardAwards = (MissionCardAwards) missionCardModel;
          BaseClientPacket.smethod_3(missionCardAwards, CardBasicId1);
          if (!((MissionCardModel) missionCardAwards).Unusable())
            MissionCardRAW.list_2.Add(missionCardAwards);
        }
      }
      if (num2 != 2)
        return;
      syncClientPacket.ReadD();
      syncClientPacket.ReadB(8);
      for (int index = 0; index < 5; ++index)
      {
        int num16 = syncClientPacket.ReadD();
        syncClientPacket.ReadD();
        int num17 = syncClientPacket.ReadD();
        uint num18 = syncClientPacket.ReadUD();
        if (num16 > 0 && obj2 == 1)
        {
          MissionStore missionStore = new MissionStore();
          missionStore.set_MissionId(num1);
          missionStore.set_Item((ItemsModel) new PlayerBonus(num17, "Mission Item", ItemEquipType.Durable, num18));
          MissionItemAward missionItemAward = (MissionItemAward) missionStore;
          MissionCardRAW.list_0.Add(missionItemAward);
        }
      }
    }
    catch (XmlException ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"File error: {MissionId}; {ex.Message}", LoggerType.Error, (Exception) ex);
    }
  }

  private static void smethod_3([In] MissionCardAwards obj0, int CardBasicId)
  {
    if (CardBasicId == 0)
      return;
    if (CardBasicId >= 1 && CardBasicId <= 50)
      ++obj0.Ribbon;
    else if (CardBasicId >= 51 && CardBasicId <= 100)
    {
      ++obj0.Ensign;
    }
    else
    {
      if (CardBasicId < 101 || CardBasicId > 116)
        return;
      ++obj0.Medal;
    }
  }

  public static MissionCardAwards GetAward(int string_0, int string_1)
  {
    foreach (MissionCardAwards award in MissionCardRAW.list_2)
    {
      if (award.Id == string_0 && award.Card == string_1)
        return award;
    }
    return (MissionCardAwards) null;
  }

  public BaseClientPacket()
  {
  }

  static BaseClientPacket()
  {
    MissionCardRAW.list_0 = new List<MissionItemAward>();
    MissionCardRAW.list_1 = new List<MissionCardModel>();
    MissionCardRAW.list_2 = new List<MissionCardAwards>();
  }

  public BaseClientPacket()
  {
  }

  protected internal byte[] ReadB([In] int obj0) => this.BReader.ReadBytes(obj0);

  protected internal byte ReadC() => this.BReader.ReadByte();

  protected internal short ReadH() => this.BReader.ReadInt16();

  protected internal ushort ReadUH() => this.BReader.ReadUInt16();

  protected internal int ReadD() => this.BReader.ReadInt32();

  protected internal uint ReadUD() => this.BReader.ReadUInt32();

  protected internal float ReadT() => this.BReader.ReadSingle();

  protected internal long ReadQ() => this.BReader.ReadInt64();
}
