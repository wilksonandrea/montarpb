// Decompiled with JetBrains decompiler
// Type: dummy_ptr.{3c03a772-ca25-42e6-b519-6caf572c6109}
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Client;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace dummy_ptr;

internal abstract class \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D
{
  private static void smethod_0(
    RoomModel Room,
    List<DeathServerData> Player,
    PlayerModel ObjM,
    PlayerModel ObjI,
    CharaDeath Damage)
  {
    ObjM.Life = 0;
    ObjM.Dead = true;
    ObjM.LastDie = DateTimeUtil.Now();
    DeffectModel deffectModel = new DeffectModel();
    deffectModel.set_Player(ObjM);
    ((DeathServerData) deffectModel).DeathType = Damage;
    DeathServerData deathServerData = (DeathServerData) deffectModel;
    AssistServerData assist = RoomsManager.GetAssist(ObjM.Slot, Room.RoomId);
    if (assist != null)
      ((DeffectModel) deathServerData).set_AssistSlot(assist.IsAssist ? assist.Killer : ObjI.Slot);
    else
      ((DeffectModel) deathServerData).set_AssistSlot(ObjI.Slot);
    Player.Add(deathServerData);
    RoomsManager.RemoveAssist(assist);
  }

  private static void smethod_1(
    List<ObjectHitInfo> roomModel_0,
    PlayerModel list_0,
    PlayerModel playerModel_0,
    CharaDeath playerModel_1,
    CharaHitPart charaDeath_0)
  {
    ObjectInfo objectInfo = new ObjectInfo(5);
    ((ObjectHitInfo) objectInfo).ObjId = list_0.Slot;
    ((ObjectHitInfo) objectInfo).KillerSlot = playerModel_0.Slot;
    objectInfo.set_DeathType(playerModel_1);
    ((ObjectHitInfo) objectInfo).ObjLife = list_0.Life;
    objectInfo.set_HitPart(charaDeath_0);
    ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
    roomModel_0.Add(objectHitInfo);
  }

  public static void BoomDeath(
    RoomModel list_0,
    PlayerModel playerModel_0,
    int playerModel_1,
    int charaDeath_0,
    List<DeathServerData> charaHitPart_0,
    [In] List<ObjectHitInfo> obj5,
    [In] List<int> obj6,
    [In] CharaHitPart obj7,
    [In] CharaDeath obj8)
  {
    if (obj6 == null || obj6.Count == 0)
      return;
    foreach (int Round in obj6)
    {
      PlayerModel ObjM;
      if (list_0.GetPlayer(Round, ref ObjM) && !ObjM.Dead)
      {
        \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.smethod_0(list_0, charaHitPart_0, ObjM, playerModel_0, obj8);
        if (playerModel_1 > 0)
        {
          if (ConfigLoader.UseHitMarker)
            MatchRoundSync.SendHitMarkerSync(list_0, playerModel_0, obj8, HitType.Normal, playerModel_1);
          ObjectInfo objectInfo = new ObjectInfo(2);
          ((ObjectHitInfo) objectInfo).ObjId = ObjM.Slot;
          ((ObjectHitInfo) objectInfo).ObjLife = ObjM.Life;
          objectInfo.set_HitPart(obj7);
          ((ObjectHitInfo) objectInfo).KillerSlot = playerModel_0.Slot;
          ((ObjectHitInfo) objectInfo).Position = (Half3) ((Vector3) ObjM.Position - (Vector3) playerModel_0.Position);
          objectInfo.set_DeathType(obj8);
          ((ObjectHitInfo) objectInfo).WeaponId = charaDeath_0;
          ObjectHitInfo objectHitInfo = (ObjectHitInfo) objectInfo;
          obj5.Add(objectHitInfo);
        }
      }
    }
  }

  public static void SimpleDeath(
    [In] RoomModel obj0,
    [In] List<DeathServerData> obj1,
    [In] List<ObjectHitInfo> obj2,
    [In] PlayerModel obj3,
    PlayerModel Deaths,
    int Objs,
    int BoomPlayers,
    CharaHitPart HitPart,
    CharaDeath DeathType)
  {
    Deaths.Life -= Objs;
    \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.smethod_2(obj0, Deaths, obj3, Deaths.Life);
    if (Deaths.Life <= 0)
      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.smethod_0(obj0, obj1, Deaths, obj3, DeathType);
    else
      \u007B3c03a772\u002Dca25\u002D42e6\u002Db519\u002D6caf572c6109\u007D.smethod_1(obj2, Deaths, obj3, DeathType, HitPart);
  }

  private static void smethod_2([In] RoomModel obj0, [In] PlayerModel obj1, [In] PlayerModel obj2, [In] int obj3)
  {
    BombPosition bombPosition = new BombPosition();
    ((AssistServerData) bombPosition).RoomId = obj0.RoomId;
    ((AssistServerData) bombPosition).Killer = obj2.Slot;
    ((AssistServerData) bombPosition).Victim = obj1.Slot;
    bombPosition.set_IsKiller(obj3 <= 0);
    bombPosition.set_VictimDead(obj3 <= 0);
    AssistServerData assistServerData = (AssistServerData) bombPosition;
    assistServerData.IsAssist = !((BombPosition) assistServerData).get_IsKiller();
    if (assistServerData.Killer == assistServerData.Victim)
      return;
    RoomsManager.AddAssist(assistServerData);
  }

  public abstract void mp000001();

  public abstract void mp000002();

  public abstract void mp000003();

  public abstract void mp000004();

  public abstract void mp000005();
}
