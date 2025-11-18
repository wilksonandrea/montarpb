using System;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F9 RID: 249
	public sealed class ConsoleAccessException : Exception
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x00007886 File Offset: 0x00005A86
		public ConsoleAccessException()
			: base(string.Format("Color conversion failed because a handle to the actual windows console was not found.", Array.Empty<object>()))
		{
		}
	}
}
