using System;

namespace Server.Match.Data.Models.Event
{
	public class FireDataInfo
	{
		public byte Effect;

		public byte Part;

		public byte Extensions;

		public byte Accessory;

		public short Index;

		public ushort X;

		public ushort Y;

		public ushort Z;

		public int WeaponId;

		public FireDataInfo()
		{
		}
	}
}