using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000150 RID: 336
	public class PROTOCOL_BASE_OPTION_SAVE_REQ : GameClientPacket
	{
		// Token: 0x0600034E RID: 846 RVA: 0x000196B4 File Offset: 0x000178B4
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
			base.ReadH();
			if ((this.int_0 & 1) == 1)
			{
				this.int_2 = (int)base.ReadH();
				this.int_3 = (int)base.ReadC();
				this.int_4 = (int)base.ReadC();
				this.int_5 = base.ReadD();
				this.int_6 = (int)base.ReadC();
				base.ReadB(5);
				this.int_7 = (int)base.ReadC();
				this.int_8 = (int)base.ReadC();
				this.int_9 = (int)base.ReadH();
				this.int_10 = (int)base.ReadC();
				this.int_11 = (int)base.ReadC();
				base.ReadC();
				base.ReadC();
				this.int_12 = (int)base.ReadC();
				this.int_13 = (int)base.ReadC();
				this.int_14 = (int)base.ReadC();
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
				this.string_0 = base.ReadU((int)(base.ReadC() * 2));
				this.string_1 = base.ReadU((int)(base.ReadC() * 2));
				this.string_2 = base.ReadU((int)(base.ReadC() * 2));
				this.string_3 = base.ReadU((int)(base.ReadC() * 2));
				this.string_4 = base.ReadU((int)(base.ReadC() * 2));
			}
			if ((this.int_0 & 8) == 8)
			{
				this.int_15 = (int)base.ReadH();
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00019860 File Offset: 0x00017A60
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					DBQuery dbquery = new DBQuery();
					PlayerConfig config = player.Config;
					if (config != null)
					{
						if ((this.int_0 & 1) == 1)
						{
							this.method_0(dbquery, config);
						}
						if ((this.int_0 & 2) == 2 && config.KeyboardKeys != this.byte_0)
						{
							config.KeyboardKeys = this.byte_0;
							dbquery.AddQuery("keyboard_keys", config.KeyboardKeys);
						}
						if ((this.int_0 & 4) == 4)
						{
							this.method_1(dbquery, config);
						}
						if ((this.int_0 & 8) == 8 && config.Nations != this.int_15)
						{
							config.Nations = this.int_15;
							dbquery.AddQuery("nations", config.Nations);
						}
						ComDiv.UpdateDB("player_configs", "owner_id", player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
					}
					this.Client.SendPacket(new PROTOCOL_BASE_OPTION_SAVE_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00019984 File Offset: 0x00017B84
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

		// Token: 0x06000351 RID: 849 RVA: 0x00019C04 File Offset: 0x00017E04
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

		// Token: 0x06000352 RID: 850 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_OPTION_SAVE_REQ()
		{
		}

		// Token: 0x04000257 RID: 599
		private byte[] byte_0;

		// Token: 0x04000258 RID: 600
		private string string_0;

		// Token: 0x04000259 RID: 601
		private string string_1;

		// Token: 0x0400025A RID: 602
		private string string_2;

		// Token: 0x0400025B RID: 603
		private string string_3;

		// Token: 0x0400025C RID: 604
		private string string_4;

		// Token: 0x0400025D RID: 605
		private int int_0;

		// Token: 0x0400025E RID: 606
		private int int_1;

		// Token: 0x0400025F RID: 607
		private int int_2;

		// Token: 0x04000260 RID: 608
		private int int_3;

		// Token: 0x04000261 RID: 609
		private int int_4;

		// Token: 0x04000262 RID: 610
		private int int_5;

		// Token: 0x04000263 RID: 611
		private int int_6;

		// Token: 0x04000264 RID: 612
		private int int_7;

		// Token: 0x04000265 RID: 613
		private int int_8;

		// Token: 0x04000266 RID: 614
		private int int_9;

		// Token: 0x04000267 RID: 615
		private int int_10;

		// Token: 0x04000268 RID: 616
		private int int_11;

		// Token: 0x04000269 RID: 617
		private int int_12;

		// Token: 0x0400026A RID: 618
		private int int_13;

		// Token: 0x0400026B RID: 619
		private int int_14;

		// Token: 0x0400026C RID: 620
		private int int_15;
	}
}
