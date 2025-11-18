using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000193 RID: 403
	public class PROTOCOL_CS_CLAN_LIST_FILTER_REQ : GameClientPacket
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000543B File Offset: 0x0000363B
		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			this.byte_1 = base.ReadC();
			this.clanSearchType_0 = (ClanSearchType)base.ReadC();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00020360 File Offset: 0x0001E560
		public override void Run()
		{
			try
			{
				int num = 0;
				using (SyncServerPacket syncServerPacket = new SyncServerPacket())
				{
					List<ClanModel> clans = ClanManager.Clans;
					lock (clans)
					{
						byte b = this.byte_0;
						while ((int)b < ClanManager.Clans.Count)
						{
							ClanModel clanModel = ClanManager.Clans[(int)b];
							this.method_0(clanModel, syncServerPacket);
							if (++num == 15)
							{
								break;
							}
							b += 1;
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_FILTER_ACK(this.byte_0, num, syncServerPacket.ToArray()));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00020438 File Offset: 0x0001E638
		private void method_0(ClanModel clanModel_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(clanModel_0.Id);
			syncServerPacket_0.WriteC((byte)(clanModel_0.Name.Length + 1));
			syncServerPacket_0.WriteN(clanModel_0.Name, clanModel_0.Name.Length + 2, "UTF-16LE");
			syncServerPacket_0.WriteD(clanModel_0.Logo);
			syncServerPacket_0.WriteH((ushort)(clanModel_0.Info.Length + 1));
			syncServerPacket_0.WriteN(clanModel_0.Info, clanModel_0.Info.Length + 2, "UTF-16LE");
			syncServerPacket_0.WriteC(0);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLAN_LIST_FILTER_REQ()
		{
		}

		// Token: 0x040002EF RID: 751
		private byte byte_0;

		// Token: 0x040002F0 RID: 752
		private byte byte_1;

		// Token: 0x040002F1 RID: 753
		private ClanSearchType clanSearchType_0;

		// Token: 0x040002F2 RID: 754
		private string string_0;
	}
}
