using System;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000215 RID: 533
	public class HostCommand : ICommand
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00006399 File Offset: 0x00004599
		public string Command
		{
			get
			{
				return "host";
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x000063A0 File Offset: 0x000045A0
		public string Description
		{
			get
			{
				return "Change room options (AI Only)";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000063A7 File Offset: 0x000045A7
		public string Permission
		{
			get
			{
				return "hostcommand";
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00006392 File Offset: 0x00004592
		public string Args
		{
			get
			{
				return "%options% %value%";
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000386D8 File Offset: 0x000368D8
		public string Execute(string Command, string[] Args, Account Player)
		{
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			Account account;
			if (room.GetLeader(out account))
			{
				if (account == Player)
				{
					string text = Args[0].ToLower();
					string text2 = Args[1].ToLower();
					if (text.Equals("wpn"))
					{
						CommandHelper tag = CommandHelperJSON.GetTag("WeaponsFlag");
						if (text2 != null)
						{
							int num = text2.Length;
							if (num != 2)
							{
								if (num == 3)
								{
									char c = text2[0];
									if (c != 'a')
									{
										if (c != 'r')
										{
											if (c == 's')
											{
												if (text2 == "smg")
												{
													room.WeaponsFlag = (RoomWeaponsFlag)tag.SubMachineGun;
													room.UpdateRoomInfo();
													return "Weapon Sub Machine Gun (Only)";
												}
											}
										}
										else if (text2 == "rpg")
										{
											room.WeaponsFlag = (RoomWeaponsFlag)tag.RPG7;
											room.UpdateRoomInfo();
											return "Weapon RPG-7 (Only) - Hot Glitch";
										}
									}
									else if (text2 == "all")
									{
										room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
										room.UpdateRoomInfo();
										return "All Weapons (AR, SMG, SR, SG, MG, SHD)";
									}
								}
							}
							else
							{
								char c = text2[0];
								if (c != 'a')
								{
									if (c != 'm')
									{
										if (c == 's')
										{
											if (text2 == "sr")
											{
												room.WeaponsFlag = (RoomWeaponsFlag)tag.SniperRifle;
												room.UpdateRoomInfo();
												return "Weapon Sniper Rifle (Only)";
											}
											if (text2 == "sg")
											{
												room.WeaponsFlag = (RoomWeaponsFlag)tag.ShotGun;
												room.UpdateRoomInfo();
												return "Weapon Shot Gun (Only)";
											}
										}
									}
									else if (text2 == "mg")
									{
										room.WeaponsFlag = (RoomWeaponsFlag)tag.MachineGun;
										room.UpdateRoomInfo();
										return "Weapon Machine Gun (Only)";
									}
								}
								else if (text2 == "ar")
								{
									room.WeaponsFlag = (RoomWeaponsFlag)tag.AssaultRifle;
									room.UpdateRoomInfo();
									return "Weapon Assault Rifle (Only)";
								}
							}
						}
						room.WeaponsFlag = (RoomWeaponsFlag)tag.AllWeapons;
						room.UpdateRoomInfo();
						return "Weapon Default (Value Not Founded)";
					}
					if (text.Equals("time"))
					{
						CommandHelper tag2 = CommandHelperJSON.GetTag("PlayTime");
						int num = int.Parse(text2);
						if (num <= 15)
						{
							if (num == 5)
							{
								room.KillTime = tag2.Minutes05;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 5 Minutes";
							}
							if (num == 10)
							{
								room.KillTime = tag2.Minutes10;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 10 Minutes";
							}
							if (num == 15)
							{
								room.KillTime = tag2.Minutes15;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 15 Minutes";
							}
						}
						else
						{
							if (num == 20)
							{
								room.KillTime = tag2.Minutes20;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 20 Minutes";
							}
							if (num == 25)
							{
								room.KillTime = tag2.Minutes25;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 25 Minutes";
							}
							if (num == 30)
							{
								room.KillTime = tag2.Minutes30;
								room.UpdateRoomInfo();
								return ComDiv.ToTitleCase(text) + " 30 Minutes";
							}
						}
						return ComDiv.ToTitleCase(text) + " None! (Wrong Value)";
					}
					if (!text.Equals("compe"))
					{
						return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
					}
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
					else
					{
						if (room.GetSlotCount() != 6 && room.GetSlotCount() != 8 && room.GetSlotCount() != 10)
						{
							return "Please change the slots to (3v3) or (4v4) or (5v5)";
						}
						room.Name = "Competitive!!!";
						room.Competitive = true;
						AllUtils.SendCompetitiveInfo(Player);
						return ComDiv.ToTitleCase(text) + "titive Enabled!";
					}
				}
			}
			return "This Command Is Only Valid For Host (Room Leader).";
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000025DF File Offset: 0x000007DF
		public HostCommand()
		{
		}
	}
}
