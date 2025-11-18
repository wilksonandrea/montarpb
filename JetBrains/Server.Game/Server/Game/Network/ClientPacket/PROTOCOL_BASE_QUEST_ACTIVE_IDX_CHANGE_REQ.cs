// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ : GameClientPacket
{
  private int int_0;
  private int int_1;
  private int int_2;

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
      DBQuery account_0 = new DBQuery();
      PlayerConfig config = player.Config;
      if (config != null)
      {
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 1) == 1)
          this.method_0(account_0, config);
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 2) == 2 && config.KeyboardKeys != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).byte_0)
        {
          config.KeyboardKeys = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).byte_0;
          account_0.AddQuery("keyboard_keys", (object) config.KeyboardKeys);
        }
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 4) == 4)
          ((PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ) this).method_1(account_0, config);
        if ((((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_0 & 8) == 8 && config.Nations != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_15)
        {
          config.Nations = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_15;
          account_0.AddQuery("nations", (object) config.Nations);
        }
        ComDiv.UpdateDB("player_configs", "owner_id", (object) player.PlayerId, account_0.GetTables(), account_0.GetValues());
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_SELECT_CHANNEL_ACK());
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0(DBQuery account_0, PlayerConfig ticketModel_0)
  {
    if (ticketModel_0.ShowBlood != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_2)
    {
      ticketModel_0.ShowBlood = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_2;
      account_0.AddQuery("show_blood", (object) ticketModel_0.ShowBlood);
    }
    if (ticketModel_0.Crosshair != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_3)
    {
      ticketModel_0.Crosshair = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_3;
      account_0.AddQuery("crosshair", (object) ticketModel_0.Crosshair);
    }
    if (ticketModel_0.HandPosition != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_4)
    {
      ticketModel_0.HandPosition = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_4;
      account_0.AddQuery("hand_pos", (object) ticketModel_0.HandPosition);
    }
    if (ticketModel_0.Config != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_5)
    {
      ticketModel_0.Config = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_5;
      account_0.AddQuery("configs", (object) ticketModel_0.Config);
    }
    if (ticketModel_0.AudioEnable != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_6)
    {
      ticketModel_0.AudioEnable = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_6;
      account_0.AddQuery("audio_enable", (object) ticketModel_0.AudioEnable);
    }
    if (ticketModel_0.AudioSFX != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_7)
    {
      ticketModel_0.AudioSFX = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_7;
      account_0.AddQuery("audio_sfx", (object) ticketModel_0.AudioSFX);
    }
    if (ticketModel_0.AudioBGM != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_8)
    {
      ticketModel_0.AudioBGM = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_8;
      account_0.AddQuery("audio_bgm", (object) ticketModel_0.AudioBGM);
    }
    if (ticketModel_0.PointOfView != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_9)
    {
      ticketModel_0.PointOfView = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_9;
      account_0.AddQuery("pov_size", (object) ticketModel_0.PointOfView);
    }
    if (ticketModel_0.Sensitivity != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_10)
    {
      ticketModel_0.Sensitivity = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_10;
      account_0.AddQuery("sensitivity", (object) ticketModel_0.Sensitivity);
    }
    if (ticketModel_0.InvertMouse != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_11)
    {
      ticketModel_0.InvertMouse = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_11;
      account_0.AddQuery("invert_mouse", (object) ticketModel_0.InvertMouse);
    }
    if (ticketModel_0.EnableInviteMsg != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_12)
    {
      ticketModel_0.EnableInviteMsg = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_12;
      account_0.AddQuery("enable_invite", (object) ticketModel_0.EnableInviteMsg);
    }
    if (ticketModel_0.EnableWhisperMsg != ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_13)
    {
      ticketModel_0.EnableWhisperMsg = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_13;
      account_0.AddQuery("enable_whisper", (object) ticketModel_0.EnableWhisperMsg);
    }
    if (ticketModel_0.Macro == ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_14)
      return;
    ticketModel_0.Macro = ((PROTOCOL_BASE_OPTION_SAVE_REQ) this).int_14;
    account_0.AddQuery("macro_enable", (object) ticketModel_0.Macro);
  }
}
