using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013C RID: 316
	public class PROTOCOL_BASE_ENTER_PASS_REQ : GameClientPacket
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00004F40 File Offset: 0x00003140
		public override void Read()
		{
			this.int_0 = (int)base.ReadH();
			this.string_0 = base.ReadS(16);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018C90 File Offset: 0x00016E90
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ChannelId < 0)
				{
					ChannelModel channel = ChannelsXML.GetChannel(this.Client.ServerId, this.int_0);
					if (channel != null)
					{
						if (!this.string_0.Equals(channel.Password))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(2147483648U));
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(0U));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_ENTER_PASS_REQ()
		{
		}

		// Token: 0x0400023D RID: 573
		private int int_0;

		// Token: 0x0400023E RID: 574
		private string string_0;
	}
}
