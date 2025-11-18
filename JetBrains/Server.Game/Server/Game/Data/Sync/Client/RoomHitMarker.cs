// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Sync.Client.RoomHitMarker
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Sync.Client;

public static class RoomHitMarker
{
  public static void RegistryFragInfos(
    [In] RoomModel obj0,
    [In] SlotModel obj1,
    [In] ref int obj2,
    [In] bool obj3,
    [In] bool obj4,
    FragInfos Y)
  {
    obj2 = 0;
    ItemClass idStatics1 = (ItemClass) ComDiv.GetIdStatics(Y.WeaponId, 1);
    ClassType idStatics2 = (ClassType) ComDiv.GetIdStatics(Y.WeaponId, 2);
    foreach (FragModel frag in Y.Frags)
    {
      CharaDeath charaDeath = (CharaDeath) ((int) frag.HitspotInfo >> 4);
      if ((int) Y.KillsCount - (obj4 ? 1 : 0) > 1)
      {
        frag.KillFlag |= charaDeath == CharaDeath.BOOM || charaDeath == CharaDeath.OBJECT_EXPLOSION || charaDeath == CharaDeath.POISON || charaDeath == CharaDeath.HOWL || charaDeath == CharaDeath.TRAMPLED || idStatics2 == ClassType.Shotgun ? KillingMessage.MassKill : KillingMessage.PiercingShot;
      }
      else
      {
        int num1 = 0;
        switch (charaDeath)
        {
          case CharaDeath.DEFAULT:
            if (idStatics1 == ItemClass.Melee)
            {
              num1 = 6;
              break;
            }
            break;
          case CharaDeath.HEADSHOT:
            num1 = 4;
            break;
        }
        if (num1 > 0)
        {
          int num2 = obj1.LastKillState >> 12;
          switch (num1)
          {
            case 4:
              if (num2 != 4)
                obj1.RepeatLastState = false;
              obj1.LastKillState = num1 << 12 | obj1.KillsOnLife + 1;
              if (obj1.RepeatLastState)
              {
                frag.KillFlag |= (obj1.LastKillState & 16383 /*0x3FFF*/) <= 1 ? KillingMessage.Headshot : KillingMessage.ChainHeadshot;
                break;
              }
              frag.KillFlag |= KillingMessage.Headshot;
              obj1.RepeatLastState = true;
              break;
            case 6:
              if (num2 != 6)
                obj1.RepeatLastState = false;
              obj1.LastKillState = num1 << 12 | obj1.KillsOnLife + 1;
              if (obj1.RepeatLastState && (obj1.LastKillState & 16383 /*0x3FFF*/) > 1)
              {
                frag.KillFlag |= KillingMessage.ChainSlugger;
                break;
              }
              obj1.RepeatLastState = true;
              break;
          }
        }
        else
        {
          obj1.LastKillState = 0;
          obj1.RepeatLastState = false;
        }
      }
      byte victimSlot = frag.VictimSlot;
      byte assistSlot = frag.AssistSlot;
      SlotModel slot1 = obj0.Slots[(int) victimSlot];
      SlotModel slot2 = obj0.Slots[(int) assistSlot];
      if (slot1.KillsOnLife > 3)
        frag.KillFlag |= KillingMessage.ChainStopper;
      if (Y.WeaponId != 19016 && Y.WeaponId != 19022 || (int) Y.KillerSlot != (int) victimSlot || !slot1.SpecGM)
        ++slot1.AllDeaths;
      if ((int) Y.KillerSlot != (int) assistSlot)
        ++slot2.AllAssists;
      if (obj0.RoomType == RoomCondition.FreeForAll)
      {
        ++obj1.AllKills;
        if (obj1.DeathState == DeadEnum.Alive)
          ++obj1.KillsOnLife;
      }
      else if (obj1.Team != slot1.Team)
      {
        obj2 += AllUtils.GetKillScore(frag.KillFlag);
        ++obj1.AllKills;
        if (obj1.DeathState == DeadEnum.Alive)
          ++obj1.KillsOnLife;
        if (slot1.Team == TeamEnum.FR_TEAM)
        {
          ++obj0.FRDeaths;
          ++obj0.CTKills;
        }
        else
        {
          ++obj0.CTDeaths;
          ++obj0.FRKills;
        }
        if (obj0.IsDinoMode("DE"))
        {
          if (obj1.Team == TeamEnum.FR_TEAM)
            obj0.FRDino += 4;
          else
            obj0.CTDino += 4;
        }
      }
      slot1.LastKillState = 0;
      slot1.KillsOnLife = 0;
      slot1.RepeatLastState = false;
      slot1.PassSequence = 0;
      slot1.DeathState = DeadEnum.Dead;
      if (!obj3)
      {
        switch (idStatics2)
        {
          case ClassType.Assault:
            ++obj1.AR[0];
            ++slot1.AR[1];
            break;
          case ClassType.SMG:
            ++obj1.SMG[0];
            ++slot1.SMG[1];
            break;
          case ClassType.Sniper:
            ++obj1.SR[0];
            ++slot1.SR[1];
            break;
          case ClassType.Shotgun:
            ++obj1.SG[0];
            ++slot1.SG[1];
            break;
          case ClassType.Machinegun:
            ++obj1.MG[0];
            ++slot1.MG[1];
            break;
          case ClassType.Shield:
            ++obj1.SHD[0];
            ++slot1.SHD[1];
            break;
        }
        AllUtils.CompleteMission(obj0, slot1, MissionType.DEATH, 0);
      }
      if (charaDeath == CharaDeath.HEADSHOT)
        ++obj1.AllHeadshots;
    }
  }
}
