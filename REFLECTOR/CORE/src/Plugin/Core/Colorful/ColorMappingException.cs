namespace Plugin.Core.Colorful
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class ColorMappingException : Exception
    {
        public ColorMappingException(int int_1) : base($"Color conversion failed with system error code {int_1}!")
        {
            this.ErrorCode = int_1;
        }

        public int ErrorCode { get; private set; }
    }
}

