using System;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands;

public class HostCommand : ICommand
{
	public string Command => "host";

	public string Description => "Change room options (AI Only)";

	public string Permission => "hostcommand";

	public string Args => "%options% %value%";

	public string Execute(string Command, string[] Args, Account Player)
	{
		RoomModel room = Player.Room;
		if (room == null)
		{
			return "A room is required to execute the command.";
		}
		if (room.GetLeader(out var Player2) && Player2 == Player)
		{
			string text = Args[0].ToLower();
			string text2 = Args[1].ToLower();
			if (text.Equals("wpn"))
			{
				CommandHelper tag = CommandHelperJSON.GetTag("WeaponsFlag");
				if (text2 != null)
				{
					switch (text2.Length)
					{
					case 3:
						switch (text2[0])
						{
						case 's':
							if (text2 == "smg")
							{
								room.WeaponsFlag = (RoomWeaponsFlag)tag.SubMachineGun;
								room.UpdateRoomInfo();
								return "Weapon Sub Machine Gun (Only)";
							}
							break;
						case 'r':
							if (text2 == "rpg")
							{
								room.WeaponsFlag = (RoomWeaponsFlag)tag.RPG7;
								room.UpdateRoomInfo();
								return "Weapon RPG-7 (Only) - Hot Glitch";
							}
							break;
						case 'a':
							if (text2 == "all")
							{
								room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
								room.UpdateRoomInfo();
								return "All Weapons (AR, SMG, SR, SG, MG, SHD)";
							}
							break;
						}
						break;
					case 2:
						switch (text2[0])
						{
						case 's':
							if (!(text2 == "sr"))
							{
								if (text2 == "sg")
								{
									room.WeaponsFlag = (RoomWeaponsFlag)tag.ShotGun;
									room.UpdateRoomInfo();
									return "Weapon Shot Gun (Only)";
								}
								break;
							}
							room.WeaponsFlag = (RoomWeaponsFlag)tag.SniperRifle;
							room.UpdateRoomInfo();
							return "Weapon Sniper Rifle (Only)";
						case 'm':
							if (text2 == "mg")
							{
								room.WeaponsFlag = (RoomWeaponsFlag)tag.MachineGun;
								room.UpdateRoomInfo();
								return "Weapon Machine Gun (Only)";
							}
							break;
						case 'a':
							if (text2 == "ar")
							{
								room.WeaponsFlag = (RoomWeaponsFlag)tag.AssaultRifle;
								room.UpdateRoomInfo();
								return "Weapon Assault Rifle (Only)";
							}
							break;
						}
						break;
					}
				}
				room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
				room.UpdateRoomInfo();
				return "Weapon Default (Value Not Founded)";
			}
			if (text.Equals("time"))
			{
				CommandHelper tag2 = CommandHelperJSON.GetTag("PlayTime");
				switch (int.Parse(text2))
				{
				case 15:
					room.KillTime = tag2.Minutes15;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 15 Minutes";
				case 10:
					room.KillTime = tag2.Minutes10;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 10 Minutes";
				case 5:
					room.KillTime = tag2.Minutes05;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 5 Minutes";
				default:
					return ComDiv.ToTitleCase(text) + " None! (Wrong Value)";
				case 30:
					room.KillTime = tag2.Minutes30;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 30 Minutes";
				case 25:
					room.KillTime = tag2.Minutes25;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 25 Minutes";
				case 20:
					room.KillTime = tag2.Minutes20;
					room.UpdateRoomInfo();
					return ComDiv.ToTitleCase(text) + " 20 Minutes";
				}
			}
			if (text.Equals("compe"))
			{
				string text3 = text2.ToLower();
				if (!(text3 == "on"))
				{
					if (!(text3 == "off"))
					{
						return "Unable to use Competitive command! (Wrong Value: " + text2 + ")";
					}
					room.Name = room.RandomName(new Random().Next(1, 4));
					room.Competitive = false;
					return ComDiv.ToTitleCase(text) + "titive Disabled!";
				}
				if (room.GetSlotCount() != 6 && room.GetSlotCount() != 8 && room.GetSlotCount() != 10)
				{
					return "Please change the slots to (3v3) or (4v4) or (5v5)";
				}
				room.Name = "Competitive!!!";
				room.Competitive = true;
				AllUtils.SendCompetitiveInfo(Player);
				return ComDiv.ToTitleCase(text) + "titive Enabled!";
			}
			return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
		}
		return "This Command Is Only Valid For Host (Room Leader).";
	}
}
