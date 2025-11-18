using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_OPTION_SAVE_REQ : GameClientPacket
{
	private byte[] byte_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private string string_3;

	private string string_4;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	private int int_4;

	private int int_5;

	private int int_6;

	private int int_7;

	private int int_8;

	private int int_9;

	private int int_10;

	private int int_11;

	private int int_12;

	private int int_13;

	private int int_14;

	private int int_15;

	public override void Read()
	{
		int_0 = ReadC();
		int_1 = ReadC();
		ReadH();
		if ((int_0 & 1) == 1)
		{
			int_2 = ReadH();
			int_3 = ReadC();
			int_4 = ReadC();
			int_5 = ReadD();
			int_6 = ReadC();
			ReadB(5);
			int_7 = ReadC();
			int_8 = ReadC();
			int_9 = ReadH();
			int_10 = ReadC();
			int_11 = ReadC();
			ReadC();
			ReadC();
			int_12 = ReadC();
			int_13 = ReadC();
			int_14 = ReadC();
			ReadC();
			ReadC();
			ReadC();
		}
		if ((int_0 & 2) == 2)
		{
			ReadB(5);
			byte_0 = ReadB(240);
		}
		if ((int_0 & 4) == 4)
		{
			string_0 = ReadU(ReadC() * 2);
			string_1 = ReadU(ReadC() * 2);
			string_2 = ReadU(ReadC() * 2);
			string_3 = ReadU(ReadC() * 2);
			string_4 = ReadU(ReadC() * 2);
		}
		if ((int_0 & 8) == 8)
		{
			int_15 = ReadH();
		}
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			DBQuery dBQuery = new DBQuery();
			PlayerConfig config = player.Config;
			if (config != null)
			{
				if ((int_0 & 1) == 1)
				{
					method_0(dBQuery, config);
				}
				if ((int_0 & 2) == 2 && config.KeyboardKeys != byte_0)
				{
					config.KeyboardKeys = byte_0;
					dBQuery.AddQuery("keyboard_keys", config.KeyboardKeys);
				}
				if ((int_0 & 4) == 4)
				{
					method_1(dBQuery, config);
				}
				if ((int_0 & 8) == 8 && config.Nations != int_15)
				{
					config.Nations = int_15;
					dBQuery.AddQuery("nations", config.Nations);
				}
				ComDiv.UpdateDB("player_configs", "owner_id", player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
			}
			Client.SendPacket(new PROTOCOL_BASE_OPTION_SAVE_ACK());
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(DBQuery dbquery_0, PlayerConfig playerConfig_0)
	{
		if (playerConfig_0.ShowBlood != int_2)
		{
			playerConfig_0.ShowBlood = int_2;
			dbquery_0.AddQuery("show_blood", playerConfig_0.ShowBlood);
		}
		if (playerConfig_0.Crosshair != int_3)
		{
			playerConfig_0.Crosshair = int_3;
			dbquery_0.AddQuery("crosshair", playerConfig_0.Crosshair);
		}
		if (playerConfig_0.HandPosition != int_4)
		{
			playerConfig_0.HandPosition = int_4;
			dbquery_0.AddQuery("hand_pos", playerConfig_0.HandPosition);
		}
		if (playerConfig_0.Config != int_5)
		{
			playerConfig_0.Config = int_5;
			dbquery_0.AddQuery("configs", playerConfig_0.Config);
		}
		if (playerConfig_0.AudioEnable != int_6)
		{
			playerConfig_0.AudioEnable = int_6;
			dbquery_0.AddQuery("audio_enable", playerConfig_0.AudioEnable);
		}
		if (playerConfig_0.AudioSFX != int_7)
		{
			playerConfig_0.AudioSFX = int_7;
			dbquery_0.AddQuery("audio_sfx", playerConfig_0.AudioSFX);
		}
		if (playerConfig_0.AudioBGM != int_8)
		{
			playerConfig_0.AudioBGM = int_8;
			dbquery_0.AddQuery("audio_bgm", playerConfig_0.AudioBGM);
		}
		if (playerConfig_0.PointOfView != int_9)
		{
			playerConfig_0.PointOfView = int_9;
			dbquery_0.AddQuery("pov_size", playerConfig_0.PointOfView);
		}
		if (playerConfig_0.Sensitivity != int_10)
		{
			playerConfig_0.Sensitivity = int_10;
			dbquery_0.AddQuery("sensitivity", playerConfig_0.Sensitivity);
		}
		if (playerConfig_0.InvertMouse != int_11)
		{
			playerConfig_0.InvertMouse = int_11;
			dbquery_0.AddQuery("invert_mouse", playerConfig_0.InvertMouse);
		}
		if (playerConfig_0.EnableInviteMsg != int_12)
		{
			playerConfig_0.EnableInviteMsg = int_12;
			dbquery_0.AddQuery("enable_invite", playerConfig_0.EnableInviteMsg);
		}
		if (playerConfig_0.EnableWhisperMsg != int_13)
		{
			playerConfig_0.EnableWhisperMsg = int_13;
			dbquery_0.AddQuery("enable_whisper", playerConfig_0.EnableWhisperMsg);
		}
		if (playerConfig_0.Macro != int_14)
		{
			playerConfig_0.Macro = int_14;
			dbquery_0.AddQuery("macro_enable", playerConfig_0.Macro);
		}
	}

	private void method_1(DBQuery dbquery_0, PlayerConfig playerConfig_0)
	{
		if (playerConfig_0.Macro1 != string_0)
		{
			playerConfig_0.Macro1 = string_0;
			dbquery_0.AddQuery("macro1", playerConfig_0.Macro1);
		}
		if (playerConfig_0.Macro2 != string_1)
		{
			playerConfig_0.Macro2 = string_1;
			dbquery_0.AddQuery("macro2", playerConfig_0.Macro2);
		}
		if (playerConfig_0.Macro3 != string_2)
		{
			playerConfig_0.Macro3 = string_2;
			dbquery_0.AddQuery("macro3", playerConfig_0.Macro3);
		}
		if (playerConfig_0.Macro4 != string_3)
		{
			playerConfig_0.Macro4 = string_3;
			dbquery_0.AddQuery("macro4", playerConfig_0.Macro4);
		}
		if (playerConfig_0.Macro5 != string_4)
		{
			playerConfig_0.Macro5 = string_4;
			dbquery_0.AddQuery("macro5", playerConfig_0.Macro5);
		}
	}
}
