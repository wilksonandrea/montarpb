using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001EA RID: 490
	public class SendFriendInfo
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0002F260 File Offset: 0x0002D460
		public static void Load(Account Player, FriendModel Friend, int Type)
		{
			try
			{
				if (Player != null)
				{
					SChannelModel server = GameXender.Sync.GetServer(Player.Status);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(17);
							syncServerPacket.WriteQ(Player.PlayerId);
							syncServerPacket.WriteC((byte)Type);
							syncServerPacket.WriteQ(Friend.PlayerId);
							if (Type != 2)
							{
								syncServerPacket.WriteC((byte)Friend.State);
								syncServerPacket.WriteC((Friend.Removed > false) ? 1 : 0);
							}
							GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000025DF File Offset: 0x000007DF
		public SendFriendInfo()
		{
		}
	}
}
