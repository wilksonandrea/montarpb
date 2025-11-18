using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_JOIN_REQUEST_REQ : GameClientPacket
{
	private int int_0;

	private string string_0;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadD();
		string_0 = ReadU(ReadC() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			ClanInvite invite = new ClanInvite
			{
				Id = int_0,
				PlayerId = Client.PlayerId,
				Text = string_0,
				InviteDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
			};
			if (player.ClanId <= 0 && player.Nickname.Length != 0)
			{
				if (ClanManager.GetClan(int_0).Id == 0)
				{
					uint_0 = 2147483648u;
				}
				else if (DaoManagerSQL.GetRequestClanInviteCount(int_0) >= 100)
				{
					uint_0 = 2147487831u;
				}
				else if (!DaoManagerSQL.CreateClanInviteInDB(invite))
				{
					uint_0 = 2147487848u;
				}
			}
			else
			{
				uint_0 = 2147487836u;
			}
			invite = null;
			Client.SendPacket(new PROTOCOL_CS_JOIN_REQUEST_ACK(uint_0, int_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_JOIN_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
