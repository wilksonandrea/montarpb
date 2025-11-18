using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
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

		public PROTOCOL_BASE_OPTION_SAVE_REQ()
		{
		}

		private void method_0(DBQuery dbquery_0, PlayerConfig playerConfig_0)
		{
			if (playerConfig_0.ShowBlood != this.int_2)
			{
				playerConfig_0.ShowBlood = this.int_2;
				dbquery_0.AddQuery("show_blood", playerConfig_0.ShowBlood);
			}
			if (playerConfig_0.Crosshair != this.int_3)
			{
				playerConfig_0.Crosshair = this.int_3;
				dbquery_0.AddQuery("crosshair", playerConfig_0.Crosshair);
			}
			if (playerConfig_0.HandPosition != this.int_4)
			{
				playerConfig_0.HandPosition = this.int_4;
				dbquery_0.AddQuery("hand_pos", playerConfig_0.HandPosition);
			}
			if (playerConfig_0.Config != this.int_5)
			{
				playerConfig_0.Config = this.int_5;
				dbquery_0.AddQuery("configs", playerConfig_0.Config);
			}
			if (playerConfig_0.AudioEnable != this.int_6)
			{
				playerConfig_0.AudioEnable = this.int_6;
				dbquery_0.AddQuery("audio_enable", playerConfig_0.AudioEnable);
			}
			if (playerConfig_0.AudioSFX != this.int_7)
			{
				playerConfig_0.AudioSFX = this.int_7;
				dbquery_0.AddQuery("audio_sfx", playerConfig_0.AudioSFX);
			}
			if (playerConfig_0.AudioBGM != this.int_8)
			{
				playerConfig_0.AudioBGM = this.int_8;
				dbquery_0.AddQuery("audio_bgm", playerConfig_0.AudioBGM);
			}
			if (playerConfig_0.PointOfView != this.int_9)
			{
				playerConfig_0.PointOfView = this.int_9;
				dbquery_0.AddQuery("pov_size", playerConfig_0.PointOfView);
			}
			if (playerConfig_0.Sensitivity != this.int_10)
			{
				playerConfig_0.Sensitivity = this.int_10;
				dbquery_0.AddQuery("sensitivity", playerConfig_0.Sensitivity);
			}
			if (playerConfig_0.InvertMouse != this.int_11)
			{
				playerConfig_0.InvertMouse = this.int_11;
				dbquery_0.AddQuery("invert_mouse", playerConfig_0.InvertMouse);
			}
			if (playerConfig_0.EnableInviteMsg != this.int_12)
			{
				playerConfig_0.EnableInviteMsg = this.int_12;
				dbquery_0.AddQuery("enable_invite", playerConfig_0.EnableInviteMsg);
			}
			if (playerConfig_0.EnableWhisperMsg != this.int_13)
			{
				playerConfig_0.EnableWhisperMsg = this.int_13;
				dbquery_0.AddQuery("enable_whisper", playerConfig_0.EnableWhisperMsg);
			}
			if (playerConfig_0.Macro != this.int_14)
			{
				playerConfig_0.Macro = this.int_14;
				dbquery_0.AddQuery("macro_enable", playerConfig_0.Macro);
			}
		}

		private void method_1(DBQuery dbquery_0, PlayerConfig playerConfig_0)
		{
			if (playerConfig_0.Macro1 != this.string_0)
			{
				playerConfig_0.Macro1 = this.string_0;
				dbquery_0.AddQuery("macro1", playerConfig_0.Macro1);
			}
			if (playerConfig_0.Macro2 != this.string_1)
			{
				playerConfig_0.Macro2 = this.string_1;
				dbquery_0.AddQuery("macro2", playerConfig_0.Macro2);
			}
			if (playerConfig_0.Macro3 != this.string_2)
			{
				playerConfig_0.Macro3 = this.string_2;
				dbquery_0.AddQuery("macro3", playerConfig_0.Macro3);
			}
			if (playerConfig_0.Macro4 != this.string_3)
			{
				playerConfig_0.Macro4 = this.string_3;
				dbquery_0.AddQuery("macro4", playerConfig_0.Macro4);
			}
			if (playerConfig_0.Macro5 != this.string_4)
			{
				playerConfig_0.Macro5 = this.string_4;
				dbquery_0.AddQuery("macro5", playerConfig_0.Macro5);
			}
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			this.int_1 = base.ReadC();
			base.ReadH();
			if ((this.int_0 & 1) == 1)
			{
				this.int_2 = base.ReadH();
				this.int_3 = base.ReadC();
				this.int_4 = base.ReadC();
				this.int_5 = base.ReadD();
				this.int_6 = base.ReadC();
				base.ReadB(5);
				this.int_7 = base.ReadC();
				this.int_8 = base.ReadC();
				this.int_9 = base.ReadH();
				this.int_10 = base.ReadC();
				this.int_11 = base.ReadC();
				base.ReadC();
				base.ReadC();
				this.int_12 = base.ReadC();
				this.int_13 = base.ReadC();
				this.int_14 = base.ReadC();
				base.ReadC();
				base.ReadC();
				base.ReadC();
			}
			if ((this.int_0 & 2) == 2)
			{
				base.ReadB(5);
				this.byte_0 = base.ReadB(240);
			}
			if ((this.int_0 & 4) == 4)
			{
				this.string_0 = base.ReadU(base.ReadC() * 2);
				this.string_1 = base.ReadU(base.ReadC() * 2);
				this.string_2 = base.ReadU(base.ReadC() * 2);
				this.string_3 = base.ReadU(base.ReadC() * 2);
				this.string_4 = base.ReadU(base.ReadC() * 2);
			}
			if ((this.int_0 & 8) == 8)
			{
				this.int_15 = base.ReadH();
			}
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					DBQuery dBQuery = new DBQuery();
					PlayerConfig config = player.Config;
					if (config != null)
					{
						if ((this.int_0 & 1) == 1)
						{
							this.method_0(dBQuery, config);
						}
						if ((this.int_0 & 2) == 2 && config.KeyboardKeys != this.byte_0)
						{
							config.KeyboardKeys = this.byte_0;
							dBQuery.AddQuery("keyboard_keys", config.KeyboardKeys);
						}
						if ((this.int_0 & 4) == 4)
						{
							this.method_1(dBQuery, config);
						}
						if ((this.int_0 & 8) == 8 && config.Nations != this.int_15)
						{
							config.Nations = this.int_15;
							dBQuery.AddQuery("nations", config.Nations);
						}
						ComDiv.UpdateDB("player_configs", "owner_id", player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					}
					this.Client.SendPacket(new PROTOCOL_BASE_OPTION_SAVE_ACK());
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}