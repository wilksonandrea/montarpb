using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Net;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_USER_ENTER_REQ : GameClientPacket
	{
		private uint uint_0;

		private long long_0;

		private string string_0;

		public PROTOCOL_BASE_USER_ENTER_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				if (this.Client != null)
				{
					if (this.Client.Player == null)
					{
						Account accountDB = AccountManager.GetAccountDB(this.long_0, 2, 31);
						if (accountDB == null || !(accountDB.Username == this.string_0) || accountDB.Status.ServerId == 255)
						{
							this.uint_0 = -2147483648;
						}
						else
						{
							this.Client.PlayerId = accountDB.PlayerId;
							accountDB.Connection = this.Client;
							accountDB.ServerId = this.Client.ServerId;
							accountDB.GetAccountInfos(7935);
							AllUtils.ValidateAuthLevel(accountDB);
							AllUtils.LoadPlayerInventory(accountDB);
							AllUtils.LoadPlayerMissions(accountDB);
							AllUtils.EnableQuestMission(accountDB);
							AllUtils.ValidatePlayerInventoryStatus(accountDB);
							accountDB.SetPublicIP(this.Client.GetAddress());
							accountDB.Session = new PlayerSession()
							{
								SessionId = this.Client.SessionId,
								PlayerId = this.Client.PlayerId
							};
							accountDB.Status.UpdateServer((byte)this.Client.ServerId);
							accountDB.UpdateCacheInfo();
							this.Client.Player = accountDB;
							ComDiv.UpdateDB("accounts", "ip4_address", accountDB.PublicIP.ToString(), "player_id", accountDB.PlayerId);
						}
					}
					else
					{
						this.uint_0 = -2147483648;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_USER_ENTER_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_USER_ENTER_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}