// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.PacketModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Data.Models;

public class PacketModel
{
  [CompilerGenerated]
  [SpecialName]
  public void set_Effects(List<DeffectModel> value) => ((ObjectModel) this).list_1 = value;

  public PacketModel(bool value)
  {
    ((ObjectModel) this).NeedSync = value;
    if (value)
      ((ObjectModel) this).Animations = new List<AnimModel>();
    ((ObjectModel) this).UpdateId = 1;
    this.set_Effects(new List<DeffectModel>());
  }

  public int CheckDestroyState(int value)
  {
    for (int index = ((ObjectModel) this).Effects.Count - 1; index > -1; --index)
    {
      DeffectModel effect = ((ObjectModel) this).Effects[index];
      if (((MapModel) effect).get_Life() >= value)
        return ((MapModel) effect).get_Id();
    }
    return 0;
  }

  public int GetRandomAnimation(RoomModel value, [In] ObjectInfo obj1)
  {
    if (((ObjectModel) this).Animations != null && ((ObjectModel) this).Animations.Count > 0)
    {
      AnimModel animation = ((ObjectModel) this).Animations[new Random().Next(((ObjectModel) this).Animations.Count)];
      obj1.Animation = animation;
      ((ObjectModel) obj1).set_UseDate(DateTimeUtil.Now());
      if (animation.OtherObj > 0)
      {
        ObjectInfo objectInfo = value.Objects[animation.OtherObj];
        this.GetAnim(((AssistServerData) animation).get_OtherAnim(), 0.0f, 0.0f, objectInfo);
      }
      return animation.Id;
    }
    obj1.Animation = (AnimModel) null;
    return (int) byte.MaxValue;
  }

  public void GetAnim(int value, [In] float obj1, [In] float obj2, [In] ObjectInfo obj3)
  {
    if (value == (int) byte.MaxValue || obj3 == null || ((ObjectModel) obj3).get_Model() == null || ((ObjectModel) obj3).get_Model().Animations == null || ((ObjectModel) obj3).get_Model().Animations.Count == 0)
      return;
    foreach (AnimModel animation in ((ObjectModel) obj3).get_Model().Animations)
    {
      if (animation.Id == value)
      {
        obj3.Animation = animation;
        obj1 -= obj2;
        ((ObjectModel) obj3).set_UseDate(DateTimeUtil.Now().AddSeconds((double) obj1 * -1.0));
        break;
      }
    }
  }

  public int Opcode { get; [param: In] set; }

  public int Slot { get; set; }

  public int Round { get; [param: In] set; }

  public int Length { get; [param: In] set; }

  public int Respawn { get; [param: In] set; }

  public int RoundNumber { get; set; }

  public int AccountId { get; set; }

  public int Unk1 { get; set; }

  public int Unk2 { get; set; }

  public float Time { get; set; }

  public byte[] Data { get; set; }

  public byte[] WithEndData { get; set; }

  public byte[] WithoutEndData
  {
    [CompilerGenerated, SpecialName] get => ((PacketModel) this).byte_2;
    [CompilerGenerated, SpecialName] set => ((PacketModel) this).byte_2 = value;
  }

  public DateTime ReceiveDate
  {
    [CompilerGenerated, SpecialName] get => ((PacketModel) this).dateTime_0;
    [CompilerGenerated, SpecialName] set => ((PacketModel) this).dateTime_0 = value;
  }
}
