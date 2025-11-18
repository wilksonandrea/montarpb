// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Models.AccountStatus
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Models;

public class AccountStatus
{
  [CompilerGenerated]
  [SpecialName]
  public int get_ItemId() => ((CouponFlag) this).int_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_ItemId(int value) => ((CouponFlag) this).int_0 = value;

  [CompilerGenerated]
  [SpecialName]
  public CouponEffects get_EffectFlag() => ((CouponFlag) this).couponEffects_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_EffectFlag(CouponEffects value) => ((CouponFlag) this).couponEffects_0 = value;

  public AccountStatus()
  {
  }

  public long PlayerId { get; set; }

  public byte ChannelId { get; set; }

  public byte RoomId { get; set; }

  public byte ClanMatchId { get; set; }

  public byte ServerId { get; set; }

  public byte[] Buffer { get; set; }

  public AccountStatus() => this.Buffer = new byte[4];

  public void ResetData(long value)
  {
    if (value == 0L)
      return;
    int channelId1 = (int) this.ChannelId;
    int roomId = (int) this.RoomId;
    int clanMatchId = (int) this.ClanMatchId;
    int serverId = (int) this.ServerId;
    this.SetData(uint.MaxValue, value);
    int channelId2 = (int) this.ChannelId;
    if (channelId1 == channelId2 && roomId == (int) this.RoomId && clanMatchId == (int) this.ClanMatchId && serverId == (int) this.ServerId)
      return;
    ComDiv.UpdateDB("accounts", "status", (object) (long) uint.MaxValue, "player_id", (object) value);
  }

  public void SetData(uint value, [In] long obj1)
  {
    this.SetData(BitConverter.GetBytes(value), obj1);
  }

  public void SetData(byte[] value, [In] long obj1)
  {
    this.PlayerId = obj1;
    this.Buffer = value;
    this.ChannelId = value[0];
    this.RoomId = value[1];
    this.ServerId = value[2];
    this.ClanMatchId = value[3];
  }
}
