using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands;

public class TitleCommand : ICommand
{
	public string Command => "title";

	public string Description => "Unlock all title";

	public string Permission => "hostcommand";

	public string Args => "";

	public string Execute(string Command, string[] Args, Account Player)
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
		int num = 0;
		for (int i = 1; i <= 44; i++)
		{
			TitleModel title2 = TitleSystemXML.GetTitle(i);
			if (title2 != null && !title.Contains(title2.Flag))
			{
				num++;
				title.Add(title2.Flag);
				if (title.Slots < title2.Slot)
				{
					title.Slots = title2.Slot;
				}
			}
		}
		if (num > 0)
		{
			ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
			DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
			Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
		}
		return "All title Successfully Opened!";
	}
}
