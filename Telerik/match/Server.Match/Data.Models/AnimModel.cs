using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class AnimModel
	{
		public float Duration
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int NextAnim
		{
			get;
			set;
		}

		public int OtherAnim
		{
			get;
			set;
		}

		public int OtherObj
		{
			get;
			set;
		}

		public AnimModel()
		{
		}
	}
}