using System;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000210 RID: 528
	public class BattleEndCommand : ICommand
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00006328 File Offset: 0x00004528
		public string Command
		{
			get
			{
				return "endbattle";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000632F File Offset: 0x0000452F
		public string Description
		{
			get
			{
				return "End work in progress battle";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00006336 File Offset: 0x00004536
		public string Permission
		{
			get
			{
				return "moderatorcommand";
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000633D File Offset: 0x0000453D
		public string Args
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00038280 File Offset: 0x00036480
		public string Execute(string Command, string[] Args, Account Player)
		{
			RoomModel room = Player.Room;
			if (room == null)
			{
				return "A room is required to execute the command.";
			}
			if (room.IsPreparing() && AllUtils.PlayerIsBattle(Player))
			{
				AllUtils.EndBattle(room);
				return "Battle ended.";
			}
			return "Oops! the battle hasn't started.";
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000025DF File Offset: 0x000007DF
		public BattleEndCommand()
		{
		}
	}
}
