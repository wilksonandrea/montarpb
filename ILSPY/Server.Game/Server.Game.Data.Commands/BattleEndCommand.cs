using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands;

public class BattleEndCommand : ICommand
{
	public string Command => "endbattle";

	public string Description => "End work in progress battle";

	public string Permission => "moderatorcommand";

	public string Args => "";

	public string Execute(string Command, string[] Args, Account Player)
	{
		RoomModel room = Player.Room;
		if (room == null)
		{
			return "A room is required to execute the command.";
		}
		if (room.IsPreparing() && AllUtils.PlayerIsBattle(Player))
		{
			AllUtils.EndBattle(room);
			return "Battle ended.";
		}
		return "Oops! the battle hasn't started.";
	}
}
