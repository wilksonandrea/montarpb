using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003F RID: 63
	public class PROTOCOL_BASE_GET_MAP_INFO_REQ : AuthClientPacket
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000750C File Offset: 0x0000570C
		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_BASE_MAP_RULELIST_ACK());
				foreach (IEnumerable<MapMatch> enumerable in SystemMapXML.Matches.Split(100))
				{
					List<MapMatch> list = enumerable.ToList<MapMatch>();
					if (list.Count > 0)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(list, list.Count));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_MAP_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_MAP_INFO_REQ()
		{
		}
	}
}
