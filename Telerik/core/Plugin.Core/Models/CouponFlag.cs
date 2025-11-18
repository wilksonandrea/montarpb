using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class CouponFlag
	{
		public CouponEffects EffectFlag
		{
			get;
			set;
		}

		public int ItemId
		{
			get;
			set;
		}

		public CouponFlag()
		{
		}
	}
}