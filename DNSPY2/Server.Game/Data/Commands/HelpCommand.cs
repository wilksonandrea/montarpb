using System;
using System.Collections.Generic;
using System.Linq;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000211 RID: 529
	public class HelpCommand : ICommand
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00006344 File Offset: 0x00004544
		public string Command
		{
			get
			{
				return "help";
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000634B File Offset: 0x0000454B
		public string Description
		{
			get
			{
				return "Show available commands";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00006352 File Offset: 0x00004552
		public string Permission
		{
			get
			{
				return "helpcommand";
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00006359 File Offset: 0x00004559
		public string Args
		{
			get
			{
				return "%page% (optional)";
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000382C0 File Offset: 0x000364C0
		public string Execute(string Command, string[] Args, Account Player)
		{
			int num = 1;
			if (Args.Length != 0)
			{
				int num2;
				if (int.TryParse(Args[0], out num2))
				{
					if (num2 == 0)
					{
						num2 = 1;
					}
					num = num2;
				}
				else
				{
					num = 1;
				}
			}
			IEnumerable<ICommand> commandsForPlayer = CommandManager.GetCommandsForPlayer(Player);
			int num3 = (commandsForPlayer.Count<ICommand>() + this.int_0 - 1) / this.int_0;
			if (num > num3)
			{
				return string.Format("Please insert a valid page. Pages: {0}", num3);
			}
			IEnumerable<ICommand> enumerable = commandsForPlayer.Split(this.int_0).ToArray<IEnumerable<ICommand>>()[num - 1];
			string text = string.Format("Commands ({0}/{1}):\n", num, num3);
			foreach (ICommand command in enumerable)
			{
				text = string.Concat(new string[] { text, ":", command.Command, " ", command.Args, " -> ", command.Description, "\n" });
			}
			Player.Connection.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text));
			return string.Format("Displayed commands page '{0}' of '{1}'", num, num3);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00006360 File Offset: 0x00004560
		public HelpCommand()
		{
		}

		// Token: 0x04000459 RID: 1113
		private readonly int int_0 = 5;
	}
}
