using System;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000214 RID: 532
	public class RoomInfoCommand : ICommand
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00006384 File Offset: 0x00004584
		public string Command
		{
			get
			{
				return "roominfo";
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000638B File Offset: 0x0000458B
		public string Description
		{
			get
			{
				return "Change room options";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00006336 File Offset: 0x00004536
		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00006392 File Offset: 0x00004592
		public string Args
		{
			get
			{
				return "%options% %value%";
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000385B0 File Offset: 0x000367B0
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
					return string.Format("{0} Changed To {1} Minutes", ComDiv.ToTitleCase(text), room.GetTimeByMask() % 60);
				}
				return "Oops! You Can't Change The 'time' While The Room Has Started Game Match.";
			}
			else
			{
				if (text.Equals("flags"))
				{
					RoomWeaponsFlag roomWeaponsFlag = (RoomWeaponsFlag)int.Parse(text2);
					room.WeaponsFlag = roomWeaponsFlag;
					room.UpdateRoomInfo();
					return string.Format("{0} Changed To {1}", ComDiv.ToTitleCase(text), roomWeaponsFlag);
				}
				if (text.Equals("killcam"))
				{
					byte b = byte.Parse(text2);
					room.KillCam = b;
					room.UpdateRoomInfo();
					return string.Format("{0} Changed To {1}", ComDiv.ToTitleCase(text), b);
				}
				return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000025DF File Offset: 0x000007DF
		public RoomInfoCommand()
		{
		}
	}
}
