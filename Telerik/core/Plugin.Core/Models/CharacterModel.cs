using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class CharacterModel
	{
		public uint CreateDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public long ObjectId
		{
			get;
			set;
		}

		public uint PlayTime
		{
			get;
			set;
		}

		public int Slot
		{
			get;
			set;
		}

		public CharacterModel()
		{
			this.Name = "";
		}
	}
}