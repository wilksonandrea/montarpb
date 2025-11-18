namespace Server.Game.Data.Commands
{
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using System;

    public class BattleEndCommand : ICommand
    {
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

        public string Command =>
            "endbattle";

        public string Description =>
            "End work in progress battle";

        public string Permission =>
            "moderatorcommand";

        public string Args =>
            "";
    }
}

