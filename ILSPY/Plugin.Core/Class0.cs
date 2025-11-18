using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[CompilerGenerated]
internal sealed class Class0<T, U>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly T gparam_0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly U gparam_1;

	public T item => gparam_0;

	public U inx => gparam_1;

	[DebuggerHidden]
	public Class0(T gparam_2, U gparam_3)
	{
		gparam_0 = gparam_2;
		gparam_1 = gparam_3;
	}

	[DebuggerHidden]
	bool object.Equals(object obj)
	{
		Class0<T, U> @class = obj as Class0<T, U>;
		if (this != @class)
		{
			if (@class != null && EqualityComparer<T>.Default.Equals(gparam_0, @class.gparam_0))
			{
				return EqualityComparer<U>.Default.Equals(gparam_1, @class.gparam_1);
			}
			return false;
		}
		return true;
	}

	[DebuggerHidden]
	int object.GetHashCode()
	{
		return (-1959725626 + EqualityComparer<T>.Default.GetHashCode(gparam_0)) * -1521134295 + EqualityComparer<U>.Default.GetHashCode(gparam_1);
	}

	[DebuggerHidden]
	string object.ToString()
	{
		object[] array = new object[2];
		T val = gparam_0;
		array[0] = ((val != null) ? val.ToString() : null);
		U val2 = gparam_1;
		array[1] = ((val2 != null) ? val2.ToString() : null);
		return string.Format(null, "{{ item = {0}, inx = {1} }}", array);
	}
}
