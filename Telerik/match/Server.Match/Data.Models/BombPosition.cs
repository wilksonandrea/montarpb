using Plugin.Core.SharpDX;
using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class BombPosition
	{
		public bool EveryWhere
		{
			get;
			set;
		}

		public Half3 Position
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

		public BombPosition()
		{
		}
	}
}