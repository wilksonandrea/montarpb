// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.PlayerQuickstart
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Utility;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Models;

public class PlayerQuickstart
{
  public byte[] GetCurrentMissionList()
  {
    if (((PlayerMissions) this).ActualMission == 0)
      return ((PlayerMissions) this).List1;
    if (((PlayerMissions) this).ActualMission == 1)
      return ((PlayerMissions) this).List2;
    return ((PlayerMissions) this).ActualMission == 2 ? ((PlayerMissions) this).List3 : ((PlayerMissions) this).List4;
  }

  public int GetCurrentCard() => this.GetCard(((PlayerMissions) this).ActualMission);

  public int GetCard(int value)
  {
    switch (value)
    {
      case 0:
        return ((PlayerMissions) this).Card1;
      case 1:
        return ((PlayerMissions) this).Card2;
      case 2:
        return ((PlayerMissions) this).Card3;
      default:
        return ((PlayerMissions) this).Card4;
    }
  }

  public int GetCurrentMissionId()
  {
    if (((PlayerMissions) this).ActualMission == 0)
      return ((PlayerMissions) this).Mission1;
    if (((PlayerMissions) this).ActualMission == 1)
      return ((PlayerMissions) this).Mission2;
    return ((PlayerMissions) this).ActualMission == 2 ? ((PlayerMissions) this).Mission3 : ((PlayerMissions) this).Mission4;
  }

  public void UpdateSelectedCard()
  {
    int currentCard = this.GetCurrentCard();
    if (ushort.MaxValue != ComDiv.GetMissionCardFlags(this.GetCurrentMissionId(), currentCard, this.GetCurrentMissionList()))
      return;
    ((PlayerMissions) this).SelectedCard = true;
  }

  public long OwnerId
  {
    get => this.long_0;
    [CompilerGenerated, SpecialName] set => ((PlayerQuickstart) this).long_0 = value;
  }

  public List<QuickstartModel> Quickjoins
  {
    [CompilerGenerated, SpecialName] get => ((PlayerQuickstart) this).list_0;
    [CompilerGenerated, SpecialName] set => ((PlayerQuickstart) this).list_0 = value;
  }
}
