using Server.Game.Data.Models;
using System;

namespace Server.Game.Data.Commands
{
	public interface ICommand
	{
		string Args
		{
			get;
		}

		string Command
		{
			get;
		}

		string Description
		{
			get;
		}

		string Permission
		{
			get;
		}

		string Execute(string Command, string[] Args, Account Player);
	}
}