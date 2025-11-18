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
	// Token: 0x020001E9 RID: 489
	public class SendClanInfo
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0002EE68 File Offset: 0x0002D068
		public static void Load(Account Player, Account Member, int Type)
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
							syncServerPacket.WriteH(16);
							syncServerPacket.WriteQ(Player.PlayerId);
							syncServerPacket.WriteC((byte)Type);
							if (Type == 1)
							{
								syncServerPacket.WriteQ(Member.PlayerId);
								syncServerPacket.WriteC((byte)(Member.Nickname.Length + 1));
								syncServerPacket.WriteS(Member.Nickname, Member.Nickname.Length + 1);
								syncServerPacket.WriteB(Member.Status.Buffer);
								syncServerPacket.WriteC((byte)Member.Rank);
							}
							else if (Type == 2)
							{
								syncServerPacket.WriteQ(Member.PlayerId);
							}
							else if (Type == 3)
							{
								syncServerPacket.WriteD(Player.ClanId);
								syncServerPacket.WriteC((byte)Player.ClanAccess);
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

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
		public static void Update(ClanModel Clan, int Type)
		{
			try
			{
				foreach (SChannelModel schannelModel in SChannelXML.Servers)
				{
					if (schannelModel.Id != 0 && schannelModel.Id != GameXender.Client.ServerId)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)schannelModel.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(22);
							syncServerPacket.WriteC((byte)Type);
							if (Type == 0)
							{
								syncServerPacket.WriteQ(Clan.OwnerId);
							}
							else if (Type == 1)
							{
								syncServerPacket.WriteC((byte)(Clan.Name.Length + 1));
								syncServerPacket.WriteS(Clan.Name, Clan.Name.Length + 1);
							}
							else if (Type == 2)
							{
								syncServerPacket.WriteC((byte)Clan.NameColor);
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

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002F0D8 File Offset: 0x0002D2D8
		public static void Load(ClanModel Clan, int Type)
		{
			try
			{
				foreach (SChannelModel schannelModel in SChannelXML.Servers)
				{
					if (schannelModel.Id != 0 && schannelModel.Id != GameXender.Client.ServerId)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)schannelModel.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(21);
							syncServerPacket.WriteC((byte)Type);
							syncServerPacket.WriteD(Clan.Id);
							if (Type == 0)
							{
								syncServerPacket.WriteQ(Clan.OwnerId);
								syncServerPacket.WriteD(Clan.CreationDate);
								syncServerPacket.WriteC((byte)(Clan.Name.Length + 1));
								syncServerPacket.WriteS(Clan.Name, Clan.Name.Length + 1);
								syncServerPacket.WriteC((byte)(Clan.Info.Length + 1));
								syncServerPacket.WriteS(Clan.Info, Clan.Info.Length + 1);
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

		// Token: 0x060005C4 RID: 1476 RVA: 0x000025DF File Offset: 0x000007DF
		public SendClanInfo()
		{
		}
	}
}
