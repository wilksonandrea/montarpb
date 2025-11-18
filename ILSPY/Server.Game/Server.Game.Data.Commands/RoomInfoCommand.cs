using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands;

public class RoomInfoCommand : ICommand
{
	public string Command => "roominfo";

	public string Description => "Change room options";

	public string Permission => "moderatorcommand";

	public string Args => "%options% %value%";

	public string Execute(string Command, string[] Args, Account Player)
	{
		RoomModel room = Player.Room;
		if (room == null)
		{
			return "A room is required to execute the command.";
		}
		string text = Args[0].ToLower();
		string text2 = Args[1].ToLower();
		if (text.Equals("time"))
		{
			int num = int.Parse(text2) * 6;
			if (num < 0)
			{
				return "Oops! Map 'index' Isn't Supposed To Be: " + text2 + ". Try an Higher Value.";
			}
			if (!room.IsPreparing() && !AllUtils.PlayerIsBattle(Player))
			{
				room.KillTime = num;
				room.UpdateRoomInfo();
				return $"{ComDiv.ToTitleCase(text)} Changed To {room.GetTimeByMask() % 60} Minutes";
			}
			return "Oops! You Can't Change The 'time' While The Room Has Started Game Match.";
		}
		if (text.Equals("flags"))
		{
			RoomWeaponsFlag roomWeaponsFlag = (room.WeaponsFlag = (RoomWeaponsFlag)int.Parse(text2));
			room.UpdateRoomInfo();
			return $"{ComDiv.ToTitleCase(text)} Changed To {roomWeaponsFlag}";
		}
		if (text.Equals("killcam"))
		{
			byte b = (room.KillCam = byte.Parse(text2));
			room.UpdateRoomInfo();
			return $"{ComDiv.ToTitleCase(text)} Changed To {b}";
		}
		return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
	}
}
