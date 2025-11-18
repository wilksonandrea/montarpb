using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Game.Data.Commands
{
	public class HelpCommand : ICommand
	{
		private readonly int int_0 = 5;

		public string Args
		{
			get
			{
				return "%page% (optional)";
			}
		}

		public string Command
		{
			get
			{
				return "help";
			}
		}

		public string Description
		{
			get
			{
				return "Show available commands";
			}
		}

		public string Permission
		{
			get
			{
				return "helpcommand";
			}
		}

		public HelpCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			int ınt32;
			int ınt321 = 1;
			if (Args.Length != 0)
			{
				if (!int.TryParse(Args[0], out ınt32))
				{
					ınt321 = 1;
				}
				else
				{
					if (ınt32 == 0)
					{
						ınt32 = 1;
					}
					ınt321 = ınt32;
				}
			}
			IEnumerable<ICommand> commandsForPlayer = CommandManager.GetCommandsForPlayer(Player);
			int ınt322 = (commandsForPlayer.Count<ICommand>() + this.int_0 - 1) / this.int_0;
			if (ınt321 > ınt322)
			{
				return string.Format("Please insert a valid page. Pages: {0}", ınt322);
			}
			IEnumerable<ICommand> array = commandsForPlayer.Split<ICommand>(this.int_0).ToArray<IEnumerable<ICommand>>()[ınt321 - 1];
			string str = string.Format("Commands ({0}/{1}):\n", ınt321, ınt322);
			foreach (ICommand command in array)
			{
				str = string.Concat(new string[] { str, ":", command.Command, " ", command.Args, " -> ", command.Description, "\n" });
			}
			Player.Connection.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str));
			return string.Format("Displayed commands page '{0}' of '{1}'", ınt321, ınt322);
		}
	}
}