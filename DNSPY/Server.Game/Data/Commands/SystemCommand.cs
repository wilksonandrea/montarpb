using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000216 RID: 534
	public class SystemCommand : ICommand
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000063AE File Offset: 0x000045AE
		public string Command
		{
			get
			{
				return "sys";
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000063B5 File Offset: 0x000045B5
		public string Description
		{
			get
			{
				return "change server settings";
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000063BC File Offset: 0x000045BC
		public string Permission
		{
			get
			{
				return "developercommand";
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00006392 File Offset: 0x00004592
		public string Args
		{
			get
			{
				return "%options% %value%";
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00038ADC File Offset: 0x00036CDC
		public string Execute(string Command, string[] Args, Account Player)
		{
			string text = Args[0].ToLower();
			string text2 = Args[1].ToLower();
			if (text.Equals("udp"))
			{
				int udpType = (int)ConfigLoader.UdpType;
				int num = int.Parse(text2);
				if (num.Equals(udpType))
				{
					return string.Format("UDP State Already Changed To: {0}", udpType);
				}
				if (num < 1 || num > 4)
				{
					return string.Format("Cannot Change UDP State To: {0}", num);
				}
				switch (num)
				{
				case 1:
					ConfigLoader.UdpType = (UdpState)num;
					return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(text), num, ConfigLoader.UdpType);
				case 2:
					ConfigLoader.UdpType = (UdpState)num;
					return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(text), num, ConfigLoader.UdpType);
				case 3:
					ConfigLoader.UdpType = (UdpState)num;
					return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(text), num, ConfigLoader.UdpType);
				case 4:
					ConfigLoader.UdpType = (UdpState)num;
					return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(text), num, ConfigLoader.UdpType);
				default:
					ConfigLoader.UdpType = UdpState.RELAY;
					return string.Format("{0} State Changed (3 - {1}). Wrong Value", ComDiv.ToTitleCase(text), ConfigLoader.UdpType);
				}
			}
			else if (text.Equals("debug"))
			{
				bool flag = int.Parse(text2).Equals(1);
				if (flag.Equals(ConfigLoader.DebugMode))
				{
					return string.Format("Debug Mode Already Changed To: {0}", flag);
				}
				if (flag)
				{
					ConfigLoader.DebugMode = flag;
					return ComDiv.ToTitleCase(text) + " Mode '" + (flag ? "Enabled" : "Disabled") + "'";
				}
				ConfigLoader.DebugMode = flag;
				return ComDiv.ToTitleCase(text) + " Mode '" + (flag ? "Enabled" : "Disabled") + "'";
			}
			else if (text.Equals("test"))
			{
				bool flag2 = int.Parse(text2).Equals(1);
				if (flag2.Equals(ConfigLoader.IsTestMode))
				{
					return string.Format("Test Mode Already Changed To: {0}", flag2);
				}
				if (flag2)
				{
					ConfigLoader.IsTestMode = flag2;
					return ComDiv.ToTitleCase(text) + " Mode '" + (flag2 ? "Enabled" : "Disabled") + "'";
				}
				ConfigLoader.IsTestMode = flag2;
				return ComDiv.ToTitleCase(text) + " Mode '" + (flag2 ? "Enabled" : "Disabled") + "'";
			}
			else if (text.Equals("ping"))
			{
				bool flag3 = int.Parse(text2).Equals(1);
				if (flag3.Equals(ConfigLoader.IsDebugPing))
				{
					return string.Format("Ping Mode Already Changed To: {0}", flag3);
				}
				if (flag3)
				{
					ConfigLoader.IsDebugPing = flag3;
					return ComDiv.ToTitleCase(text) + " Mode '" + (flag3 ? "Enabled" : "Disabled") + "'";
				}
				ConfigLoader.IsDebugPing = flag3;
				return ComDiv.ToTitleCase(text) + " Mode '" + (flag3 ? "Enabled" : "Disabled") + "'";
			}
			else
			{
				if (!text.Equals("title"))
				{
					return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
				}
				if (text2.Equals("all"))
				{
					if (Player.Title.OwnerId == 0L)
					{
						DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
						Player.Title = new PlayerTitles
						{
							OwnerId = Player.PlayerId
						};
					}
					PlayerTitles title = Player.Title;
					int num2 = 0;
					for (int i = 1; i <= 44; i++)
					{
						TitleModel title2 = TitleSystemXML.GetTitle(i, true);
						if (title2 != null && !title.Contains(title2.Flag))
						{
							num2++;
							title.Add(title2.Flag);
							if (title.Slots < title2.Slot)
							{
								title.Slots = title2.Slot;
							}
						}
					}
					if (num2 > 0)
					{
						ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
						DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
						Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
					}
					return ComDiv.ToTitleCase(text) + " Successfully Opened!";
				}
				return ComDiv.ToTitleCase(text) + " Arguments was not valid!";
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000025DF File Offset: 0x000007DF
		public SystemCommand()
		{
		}
	}
}
