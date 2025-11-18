using System;

namespace Server.Match.Data.Models.Event
{
	public class WeaponRecoilInfo
	{
		public float RecoilHorzAngle;

		public float RecoilHorzMax;

		public float RecoilVertAngle;

		public float RecoilVertMax;

		public float Deviation;

		public int WeaponId;

		public byte Extensions;

		public byte Accessory;

		public byte RecoilHorzCount;

		public WeaponRecoilInfo()
		{
		}
	}
}