namespace Server.Game.Data.Commands
{
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;

    public class TitleCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            if (Player.Title.OwnerId == 0)
            {
                DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
                PlayerTitles titles1 = new PlayerTitles();
                titles1.OwnerId = Player.PlayerId;
                Player.Title = titles1;
            }
            PlayerTitles title = Player.Title;
            int num = 0;
            for (int i = 1; i <= 0x2c; i++)
            {
                TitleModel model = TitleSystemXML.GetTitle(i, true);
                if ((model != null) && !title.Contains(model.Flag))
                {
                    num++;
                    title.Add(model.Flag);
                    if (title.Slots < model.Slot)
                    {
                        title.Slots = model.Slot;
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

        public string Command =>
            "title";

        public string Description =>
            "Unlock all title";

        public string Permission =>
            "hostcommand";

        public string Args =>
            "";
    }
}

