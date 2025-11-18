using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Commands
{
	public class TitleCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "";
			}
		}

		public string Command
		{
			get
			{
				return "title";
			}
		}

		public string Description
		{
			get
			{
				return "Unlock all title";
			}
		}

		public string Permission
		{
			get
			{
				return "hostcommand";
			}
		}

		public TitleCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			if (Player.Title.OwnerId == 0)
			{
				DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
				Player.Title = new PlayerTitles()
				{
					OwnerId = Player.PlayerId
				};
			}
			PlayerTitles title = Player.Title;
			int ınt32 = 0;
			for (int i = 1; i <= 44; i++)
			{
				TitleModel titleModel = TitleSystemXML.GetTitle(i, true);
				if (titleModel != null && !title.Contains(titleModel.Flag))
				{
					ınt32++;
					title.Add(titleModel.Flag);
					if (title.Slots < titleModel.Slot)
					{
						title.Slots = titleModel.Slot;
					}
				}
			}
			if (ınt32 > 0)
			{
				ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
				DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
				Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
			}
			return "All title Successfully Opened!";
		}
	}
}