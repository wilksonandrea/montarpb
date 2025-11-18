using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BF RID: 447
	public class PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ : GameClientPacket
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0002477C File Offset: 0x0002297C
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				this.list_0.Add(base.ReadD());
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000247B0 File Offset: 0x000229B0
		public override void Run()
		{
			try
			{
				if (ComDiv.UpdateDB("player_messages", "object_id", this.list_0.ToArray(), "owner_id", this.Client.PlayerId, new string[] { "expire_date", "state" }, new object[]
				{
					long.Parse(DateTimeUtil.Now().AddDays(7.0).ToString("yyMMddHHmm")),
					0
				}))
				{
					this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_CHECK_READED_ACK(this.list_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00005711 File Offset: 0x00003911
		public PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ()
		{
		}

		// Token: 0x04000334 RID: 820
		private readonly List<int> list_0 = new List<int>();
	}
}
