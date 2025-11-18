using Plugin.Core.Enums;
using System;

namespace Server.Match.Data.Models.Event
{
	public class WeaponSyncInfo
	{
		public int WeaponId;

		public byte Accessory;

		public byte Extensions;

		public ClassType WeaponClass;

		public WeaponSyncInfo()
		{
		}
	}
}