using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using System;

namespace Server.Match.Data.Models.Event
{
	public class SuicideInfo
	{
		public uint HitInfo;

		public Half3 PlayerPos;

		public ClassType WeaponClass;

		public byte Extensions;

		public byte Accessory;

		public int WeaponId;

		public SuicideInfo()
		{
		}
	}
}