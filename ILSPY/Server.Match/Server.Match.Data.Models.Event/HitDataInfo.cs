using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models.Event;

public class HitDataInfo
{
	public byte Extensions;

	public byte Accessory;

	public ushort BoomInfo;

	public ushort ObjectId;

	public uint HitIndex;

	public int WeaponId;

	public Half3 StartBullet;

	public Half3 EndBullet;

	public Half3 BulletPos;

	public List<int> BoomPlayers;

	public HitType HitEnum;

	public ClassType WeaponClass;
}
