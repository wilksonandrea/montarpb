using System;
using System.Reflection;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000835 RID: 2101
	internal static class AsyncMessageHelper
	{
		// Token: 0x060059C3 RID: 22979 RVA: 0x0013C3E8 File Offset: 0x0013A5E8
		internal static void GetOutArgs(ParameterInfo[] syncParams, object[] syncArgs, object[] endArgs)
		{
			int num = 0;
			for (int i = 0; i < syncParams.Length; i++)
			{
				if (syncParams[i].IsOut || syncParams[i].ParameterType.IsByRef)
				{
					endArgs[num++] = syncArgs[i];
				}
			}
		}
	}
}
