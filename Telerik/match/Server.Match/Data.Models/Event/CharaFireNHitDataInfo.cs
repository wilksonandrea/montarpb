using Plugin.Core.Enums;
using System;

namespace Server.Match.Data.Models.Event
{
	public class CharaFireNHitDataInfo
	{
		public byte Extensions;

		public byte Accessory;

		public ushort X;

		public ushort Y;

		public ushort Z;

		public uint HitInfo;

		public int WeaponId;

		public short Unk;

		public ClassType WeaponClass;

		public CharaFireNHitDataInfo()
		{
		}
	}
}