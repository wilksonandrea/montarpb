using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System;

namespace Server.Game.Data.Commands
{
	public class HostCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "%options% %value%";
			}
		}

		public string Command
		{
			get
			{
				return "host";
			}
		}

		public string Description
		{
			get
			{
				return "Change room options (AI Only)";
			}
		}

		public string Permission
		{
			get
			{
				return "hostcommand";
			}
		}

		public HostCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			Account account;
			int length;
			char chr;
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			if (room.GetLeader(out account))
			{
				if (account == Player)
				{
					string lower = Args[0].ToLower();
					string str = Args[1].ToLower();
					if (lower.Equals("wpn"))
					{
						CommandHelper tag = CommandHelperJSON.GetTag("WeaponsFlag");
						if (str != null)
						{
							length = str.Length;
							if (length == 2)
							{
								chr = str[0];
								if (chr == 'a')
								{
									if (str == "ar")
									{
										room.WeaponsFlag = (RoomWeaponsFlag)tag.AssaultRifle;
										room.UpdateRoomInfo();
										return "Weapon Assault Rifle (Only)";
									}
								}
								else if (chr != 'm')
								{
									if (chr == 's')
									{
										if (str == "sr")
										{
											room.WeaponsFlag = (RoomWeaponsFlag)tag.SniperRifle;
											room.UpdateRoomInfo();
											return "Weapon Sniper Rifle (Only)";
										}
										if (str == "sg")
										{
											room.WeaponsFlag = (RoomWeaponsFlag)tag.ShotGun;
											room.UpdateRoomInfo();
											return "Weapon Shot Gun (Only)";
										}
									}
								}
								else if (str == "mg")
								{
									room.WeaponsFlag = (RoomWeaponsFlag)tag.MachineGun;
									room.UpdateRoomInfo();
									return "Weapon Machine Gun (Only)";
								}
							}
							else if (length == 3)
							{
								chr = str[0];
								if (chr == 'a')
								{
									if (str == "all")
									{
										room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
										room.UpdateRoomInfo();
										return "All Weapons (AR, SMG, SR, SG, MG, SHD)";
									}
								}
								else if (chr != 'r')
								{
									if (chr == 's')
									{
										if (str == "smg")
										{
											room.WeaponsFlag = (RoomWeaponsFlag)tag.SubMachineGun;
											room.UpdateRoomInfo();
											return "Weapon Sub Machine Gun (Only)";
										}
									}
								}
								else if (str == "rpg")
								{
									room.WeaponsFlag = (RoomWeaponsFlag)tag.RPG7;
									room.UpdateRoomInfo();
									return "Weapon RPG-7 (Only) - Hot Glitch";
								}
							}
						}
						room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
						room.UpdateRoomInfo();
						return "Weapon Default (Value Not Founded)";
					}
					if (!lower.Equals("time"))
					{
						if (!lower.Equals("compe"))
						{
							return string.Concat("Command ", ComDiv.ToTitleCase(lower), " was not founded!");
						}
						string lower1 = str.ToLower();
						if (lower1 != "on")
						{
							if (lower1 != "off")
							{
								return string.Concat("Unable to use Competitive command! (Wrong Value: ", str, ")");
							}
							room.Name = room.RandomName((new Random()).Next(1, 4));
							room.Competitive = false;
							return string.Concat(ComDiv.ToTitleCase(lower), "titive Disabled!");
						}
						if (room.GetSlotCount() != 6 && room.GetSlotCount() != 8 && room.GetSlotCount() != 10)
						{
							return "Please change the slots to (3v3) or (4v4) or (5v5)";
						}
						room.Name = "Competitive!!!";
						room.Competitive = true;
						AllUtils.SendCompetitiveInfo(Player);
						return string.Concat(ComDiv.ToTitleCase(lower), "titive Enabled!");
					}
					CommandHelper commandHelper = CommandHelperJSON.GetTag("PlayTime");
					length = int.Parse(str);
					if (length > 15)
					{
						if (length == 20)
						{
							room.KillTime = commandHelper.Minutes20;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 20 Minutes");
						}
						if (length == 25)
						{
							room.KillTime = commandHelper.Minutes25;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 25 Minutes");
						}
						if (length == 30)
						{
							room.KillTime = commandHelper.Minutes30;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 30 Minutes");
						}
					}
					else
					{
						if (length == 5)
						{
							room.KillTime = commandHelper.Minutes05;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 5 Minutes");
						}
						if (length == 10)
						{
							room.KillTime = commandHelper.Minutes10;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 10 Minutes");
						}
						if (length == 15)
						{
							room.KillTime = commandHelper.Minutes15;
							room.UpdateRoomInfo();
							return string.Concat(ComDiv.ToTitleCase(lower), " 15 Minutes");
						}
					}
					return string.Concat(ComDiv.ToTitleCase(lower), " None! (Wrong Value)");
				}
			}
			return "This Command Is Only Valid For Host (Room Leader).";
		}
	}
}