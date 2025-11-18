using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C0 RID: 448
	public class PROTOCOL_MESSENGER_NOTE_DELETE_REQ : GameClientPacket
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00024888 File Offset: 0x00022A88
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				long num2 = (long)((ulong)base.ReadUD());
				this.list_0.Add(num2);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000248C4 File Offset: 0x00022AC4
		public override void Run()
		{
			try
			{
				if (!DaoManagerSQL.DeleteMessages(this.list_0, this.Client.PlayerId))
				{
					this.uint_0 = 2147483648U;
				}
				this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_DELETE_ACK(this.uint_0, this.list_0));
				this.list_0 = null;
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MESSENGER_NOTE_DELETE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00005724 File Offset: 0x00003924
		public PROTOCOL_MESSENGER_NOTE_DELETE_REQ()
		{
		}

		// Token: 0x04000335 RID: 821
		private uint uint_0;

		// Token: 0x04000336 RID: 822
		private List<object> list_0 = new List<object>();
	}
}
