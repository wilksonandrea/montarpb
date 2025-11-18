namespace Server.Game.Data.Commands
{
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using System;

    public class HostCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            Account account;
            int length;
            RoomModel room = Player.Room;
            if (room == null)
            {
                return "A room is required to execute the command.";
            }
            if (!room.GetLeader(out account) || !ReferenceEquals(account, Player))
            {
                return "This Command Is Only Valid For Host (Room Leader).";
            }
            string text = Args[0].ToLower();
            string s = Args[1].ToLower();
            if (!text.Equals("wpn"))
            {
                if (!text.Equals("time"))
                {
                    if (!text.Equals("compe"))
                    {
                        return ("Command " + ComDiv.ToTitleCase(text) + " was not founded!");
                    }
                    string str3 = s.ToLower();
                    if (str3 != "on")
                    {
                        if (str3 != "off")
                        {
                            return ("Unable to use Competitive command! (Wrong Value: " + s + ")");
                        }
                        room.Name = room.RandomName(new Random().Next(1, 4));
                        room.Competitive = false;
                        return (ComDiv.ToTitleCase(text) + "titive Disabled!");
                    }
                    if ((room.GetSlotCount() != 6) && ((room.GetSlotCount() != 8) && (room.GetSlotCount() != 10)))
                    {
                        return "Please change the slots to (3v3) or (4v4) or (5v5)";
                    }
                    room.Name = "Competitive!!!";
                    room.Competitive = true;
                    AllUtils.SendCompetitiveInfo(Player);
                    return (ComDiv.ToTitleCase(text) + "titive Enabled!");
                }
                CommandHelper helper2 = CommandHelperJSON.GetTag("PlayTime");
                length = int.Parse(s);
                if (length <= 15)
                {
                    if (length == 5)
                    {
                        room.KillTime = helper2.Minutes05;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 5 Minutes");
                    }
                    if (length == 10)
                    {
                        room.KillTime = helper2.Minutes10;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 10 Minutes");
                    }
                    if (length == 15)
                    {
                        room.KillTime = helper2.Minutes15;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 15 Minutes");
                    }
                }
                else
                {
                    if (length == 20)
                    {
                        room.KillTime = helper2.Minutes20;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 20 Minutes");
                    }
                    if (length == 0x19)
                    {
                        room.KillTime = helper2.Minutes25;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 25 Minutes");
                    }
                    if (length == 30)
                    {
                        room.KillTime = helper2.Minutes30;
                        room.UpdateRoomInfo();
                        return (ComDiv.ToTitleCase(text) + " 30 Minutes");
                    }
                }
                return (ComDiv.ToTitleCase(text) + " None! (Wrong Value)");
            }
            CommandHelper tag = CommandHelperJSON.GetTag("WeaponsFlag");
            if (s != null)
            {
                char ch;
                length = s.Length;
                if (length != 2)
                {
                    if (length == 3)
                    {
                        ch = s[0];
                        if (ch == 'a')
                        {
                            if (s == "all")
                            {
                                room.WeaponsFlag = (RoomWeaponsFlag) tag.AllWeapons;
                                room.UpdateRoomInfo();
                                return "All Weapons (AR, SMG, SR, SG, MG, SHD)";
                            }
                        }
                        else if (ch != 'r')
                        {
                            if ((ch == 's') && (s == "smg"))
                            {
                                room.WeaponsFlag = (RoomWeaponsFlag) tag.SubMachineGun;
                                room.UpdateRoomInfo();
                                return "Weapon Sub Machine Gun (Only)";
                            }
                        }
                        else if (s == "rpg")
                        {
                            room.WeaponsFlag = (RoomWeaponsFlag) tag.RPG7;
                            room.UpdateRoomInfo();
                            return "Weapon RPG-7 (Only) - Hot Glitch";
                        }
                    }
                }
                else
                {
                    ch = s[0];
                    if (ch == 'a')
                    {
                        if (s == "ar")
                        {
                            room.WeaponsFlag = (RoomWeaponsFlag) tag.AssaultRifle;
                            room.UpdateRoomInfo();
                            return "Weapon Assault Rifle (Only)";
                        }
                    }
                    else if (ch == 'm')
                    {
                        if (s == "mg")
                        {
                            room.WeaponsFlag = (RoomWeaponsFlag) tag.MachineGun;
                            room.UpdateRoomInfo();
                            return "Weapon Machine Gun (Only)";
                        }
                    }
                    else if (ch == 's')
                    {
                        if (s == "sr")
                        {
                            room.WeaponsFlag = (RoomWeaponsFlag) tag.SniperRifle;
                            room.UpdateRoomInfo();
                            return "Weapon Sniper Rifle (Only)";
                        }
                        if (s == "sg")
                        {
                            room.WeaponsFlag = (RoomWeaponsFlag) tag.ShotGun;
                            room.UpdateRoomInfo();
                            return "Weapon Shot Gun (Only)";
                        }
                    }
                }
            }
            room.WeaponsFlag = (RoomWeaponsFlag) tag.AllWeapons;
            room.UpdateRoomInfo();
            return "Weapon Default (Value Not Founded)";
        }

        public string Command =>
            "host";

        public string Description =>
            "Change room options (AI Only)";

        public string Permission =>
            "hostcommand";

        public string Args =>
            "%options% %value%";
    }
}

