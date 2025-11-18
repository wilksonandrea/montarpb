using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using System;
using System.Collections.Generic;
using System.Net;

namespace Server.Game.Data.Sync.Server
{
	public class SendClanInfo
	{
		public SendClanInfo()
		{
		}

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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void Load(ClanModel Clan, int Type)
		{
			try
			{
				foreach (SChannelModel server in SChannelXML.Servers)
				{
					if (server.Id == 0 || server.Id == GameXender.Client.ServerId)
					{
						continue;
					}
					IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void Update(ClanModel Clan, int Type)
		{
			try
			{
				foreach (SChannelModel server in SChannelXML.Servers)
				{
					if (server.Id == 0 || server.Id == GameXender.Client.ServerId)
					{
						continue;
					}
					IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}