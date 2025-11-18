using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000158 RID: 344
	public class PROTOCOL_BASE_URL_LIST_REQ : GameClientPacket
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001A78C File Offset: 0x0001898C
		public override void Run()
		{
			try
			{
				ServerConfig config = GameXender.Client.Config;
				if ((config != null) & config.OfficialBannerEnabled)
				{
					this.Client.SendPacket(new PROTOCOL_BASE_URL_LIST_ACK(config));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_URL_LIST_REQ()
		{
		}
	}
}
