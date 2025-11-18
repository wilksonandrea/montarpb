using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class FragModel
	{
		public byte AssistSlot
		{
			get;
			set;
		}

		public byte HitspotInfo
		{
			get;
			set;
		}

		public KillingMessage KillFlag
		{
			get;
			set;
		}

		public byte Unk
		{
			get;
			set;
		}

		public byte[] Unks
		{
			get;
			set;
		}

		public byte VictimSlot
		{
			get;
			set;
		}

		public byte WeaponClass
		{
			get;
			set;
		}

		public float X
		{
			get;
			set;
		}

		public float Y
		{
			get;
			set;
		}

		public float Z
		{
			get;
			set;
		}

		public FragModel()
		{
		}
	}
}