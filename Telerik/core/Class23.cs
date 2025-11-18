using Plugin.Core.Colorful;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

internal static class Class23
{
	internal static IEnumerable<T> smethod_0<T>(IEnumerable<T> ienumerable_0)
	where T : IPrototypable<T>
	{
		foreach (T ienumerable0 in ienumerable_0)
		{
			yield return ienumerable0.Prototype();
		}
	}

	internal static IEnumerable<T> smethod_1<T>(IEnumerable<T> ienumerable_0)
	where T : struct
	{
		foreach (T ienumerable0 in ienumerable_0)
		{
			yield return ienumerable0;
		}
	}

	internal static string smethod_2<T>(this T gparam_0)
	{
		return (string)string.Join(string.Empty, (dynamic)gparam_0);
	}

	internal static dynamic smethod_3<T>(this T gparam_0)
	{
		List<object> objs = new List<object>();
		object[] gparam0 = (object)gparam_0 as object[];
		if (gparam0 == null)
		{
			objs.Add((dynamic)gparam_0);
		}
		else
		{
			object[] objArray = gparam0;
			for (int i = 0; i < (int)objArray.Length; i++)
			{
				objs.Add(objArray[i]);
			}
		}
		return objs.ToArray();
	}
}