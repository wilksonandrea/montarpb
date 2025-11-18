using System.Collections.Generic;

namespace Plugin.Core.Utility;

public class REComparer : EqualityComparer<object>
{
	public override bool Equals(object X, object Y)
	{
		return X == Y;
	}

	public override int GetHashCode(object OBJ)
	{
		return OBJ?.GetHashCode() ?? 0;
	}
}
