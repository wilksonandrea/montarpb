using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015A RID: 346
	public class PROTOCOL_BASE_USER_ENTER_REQ : GameClientPacket
	{
		// Token: 0x0600036E RID: 878 RVA: 0x000050A7 File Offset: 0x000032A7
		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
			this.long_0 = base.ReadQ();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001A830 File Offset: 0x00018A30
		public override void Run()
		{
			try
			{
				if (this.Client != null)
				{
					if (this.Client.Player != null)
					{
						this.uint_0 = 2147483648U;
					}
					else
					{
						Account accountDB = AccountManager.GetAccountDB(this.long_0, 2, 31);
						if (accountDB != null && accountDB.Username == this.string_0 && accountDB.Status.ServerId != 255)
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
							accountDB.Session = new PlayerSession
							{
								SessionId = this.Client.SessionId,
								PlayerId = this.Client.PlayerId
							};
							accountDB.Status.UpdateServer((byte)this.Client.ServerId);
							accountDB.UpdateCacheInfo();
							this.Client.Player = accountDB;
							ComDiv.UpdateDB("accounts", "ip4_address", accountDB.PublicIP.ToString(), "player_id", accountDB.PlayerId);
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
					}
					this.Client.SendPacket(new PROTOCOL_BASE_USER_ENTER_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_USER_ENTER_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_USER_ENTER_REQ()
		{
		}

		// Token: 0x04000277 RID: 631
		private uint uint_0;

		// Token: 0x04000278 RID: 632
		private long long_0;

		// Token: 0x04000279 RID: 633
		private string string_0;
	}
}
