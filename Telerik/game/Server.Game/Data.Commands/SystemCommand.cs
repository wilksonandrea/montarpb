using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Commands
{
	public class SystemCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "%options% %value%";
			}
		}

		public string Command
		{
			get
			{
				return "sys";
			}
		}

		public string Description
		{
			get
			{
				return "change server settings";
			}
		}

		public string Permission
		{
			get
			{
				return "developercommand";
			}
		}

		public SystemCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			int ınt32;
			string lower = Args[0].ToLower();
			string str = Args[1].ToLower();
			if (lower.Equals("udp"))
			{
				int udpType = (int)ConfigLoader.UdpType;
				int ınt321 = int.Parse(str);
				if (ınt321.Equals(udpType))
				{
					return string.Format("UDP State Already Changed To: {0}", udpType);
				}
				if (ınt321 < 1 || ınt321 > 4)
				{
					return string.Format("Cannot Change UDP State To: {0}", ınt321);
				}
				switch (ınt321)
				{
					case 1:
					{
						ConfigLoader.UdpType = (UdpState)ınt321;
						return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(lower), ınt321, ConfigLoader.UdpType);
					}
					case 2:
					{
						ConfigLoader.UdpType = (UdpState)ınt321;
						return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(lower), ınt321, ConfigLoader.UdpType);
					}
					case 3:
					{
						ConfigLoader.UdpType = (UdpState)ınt321;
						return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(lower), ınt321, ConfigLoader.UdpType);
					}
					case 4:
					{
						ConfigLoader.UdpType = (UdpState)ınt321;
						return string.Format("{0} State Changed ({1} - {2})", ComDiv.ToTitleCase(lower), ınt321, ConfigLoader.UdpType);
					}
					default:
					{
						ConfigLoader.UdpType = UdpState.RELAY;
						return string.Format("{0} State Changed (3 - {1}). Wrong Value", ComDiv.ToTitleCase(lower), ConfigLoader.UdpType);
					}
				}
			}
			if (lower.Equals("debug"))
			{
				ınt32 = int.Parse(str);
				bool flag = ınt32.Equals(1);
				if (flag.Equals(ConfigLoader.DebugMode))
				{
					return string.Format("Debug Mode Already Changed To: {0}", flag);
				}
				if (flag)
				{
					ConfigLoader.DebugMode = flag;
					return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag ? "Enabled" : "Disabled"), "'");
				}
				ConfigLoader.DebugMode = flag;
				return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag ? "Enabled" : "Disabled"), "'");
			}
			if (lower.Equals("test"))
			{
				ınt32 = int.Parse(str);
				bool flag1 = ınt32.Equals(1);
				if (flag1.Equals(ConfigLoader.IsTestMode))
				{
					return string.Format("Test Mode Already Changed To: {0}", flag1);
				}
				if (flag1)
				{
					ConfigLoader.IsTestMode = flag1;
					return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag1 ? "Enabled" : "Disabled"), "'");
				}
				ConfigLoader.IsTestMode = flag1;
				return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag1 ? "Enabled" : "Disabled"), "'");
			}
			if (lower.Equals("ping"))
			{
				ınt32 = int.Parse(str);
				bool flag2 = ınt32.Equals(1);
				if (flag2.Equals(ConfigLoader.IsDebugPing))
				{
					return string.Format("Ping Mode Already Changed To: {0}", flag2);
				}
				if (flag2)
				{
					ConfigLoader.IsDebugPing = flag2;
					return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag2 ? "Enabled" : "Disabled"), "'");
				}
				ConfigLoader.IsDebugPing = flag2;
				return string.Concat(ComDiv.ToTitleCase(lower), " Mode '", (flag2 ? "Enabled" : "Disabled"), "'");
			}
			if (!lower.Equals("title"))
			{
				return string.Concat("Command ", ComDiv.ToTitleCase(lower), " was not founded!");
			}
			if (!str.Equals("all"))
			{
				return string.Concat(ComDiv.ToTitleCase(lower), " Arguments was not valid!");
			}
			if (Player.Title.OwnerId == 0)
			{
				DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
				Player.Title = new PlayerTitles()
				{
					OwnerId = Player.PlayerId
				};
			}
			PlayerTitles title = Player.Title;
			int ınt322 = 0;
			for (int i = 1; i <= 44; i++)
			{
				TitleModel titleModel = TitleSystemXML.GetTitle(i, true);
				if (titleModel != null && !title.Contains(titleModel.Flag))
				{
					ınt322++;
					title.Add(titleModel.Flag);
					if (title.Slots < titleModel.Slot)
					{
						title.Slots = titleModel.Slot;
					}
				}
			}
			if (ınt322 > 0)
			{
				ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
				DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
				Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
			}
			return string.Concat(ComDiv.ToTitleCase(lower), " Successfully Opened!");
		}
	}
}