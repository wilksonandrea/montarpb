using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Client;

public class AccountInfo
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		int num = C.ReadC();
		string packetName = C.ReadS(C.ReadC());
		byte[] data = C.ReadB(C.ReadUH());
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null)
		{
			if (num == 0)
			{
				account.SendPacket(data, packetName);
			}
			else
			{
				account.SendCompletePacket(data, packetName);
			}
		}
	}
}
