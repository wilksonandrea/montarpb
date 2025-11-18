using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_CLAN_LIST_FILTER_REQ : GameClientPacket
{
	private byte byte_0;

	private byte byte_1;

	private ClanSearchType clanSearchType_0;

	private string string_0;

	public override void Read()
	{
		byte_0 = ReadC();
		string_0 = ReadU(ReadC() * 2);
		byte_1 = ReadC();
		clanSearchType_0 = (ClanSearchType)ReadC();
	}

	public override void Run()
	{
		try
		{
			int num = 0;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			lock (ClanManager.Clans)
			{
				byte b = byte_0;
				while (b < ClanManager.Clans.Count)
				{
					ClanModel clanModel_ = ClanManager.Clans[b];
					method_0(clanModel_, syncServerPacket);
					if (++num != 15)
					{
						b = (byte)(b + 1);
						continue;
					}
					break;
				}
			}
			Client.SendPacket(new PROTOCOL_CS_CLAN_LIST_FILTER_ACK(byte_0, num, syncServerPacket.ToArray()));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

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
}
