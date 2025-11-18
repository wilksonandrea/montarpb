using System.Collections.Generic;
using System.Linq;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands;

public class HelpCommand : ICommand
{
	private readonly int int_0 = 5;

	public string Command => "help";

	public string Description => "Show available commands";

	public string Permission => "helpcommand";

	public string Args => "%page% (optional)";

	public string Execute(string Command, string[] Args, Account Player)
	{
		int num = 1;
		if (Args.Length != 0)
		{
			if (int.TryParse(Args[0], out var result))
			{
				if (result == 0)
				{
					result = 1;
				}
				num = result;
			}
			else
			{
				num = 1;
			}
		}
		IEnumerable<ICommand> commandsForPlayer = CommandManager.GetCommandsForPlayer(Player);
		int num2 = (commandsForPlayer.Count() + int_0 - 1) / int_0;
		if (num > num2)
		{
			return $"Please insert a valid page. Pages: {num2}";
		}
		IEnumerable<ICommand> obj = commandsForPlayer.Split(int_0).ToArray()[num - 1];
		string text = $"Commands ({num}/{num2}):\n";
		foreach (ICommand item in obj)
		{
			text = text + ":" + item.Command + " " + item.Args + " -> " + item.Description + "\n";
		}
		Player.Connection.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text));
		return $"Displayed commands page '{num}' of '{num2}'";
	}
}
