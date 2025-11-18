using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_ENTER_REQ : GameClientPacket
{
	private uint uint_0;

	private long long_0;

	private string string_0;

	public override void Read()
	{
		string_0 = ReadS(ReadC());
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			if (Client == null)
			{
				return;
			}
			if (Client.Player != null)
			{
				uint_0 = 2147483648u;
			}
			else
			{
				Account accountDB = AccountManager.GetAccountDB(long_0, 2, 31);
				if (accountDB != null && accountDB.Username == string_0 && accountDB.Status.ServerId != byte.MaxValue)
				{
					Client.PlayerId = accountDB.PlayerId;
					accountDB.Connection = Client;
					accountDB.ServerId = Client.ServerId;
					accountDB.GetAccountInfos(7935);
					AllUtils.ValidateAuthLevel(accountDB);
					AllUtils.LoadPlayerInventory(accountDB);
					AllUtils.LoadPlayerMissions(accountDB);
					AllUtils.EnableQuestMission(accountDB);
					AllUtils.ValidatePlayerInventoryStatus(accountDB);
					accountDB.SetPublicIP(Client.GetAddress());
					accountDB.Session = new PlayerSession
					{
						SessionId = Client.SessionId,
						PlayerId = Client.PlayerId
					};
					accountDB.Status.UpdateServer((byte)Client.ServerId);
					accountDB.UpdateCacheInfo();
					Client.Player = accountDB;
					ComDiv.UpdateDB("accounts", "ip4_address", accountDB.PublicIP.ToString(), "player_id", accountDB.PlayerId);
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			Client.SendPacket(new PROTOCOL_BASE_USER_ENTER_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
