namespace Plugin.Core.Colorful
{
    using System;

    public sealed class ConsoleAccessException : Exception
    {
        public ConsoleAccessException() : base(string.Format("Color conversion failed because a handle to the actual windows console was not found.", Array.Empty<object>()))
        {
        }
    }
}

