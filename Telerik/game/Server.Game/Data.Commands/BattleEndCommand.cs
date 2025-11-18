using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System;

namespace Server.Game.Data.Commands
{
	public class BattleEndCommand : ICommand
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
				return "endbattle";
			}
		}

		public string Description
		{
			get
			{
				return "End work in progress battle";
			}
		}

		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		public BattleEndCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			if (!room.IsPreparing() || !AllUtils.PlayerIsBattle(Player))
			{
				return "Oops! the battle hasn't started.";
			}
			AllUtils.EndBattle(room);
			return "Battle ended.";
		}
	}
}