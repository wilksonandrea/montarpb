using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	public class REComparer : EqualityComparer<object>
	{
		public REComparer()
		{
		}

		public override bool Equals(object X, object Y)
		{
			return X == Y;
		}

		public override int GetHashCode(object OBJ)
		{
			if (OBJ == null)
			{
				return 0;
			}
			return OBJ.GetHashCode();
		}
	}
}