namespace Server.Game.Data.Commands
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;

    public class SystemCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            string text = Args[0].ToLower();
            string s = Args[1].ToLower();
            if (text.Equals("udp"))
            {
                int udpType = (int) ConfigLoader.UdpType;
                int num2 = int.Parse(s);
                if (num2.Equals(udpType))
                {
                    return $"UDP State Already Changed To: {udpType}";
                }
                if ((num2 < 1) || (num2 > 4))
                {
                    return $"Cannot Change UDP State To: {num2}";
                }
                switch (num2)
                {
                    case 1:
                        ConfigLoader.UdpType = (UdpState) num2;
                        return $"{ComDiv.ToTitleCase(text)} State Changed ({num2} - {ConfigLoader.UdpType})";

                    case 2:
                        ConfigLoader.UdpType = (UdpState) num2;
                        return $"{ComDiv.ToTitleCase(text)} State Changed ({num2} - {ConfigLoader.UdpType})";

                    case 3:
                        ConfigLoader.UdpType = (UdpState) num2;
                        return $"{ComDiv.ToTitleCase(text)} State Changed ({num2} - {ConfigLoader.UdpType})";

                    case 4:
                        ConfigLoader.UdpType = (UdpState) num2;
                        return $"{ComDiv.ToTitleCase(text)} State Changed ({num2} - {ConfigLoader.UdpType})";
                }
                ConfigLoader.UdpType = UdpState.RELAY;
                return $"{ComDiv.ToTitleCase(text)} State Changed (3 - {ConfigLoader.UdpType}). Wrong Value";
            }
            if (text.Equals("debug"))
            {
                bool flag = int.Parse(s).Equals(1);
                if (flag.Equals(ConfigLoader.DebugMode))
                {
                    return $"Debug Mode Already Changed To: {flag}";
                }
                if (flag)
                {
                    ConfigLoader.DebugMode = flag;
                    return (ComDiv.ToTitleCase(text) + " Mode '" + (flag ? "Enabled" : "Disabled") + "'");
                }
                ConfigLoader.DebugMode = flag;
                return (ComDiv.ToTitleCase(text) + " Mode '" + (flag ? "Enabled" : "Disabled") + "'");
            }
            if (text.Equals("test"))
            {
                bool flag2 = int.Parse(s).Equals(1);
                if (flag2.Equals(ConfigLoader.IsTestMode))
                {
                    return $"Test Mode Already Changed To: {flag2}";
                }
                if (flag2)
                {
                    ConfigLoader.IsTestMode = flag2;
                    return (ComDiv.ToTitleCase(text) + " Mode '" + (flag2 ? "Enabled" : "Disabled") + "'");
                }
                ConfigLoader.IsTestMode = flag2;
                return (ComDiv.ToTitleCase(text) + " Mode '" + (flag2 ? "Enabled" : "Disabled") + "'");
            }
            if (text.Equals("ping"))
            {
                bool flag3 = int.Parse(s).Equals(1);
                if (flag3.Equals(ConfigLoader.IsDebugPing))
                {
                    return $"Ping Mode Already Changed To: {flag3}";
                }
                if (flag3)
                {
                    ConfigLoader.IsDebugPing = flag3;
                    return (ComDiv.ToTitleCase(text) + " Mode '" + (flag3 ? "Enabled" : "Disabled") + "'");
                }
                ConfigLoader.IsDebugPing = flag3;
                return (ComDiv.ToTitleCase(text) + " Mode '" + (flag3 ? "Enabled" : "Disabled") + "'");
            }
            if (!text.Equals("title"))
            {
                return ("Command " + ComDiv.ToTitleCase(text) + " was not founded!");
            }
            if (!s.Equals("all"))
            {
                return (ComDiv.ToTitleCase(text) + " Arguments was not valid!");
            }
            if (Player.Title.OwnerId == 0)
            {
                DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
                PlayerTitles titles1 = new PlayerTitles();
                titles1.OwnerId = Player.PlayerId;
                Player.Title = titles1;
            }
            PlayerTitles title = Player.Title;
            int num4 = 0;
            for (int i = 1; i <= 0x2c; i++)
            {
                TitleModel model = TitleSystemXML.GetTitle(i, true);
                if ((model != null) && !title.Contains(model.Flag))
                {
                    num4++;
                    title.Add(model.Flag);
                    if (title.Slots < model.Slot)
                    {
                        title.Slots = model.Slot;
                    }
                }
            }
            if (num4 > 0)
            {
                ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
                DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
                Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
            }
            return (ComDiv.ToTitleCase(text) + " Successfully Opened!");
        }

        public string Command =>
            "sys";

        public string Description =>
            "change server settings";

        public string Permission =>
            "developercommand";

        public string Args =>
            "%options% %value%";
    }
}

