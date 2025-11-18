using System;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000218 RID: 536
	public class TitleCommand : ICommand
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x000063DF File Offset: 0x000045DF
		public string Command
		{
			get
			{
				return "title";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x000063E6 File Offset: 0x000045E6
		public string Description
		{
			get
			{
				return "Unlock all title";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000063A7 File Offset: 0x000045A7
		public string Permission
		{
			get
			{
				return "hostcommand";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0000633D File Offset: 0x0000453D
		public string Args
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00039054 File Offset: 0x00037254
		public string Execute(string Command, string[] Args, Account Player)
		{
			if (Player.Title.OwnerId == 0L)
			{
				DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
				Player.Title = new PlayerTitles
				{
					OwnerId = Player.PlayerId
				};
			}
			PlayerTitles title = Player.Title;
			int num = 0;
			for (int i = 1; i <= 44; i++)
			{
				TitleModel title2 = TitleSystemXML.GetTitle(i, true);
				if (title2 != null && !title.Contains(title2.Flag))
				{
					num++;
					title.Add(title2.Flag);
					if (title.Slots < title2.Slot)
					{
						title.Slots = title2.Slot;
					}
				}
			}
			if (num > 0)
			{
				ComDiv.UpdateDB("player_titles", "slots", title.Slots, "owner_id", Player.PlayerId);
				DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title.Flags);
				Player.SendPacket(new PROTOCOL_BASE_USER_TITLE_INFO_ACK(Player));
			}
			return "All title Successfully Opened!";
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000025DF File Offset: 0x000007DF
		public TitleCommand()
		{
		}
	}
}
