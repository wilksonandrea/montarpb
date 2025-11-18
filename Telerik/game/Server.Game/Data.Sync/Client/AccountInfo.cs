using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class AccountInfo
	{
		public AccountInfo()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			long ınt64 = C.ReadQ();
			int ınt32 = C.ReadC();
			string str = C.ReadS((int)C.ReadC());
			byte[] numArray = C.ReadB((int)C.ReadUH());
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account != null)
			{
				if (ınt32 == 0)
				{
					account.SendPacket(numArray, str);
					return;
				}
				account.SendCompletePacket(numArray, str);
			}
		}
	}
}