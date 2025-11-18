namespace Server.Game.Data.Commands
{
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HelpCommand : ICommand
    {
        private readonly int int_0 = 5;

        public string Execute(string Command, string[] Args, Account Player)
        {
            int num = 1;
            if (Args.Length != 0)
            {
                int num3;
                if (!int.TryParse(Args[0], out num3))
                {
                    num = 1;
                }
                else
                {
                    num3 ??= 1;
                    num = num3;
                }
            }
            IEnumerable<ICommand> commandsForPlayer = CommandManager.GetCommandsForPlayer(Player);
            int num2 = ((commandsForPlayer.Count<ICommand>() + this.int_0) - 1) / this.int_0;
            if (num > num2)
            {
                return $"Please insert a valid page. Pages: {num2}";
            }
            string str = $"Commands ({num}/{num2}):
";
            foreach (ICommand command in commandsForPlayer.Split<ICommand>(this.int_0).ToArray<IEnumerable<ICommand>>()[num - 1])
            {
                string[] textArray1 = new string[] { str, ":", command.Command, " ", command.Args, " -> ", command.Description, "\n" };
                str = string.Concat(textArray1);
            }
            Player.Connection.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str));
            return $"Displayed commands page '{num}' of '{num2}'";
        }

        public string Command =>
            "help";

        public string Description =>
            "Show available commands";

        public string Permission =>
            "helpcommand";

        public string Args =>
            "%page% (optional)";
    }
}

