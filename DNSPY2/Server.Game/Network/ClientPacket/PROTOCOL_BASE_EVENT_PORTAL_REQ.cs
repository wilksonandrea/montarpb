using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013D RID: 317
	public class PROTOCOL_BASE_EVENT_PORTAL_REQ : GameClientPacket
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00004F5C File Offset: 0x0000315C
		public override void Read()
		{
			this.string_0 = base.ReadS(32);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018D30 File Offset: 0x00016F30
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!player.LoadedShop)
					{
						player.LoadedShop = true;
					}
					if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/EventPortal.dat") == this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(false));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_EVENT_PORTAL_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_EVENT_PORTAL_REQ()
		{
		}

		// Token: 0x0400023F RID: 575
		private string string_0;
	}
}
