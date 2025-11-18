using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A1 RID: 417
	public class PROTOCOL_CS_JOIN_REQUEST_REQ : GameClientPacket
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00005532 File Offset: 0x00003732
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00021B4C File Offset: 0x0001FD4C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanInvite clanInvite = new ClanInvite
					{
						Id = this.int_0,
						PlayerId = this.Client.PlayerId,
						Text = this.string_0,
						InviteDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
					};
					if (player.ClanId <= 0 && player.Nickname.Length != 0)
					{
						if (ClanManager.GetClan(this.int_0).Id == 0)
						{
							this.uint_0 = 2147483648U;
						}
						else if (DaoManagerSQL.GetRequestClanInviteCount(this.int_0) >= 100)
						{
							this.uint_0 = 2147487831U;
						}
						else if (!DaoManagerSQL.CreateClanInviteInDB(clanInvite))
						{
							this.uint_0 = 2147487848U;
						}
					}
					else
					{
						this.uint_0 = 2147487836U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_JOIN_REQUEST_ACK(this.uint_0, this.int_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_JOIN_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_JOIN_REQUEST_REQ()
		{
		}

		// Token: 0x04000306 RID: 774
		private int int_0;

		// Token: 0x04000307 RID: 775
		private string string_0;

		// Token: 0x04000308 RID: 776
		private uint uint_0;
	}
}
