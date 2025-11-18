namespace Server.Game.Data.Commands
{
    using Server.Game.Data.Models;
    using System;

    public interface ICommand
    {
        string Execute(string Command, string[] Args, Account Player);

        string Command { get; }

        string Description { get; }

        string Permission { get; }

        string Args { get; }
    }
}

