using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	public class MapModel
	{
		public List<BombPosition> Bombs
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public List<ObjectModel> Objects
		{
			get;
			set;
		}

		public MapModel()
		{
		}

		public BombPosition GetBomb(int BombId)
		{
			BombPosition ıtem;
			try
			{
				ıtem = this.Bombs[BombId];
			}
			catch
			{
				ıtem = null;
			}
			return ıtem;
		}
	}
}