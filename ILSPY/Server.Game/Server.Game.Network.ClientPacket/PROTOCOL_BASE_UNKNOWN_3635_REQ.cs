using System;
using Plugin.Core;
using Plugin.Core.Enums;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_UNKNOWN_3635_REQ : GameClientPacket
{
	private byte byte_0;

	private byte byte_1;

	private byte byte_2;

	private string string_0;

	private short short_0;

	public override void Read()
	{
		byte_0 = ReadC();
		string_0 = ReadU(66);
		ReadD();
		ReadH();
		byte_1 = ReadC();
		ReadH();
		ReadB(16);
		ReadB(12);
		short_0 = ReadH();
		byte_2 = ReadC();
	}

	public override void Run()
	{
		try
		{
			CLogger.Print($"{GetType().Name}; Unk1: {byte_0}; Nickname: {string_0}; Unk2: {byte_1}; Unk3: {short_0}; Unk4: {byte_2}", LoggerType.Warning);
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + " Error: " + ex.Message, LoggerType.Error);
		}
	}
}
