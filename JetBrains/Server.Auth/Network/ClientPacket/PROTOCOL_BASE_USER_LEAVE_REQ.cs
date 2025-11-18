// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_USER_LEAVE_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_USER_LEAVE_REQ : AuthClientPacket
{
  public virtual void Read()
  {
    ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 = (int) this.ReadC();
    ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_1 = (int) this.ReadC();
    int num1 = (int) this.ReadH();
    if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 1) == 1)
    {
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_2 = (int) this.ReadH();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_3 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_4 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_5 = this.ReadD();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_6 = (int) this.ReadC();
      this.ReadB(5);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_7 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_8 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_9 = (int) this.ReadH();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_10 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_11 = (int) this.ReadC();
      int num2 = (int) this.ReadC();
      int num3 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_12 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_13 = (int) this.ReadC();
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_14 = (int) this.ReadC();
      int num4 = (int) this.ReadC();
      int num5 = (int) this.ReadC();
      int num6 = (int) this.ReadC();
    }
    if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 2) == 2)
    {
      this.ReadB(5);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).byte_0 = this.ReadB(240 /*0xF0*/);
    }
    if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 4) == 4)
    {
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_1 = this.ReadU((int) this.ReadC() * 2);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_2 = this.ReadU((int) this.ReadC() * 2);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_3 = this.ReadU((int) this.ReadC() * 2);
      ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).string_4 = this.ReadU((int) this.ReadC() * 2);
    }
    if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 8) != 8)
      return;
    ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_15 = (int) this.ReadH();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      DBQuery int_0 = new DBQuery();
      PlayerConfig config = player.Config;
      if (config != null)
      {
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 1) == 1)
          this.method_0(int_0, config);
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 2) == 2 && config.KeyboardKeys != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).byte_0)
        {
          config.KeyboardKeys = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).byte_0;
          int_0.AddQuery("keyboard_keys", (object) config.KeyboardKeys);
        }
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 4) == 4)
          ((PROTOCOL_MATCH_SERVER_IDX_REQ) this).method_1(int_0, config);
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 8) == 8 && config.Nations != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_15)
        {
          config.Nations = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_15;
          int_0.AddQuery("nations", (object) config.Nations);
        }
        ComDiv.UpdateDB("player_configs", "owner_id", (object) player.PlayerId, int_0.GetTables(), int_0.GetValues());
      }
      this.Client.SendPacket((AuthServerPacket) new PROTOCOL_BASE_USER_EVENT_SYNC_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0(DBQuery int_0, PlayerConfig int_1)
  {
    if (int_1.ShowBlood != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_2)
    {
      int_1.ShowBlood = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_2;
      int_0.AddQuery("show_blood", (object) int_1.ShowBlood);
    }
    if (int_1.Crosshair != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_3)
    {
      int_1.Crosshair = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_3;
      int_0.AddQuery("crosshair", (object) int_1.Crosshair);
    }
    if (int_1.HandPosition != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_4)
    {
      int_1.HandPosition = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_4;
      int_0.AddQuery("hand_pos", (object) int_1.HandPosition);
    }
    if (int_1.Config != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_5)
    {
      int_1.Config = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_5;
      int_0.AddQuery("configs", (object) int_1.Config);
    }
    if (int_1.AudioEnable != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_6)
    {
      int_1.AudioEnable = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_6;
      int_0.AddQuery("audio_enable", (object) int_1.AudioEnable);
    }
    if (int_1.AudioSFX != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_7)
    {
      int_1.AudioSFX = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_7;
      int_0.AddQuery("audio_sfx", (object) int_1.AudioSFX);
    }
    if (int_1.AudioBGM != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_8)
    {
      int_1.AudioBGM = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_8;
      int_0.AddQuery("audio_bgm", (object) int_1.AudioBGM);
    }
    if (int_1.PointOfView != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_9)
    {
      int_1.PointOfView = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_9;
      int_0.AddQuery("pov_size", (object) int_1.PointOfView);
    }
    if (int_1.Sensitivity != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_10)
    {
      int_1.Sensitivity = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_10;
      int_0.AddQuery("sensitivity", (object) int_1.Sensitivity);
    }
    if (int_1.InvertMouse != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_11)
    {
      int_1.InvertMouse = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_11;
      int_0.AddQuery("invert_mouse", (object) int_1.InvertMouse);
    }
    if (int_1.EnableInviteMsg != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_12)
    {
      int_1.EnableInviteMsg = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_12;
      int_0.AddQuery("enable_invite", (object) int_1.EnableInviteMsg);
    }
    if (int_1.EnableWhisperMsg != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_13)
    {
      int_1.EnableWhisperMsg = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_13;
      int_0.AddQuery("enable_whisper", (object) int_1.EnableWhisperMsg);
    }
    if (int_1.Macro == ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_14)
      return;
    int_1.Macro = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_14;
    int_0.AddQuery("macro_enable", (object) int_1.Macro);
  }
}
