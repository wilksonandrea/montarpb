using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class ColorMappingException : Exception
	{
		public int ErrorCode
		{
			get;
			private set;
		}

		public ColorMappingException(int int_1) : base(string.Format("Color conversion failed with system error code {0}!", int_1))
		{
			this.ErrorCode = int_1;
		}
	}
}