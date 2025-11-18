using System;
using Server.Game.Data.Models;

namespace Server.Game.Data.Commands
{
	// Token: 0x02000212 RID: 530
	public interface ICommand
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000706 RID: 1798
		string Command { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000707 RID: 1799
		string Description { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000708 RID: 1800
		string Permission { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000709 RID: 1801
		string Args { get; }

		// Token: 0x0600070A RID: 1802
		string Execute(string Command, string[] Args, Account Player);
	}
}
