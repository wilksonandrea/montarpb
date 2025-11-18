namespace Server.Game.Data.Commands
{
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using System;

    public class RoomInfoCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            RoomModel room = Player.Room;
            if (room == null)
            {
                return "A room is required to execute the command.";
            }
            string text = Args[0].ToLower();
            string s = Args[1].ToLower();
            if (text.Equals("time"))
            {
                int num = int.Parse(s) * 6;
                if (num < 0)
                {
                    return ("Oops! Map 'index' Isn't Supposed To Be: " + s + ". Try an Higher Value.");
                }
                if (room.IsPreparing() || AllUtils.PlayerIsBattle(Player))
                {
                    return "Oops! You Can't Change The 'time' While The Room Has Started Game Match.";
                }
                room.KillTime = num;
                room.UpdateRoomInfo();
                return $"{ComDiv.ToTitleCase(text)} Changed To {(room.GetTimeByMask() % 60)} Minutes";
            }
            if (text.Equals("flags"))
            {
                RoomWeaponsFlag flag = (RoomWeaponsFlag) int.Parse(s);
                room.WeaponsFlag = flag;
                room.UpdateRoomInfo();
                return $"{ComDiv.ToTitleCase(text)} Changed To {flag}";
            }
            if (!text.Equals("killcam"))
            {
                return ("Command " + ComDiv.ToTitleCase(text) + " was not founded!");
            }
            byte num2 = byte.Parse(s);
            room.KillCam = num2;
            room.UpdateRoomInfo();
            return $"{ComDiv.ToTitleCase(text)} Changed To {num2}";
        }

        public string Command =>
            "roominfo";

        public string Description =>
            "Change room options";

        public string Permission =>
            "moderatorcommand";

        public string Args =>
            "%options% %value%";
    }
}

