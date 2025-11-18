using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using System;

namespace Server.Game.Data.Commands
{
	public class RoomInfoCommand : ICommand
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
				return "roominfo";
			}
		}

		public string Description
		{
			get
			{
				return "Change room options";
			}
		}

		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		public RoomInfoCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			string lower = Args[0].ToLower();
			string str = Args[1].ToLower();
			if (lower.Equals("time"))
			{
				int ınt32 = int.Parse(str) * 6;
				if (ınt32 < 0)
				{
					return string.Concat("Oops! Map 'index' Isn't Supposed To Be: ", str, ". Try an Higher Value.");
				}
				if (room.IsPreparing() || AllUtils.PlayerIsBattle(Player))
				{
					return "Oops! You Can't Change The 'time' While The Room Has Started Game Match.";
				}
				room.KillTime = ınt32;
				room.UpdateRoomInfo();
				return string.Format("{0} Changed To {1} Minutes", ComDiv.ToTitleCase(lower), room.GetTimeByMask() % 60);
			}
			if (lower.Equals("flags"))
			{
				RoomWeaponsFlag roomWeaponsFlag = (RoomWeaponsFlag)int.Parse(str);
				room.WeaponsFlag = roomWeaponsFlag;
				room.UpdateRoomInfo();
				return string.Format("{0} Changed To {1}", ComDiv.ToTitleCase(lower), roomWeaponsFlag);
			}
			if (!lower.Equals("killcam"))
			{
				return string.Concat("Command ", ComDiv.ToTitleCase(lower), " was not founded!");
			}
			byte num = byte.Parse(str);
			room.KillCam = num;
			room.UpdateRoomInfo();
			return string.Format("{0} Changed To {1}", ComDiv.ToTitleCase(lower), num);
		}
	}
}